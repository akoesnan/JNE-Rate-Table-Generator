namespace JNEExcelReader
{
    public class JNEOKESurabayaParser : JNESurabayaParser
    {
        public JNEOKESurabayaParser()
        {
            NoPropinsiColumnIndex = 0;
            PropinsiColumnIndex = 1;
            KotamadyaOrKabupatenColumnIndex = 3;
            KecamatanColumnIndex = 4;
            HargaColumnIndex = 5;
            HargaOtherColumnIndex = 12;
            RateType = JNERateType.OKE;
        }
    }
}