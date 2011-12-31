using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace JNEExcelReader
{
    public class GenerateRateList
    {
        private const string FileName = @".\Rate.csv";
        private const string ErrorFileName = @".\RateError.csv";
        public void Execute(ProvinceList provinces)
        {

            var kabupatenKotaNames = provinces.Provinces.SelectMany(p => p.KotaList).Where(k => k.KotaType == KotaType.Kabupaten).SelectMany(k => k.KecamatanList).Select(k => CsvUtility.ToLine(k.Kota.Province.Name, k.Kota.Name, k.Name, k.JneRate.OkeRate, k.JneRate.RegulerRate));
            var kotaNames = provinces.Provinces.SelectMany(p => p.KotaList).Where(k => k.KotaType == KotaType.Kota).Select(k => CsvUtility.ToLine(k.Province.Name, k.Name, k.Name, k.JneRate.OkeRate, k.JneRate.RegulerRate));
            kotaNames = kotaNames.Union(kabupatenKotaNames).OrderBy(k => k);

            CsvUtility.WriteFile(FileName, "provice, city-kabupaten, city, jneoke, jnereguler", kotaNames);

            // print out the one that has no rate
            var errorkotaNames = provinces.Provinces.SelectMany(p => p.KotaList).SelectMany(k => k.KecamatanList).Where(k => k.JneRate.OkeRate == 0 && k.JneRate.RegulerRate == 0).Select(k => CsvUtility.ToLine(k.Kota.Province.Name, k.Name, k.JneRate.OkeRate, k.JneRate.RegulerRate));

            CsvUtility.WriteFile(ErrorFileName, "provice, city, jneoke, jnereguler", errorkotaNames);

        }
    }
}
