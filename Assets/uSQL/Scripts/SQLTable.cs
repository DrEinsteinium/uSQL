using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace uSQL
{
    /// <summary>
    /// SQLTable is a storage system used to store row and column results from 
    /// statements sent to an SQLConnector.
    /// </summary>
    public class SQLTable
    {
        private List<Dictionary<string, object>> table;

        public SQLTable()
        {
            this.table = new List<Dictionary<string, object>>();
        }

        public int RowCount()
        {
            return this.table.Count;
        }

        public Dictionary<string, object> this[int row]
        {
            get
            {
                return table[row];
            }
        }

        public object this[int row, string col]
        {
            get
            {
                return table[row][col];
            }
            set
            {
                table[row][col] = value;
            }
        }

        public Dictionary<string, object> AddRow()
        {
            Dictionary<string, object> row = new Dictionary<string, object>();
            this.table.Add(row);
            return row;
        }
    }
}
