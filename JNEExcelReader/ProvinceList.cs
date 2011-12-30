using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace JNEExcelReader
{
    [DataContract]
    public class ProvinseList
    {
        [DataMember]
        public IList<Province> ProvinseLists { get; set; }

        public ProvinseList()
        {
            this.ProvinseLists = new List<Province>();
        }
    }
}
