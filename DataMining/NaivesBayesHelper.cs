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

        
    }
}
