using System.Collections.Generic;
using System.Text;

namespace DataMining
{
    public class NaivesBayesHelper
    {
        public struct Answer
        {
            public float yesProbability, noProbability;
            public Table table;
            public IList<Table> groupTable;
            public string ans;
        }

        NaivesBayes naviesbayes;
        TableRow condition;

        public NaivesBayesHelper(NaivesBayes nb, TableRow condition)
        {
            naviesbayes = nb;
            this.condition = condition;
        }

        public static NaivesBayesHelper GetNaivesBayesInstance(string absolutePath)
        {
            string[] s = System.IO.File.ReadAllLines(absolutePath);

            TableRow row;
            Table table;
            int len = s.Length;

            row.data = s[0].Split(' ');
            table = new Table(row);

            for (int i = 1; i < len - 1; i++)
            {
                row.data = s[i].Split(' ');
                table.Add(row);
            }
            row.data = s[len - 1].Split(' ');

            return new NaivesBayesHelper(new NaivesBayes(table), row);
        }

        public Answer Start()
        {
            Answer ans;
            var dictionary = naviesbayes.FindFor(condition);
            dictionary.TryGetValue("yes", out ans.yesProbability);
            dictionary.TryGetValue("no", out ans.noProbability);
            ans.groupTable = naviesbayes.GroupTables;
            ans.table = naviesbayes.Table;

            if (ans.yesProbability > ans.noProbability)
                ans.ans = "yes";
            else
                ans.ans = "no";

            return ans;
        }

        public static string FormatTable(Table table)
        {
            int[] lengths = FindMaxColumnLenghts(table);
            StringBuilder s = new StringBuilder("");

            for(int i = 0; i < lengths.Length; i++)
            {
                s.Append( AddPadding( table.ColumnNames.data[i], lengths[i]) + " ");
            }

            s.Append("\n");

            for (int i = 0; i < table.TotalRows; i++)
            {
                for (int j = 0; j < table.TotalColumns; j++)
                {
                    s.Append($"{ AddPadding( table[i].data[j], lengths[j] ) } ");
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
                    if(lengths[j] < table[i].data[j].Length)
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
