using UnityEngine;
using System.Collections;

namespace uSQL
{
    /// <summary>
    /// Represents a value comparison in SQL syntax. Example:
    /// <example>WHERE ('column-value-a'<>'4') AND ('column-value-b'='yes')</example>
    /// </summary>
    public class SQLParameter
    {
        private string name;
        private string value;
        private SQLComparator comp;

        public SQLParameter(string parameterName, string parameterValue, SQLComparator comp)
        {
            this.name = parameterName;
            this.value = parameterValue;
            this.comp = comp;
        }

        public string GetName()
        {
            return name;
        }

        public string GetValue()
        {
            return value;
        }

        public SQLComparator GetComparator()
        {
            return this.comp;
        }
    }
}
