using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace JNEExcelReader
{
    public class GenerateStateList
    {
        private const string fileName = @".\propinsi.csv";
        public void Execute(ProvinceList provinces)
        {            
            var provinceNames = provinces.Provinces.Select(p => CsvUtility.ToLine(p.Name));

            CsvUtility.WriteFile(fileName, "provice", provinceNames);

        }
    }
}
