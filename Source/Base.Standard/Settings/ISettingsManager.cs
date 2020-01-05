using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Standard.Settings
{
    public interface ISettingsManager
    {
        ISetting<T> GetSetting<T>(string key, SettingScope scope, T defaultValue);
        void Save();
    }
}
