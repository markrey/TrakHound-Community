﻿// Copyright (c) 2016 Feenux LLC, All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

using TrakHound.Tools.Web;

namespace TrakHound.API
{
    public static partial class Data
    {
        public class DeviceInfo
        {
            private object _lock = new object();


            [JsonProperty("unique_id")]
            public string UniqueId { get; set; }

            [JsonProperty("enabled")]
            public bool Enabled { get; set; }

            [JsonProperty("index")]
            public int Index { get; set; }


            [JsonProperty("description")]
            public DescriptionInfo Description
            {
                get
                {
                    var obj = GetClass("description");
                    if (obj != null) return (DescriptionInfo)obj;
                    return null;
                }
                set
                {
                    var o = value;
                    if (o != null)
                    {
                        AddClass("description", o);
                    }
                }
            }

            [JsonProperty("status")]
            public StatusInfo Status
            {
                get
                {
                    var obj = GetClass("status");
                    if (obj != null) return (StatusInfo)obj;
                    return null;
                }
                set
                {
                    var o = value;
                    if (o != null)
                    {
                        AddClass("status", o);
                    }
                }
            }

            [JsonProperty("controller")]
            public ControllerInfo Controller
            {
                get
                {
                    var obj = GetClass("controller");
                    if (obj != null) return (ControllerInfo)obj;
                    return null;
                }
                set
                {
                    var o = value;
                    if (o != null)
                    {
                        AddClass("controller", o);
                    }
                }
            }

            [JsonProperty("oee")]
            public OeeInfo Oee
            {
                get
                {
                    var obj = GetClass("oee");
                    if (obj != null) return (OeeInfo)obj;
                    return null;
                }
                set
                {
                    var o = value;
                    if (o != null)
                    {
                        AddClass("oee", o);
                    }
                }
            }

            [JsonProperty("timers")]
            public TimersInfo Timers
            {
                get
                {
                    var obj = GetClass("timers");
                    if (obj != null) return (TimersInfo)obj;
                    return null;
                }
                set
                {
                    var o = value;
                    if (o != null)
                    {
                        AddClass("timers", o);
                    }
                }
            }

            [JsonProperty("cycles")]
            public List<CycleInfo> Cycles
            {
                get
                {
                    var obj = GetClass("cycles");
                    if (obj != null) return (List<CycleInfo>)obj;
                    return null;
                }
                set
                {
                    var o = value;
                    if (o != null)
                    {
                        AddClass("cycles", o);
                    }
                }
            }

            [JsonProperty("hours")]
            public List<HourInfo> Hours
            {
                get
                {
                    var obj = GetClass("hours");
                    if (obj != null) return (List<HourInfo>)obj;
                    return null;
                }
                set
                {
                    var o = value;
                    if (o != null)
                    {
                        AddClass("hours", o);
                    }
                }
            }

            [JsonProperty("agent")]
            public AgentInfo Agent
            {
                get
                {
                    var obj = GetClass("agent");
                    if (obj != null) return (AgentInfo)obj;
                    return null;
                }
                set
                {
                    var o = value;
                    if (o != null)
                    {
                        AddClass("agent", o);
                    }
                }
            }

            public void CombineHours()
            {
                lock (_lock)
                {
                    if (Hours != null && Hours.Count > 0)
                    {
                        var newHours = new List<HourInfo>();

                        var _hours = Hours.ToList();
                        if (_hours != null) _hours = _hours.FindAll(o => o != null); // Clean list of any null HourInfos

                        var distinctDates = _hours.Select(o => o.Date).Distinct();

                        foreach (string distinctDate in distinctDates.ToList())
                        {
                            var sameDate = _hours.FindAll(o => o.Date == distinctDate);

                            var distinctHours = sameDate.Select(o => o.Hour).Distinct();

                            foreach (int distinctHour in distinctHours.ToList())
                            {
                                var hourInfo = new HourInfo();
                                hourInfo.Date = distinctDate;
                                hourInfo.Hour = distinctHour;

                                var sameHours = _hours.FindAll(o => o.Hour == distinctHour);

                                foreach (var sameHour in sameHours.ToList())
                                {
                                    // OEE
                                    hourInfo.PlannedProductionTime += sameHour.PlannedProductionTime;
                                    hourInfo.OperatingTime += sameHour.OperatingTime;
                                    hourInfo.IdealOperatingTime += sameHour.IdealOperatingTime;
                                    hourInfo.TotalPieces += sameHour.TotalPieces;
                                    hourInfo.GoodPieces += sameHour.GoodPieces;

                                    hourInfo.TotalTime += sameHour.TotalTime;

                                    // Device Status
                                    hourInfo.Active += sameHour.Active;
                                    hourInfo.Idle += sameHour.Idle;
                                    hourInfo.Alert += sameHour.Alert;

                                    // Production Status
                                    hourInfo.Production += sameHour.Production;
                                    hourInfo.Setup += sameHour.Setup;
                                    hourInfo.Teardown += sameHour.Teardown;
                                    hourInfo.Maintenance += sameHour.Maintenance;
                                    hourInfo.ProcessDevelopment += sameHour.ProcessDevelopment;
                                }

                                newHours.Add(hourInfo);
                            }

                        }

                        Hours = newHours;
                    }
                }
            }


