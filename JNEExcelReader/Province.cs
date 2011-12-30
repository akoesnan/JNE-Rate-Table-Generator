using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace JNEExcelReader
{
    [DataContract]
    [DebuggerDisplay("Name = {Name}")]
    public class Province
    {
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public String RawName { get; set; }

        [DataMember]
        public IList<Kota> KotaList { get; set; }

        public Province(string name)
        {
            this.RawName = name;
            this.Name = name.Trim().Capitalize();
            KotaList = new List<Kota>();
        }

        public bool Is (string name)
        {
            return String.Equals(this.Name, name, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Province;
            if (other != null)
            {
                return this.Name == other.Name;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
