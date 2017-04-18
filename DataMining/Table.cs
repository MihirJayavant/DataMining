using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataMining
{
    /// <include file='docs.xml' path='docs/members[@name="TableRow"]/TableRow/*'/>
    public struct TableRow
    {
        /// <include file='docs.xml' path='docs/members[@name="TableRow"]/data/*'/>
        public string[] data;
    }

    /// <include file='docs.xml' path='docs/members[@name="Table"]/Table/*'/>
    public class Table : ITable
    {
       
        IList<TableRow> tableList;

        /// <include file='docs.xml' path='docs/members[@name="Table"]/TableConstructor/*'/>
        public Table(TableRow columns)
        {
            TotalColumns = columns.data.Length;
            ColumnNames = columns;
            tableList = new List<TableRow>();
        }

        public TableRow this[int row]
        {
            set
            {
                if(value.data.Length == TotalColumns)
                    tableList[row] = value;
            }
            get
            {
                return tableList[row];
            }
        }

        ///<include file='docs.xml' path='docs/members[@name="Table"]/propertise/ColumnNames/*'/>
        public TableRow ColumnNames
        {
            private set;
            get;
        }

        public int TotalColumns
        {
            private set;
            get;
        }

        public int TotalRows
        {
            get
            {
                return tableList.Count;
            }
        }

        public bool Add(TableRow t)
        {
            if(t.data.Length == TotalColumns)
            {
                tableList.Add(t);
                return true;
            }

            return false;
        }

        public IEnumerator GetEnumerator()
        {
            return tableList.GetEnumerator();
        }

        IEnumerator<TableRow> IEnumerable<TableRow>.GetEnumerator()
        {
            return (IEnumerator<TableRow>) GetEnumerator();
        }

        public static string FormatTable(Table table)
        {
            int[] lengths = FindMaxColumnLenghts(table);
            StringBuilder s = new StringBuilder("");

            for (int i = 0; i < lengths.Length; i++)
            {
                s.Append(AddPadding(table.ColumnNames.data[i], lengths[i]) + " ");
            }

            s.Append("\n");

            for (int i = 0; i < table.TotalRows; i++)
            {
                for (int j = 0; j < table.TotalColumns; j++)
                {
                    s.Append($"{ AddPadding(table[i].data[j], lengths[j]) } ");
                }
                s.Append("\n");
            }

            return s.ToString();
        }

        private static int[] FindMaxColumnLenghts(Table table)
        {
            int len = table.ColumnNames.data.Length;
            int[] lengths = new int[len];

            for (int i = 0; i < len; i++)
            {
                if (lengths[i] < table.ColumnNames.data[i].Length)
                {
                    lengths[i] = table.ColumnNames.data[i].Length;
                }
            }

            for (int i = 0; i < table.TotalRows; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    if (lengths[j] < table[i].data[j].Length)
                    {
                        lengths[j] = table[i].data[j].Length;
                    }
                }
            }

            return lengths;
        }

        private static string AddPadding(string s, int size)
        {
            StringBuilder s2 = new StringBuilder(s);
            for (int i = s.Length; i <= size; i++)
            {
                s2.Append(' ');
            }
            return s2.ToString();
        }
    }
}
