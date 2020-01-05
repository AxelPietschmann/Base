using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.Standard.Factory
{
    public class InstanceBuilder : IInstanceBuilder
    {
        #region Fields

        private readonly List<RegistrationInfo> _Register = new List<RegistrationInfo>();
        private readonly object _SyncLock = new object();

        #endregion

        #region IInstanceBuilder

        public void Register<TLookupType, TInstanceType>(bool alwaysCreateNew = true, TInstanceType instance = null) where TInstanceType : class, TLookupType
        {
            lock (_SyncLock)
            {
                if (_Register.Any(x => x.LookupType == typeof(TLookupType)))
                {
                    throw new InvalidOperationException("Already registered.");
                }
                if (alwaysCreateNew && instance != null)
                {
                    throw new InvalidOperationException("Passing an instance for always create new is not allowed.");
                }
                var info = new RegistrationInfo
                {
                    LookupType = typeof(TLookupType),
                    InstanceType = typeof(TInstanceType),
                    AlwaysCreateNew = alwaysCreateNew,
                    Instance = instance
                };
                _Register.Add(info);
            }
        }

        public T Resolve<T>()
        {
            lock (_SyncLock)
            {
                var result = Resolve(typeof(T));
                return (T)result;
            }
        }

        #endregion

        #region Helper

        private object Resolve(Type type)
        {
            var info = _Register.FirstOrDefault(x => x.LookupType == type);
            if (info == null)
            {
                throw new InvalidOperationException("Not registered.");
            }
            if (info.Instance != null)
            {
                return info.Instance;
            }
            var instance = CreateInstance(info.InstanceType);
            if (!info.AlwaysCreateNew)
            {
                info.Instance = instance;
            }
            return instance;

        }

        private object CreateInstance(Type type)
        {
            var ctors = type.GetConstructors(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).ToList();
            var ctorToUse = ctors.First();

            var parameterInstances = new List<object>();

            foreach (var parameterInfo in ctorToUse.GetParameters())
            {
                var parameterInstance = Resolve(parameterInfo.ParameterType);
                parameterInstances.Add(parameterInstance);
            }

            var instance = ctorToUse.Invoke(parameterInstances.ToArray());
            return instance;
        }

        #endregion
    }
}
