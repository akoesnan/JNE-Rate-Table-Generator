using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace JNEExcelReader
{
    public class GenerateCityList
    {
        private const string FileName = @".\Kota.csv";

        public void Execute(ProvinceList provinces)
        {

            var kabupatenKotaNames = provinces.Provinces.SelectMany(p => p.KotaList).Where(k => k.KotaType == KotaType.Kabupaten).SelectMany(k => k.KecamatanList).Select(k => CsvUtility.ToLine(k.Kota.Province.Name, k.Kota.Name, k.Name));
            var kotaNames = provinces.Provinces.SelectMany(p => p.KotaList).Where(k => k.KotaType == KotaType.Kota).Select(k => CsvUtility.ToLine(k.Province.Name, k.Name, k.Name));
            kotaNames = kotaNames.Union(kabupatenKotaNames).OrderBy(k => k);

            CsvUtility.WriteFile(FileName, "", kotaNames);
        }
    }
}
