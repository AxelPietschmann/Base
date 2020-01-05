using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Standard.Factory
{
    class RegistrationInfo
    {
        public Type LookupType { get; set; }

        public Type InstanceType { get; set; }

        public bool AlwaysCreateNew { get; set; }

        public object Instance { get; set; }
    }
}
