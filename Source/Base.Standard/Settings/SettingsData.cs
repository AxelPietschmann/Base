using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Standard.Settings
{
    [Serializable]
    public class SettingsData
    {

        public SettingsData()
        {
            SavedSettings = new List<SavedSetting>();
        }

        public List<SavedSetting> SavedSettings { get; set; }
    }

    [Serializable]
    public class SavedSetting
    {
        public object Value { get; set; }

        public string Key { get; set; }

        public string User { get; set; }


    }
}
