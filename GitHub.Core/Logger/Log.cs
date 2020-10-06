using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Core.Logger
{
    public class Log
    {
        public static void Write(Exception ex, string fileName = "Error", string folder = "Adapter", string methodName = "")
        {
            Write(ex.Message + " - " + ex.StackTrace, fileName, folder, methodName);
        }

        public static void Write(string text, string fileName = "Error", string folder = "Adapter", string methodName = "")
        {
            try
            {
                if (!Directory.Exists(@"C:\LogFiles\"))
                    Directory.CreateDirectory(@"C:\LogFiles");
                if (!Directory.Exists(@"C:\LogFiles\" + folder))
                    Directory.CreateDirectory(@"C:\LogFiles\" + folder);


                text = Environment.NewLine +
                    "--------------------------------------------------" + Environment.NewLine +
                    "Time: " + DateTime.Now.ToString() + Environment.NewLine +
                    "Method Name: " + methodName + Environment.NewLine +
                    "Error: " + text;

                File.AppendAllText(@"C:\LogFiles\" + folder + @"\" + fileName + ".log", text);

            }
            catch (Exception)
            { }

            finally
            { }
        }
    }
}