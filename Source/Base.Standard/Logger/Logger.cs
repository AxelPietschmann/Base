using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Standard.Logger
{
    public class Logger : ILogger
    {

        public void Error(object message)
        {
            //TODO
            Console.WriteLine(DateTime.Now.ToString() + ": " + message);
        }

    }
}
