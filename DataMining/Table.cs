using System.Collections;
using System.Collections.Generic;

namespace DataMining
{
    /// <include file='docs.xml' path='docs/members[@name="TableRow"]/TableRow/*'/>
    public struct TableRow
    {
        /// <include file='docs.xml' path='docs/members[@name="TableRow"]/data/*'/>
        public string[] data;
    }

    /// <include file='docs.xml' path='docs/members[@name="Table"]/Table/*'/>
    public class Table : IEnumerable<TableRow>
    {
       
        IList<TableRow> tableList;

        /// <include file='docs.xml' path='docs/members[@name="TableConstructor"]/Table/*'/>
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
    }
}
