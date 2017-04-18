using DataMining;
using System;

namespace TestDataMining
{
    class Program
    {
        static void Main(string[] args)
        {
            NaivesBayesHelper nbh = NaivesBayesHelper.GetNaivesBayesInstance(@"D:\Main-doc\not\test.txt");
            NaivesBayesHelper.Answer ans = nbh.Start();
            Console.WriteLine("Answer is " + ans.ans);

            Console.WriteLine("\nGiven table was as follows:");
            Console.WriteLine(Table.FormatTable(ans.table));

            Console.WriteLine("\nSolution is as follows:\nGroup tables generated are:\n");
            foreach (var table in ans.groupTable)
            {
                Console.WriteLine(Table.FormatTable(table) + "\n");
            }
            Console.WriteLine("Yes probability: " + ans.yesProbability + "\nNo probability: " + ans.noProbability);
            Console.WriteLine("Answer is " + ans.ans);
            Console.ReadLine();
        }
    }
}