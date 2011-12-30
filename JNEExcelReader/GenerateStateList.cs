using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace JNEExcelReader
{
    public class GenerateStateList
    {

        public void Execute(ProvinseList provinses)
        {
            var fileName = @".\propinsi.tsv";
            var provinceNames = provinses.ProvinseLists.Select(p => p.Name);

            var sb = new StringBuilder();
            sb.AppendLine("provinse");
            foreach (var p in provinceNames)
            {
                sb.AppendLine(p);
            }

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (var sw = new StreamWriter(fileName))
            {
                sw.Write(sb.ToString());
            }
        }
    }
}
