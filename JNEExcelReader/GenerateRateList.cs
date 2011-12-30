using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace JNEExcelReader
{
    public class GenerateRateList
    {

        public void Execute(ProvinseList provinses)
        {
            var fileName = @".\Rate.tsv";
            var errorFileName = @".\RateError.tsv";
            var KabupatenKotaNames = provinses.ProvinseLists.SelectMany(p => p.KotaList).Where(k => k.KotaType == KotaType.Kabupaten).SelectMany(k => k.KecamatanList).Select(k => String.Format("{0}\t{1}\t{2}\t{3}\t{4}", k.Kota.Province.Name, k.Kota.Name, k.Name, k.JneRate.OkeRate, k.JneRate.RegulerRate));
            var KotaNames = provinses.ProvinseLists.SelectMany(p => p.KotaList).Where(k => k.KotaType == KotaType.Kota).Select(k => String.Format("{0}\t{1}\t{2}\t{3}\t{4}", k.Province.Name, k.Name, k.Name, k.JneRate.OkeRate, k.JneRate.RegulerRate));
            KotaNames = KotaNames.Union(KabupatenKotaNames).OrderBy(k => k);

            var sb = new StringBuilder();
            sb.AppendLine("provinse, city-kabupaten, city, jneoke, jnereguler");
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

            // print out the one that has no rate
            var errorkotaNames = provinses.ProvinseLists.SelectMany(p => p.KotaList).SelectMany(k => k.KecamatanList).Where(k => k.JneRate.OkeRate == 0 && k.JneRate.RegulerRate == 0).Select(k => String.Format("{0}\t{1}\t{2}\t{3}", k.Kota.Province.Name, k.Name, k.JneRate.OkeRate, k.JneRate.RegulerRate));

            sb = new StringBuilder();
            sb.AppendLine("provinse, city, jneoke, jnereguler");
            foreach (var p in errorkotaNames)
            {
                sb.AppendLine(p);
            }

            if (File.Exists(errorFileName))
                File.Delete(errorFileName);

            using (var sw = new StreamWriter(errorFileName))
            {
                sw.Write(sb.ToString());
            }

        }
    }
}
