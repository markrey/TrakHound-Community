﻿// Copyright (c) 2016 Feenux LLC, All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

using TH_Configuration;
using TH_Global;
using TH_Global.Functions;
using MTConnect;
using MTConnect.Application.Components;
using TH_UserManagement.Management;

namespace TH_DeviceManager.AddDevice.Pages
{
    /// <summary>
    /// Page containing options for adding Devices that were automatically found on the network
    /// </summary>
    public partial class AutoDetect : UserControl, IPage
    {
        public AutoDetect()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region "IPage"

        public string Title { get { return "Auto Detect"; } }

        public ImageSource Image { get { return new BitmapImage(new Uri("pack://application:,,,/TH_DeviceManager;component/Resources/options_gear_30px.png")); } }


        public void Opened() { }
        public bool Opening() { return true; }

        public void Closed() { }
        public bool Closing() { return true; }

        #endregion

        /// <summary>
        /// Parent AddDevice.Page object
        /// </summary>
        public Page ParentPage { get; set; }

        #region "Dependency Properties"

        /// <summary>
        /// Used to tell if the Devices are currently being loaded
        /// </summary>
        public bool DevicesLoading
        {
            get { return (bool)GetValue(DevicesLoadingProperty); }
            set { SetValue(DevicesLoadingProperty, value); }
        }

        public static readonly DependencyProperty DevicesLoadingProperty =
            DependencyProperty.Register("DevicesLoading", typeof(bool), typeof(AutoDetect), new PropertyMetadata(true));

        /// <summary>
        /// Number of Devices that were found that have already been added
        /// </summary>
        public int DevicesAlreadyAdded
        {
            get { return (int)GetValue(DevicesAlreadyAddedProperty); }
            set { SetValue(DevicesAlreadyAddedProperty, value); }
        }

        public static readonly DependencyProperty DevicesAlreadyAddedProperty =
            DependencyProperty.Register("DevicesAlreadyAdded", typeof(int), typeof(AutoDetect), new PropertyMetadata(0));

        /// <summary>
        /// Number of Devices that were found but haven't been added yet
        /// </summary>
        public int DevicesNotAdded
        {
            get { return (int)GetValue(DevicesNotAddedProperty); }
            set { SetValue(DevicesNotAddedProperty, value); }
        }

        public static readonly DependencyProperty DevicesNotAddedProperty =
            DependencyProperty.Register("DevicesNotAdded", typeof(int), typeof(AutoDetect), new PropertyMetadata(0));

        /// <summary>
        /// Number of Network Nodes found using Ping
        /// </summary>
        public int NetworkNodesFound
        {
            get { return (int)GetValue(NetworkNodesFoundProperty); }
            set { SetValue(NetworkNodesFoundProperty, value); }
        }

        public static readonly DependencyProperty NetworkNodesFoundProperty =
            DependencyProperty.Register("NetworkNodesFound", typeof(int), typeof(AutoDetect), new PropertyMetadata(0));

        #endregion

        const System.Windows.Threading.DispatcherPriority PRIORITY_BACKGROUND = System.Windows.Threading.DispatcherPriority.Background;
      
        /// <summary>
        /// Search the Network for Devices
        /// </summary>
        public void FindDevices()
        {
            LoadCatalog();
        }

        #region "Device Catalog"

        private class CatalogInfo
        {
            public Shared.SharedListItem Item { get; set; }
            public System.Drawing.Image Image { get; set; }
        }

        List<CatalogInfo> catalogInfos = new List<CatalogInfo>();

        Thread LoadCatalog_THREAD;

        private void LoadCatalog()
        {
            if (ParentPage != null && ParentPage.DeviceManager != null)
            {
                DevicesLoading = true;
                DeviceInfos.Clear();
                catalogInfos.Clear();
                DevicesAlreadyAdded = 0;
                DevicesNotAdded = 0;
                NetworkNodesFound = 0;

                if (LoadCatalog_THREAD != null) LoadCatalog_THREAD.Abort();

                LoadCatalog_THREAD = new Thread(new ThreadStart(LoadCatalog_Worker));
                LoadCatalog_THREAD.Start();
            }
        }

        private void LoadCatalog_Worker()
        {
            var sharedItems = Shared.GetSharedList();

            foreach (var item in sharedItems)
            {
                var info = new CatalogInfo();
                info.Item = item;
                info.Image = GetCatalogImage(item);

                catalogInfos.Add(info);
            }

            FindNodes();
        }

