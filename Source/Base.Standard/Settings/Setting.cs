using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Base.Standard.Settings
{
    class Setting<T> : ISetting<T>
    {
        private readonly SavedSetting _SavedSetting;

        public Setting(SavedSetting savedSetting, SettingScope scope)
        {
            _SavedSetting = savedSetting ?? throw new ArgumentNullException(nameof(savedSetting));
            Scope = scope;
        }

        public string Key => _SavedSetting.Key;
        
        public SettingScope Scope { get; }

        public T Value
        {
            get => (T)_SavedSetting.Value;
            set
            {
                _SavedSetting.Value = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler ValueChanged;
    }
}
