using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog;

namespace SupportBank
{
    public class ReadFile
    {
        public List<string> lines = new List<string>();
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public ReadFile(string fname)
        {
            try
            {
                foreach (string line in File.ReadLines(fname).Skip(1))
                {
                    lines.Add(line);
                }
            }
            catch (FileNotFoundException)
            {
                logger.Error("File not found");
                throw;
            }

        } 
    }
}