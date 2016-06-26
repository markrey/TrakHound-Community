﻿// Copyright (c) 2016 Feenux LLC, All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

using TH_Configuration;
using TH_Plugins;
using TH_Plugins.Client;

namespace TH_DeviceCompare_CNC.Text.Program_Name
{
    public partial class Plugin : UserControl, IClientPlugin
    {

        #region "Descriptive"

        public string Title { get { return "Program Name"; } }

        public string Description { get { return null; } }

        public ImageSource Image { get { return null; } }


        public string Author { get { return "TrakHound"; } }

        public string AuthorText { get { return "©2016 Feenux LLC. All Rights Reserved"; } }

        public ImageSource AuthorImage { get { return null; } }


        public string LicenseName { get { return "GPLv3"; } }

        public string LicenseText { get { return null; } }

        #endregion

        #region "Update Information"

        public string UpdateFileURL { get { return null; } }

        #endregion

        #region "Plugin Properties/Options"

        public string DefaultParent { get { return "Device Compare"; } }
        public string DefaultParentCategory { get { return "Components"; } }

        public bool AcceptsPlugins { get { return false; } }

        public bool OpenOnStartUp { get { return false; } }

        public bool ShowInAppMenu { get { return false; } }

        public List<PluginConfigurationCategory> SubCategories { get; set; }

        public List<IClientPlugin> Plugins { get; set; }

        #endregion

        #region "Methods"

        public void Initialize() { }

        public void Opened() { }
        public bool Opening() { return true; }

        public void Closed() { }
        public bool Closing() { return true; }

        #endregion

        #region "Events"

        public void GetSentData(EventData data)
        {
            Update(data);
        }

        public event SendData_Handler SendData;

        #endregion

        #region "Device Properties"

        private ObservableCollection<DeviceConfiguration> _devices;
        public ObservableCollection<DeviceConfiguration> Devices
        {
            get { return null; }
            set { _devices = null; }
        }

        #endregion

        #region "Options"

        public TH_Global.IPage Options { get; set; }

        #endregion

    }
}
