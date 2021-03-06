﻿using UnityEngine;
using System.Collections;

namespace uSQL
{
    public class SQLStatement
    {
        public static string ALL = "*";
        private string statement;

        public SQLStatement()
        {
            this.statement = "";
        }

        public SQLStatement SELECT(string selection = "*")
        {
            this.statement += "SELECT " + selection;
            return this;
        }

        public SQLStatement FROM(string from)
        {
            this.statement += " FROM " + from;
            return this;
        }

        public SQLStatement WHERE(params SQLParameter[] parameters)
        {
            this.statement += " WHERE";
            int paramCount = 0;
            foreach (SQLParameter param in parameters)
            {
                if (paramCount != 0)
                    this.statement += string.Format(" AND ({0}{2}{1})", param.GetName(), param.GetValue(), param.GetComparator().GetOperator());
                else // if its the first statement we dont need AND
                    this.statement += string.Format(" ({0}{2}{1})", param.GetName(), param.GetValue(), param.GetComparator().GetOperator());

                paramCount++;
            }

            return this;
        }

        public SQLStatement ON(params SQLParameter[] parameters)
        {
            this.statement += " ON";
            int paramCount = 0;
            foreach (SQLParameter param in parameters)
            {
                if (paramCount != 0)
                    this.statement += string.Format(" AND ({0}{2}{1})", param.GetName(), param.GetValue(), param.GetComparator().GetOperator());
                else // if its the first statement we dont need AND
                    this.statement += string.Format(" ({0}{2}{1})", param.GetName(), param.GetValue(), param.GetComparator().GetOperator());

                paramCount++;
            }
            return this;
        }

        public SQLStatement JOIN(SQLJoinType type, string by)
        {
            this.statement += string.Format(" {0} JOIN {1}", type.GetJoinTypeString(), by);
            return this;
        }

        public SQLStatement LIMIT(int start, int finish)
        {
            this.statement += string.Format(" LIMIT {0},{1}", start, finish);
            return this;
        }

        public SQLStatement COUNT(string count_by, bool distinct = false)
        {
            if (distinct)
                this.statement += string.Format(", COUNT(distinct {0})", count_by);
            else this.statement += string.Format(", COUNT({0})", count_by);
            return this;
        }

        public SQLStatement GROUP(string by)
        {
            this.statement += string.Format(" GROUP BY {0}", by);
            return this;
        }


        public string FinalizeStatement()
        {
            return this.statement + ";";
        }
    }
}