        private System.Drawing.Image GetCatalogImage(Shared.SharedListItem item)
        {
            System.Drawing.Image img = null;

            if (item.image_url != null)
            {
                // Just use Remote.Images (don't look for local)
                img = TH_UserManagement.Management.Remote.Images.GetImage(item.image_url);
            }

            return img;
        }

        #endregion

        #region "Network Nodes"

        bool pingFinished = false;

        private void FindNodes()
        {
            portCount = 0;
            returnedPorts = 0;

            pingFinished = false;

            var pingNodes = new Network_Functions.PingNodes();
            pingNodes.PingSuccessful += PingNodes_PingSuccessful;
            pingNodes.Finished += PingNodes_Finished;
            pingNodes.Start();
        }

        private void PingNodes_PingSuccessful(System.Net.NetworkInformation.PingReply reply)
        {
            Dispatcher.BeginInvoke(new Action(() => { NetworkNodesFound++; }), UI_Functions.PRIORITY_BACKGROUND, new object[] { });
            
            RunProbe(reply.Address);
        }

        private void PingNodes_Finished()
        {
            pingFinished = true;
        }

        #endregion

        #region "MTConnect Probe"

        private class RunProbeInfo
        {
            public IPAddress Address { get; set; }
            public int Port { get; set; }
        }

        private void RunProbe(object o)
        {
            var ip = (IPAddress)o;

            var ports = CreatePortArray(5000, 20);

            portCount += ports.Length;

            foreach (var port in ports)
            {
                var info = new RunProbeInfo();
                info.Address = ip;
                info.Port = port;

                ThreadPool.QueueUserWorkItem(new WaitCallback(RunProbeOnPort), info);
            } 
        }

        private static int[] CreatePortArray(int start, int count)
        {
            var ports = new List<int>();
            for (var x = start; x <= (start + count) - 1; x++) ports.Add(x);
            return ports.ToArray();
        }

        private void RunProbeOnPort(object o)
        {
            if (o != null)
            {
                var info = (RunProbeInfo)o;

                string url = MTConnect.HTTP.GetUrl(info.Address.ToString(), info.Port, null);

                var probe = Requests.Get(url, null, 1000, 1);
                if (probe != null)
                {
                    foreach (var device in probe.Devices)
                    {
                        Dispatcher.BeginInvoke(new Action<IPAddress, int, Device>(AddDeviceInfo), PRIORITY_BACKGROUND, new object[] { info.Address, info.Port, device });
                    }
                }

                returnedPorts++;
                StopDevicesLoading(returnedPorts);
            }
        }

        int portCount;
        int returnedPorts;

        private void StopDevicesLoading(int ports)
        {
            if (ports >= portCount)
            {
                Dispatcher.BeginInvoke(new Action(() => { DevicesLoading = false; }), PRIORITY_BACKGROUND, new object[] { });
            }
        }

        #endregion

        #region "Device Infos"

        private ObservableCollection<DeviceInfo> _deviceInfos;
        public ObservableCollection<DeviceInfo> DeviceInfos
        {
            get
            {
                if (_deviceInfos == null) _deviceInfos = new ObservableCollection<DeviceInfo>();
                return _deviceInfos;
            }
            set
            {
                _deviceInfos = value;
            }
        }

        private void AddDeviceInfo(IPAddress address, int port, Device device)
        {
            // Check if already in DeviceManagerList.Devices
            bool alreadyAdded = ParentPage.DeviceManager.Devices.ToList().Exists(x => 
            TestAgentConfiguration(x, address, port, device));

            if (!alreadyAdded)
            {
                // Look for matching shared configuration
                var catalogInfo = catalogInfos.Find(x => GetLinkTagMatchValue(device, x));
                if (catalogInfo != null)
                {
                    var info = new DeviceInfo();
                    info.Id = String_Functions.RandomString(80);
                    info.IPAddress = address.ToString();
                    info.Port = port;
                    info.Device = device;
                    if (info.Device.Description.CDATA != null) info.Device.Description.CDATA = info.Device.Description.CDATA.Trim();
                    info.Image = GetSourceFromImage(catalogInfo.Image);
                    info.SharedListItem = catalogInfo.Item;

                    DeviceInfos.Add(info);

                    DevicesNotAdded = DeviceInfos.Count;
                }
            }
            else
            {
                DevicesAlreadyAdded += 1;
            }
        }

        private bool TestAgentConfiguration(DeviceConfiguration config, IPAddress address, int port, Device device)
        {
            bool result = false;

            var ac = AgentConfiguration.Read(config);
            if (ac != null)
            {
                if (address.ToString() == ac.Address && port == ac.Port && device.Name == ac.DeviceName)
                {
                    result = true;
                }
            }

            return result;
        }

