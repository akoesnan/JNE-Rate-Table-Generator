using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace JNEExcelReader
{
    public class CsvUtility
    {
        public static string ToLine (params object[] values)
        {
            if (values != null && values.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < values.Length; i++)
                {
                    var value = values[i] is string ? (string)values[i] : values[i].ToString();
                    value = value.Contains(",") ? string.Format("\"{0}\"", value) : value;
                    sb.Append(value);                
                    if (i < values.Length - 1)
                    {
                        sb.Append(",");
                    }
                }
                return sb.ToString();
            }
            return string.Empty;
        }

        public static void WriteFile (string fileName, string firstLine, IEnumerable<string> lines )
        {
            var sb = new StringBuilder();
            sb.AppendLine(firstLine);
            foreach (var p in lines)
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
