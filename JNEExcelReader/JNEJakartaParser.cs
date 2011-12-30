using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Excel;
using System.Data;

namespace JNEExcelReader
{
    public class JNEJakartaParser
    {
        protected int PropinsiColumnIndex = 2;
        protected int KotamadyaOrKabupatenColumnIndex = 0;
        protected int KecamatanColumnIndex = 1;

        protected JNERateType RateType { get; set; }

        public void Parse(string filePath, IList<Province> ProvinseList)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            //...
            //1. Reading from a binary Excel file ('97-2003 format; *.xls)
            IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            //...
            //3. DataSet - The result of each spreadsheet will be created in the result.Tables
            DataSet result = excelReader.AsDataSet();

            //5. Data Reader methods
            Province currentProvince = null;
            Kota currentKota = null;
            var row = 0;
            var l = new List<string>();
            while (excelReader.Read())
            {
                
                var PropinsiName = excelReader.GetString(PropinsiColumnIndex);
                var KotaMadyaOrKabupatenName = excelReader.GetString(KotamadyaOrKabupatenColumnIndex);
                var KecamatanName = excelReader.GetString(KecamatanColumnIndex);
                if (row >= 9)
                {
                    int j = 0;
                    if (!string.IsNullOrWhiteSpace(PropinsiName) && String.IsNullOrEmpty(KotaMadyaOrKabupatenName) &&
                        string.IsNullOrEmpty(KecamatanName))
                    {
                        if (
                            !ProvinseList.Any(
                                p => String.Equals(p.Name, PropinsiName, StringComparison.InvariantCultureIgnoreCase)))
                        {

                            ProvinseList.Add(new Province(PropinsiName));
                        }
                        var Propinsi = ProvinseList.First(
                            p => String.Equals(p.Name, PropinsiName, StringComparison.InvariantCultureIgnoreCase));
                        currentProvince = Propinsi;
                    }
                    else if (string.IsNullOrWhiteSpace(PropinsiName) && !String.IsNullOrEmpty(KotaMadyaOrKabupatenName) &&
                             string.IsNullOrEmpty(KecamatanName))
                    {
                        if (currentProvince != null)
                        {
                            if (
                                !currentProvince.KotaList.Any(
                                    k =>
                                    string.Equals(k.RawName, KotaMadyaOrKabupatenName,
                                                  StringComparison.CurrentCultureIgnoreCase)))
                            {
                                var kota = new Kota(KotaMadyaOrKabupatenName, string.Empty, currentProvince);
                                currentProvince.KotaList.Add(kota);
                            }
                            currentKota =
                                currentProvince.KotaList.First(
                                    k =>
                                    string.Equals(k.RawName, KotaMadyaOrKabupatenName,
                                                  StringComparison.CurrentCultureIgnoreCase));
                        }
                        else
                        {
                            throw new Exception("The current provinse should never be null");
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(PropinsiName) &&
                             String.IsNullOrEmpty(KotaMadyaOrKabupatenName) &&
                             !string.IsNullOrEmpty(KecamatanName))
                    {
                        if (currentKota != null)
                        {
                            if (
                                !currentKota.KecamatanList.Any(
                                    k =>
                                    string.Equals(k.RawName, KecamatanName, StringComparison.CurrentCultureIgnoreCase)))
                            {
                                var kecamatan = new Kecamatan(KecamatanName, currentKota);    
                                currentKota.KecamatanList.Add(kecamatan);
                            }
                        }
                    }
                }
                row++;

                //6. Free resources (IExcelDataReader is IDisposable)
                excelReader.Close();
            }
        }
    }
}
