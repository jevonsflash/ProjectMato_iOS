using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectMato.iOS.Common
{
    public class WindowArg
    {
        public WindowArg(string name, object[] args = null, bool isNavigate = false)
        {
            Name = name;
            Args = args;
            IsNavigate = isNavigate;

        }
        public string Name { get; set; }
        public object[] Args { get; set; }
        public bool IsNavigate { get; set; }
    }
}