            #region "SubClass Management"

            private Dictionary<string, object> _classes;
            public Dictionary<string, object> Classes
            {
                get
                {
                    if (_classes == null) _classes = new Dictionary<string, object>();
                    return _classes;
                }
                set { _classes = value; }
            }


            public void AddClass(string id, object obj)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    lock (_lock)
                    {
                        object o = null;
                        Classes.TryGetValue(id, out o);
                        if (o == null) Classes.Add(id, obj);
                        else
                        {
                            Classes.Remove(id);
                            Classes.Add(id, obj);
                        }
                    }
                }             
            }

            public object GetClass(string id)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    lock (_lock)
                    {
                        object obj = null;
                        Classes.TryGetValue(id, out obj);

                        return obj;
                    }
                }

                return null;                
            }

            public void RemoveClass(string id)
            {
                lock (_lock)
                {
                    if (id != null) Classes.Remove(id);
                } 
            }

            public void ClearClasses()
            {
                lock (_lock)
                {
                    Classes.Clear();
                }        
            }

            #endregion


            public string ToJson()
            {
                return JSON.FromObject(ToJsonObject());
            }

            private object ToJsonObject()
            {
                lock (_lock)
                {
                    var data = new Dictionary<string, object>();

                    data.Add("unique_id", UniqueId);
                    data.Add("enabled", Enabled);
                    data.Add("index", Index);

                    foreach (var c in Classes)
                    {
                        object match = false;
                        if (!data.TryGetValue(c.Key, out match))
                        {

                            data.Add(c.Key, c.Value);
                        }
                    }

                    if (data.Count > 1) return data;
                    else return null;
                }              
            }

            public static string ListToJson(List<DeviceInfo> deviceInfos)
            {
                var datas = new List<object>();

                foreach (var deviceInfo in deviceInfos.ToList())
                {
                    var data = deviceInfo.ToJsonObject();
                    if (data != null) datas.Add(data);
                }

                if (datas.Count > 0) return JSON.FromList<object>(datas);
                else return null;
            }

            public DeviceInfo Copy()
            {
                lock (_lock)
                {
                    var result = new DeviceInfo();
                    foreach (var c in Classes)
                    {
                        object o = null;
                        Classes.TryGetValue(c.Key, out o);
                        if (o == null) Classes.Add(c.Key, c.Value);
                        else
                        {
                            Classes.Remove(c.Key);
                            Classes.Add(c.Key, c.Value);
                        }
                    }

                    return result;
                }
            }


            public void AddHourInfo(HourInfo hourInfo)
            {
                lock (_lock)
                {
                    if (hourInfo != null)
                    {

                        var hours = Hours;
                        if (hours == null) hours = new List<HourInfo>();

                        hours.Add(hourInfo);

                        Hours = hours;
                    }
                }
            }

            public void AddHourInfos(List<HourInfo> hourInfos)
            {
                lock (_lock)
                {
                    if (hourInfos != null && hourInfos.Count > 0)
                    {
                        var _hourInfos = hourInfos.FindAll(o => o != null);

                        var hours = Hours;
                        if (hours == null) hours = new List<HourInfo>();

                        foreach (var hourInfo in _hourInfos)
                        {
                            if (hourInfo != null) hours.Add(hourInfo);
                        }

                        Hours = hours;
                    }
                }
            }

        }
    }
}
