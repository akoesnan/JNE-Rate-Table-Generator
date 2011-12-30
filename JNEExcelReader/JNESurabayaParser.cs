using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Excel;

namespace JNEExcelReader
{
    public abstract class JNESurabayaParser
    {
        protected int NoPropinsiColumnIndex = 0;
        protected int PropinsiColumnIndex = 1;
        protected int KotamadyaOrKabupatenColumnIndex = 3;
        protected int KecamatanColumnIndex = 4;
        protected int HargaColumnIndex = 6;
        protected int HargaOtherColumnIndex = 15;

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

            var row = 0;
            var l = new List<string>();
            while (excelReader.Read())
            {
                //for (var i = 0; i < excelReader.FieldCount; ++i)
                //{
                //    l.Add(excelReader.GetString(i));
                //}
                //Console.WriteLine(string.Join(",", l));

                var NoPropinsi = excelReader.GetString(NoPropinsiColumnIndex);
                var Propinse = excelReader.GetString(PropinsiColumnIndex);
                var KotaMadyaOrKabupaten = excelReader.GetString(KotamadyaOrKabupatenColumnIndex);
                var kecamatan = excelReader.GetString(KecamatanColumnIndex);
                var Harga = excelReader.GetString(HargaColumnIndex);
                var HargaOther = excelReader.FieldCount >= HargaOtherColumnIndex ? excelReader.GetString(HargaOtherColumnIndex) : Harga;

                int j = 0;
                if (!string.IsNullOrEmpty(NoPropinsi) && Int32.TryParse(NoPropinsi, out j) && !string.IsNullOrEmpty(Propinse))
                {
                    Propinse = Propinse.Trim();


                    if (!ProvinseList.Any(p => p.Is(Propinse)))
                    {
                        var provinse = new Province(Propinse);
                        currentProvince = provinse;
                        ProvinseList.Add(provinse);
                    }
                    else
                    {
                        currentProvince = ProvinseList.First(p => p.Is(Propinse));
                    }
                }
                else if (!string.IsNullOrEmpty(Propinse))
                {
                    if (currentProvince != null)
                    {
                        currentProvince = ProvinseList.First(p => p.Is(Propinse));
                    }
                }

                if (!string.IsNullOrEmpty(KotaMadyaOrKabupaten) || !string.IsNullOrEmpty(kecamatan))
                {
                    if (currentProvince != null)
                    {

                        var kotaName = !string.IsNullOrEmpty(KotaMadyaOrKabupaten) ? KotaMadyaOrKabupaten : kecamatan;
                        kotaName = kotaName.Trim();
                        kecamatan = kecamatan.Trim();
                        Kota kotaObject = new Kota(kotaName, kecamatan, currentProvince);

                        if (!currentProvince.KotaList.Any(k => k.Equals(kotaObject)))
                        {
                            currentProvince.KotaList.Add(kotaObject);
                        }
                        else
                        {
                            kotaObject = currentProvince.KotaList.First(k => k.Equals(kotaObject));
                        }

                        kotaObject.FirstKecamatan = kecamatan;

                        if (!string.IsNullOrWhiteSpace(HargaOther) || !string.IsNullOrWhiteSpace(Harga))
                        {
                            var otherHarga = !string.IsNullOrWhiteSpace(HargaOther) ? HargaOther : Harga;
                            if (this.RateType == JNERateType.REG)
                            {
                                kotaObject.JneRate.RegulerRate = decimal.Parse(Harga);
                                kotaObject.JneRate.OtherRegulerRate = decimal.Parse(otherHarga);
                            }
                            else if (this.RateType == JNERateType.OKE)
                            {
                                kotaObject.JneRate.OkeRate = decimal.Parse(Harga);
                                kotaObject.JneRate.OtherOkeRate = decimal.Parse(otherHarga);
                            }
                        }
                    }
                }

                row++;
            }

            //6. Free resources (IExcelDataReader is IDisposable)
            excelReader.Close();
        }
    }
}