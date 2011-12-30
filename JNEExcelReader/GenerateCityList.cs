using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace JNEExcelReader
{
    public class GenerateCityList
    {
        public void Execute(ProvinseList provinses)
        {
            var fileName = @".\Kota.csv";
            var KabupatenKotaNames = provinses.ProvinseLists.SelectMany(p => p.KotaList).Where(k => k.KotaType == KotaType.Kabupaten).SelectMany(k => k.KecamatanList).Select(k => String.Format("{0}\t{1}\t{2}", k.Kota.Province.Name, k.Kota.Name, k.Name));
            var KotaNames = provinses.ProvinseLists.SelectMany(p => p.KotaList).Where(k => k.KotaType == KotaType.Kota).Select(k => String.Format("{0}\t{1}\t{2}", k.Province.Name, k.Name, k.Name));
            KotaNames = KotaNames.Union(KabupatenKotaNames).OrderBy(k => k);


            var sb = new StringBuilder();
            sb.AppendLine("provinse, kota-kabupaten, city");
            foreach (var p in KotaNames)
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
