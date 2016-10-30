using RGiesecke.DllExport;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PersistenceLib
{
    public class DllEntry
    {
        /*
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

            try
            {
                switch (command)
                {
                    case "saveKit":
                        output.Append(Kits.Save(arguments));
                        break;
                    case "loadKit":
                        output.Append(Kits.Load(arguments));
                        break;
                    case "saveContainer":
                        output.Append(Containers.Save(arguments));
                        break;
                    case "loadContainer":
                        output.Append(Containers.Load(arguments));
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
        */

        [DllExport("_RVExtension@12", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi)]
        public static void RVExtension(StringBuilder output, int outputSize, [MarshalAs(UnmanagedType.LPStr)] string function)
        {
            Trace.AutoFlush = true;
            Trace.Listeners.Add(new TextWriterTraceListener("SIMSLib.log"));

            Trace.TraceInformation("Loaded");

            // Reduce output buffer to stop overrun.
            outputSize--;

            String[] parts = function.Split(' ');
            String command = parts[0];
            String arguments = parts[1];

            String response = commitWebRequest(command, arguments);

            Trace.TraceInformation(response);

            output.Append(response);
        }

        private static String commitWebRequest(String command, String arguments)
        {
            AppConfig config = new AppConfig();
            String server = config.GetAppSetting("server");
            String username = config.GetAppSetting("username");
            String password = config.GetAppSetting("password");

            server = server + "?command=" + command;

            if (getAction(command) == WebRequestMethods.Http.Get)
            {
                server = server + "&data=" + WebUtility.UrlEncode(arguments);
            }

            Trace.TraceInformation("Request to " + server);

            Stream dataStream;
            WebRequest request = WebRequest.Create(server);
            request.Headers.Add("Authorization", "Basic " + encodeUsernamePassword(username, password));
            request.Headers.Add("X-Command", command);

            if (getAction(command) == WebRequestMethods.Http.Post)
            {
                request.Method = WebRequestMethods.Http.Post;
                byte[] byteArray = Encoding.UTF8.GetBytes("data=" + WebUtility.UrlEncode(arguments));
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
            }

            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            Trace.TraceInformation(responseFromServer);

            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        private static String getAction(String command)
        {
            if (command.IndexOf("save") != -1)
            {
                Trace.TraceInformation("Going to POST");
                return WebRequestMethods.Http.Post;
            }
            else
            {
                Trace.TraceInformation("Going to GET");
                return WebRequestMethods.Http.Get;
            }
        }

        private static String encodeUsernamePassword(String username, String password)
        {
            return Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
        }
    }
}
