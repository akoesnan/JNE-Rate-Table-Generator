namespace JNEExcelReader
{
    public class JNERegulerSurabayaParser : JNESurabayaParser
    {
        public JNERegulerSurabayaParser()
        {
            NoPropinsiColumnIndex = 0;
            PropinsiColumnIndex = 1;
            KotamadyaOrKabupatenColumnIndex = 3;
            KecamatanColumnIndex = 4;
            HargaColumnIndex = 6;
            HargaOtherColumnIndex = 15;

            RateType = JNERateType.REG;
        }
    }
}