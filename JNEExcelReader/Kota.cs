using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Globalization;

namespace JNEExcelReader
{
    [DataContract]
    [DebuggerDisplay("Name = {Name}")]
    public class Kota
    {
        [DataMember]
        public string FirstKecamatan { get; set; }

        [DataMember]
        public string RawName { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public JNERate JneRate { get; set; }

        public Province Province { get; set; }

        [DataMember]
        public KotaType KotaType
        {
            get;
            set;
        }

        [DataMember]
        public IList<Kecamatan> KecamatanList { get; set; }

        public Kota(string kotaMadyaOrKabupaten, string kecamatan, Province province)
        {
            this.Province = province;
            if (!string.IsNullOrEmpty(kotaMadyaOrKabupaten))
            {
                this.KotaType = this.GetKotaType(kotaMadyaOrKabupaten);
                this.Name = ScrubKotaName(kotaMadyaOrKabupaten);
                this.RawName = kotaMadyaOrKabupaten;
            }
            this.JneRate = new JNERate(this);
            this.KecamatanList = new List<Kecamatan>();
            this.FirstKecamatan = kecamatan;
        }

        public KotaType GetKotaType(string name)
        {
            if (name.StartsWith("Kab.", StringComparison.CurrentCultureIgnoreCase))
            {
                return KotaType.Kabupaten;
            }
            else if (name.StartsWith("Kota", StringComparison.CurrentCultureIgnoreCase))
            {
                return KotaType.Kota;
            }
            else
            {
                return KotaType.Unknown;
            }
        }

        public String ScrubKotaName(string name)
        {
            var city = Regex.Replace(name, @"^(Kab\.|Kota.(Administrasi)?)", string.Empty, RegexOptions.IgnoreCase);
            return city.Trim().Capitalize();
        }
       
        public override bool Equals(object obj)
        {
            var other = obj as Kota;
            if (other != null)
            {
                if (this.KotaType == other.KotaType)
                {
                    if (String.Equals(other.RawName, this.RawName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return true;
                    }

                    if (String.Equals(other.Name, this.Name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return true;
                    }

                    if (this.Name.ToLower().Distance(other.Name) < 2)
                    {
                        return true;
                    }
                }
                return false;                
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

   

    [DataContract]
    public enum KotaType
    {
        Kota, Kabupaten, Unknown
    }

    [DataContract]
    [DebuggerDisplay("RegulerRate = {RegulerRate}, OkeRate = {OkeRate}")]
    public class JNERate2
    {
        public Kecamatan Kecamatan { get; set; }

        [DataMember]
        public decimal RegulerRate { get; set; }
        [DataMember]
        public decimal OkeRate { get; set; }

        public JNERate2(Kecamatan kecamatan)
        {
            this.Kecamatan = kecamatan;
        }
    }

    public enum JNERateType
    {
        OKE, REG, YES
    }
}
