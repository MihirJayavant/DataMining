# DataMining library in c#
>version 1.0.0

It contains classes that implemented some data mining algorithms and also steps on how it solved it.
This library was built for primary reasons:
  - Steps are shown on how it solved the problem
  - Take input fron user or text file or any other source and solve it
  - It was design to help students in data mining field in solving data mining algos and help them understand how it was done

## Features!

  - It shows complete solution of the algorithm implemented.
  - Documentation of this library is made using visual studio 2017


## Classes Available

  - specific algorithm classes
  - data structure classes
  - helper classes built around algorithm classes 

## DataMiningTest namespace
Its a console application to test the output of `DataMining` library

## Example
   - This few lines of c# code displays the complete solution in steps for naives bayes algorithm.
   - The input is taken from text file stored locally using helper class.
```cs
 NaivesBayesHelper nbh = NaivesBayesHelper.GetNaivesBayesInstance(@"D:\Main-doc\not\test.txt");
            NaivesBayesHelper.Answer ans = nbh.Start();
            Console.WriteLine("Answer is " + ans.ans);

            Console.WriteLine("\nGiven table was as follows:");
            Console.WriteLine(NaivesBayesHelper.FormatTable(ans.table));

            Console.WriteLine("\nSolution is as follows:\nGroup tables generated are:\n");
            foreach (var table in ans.groupTable)
            {
                Console.WriteLine(NaivesBayesHelper.FormatTable(table) + "\n");
            }
            Console.WriteLine("Yes probability: " + ans.yesProbability + "\nNo probability: " + ans.noProbability);
            Console.WriteLine("Answer is " + ans.ans);
            Console.ReadLine();
```

## Limitation
Atleast for now only naivesbayes algorithm is implemented.
