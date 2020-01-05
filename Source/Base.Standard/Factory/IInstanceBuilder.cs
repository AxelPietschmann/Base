using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Standard.Factory
{
    public interface IInstanceBuilder
    {

        void Register<TInterfaceType, TInstanceType>(bool alwaysCreateNew = true, TInstanceType instance = null) where TInstanceType : class, TInterfaceType;

        T Resolve<T>();

    }
}
