using UnityEngine;
using System.Collections;

namespace uSQL
{
    public enum SQLComparator
    {
        EQUAL,
        LESS_THAN,
        MORE_THAN,
        LESS_THAN_EQUAL_TO,
        MORE_THAN_EQUAL_TO,
        NOT_EQUAL
    }

    public static class SQLComparatorExtensions
    {
        public static string GetOperator(this SQLComparator comp)
        {
            switch(comp)
            {
                case SQLComparator.EQUAL: return "=";
                case SQLComparator.LESS_THAN: return "<";
                case SQLComparator.MORE_THAN: return ">";
                case SQLComparator.LESS_THAN_EQUAL_TO: return "<=";
                case SQLComparator.MORE_THAN_EQUAL_TO: return "<=";
                case SQLComparator.NOT_EQUAL: return "<>";

                default: return "=";
            }
        }
    }
}
