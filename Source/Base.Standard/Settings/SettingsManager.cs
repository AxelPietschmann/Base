using Base.Standard.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base.Standard.Logger;

namespace Base.Standard.Settings
{
    public class SettingsManager : ISettingsManager
    {
        #region Fields

        private readonly ISerializer _Serializer;
        private readonly string _SettingsFile;
        private readonly string _CurrentUser;
        private readonly string _AllUsers;
        private readonly SettingsData _SettingsData;
        private readonly ILogger _Logger = LogManager.GetLogger(typeof(SettingsManager));

        #endregion

        #region C'tor

        public SettingsManager(ISerializer serializer)
        {
            _Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            var folder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), AppDomain.CurrentDomain.FriendlyName);
            if(!System.IO.Directory.Exists(folder))
            {
                System.IO.Directory.CreateDirectory(folder);
            }
            _SettingsFile = System.IO.Path.Combine(folder, "Settings.xml");
            _AllUsers = "AllUsers";
            _CurrentUser = Environment.UserName;
            _SettingsData = TryLoad();
        }

        #endregion

        #region ISettingsManager

        public ISetting<T> GetSetting<T>(string key, SettingScope scope, T defaultValue) 
        {
            var user = ScopeToString(scope);
            var setting = _SettingsData.SavedSettings.FirstOrDefault(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase) && user.Equals(x.User, StringComparison.OrdinalIgnoreCase));
            if(setting == null)
            {
                setting = new SavedSetting { Key = key, User = user, Value = defaultValue };
                _SettingsData.SavedSettings.Add(setting);
            }
            return new Setting<T>(setting, scope);
        }

        public void Save()
        {
            try
            {
                _Serializer.SerializeToFile<SettingsData>(_SettingsData, _SettingsFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #endregion

        #region Helper

        private string ScopeToString(SettingScope scope)
        {
            switch(scope)
            {
                case SettingScope.AllUsers:
                    return _AllUsers;
                case SettingScope.CurrentUser:
                    return _CurrentUser;
                default:
                    _Logger.Error("Unsupported scope");
                    throw new NotImplementedException();

            }
        }

        private SettingsData TryLoad()
        {
            try
            {
                var deserilized = _Serializer.DeserializeFromFile<SettingsData>(_SettingsFile);
                return deserilized;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new SettingsData();
            }
        }

        #endregion
    }
}
