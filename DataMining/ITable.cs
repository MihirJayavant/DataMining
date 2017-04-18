
using System.Collections.Generic;

namespace DataMining
{
    interface ITable : IEnumerable<TableRow>
    {
        TableRow this[int row]
        {
            set;
            get;
        }

        TableRow ColumnNames
        {
            get;
        }

        int TotalColumns
        {
            get;
        }

        int TotalRows
        {
            get;           
        }

        bool Add(TableRow t);
    }
}
