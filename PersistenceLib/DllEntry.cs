using RGiesecke.DllExport;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLib
{
    public class DllEntry
    {
        [DllExport("_RVExtension@12", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi)]
        public static void RVExtension(StringBuilder output, int outputSize, [MarshalAs(UnmanagedType.LPStr)] string function)
        {
            Trace.AutoFlush = true;
            Trace.Listeners.Add(new TextWriterTraceListener("PersistenceLib.log"));

            Trace.TraceInformation("Loaded");

            // Reduce output buffer to stop overrun.
            outputSize--;

            var parts = function.Split(' ');
            var command = parts[0];
            var arguments = parts[1];


            // Cases needed:
            /* Get Kits
             * Load Kit
             * Delete Kit 
             */

            try
            {
                switch (command)
                {
                    case "saveKit":
                        output.Append(Kits.SaveKit(arguments));
                        break;
                    case "loadKit":
                        output.Append(Kits.LoadKit(arguments));
                        break;
                    default:
                        output.Append("ERROR: Unknown command " + command);
                        break;
                }
            }
            catch (Exception ex)
            {
                output.Append(ex.Message);
            }
        }       
    }
}
