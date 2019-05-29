using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests
{
    public class Messaging
    {
        public static void WriteMessage(string path, string filename, string message, bool printToConsole)
        {
            if (printToConsole) Console.WriteLine(message);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            File.AppendAllText(Path.Combine(path, filename), message);
        }
    }
}
