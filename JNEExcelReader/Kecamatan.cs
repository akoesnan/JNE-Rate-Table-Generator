using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace JNEExcelReader
{
    [DataContract]
    [DebuggerDisplay("Name = {Name}")]
    public class Kecamatan
    {
        public Kota Kota { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string RawName { get; set; }

        [DataMember]
        public JNERate2 JneRate { get; set; }

        public Kecamatan(string name, Kota kota)
        {
            this.RawName = name;
            this.JneRate = new JNERate2(this);
            this.Name = GetDisplayName(name, kota);
            this.Kota = kota;
        }

        private string GetDisplayName(string name, JNEExcelReader.Kota kota)
        {
            name = name.Capitalize().Trim();
            if (string.Equals(name, kota.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                return name;
            }
            else
            {
                return String.Format("{0}, {1}", name, kota.Name);
            }
        }

        public bool Is(string name)
        {
            if (String.Equals(this.RawName, name, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            else if (String.Equals(this.Kota.Name, name, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Kecamatan;
            if (other != null)
            {
                return this.RawName == other.RawName;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return RawName.GetHashCode();
        }
    }
}
