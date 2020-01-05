using System;

namespace Base.Standard.Settings
{
    public interface ISetting<T>
    {
        SettingScope Scope { get; }

        T Value { get; set; }

        string Key { get; }


        event EventHandler ValueChanged;
    }
}