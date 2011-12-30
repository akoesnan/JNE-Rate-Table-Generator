using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace JNEExcelReader
{
    [DataContract]
    [DebuggerDisplay("RegulerRate = {RegulerRate}, OkeRate = {OkeRate}")]
    public class JNERate
    {
        public Kota Kota { get; set; }

        private decimal regulerRate;
        [DataMember]
        public decimal RegulerRate
        {
            get { return regulerRate; }
            set
            {
                this.regulerRate = value;
                foreach (var k in Kota.KecamatanList)
                {
                    if (Kota.KotaType == KotaType.Kota
                        || (Kota.KotaType == KotaType.Kabupaten && String.Equals(Kota.FirstKecamatan, k.RawName, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        k.JneRate.RegulerRate = value;
                    }
                }
            }
        }

        private decimal okeRate;
        [DataMember]
        public decimal OkeRate
        {
            get { return okeRate; }
            set
            {
                this.okeRate = value;
                foreach (var k in Kota.KecamatanList)
                {
                    if (Kota.KotaType == KotaType.Kota
                        || (Kota.KotaType == KotaType.Kabupaten && string.Equals(Kota.FirstKecamatan, k.RawName, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        k.JneRate.OkeRate = value;
                    }
                }
            }
        }

        private decimal otherRegulerRate;

        [DataMember]
        public decimal OtherRegulerRate
        {
            get { return otherRegulerRate; }
            set
            {
                this.otherRegulerRate = value;
                foreach (var k in Kota.KecamatanList)
                {
                    if (Kota.KotaType == KotaType.Kabupaten && !string.Equals(k.RawName, Kota.FirstKecamatan, StringComparison.InvariantCultureIgnoreCase))
                    {
                        k.JneRate.RegulerRate = value;
                    }
                }
            }
        }

        private decimal otherOkeRate;
        [DataMember]
        public decimal OtherOkeRate
        {
            get { return otherOkeRate; }
            set
            {
                this.otherOkeRate = value;
                foreach (var k in Kota.KecamatanList)
                {
                    if (Kota.KotaType == KotaType.Kabupaten && !string.Equals(k.RawName, Kota.FirstKecamatan, StringComparison.InvariantCultureIgnoreCase))
                    {
                        k.JneRate.OkeRate = value;
                    }
                }
            }
        }

        public JNERate(Kota kota)
        {
            this.Kota = kota;
        }

    }
}
