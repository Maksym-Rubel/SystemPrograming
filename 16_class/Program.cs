using System.Collections.Generic;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    private static void Main(string[] args)
    {
        //Task 1
        List<int> numbers = new List<int> { };
        using (StreamReader sr = new StreamReader("new.txt"))
        {
            string[] number = sr.ReadToEnd().Split(new[] { ' ', ',', '\n', '\r' });
            foreach (var num in number)
            {
                int num1 = int.Parse(num);
                numbers.Add(num1);
            }
        }
        Console.Write("Numbers --> ");
        foreach (var num in numbers)
        {
            Console.Write(num + "\t");
        }
        Console.WriteLine();
        int uniqueNumbersCount = numbers.AsParallel()
                           .Where(n => IsUnicua(n, numbers))
                           .ToList()
                           .Count();

        Console.WriteLine("Unique numbers --> " + uniqueNumbersCount);

        //Task 2
        List<int> numbers1 = new List<int> { };
        using (StreamReader sr1 = new StreamReader("new2.txt"))
        {
            string[] number = sr1.ReadToEnd().Split(new[] { ' ', ',', '\n', '\r' });
            foreach (var num in number)
            {
                int num1 = int.Parse(num);
                numbers1.Add(num1);
            }
        }
        Console.Write("Numbers --> ");
        foreach (var num in numbers1)
        {
            Console.Write(num + "\t");
        }
        Console.WriteLine();

        int UpperCount = IsUpper(numbers1); // незнаю як тут з PLINQ викликати



        Console.WriteLine("Upper numbers --> " + UpperCount);
    }

    public static bool IsUnicua(int num, List<int> numbers)
    {
        int count = 0;
        foreach (var item in numbers)
        {
            if (item == num)
            {
                count++;
                if(count > 1)
                {
                    return false;
                }
                
            }
        }
        return true;



    }
    public static int IsUpper(List<int> numbers)
    {
        int maxCount = 0;
        int upperCount = 1;
        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] > numbers[i - 1])
            {
                upperCount++;
            }
            else
            {
                if(upperCount > maxCount)
                {
                    maxCount = upperCount;
                }
                upperCount = 1;
            }
        }

        return maxCount;



    }
}