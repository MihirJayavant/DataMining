using System;
using System.Collections.Generic;

namespace DataMining
{
    public class NaivesBayes
    {
        Table table;
        int totalColumns;
        int totalYes, totalNo;

        public NaivesBayes(Table table)
        {
            this.table = table;
            totalColumns = table.TotalColumns;
            Compute_Yes_No();
            GroupTables = new List<Table>();
        }

        public Table Table
        {
            get
            {
                return table;
            }
        }

        public IList<Table> GroupTables
        {
            private set;
            get;
        }

        public IDictionary<string,float> FindFor(TableRow condition)
        {
            if (condition.data.Length != totalColumns)
                throw new InvalidOperationException("Column length mismatch exception");


            for (int i = 0; i < totalColumns - 1; i++)
            {
                GroupTables.Add(GetGroupTableOf(i));
            }

            //variables needed
            IList<(float yes, float no)> neededProbablilities = new List<(float, float)>();
            float yes = 0, no = 0;
            float yesProbability = (float) totalYes / (totalYes + totalNo);
            float noProbability = (float) totalNo / (totalYes + totalNo);
            (float yes, float no) values;

            for (int i = 0; i < totalColumns - 1; i++)
            {
                values = GetProbabilityFrom(GroupTables[i], condition.data[i]);
                neededProbablilities.Add(values);
            }

            //probability for yes
            foreach (var value in neededProbablilities)
            {
                if (yes == 0)
                    yes = value.yes;
                else
                    yes *= value.yes;
            }
            yes *= yesProbability;

            //probability for no
            foreach (var value in neededProbablilities)
            {
                if (no == 0)
                    no = value.no;
                else
                    no *= value.no;
            }
            no *= noProbability;

            IDictionary<string, float> probability = new Dictionary<string, float>
            {
                { "yes", yes },
                { "no", no }
            };
            return probability;
        }

        private Table GetGroupTableOf(int column)
        {
            IList<string> names = new List<string>();
            Table groupTable;
            TableRow groupRow;
            (float yes, float no) tuple;
            string[] s = {table.ColumnNames.data[column], "yes", "no" };

            groupRow.data = s;
            groupTable = new Table(groupRow);

            foreach (TableRow row in table)
            {
                if(!names.Contains( row.data[column] ))
                {
                    names.Add(row.data[column]);
                    tuple = GetProbabilityOf(column, row.data[column]);
                    s = new string[3];
                    s[0] = row.data[column];
                    s[1] = tuple.yes + "";
                    s[2] = tuple.no + "";
                    groupRow.data = s;
                    groupTable.Add(groupRow);
                }
            }
            return groupTable;
        }

        private (float yes, float no) GetProbabilityOf(int column, string name)
        {
            float yes = 0, no = 0;

            foreach (TableRow row in table)
            {
                if( row.data[column] .Equals (name, StringComparison.CurrentCultureIgnoreCase) )
                {
                    if (row.data[totalColumns - 1].Equals("yes", StringComparison.CurrentCultureIgnoreCase))
                    {
                        yes++;
                    }
                    else
                        no++;
                }
            }
            return ( yes/totalYes, no/totalNo);
        }

        private void Compute_Yes_No()
        {

            foreach (TableRow row in table)
            {
                if (row.data[totalColumns - 1].Equals("yes", StringComparison.CurrentCultureIgnoreCase))
                {
                    totalYes++;
                }
                else
                    totalNo++;
            }
        }

        private (float yes, float no) GetProbabilityFrom(Table table, string name)
        {
            float yes = 0, no = 0;

            foreach (TableRow row in table)
            {
                if( row.data[0]. Equals( name, StringComparison.CurrentCultureIgnoreCase ) )
                {
                    yes = float.Parse(row.data[1]);
                    no = float.Parse(row.data[2]);
                    break;
                }
            }
            return (yes, no);
        }
    }
}
