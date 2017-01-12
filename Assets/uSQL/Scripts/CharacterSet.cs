using UnityEngine;
using System.Collections;

namespace uSQL
{
    public enum CharacterSet
    {
        UTF8,
        UTF8MB4,
        LATIN1,
        LATIN2,
        LATIN3,
        LATIN4,
        LATIN5,
        LATIN7,
        BIG5,
        DEC8,
        CP850,
        HP8,
        KOI8R,
        SWE7,
        UJIS,
        EUCJPMS,
        SJIS,
        CP932,
        HEBREW,
        TIS620,
        EUCKR,
        EUC_KR,
        KOI8U,
        KOI8_RU,
        GB2312,
        GBK,
        GREEK,
        CP1250,
        WIN1250,
        ARMSCII8,
        UCS2,
        CP866,
        KEYBCS2,
        MACCE,
        MACROMAN,
        CP852,
        CP1251,
        WIN1251,
        WIN1251UKR,
        CP1251CSAS,
        CP1252CIAS,
       // WIN1251,
        CP1256,
        CP1257,
        ASCII,
        USA7,
        BINARY,
        LATIN1_DE,
        GERMAN1,
        DANISH,
        CZECH,
        HUNGARIAN,
        CROAT,
        LATVIAN,
        LATVIAN1,
        ESTONIA,
        DOS,
    }

    public static class CharacterSetExtensions
    {
        public static string SqlConnectionType(this CharacterSet set)
        {
            return System.Enum.GetName(typeof(CharacterSet), set).ToLower();
        }
    }
}