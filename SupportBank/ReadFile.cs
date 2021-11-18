using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SupportBank
{
    public class ReadFile
    {
        public List<string> lines = new List<string>();
        public ReadFile(string fname)
        {
            foreach (string line in File.ReadLines(fname).Skip(1))
            {
                lines.Add(line);
            }
        } 
    }
}