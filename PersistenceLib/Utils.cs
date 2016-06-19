using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLib
{
    static class Utils
    {
        public static String[] PrepareArguments(String arguments)
        {
            return arguments.Replace("\"", "").Split('~');
        }
    }
}
