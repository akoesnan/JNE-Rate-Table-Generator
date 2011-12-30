﻿using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using System;

namespace JNEExcelReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var ProvinseList = new ProvinseList();

            var jp = new JNEJakartaParser();
            jp.Parse(@"C:\JKT.xls", ProvinseList.ProvinseLists);

            JNESurabayaParser p1 = new JNERegulerSurabayaParser();
            p1.Parse(@"C:\REGULAR.xls", ProvinseList.ProvinseLists);

            JNESurabayaParser p2 = new JNEOKESurabayaParser();
            p2.Parse(@"C:\OKE.xls", ProvinseList.ProvinseLists);

            FileStream stream1 = new FileStream("final.json", FileMode.OpenOrCreate);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ProvinseList));
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