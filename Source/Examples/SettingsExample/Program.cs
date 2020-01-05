using Base.Standard.Factory;
using Base.Standard.Serializer;
using Base.Standard.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var instanceBuilder = new InstanceBuilder();
            instanceBuilder.Register<ISerializer, XmlSerializer>();
            instanceBuilder.Register<ISettingsManager, SettingsManager>();

            var settingsManager = instanceBuilder.Resolve<ISettingsManager>();

            var setting = settingsManager.GetSetting<int>("Age", SettingScope.CurrentUser, 10);

            setting.Value += 1;
            settingsManager.Save();
        }
    }
}