        private class AgentConfiguration
        {
            public string Address { get; set; }
            public int Port { get; set; }
            public string DeviceName { get; set; }

            public static AgentConfiguration Read(DeviceConfiguration config)
            {
                var result = new AgentConfiguration();
                result.Address = XML_Functions.GetInnerText(config.ConfigurationXML, "/Agent/Address");

                int port = 80;
                int.TryParse(XML_Functions.GetInnerText(config.ConfigurationXML, "/Agent/Port"), out port);
                result.Port = port;

                result.DeviceName = XML_Functions.GetInnerText(config.ConfigurationXML, "/Agent/DeviceName");

                return result;
            }
        }


        private class GetDeviceInfoImageInfo
        {
            public DeviceInfo DeviceInfo { get; set; }
            public System.Drawing.Image Image { get; set; }
        }

        private void GetDeviceInfoImage(DeviceInfo info, System.Drawing.Image img)
        {
            if (img != null)
            {
                var getInfo = new GetDeviceInfoImageInfo();
                getInfo.DeviceInfo = info;
                getInfo.Image = img;

                ThreadPool.QueueUserWorkItem(new WaitCallback(GetDeviceInfoImage_Worker), getInfo);
            }
        }

        private void GetDeviceInfoImage_Worker(object o)
        {
            if (o != null)
            {
                var info = (GetDeviceInfoImageInfo)o;

                var src = GetSourceFromImage(info.Image);

                if (src != null) Dispatcher.BeginInvoke(new Action<DeviceInfo, ImageSource>(GetDeviceInfoImage_GUI), UI_Functions.PRIORITY_BACKGROUND, new object[] { info.DeviceInfo, src });
            }
        }

        private void GetDeviceInfoImage_GUI(DeviceInfo info, ImageSource src)
        {
            info.Image = src;
        }

        private ImageSource GetSourceFromImage(System.Drawing.Image img)
        {
            BitmapImage bitmap = null;

            if (img != null)
            {
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);

                IntPtr bmpPt = bmp.GetHbitmap();
                BitmapSource bmpSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bmpPt, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                bmpSource.Freeze();

                if (bmpSource.PixelWidth > bmpSource.PixelHeight)
                {
                    bitmap = Image_Functions.SetImageSize(bmpSource, 100);
                }
                else
                {
                    bitmap = Image_Functions.SetImageSize(bmpSource, 0, 40);
                }
            }

            return bitmap;
        }

        /// <summary>
        /// Matching Syntax:
        /// [MANUFACTURER].[MODEL].[DEVICENAME]
        /// </summary>
        /// <param name="device"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        private static bool GetLinkTagMatchValue(Device device, CatalogInfo info)
        {
            if (info != null && info.Item != null && info.Item.link_tag != null && device != null)
            {
                string[] tags = info.Item.link_tag.Split(';');
                if (tags != null)
                {
                    foreach (var tag in tags)
                    {
                        if (!String.IsNullOrEmpty(tag))
                        {
                            bool match = GetLinkTagMatchValue(device, tag);
                            if (match) return true;
                        }
                    }
                }
            }

            return false;
        }

        private static bool GetLinkTagMatchValue(Device device, string tag)
        {
            string x = tag.ToLower();

            string manufacturer = ToLower(device.Description.Manufacturer);
            string model = ToLower(device.Description.Model);
            string deviceName = ToLower(device.Name);

            if (GetLinkTagMatchValue(deviceName, x)) return true;
            if (GetLinkTagMatchValue(model, x)) return true;
            if (GetLinkTagMatchValue(manufacturer, x)) return true;
            
            return false;
        }

        private static bool GetLinkTagMatchValue(string test, string tag)
        {
            bool match = false;

            match = MatchLike(tag, test);

            if (!match) match = test == tag;

            return match;
        }

        private static bool MatchLike(string str, string test)
        {
            if (str != null && test != null)
            {
                if (str.StartsWith("%") && str.EndsWith("%") & str.Length > 2)
                {
                    string x = str.Remove(0, 1);
                    x = x.Remove(x.Length - 1, 1);
                    return MatchContains(x, test);
                }
                else if (str.StartsWith("%"))
                {
                    string x = str.Remove(0, 1);
                    return MatchEndsWith(x, test);
                }
                else if (str.EndsWith("%"))
                {
                    string x = str.Remove(str.Length - 1, 1);
                    return MatchStartsWith(x, test);
                }
            }
            return false;
        }

