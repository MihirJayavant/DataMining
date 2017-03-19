using System.Collections;
using System.Collections.Generic;

namespace DataMining
{
    /// <summary>
    /// TableRow structure indicates a row in Table class
    /// </summary>
    public struct TableRow
    {
        /// <summary>
        /// Each string in this data array is used to store a column data of a single row.
        /// Length of data array is same as number of columns in table.
        /// columns number starts from 0.
        /// </summary>
        public string[] data;
    }

    /// <summary>
    /// Table class is a data structure which reprensents a table.
    /// It contains rows and columns and also table header.
    /// </summary>
    public class Table : IEnumerable<TableRow>
    {
        /// <summary>
        /// Rows are stored in form of a collection list.
        /// </summary>
        IList<TableRow> tableList;

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
