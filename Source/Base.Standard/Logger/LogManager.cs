using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Standard.Logger
{
    public static class LogManager
    {
        public static ILogger GetLogger(Type type)
        {
            return new Logger();
        }
    }
}