        private static bool MatchContains(string str, string test)
        {
            if (str != null && test != null)
            {
                return test.Contains(str);
            }
            return false;
        }

        private static bool MatchStartsWith(string str, string test)
        {
            if (str != null && test != null)
            {
                return test.StartsWith(str);
            }
            return false;
        }

        private static bool MatchEndsWith(string str, string test)
        {
            if (str != null && test != null)
            {
                return test.EndsWith(str);
            }
            return false;
        }

        private static string ToLower(string s)
        {
            if (s != null) return s.ToLower();
            return s;
        }

        #endregion

        #region "Add Device"

        class AddDevice_Return
        {
            public DeviceInfo DeviceInfo { get; set; }
            public DeviceConfiguration Configuration { get; set; }
            public bool Success { get; set; }
        }

        void AddDevice(DeviceInfo info)
        {
            if (ParentPage != null && ParentPage.DeviceManager != null)
            {
                info.Loading = true;
                Devices_DG.Items.Refresh();

                ThreadPool.QueueUserWorkItem(new WaitCallback(AddDevice_Worker), info);
            }
        }

        void AddDevice_Worker(object o)
        {
            var result = new AddDevice_Return();
            result.Success = false;

            if (o != null)
            {
                var info = (DeviceInfo)o;

                result.DeviceInfo = info;

                if (info.SharedListItem != null)
                {
                    string tablename = info.SharedListItem.tablename;

                    if (tablename != null)
                    {
                        // Update the number of times this Shared Configuration has been downloaded
                        Shared.UpdateDownloads(info.SharedListItem);

                        // Download the shared configuration from the TrakHound Device Catalog
                        DataTable dt = TH_UserManagement.Management.Remote.Configurations.GetTable(tablename);
                        if (dt != null)
                        {
                            XmlDocument xml = Converter.TableToXML(dt);
                            if (xml != null)
                            {
                                var config = DeviceConfiguration.Read(xml);
                                if (config != null)
                                {
                                    // Update Configuration to be Enabled, use the selected Agent Settings, and
                                    // if no databases are set then create new SQLite database configs
                                    UpdateEnabled(config);
                                    UpdateDescriptionConfiguration(info, config);
                                    UpdateAgentConfiguration(info, config);
                                    UpdateDatabaseConfiguration(config);

                                    result.Configuration = config;

                                    if (UserManagementSettings.Database != null)
                                    {
                                        SaveLocalImage(config.FileLocations.Manufacturer_Logo_Path);
                                        SaveLocalImage(config.FileLocations.Image_Path);
                                    }

                                    // Add page to user (or save to disk if local)
                                    if (ParentPage.DeviceManager.CurrentUser != null)
                                    {
                                        var userConfig = UserConfiguration.FromNewUserConfiguration(ParentPage.DeviceManager.CurrentUser);

                                        result.Success = Configurations.AddConfigurationToUser(userConfig, config);

                                        result.Configuration.TableName = config.TableName;
                                    }
                                    else
                                    {
                                        result.Success = SaveLocalConfigurationToUser(config);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            this.Dispatcher.BeginInvoke(new Action<AddDevice_Return>(AddDevice_GUI), PRIORITY_BACKGROUND, new object[] { result });
        }


        void SaveLocalImage(string filename)
        {
            if (filename != null)
            {
                var image = TH_UserManagement.Management.Remote.Images.GetImage(filename);
                if (image != null)
                {
                    string savePath = FileLocations.TrakHoundTemp + "\\" + filename;

                    image.Save(savePath);
                }
            }
        }

        private void UpdateEnabled(DeviceConfiguration config)
        {
            config.ClientEnabled = true;
            XML_Functions.SetInnerText(config.ConfigurationXML, "/ClientEnabled", "True");

            config.ServerEnabled = true;
            XML_Functions.SetInnerText(config.ConfigurationXML, "/ServerEnabled", "True");
        }

        private void UpdateAgentConfiguration(DeviceInfo info, DeviceConfiguration config)
        {
            // Save IP Address
            //config.Agent.Address = info.IPAddress;
            XML_Functions.SetInnerText(config.ConfigurationXML, "/Agent/Address", info.IPAddress);

            // Save Port
            //config.Agent.Port = info.Port;
            XML_Functions.SetInnerText(config.ConfigurationXML, "/Agent/Port", info.Port.ToString());

            // Save DeviceName
            //config.Agent.DeviceName = info.Device.Name;
            XML_Functions.SetInnerText(config.ConfigurationXML, "/Agent/DeviceName", info.Device.Name);

            // Save Heartbeat
            //config.Agent.Heartbeat = 5000;
            XML_Functions.SetInnerText(config.ConfigurationXML, "/Agent/Heartbeat", "5000");
        }

        private void UpdateDatabaseConfiguration(DeviceConfiguration config)
        {
            // If NO databases are configured then add a SQLite database for both client and server
            if (config.Databases_Client.Databases.Count == 0 && config.Databases_Server.Databases.Count == 0)
            {
                config.DatabaseId = DeviceConfiguration.GenerateDatabaseId();

                AddDatabaseConfiguration("/Databases_Client", config);
                AddDatabaseConfiguration("/Databases_Server", config);
            }
        }

        private void AddDatabaseConfiguration(string prefix, DeviceConfiguration config)
        {
            XML_Functions.AddNode(config.ConfigurationXML, prefix + "/SQLite");
            XML_Functions.SetAttribute(config.ConfigurationXML, prefix + "/SQLite", "id", "00");

            string path = FileLocations.Databases + "\\TrakHound.db";

            XML_Functions.SetInnerText(config.ConfigurationXML, prefix + "/SQLite/DatabasePath", path);
        }

        private void UpdateDescriptionConfiguration(DeviceInfo info, DeviceConfiguration config)
        {
            // Save Device Description
            string val = info.Device.Description.CDATA;
            if (val != null) val.Trim();
            config.Description.Description = val;
            XML_Functions.SetInnerText(config.ConfigurationXML, "/Description/Description", val);

            // Save Serial Number
            val = info.Device.Description.SerialNumber;
            if (val != null) val.Trim();
            config.Description.Serial = val;
            XML_Functions.SetInnerText(config.ConfigurationXML, "/Description/Serial", val);

            // Save Manufacturer
            val = info.Device.Description.Manufacturer;
            if (val != null) val.Trim();
            config.Description.Manufacturer = val;
            XML_Functions.SetInnerText(config.ConfigurationXML, "/Description/Manufacturer", val);
        }

        private bool SaveLocalConfigurationToUser(DeviceConfiguration config)
        {
            bool result = false;

            // Set new Unique Id
            string uniqueId = DeviceConfiguration.GenerateUniqueID();
            config.UniqueId = uniqueId;
            XML_Functions.SetInnerText(config.ConfigurationXML, "UniqueId", uniqueId);

            // Set new FilePath
            config.FilePath = uniqueId;
            XML_Functions.SetInnerText(config.ConfigurationXML, "FilePath", config.FilePath);

            result = DeviceConfiguration.Save(config);

            return result;
        }


        void AddDevice_GUI(AddDevice_Return result)
        {
            if (result.Success && result.Configuration != null)
            {
                // Add to DeviceManager
                ParentPage.DeviceManager.AddDevice(result.Configuration);

                // Remove from AutoDetect list
                int index = DeviceInfos.ToList().FindIndex(x => x.Id == result.DeviceInfo.Id);
                if (index >= 0) DeviceInfos.RemoveAt(index);

                // Increment counters
                DevicesNotAdded = DeviceInfos.Count;
                DevicesAlreadyAdded += 1;
            }
            else
            {
                if (result.DeviceInfo != null) result.DeviceInfo.Loading = false;
                TH_WPF.MessageBox.Show("Add device failed. Try Again.", "Add device failed", TH_WPF.MessageBoxButtons.Ok);
            }
        }

        #endregion

        #region "Buttons"

        private void Add_Clicked(TH_WPF.Button bt)
        {
            if (bt.DataObject != null)
            {
                var info = (DeviceInfo)bt.DataObject;

                AddDevice(info);
            }
        }

        private void Refresh_Clicked(TH_WPF.Button bt)
        {
            LoadCatalog();
        }

        private void AddAll_Clicked(TH_WPF.Button bt)
        {
            foreach (var info in DeviceInfos)
            {
                AddDevice(info);
            }
        }

        #endregion

        private void DeviceManager_Clicked(TH_WPF.Button bt) { if (ParentPage != null) ParentPage.OpenDeviceList(); }

        private void AddDevicesManually_Clicked(TH_WPF.Button bt) { if (ParentPage != null) ParentPage.ShowManual(); }

        private void Cancel_Clicked(TH_WPF.Button bt)
        {
            if (LoadCatalog_THREAD != null) LoadCatalog_THREAD.Abort();

            DevicesLoading = false;
        }
    }
}
