using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace JNEExcelReader
{
    [DataContract]
    public class ProvinceList
    {
        [DataMember]
        public IList<Province> Provinces { get; set; }

        public ProvinceList()
        {
            this.Provinces = new List<Province>();
        }
    }
}
