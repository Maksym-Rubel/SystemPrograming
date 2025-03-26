using System.Net.Sockets;
using System.Threading;

internal class Program
{
    private static int max = 0;
    private static int min = 100;
    private static double average = 0;
    private static int[] numbers;
    private static void Main(string[] args)
    {
        int for_count = 10_000;
        numbers = new int[for_count];
        int num = 0;
        Random random = new Random();
        for (int i = 0; i < for_count; i++)
        {
            numbers[i] = random.Next(1, 101);

            Console.WriteLine(numbers[i]);
            


        }
        Thread Tmax = new Thread(Max);
        
        Thread Tmin = new Thread(Min);
   
        Thread Taverage = new Thread(Average);
   
        Thread Tfiles = new Thread(File);
        Tmax.Start();
        Tmin.Start();
        Taverage.Start();
    
        Tmax.Join();
        Tmin.Join();
        Taverage.Join();

        Tfiles.Start();
        Tfiles.Join();
        Console.WriteLine($"Max --> {max}");
        Console.WriteLine($"Max --> {min}");
        Console.WriteLine($"Average --> {average}");

        
    }

    static void Max()
    {
        

        foreach (var num in numbers)
        {
            
                if (num > max)
                {
                    max = num;
                }
            
        }
    }
    static void Min()
    {

        foreach (var num in numbers)
        {

            if (num < min)
            {
                min = num;
            }

        }
    }
    static void Average()
    {

        average = numbers.Average();

    }
    static void File()
    {
        
        string path = @"numbers.txt";
        using (StreamWriter sw = new StreamWriter(path,true))
        {
            sw.WriteLine("Згенеровані числа:");
            sw.WriteLine(string.Join("\n", numbers));
            sw.WriteLine($"Максимальне число: {max}");
            sw.WriteLine($"Мінімальне число: {min}");
            sw.WriteLine($"Середнє арифметичне: {average}");
        }
        
    }
}