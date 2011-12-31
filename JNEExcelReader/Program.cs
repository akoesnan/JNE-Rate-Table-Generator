using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using System;

namespace JNEExcelReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var ProvinseList = new ProvinceList();

            var jp = new JNEJakartaParser();
            jp.Parse(@"Input\JKT.xls", ProvinseList.Provinces);

            JNESurabayaParser p1 = new JNERegulerSurabayaParser();
            p1.Parse(@"Input\REGULAR.xls", ProvinseList.Provinces);

            JNESurabayaParser p2 = new JNEOKESurabayaParser();
            p2.Parse(@"Input\OKE.xls", ProvinseList.Provinces);

            FileStream stream1 = new FileStream("final.json", FileMode.OpenOrCreate);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ProvinceList));
            ser.WriteObject(stream1, ProvinseList);

            var StateData = new GenerateStateList();
            StateData.Execute(ProvinseList);

            var CityData = new GenerateCityList();
            CityData.Execute(ProvinseList);

            var RateData = new GenerateRateList();
            RateData.Execute(ProvinseList);

        }
    }
}
