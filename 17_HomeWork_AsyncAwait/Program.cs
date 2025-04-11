using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Parallel.Invoke(
               () => Factorial(5)
        );

        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Console.Write("Введіть нижню межу діапазону: ");
        int MinT = int.Parse(Console.ReadLine());

        Console.Write("Введіть верхню межу діапазону: ");
        int MaxT = int.Parse(Console.ReadLine());

        if (MinT > MaxT)
        {

            int temp = MinT;
            MinT = MaxT;
            MaxT = temp;
        }

        int length = MaxT - MinT + 1;
        string[] output = new string[length];
        Parallel.For(MinT, MaxT + 1, i =>
            output[i - MinT] = TableSub(i)
        );
        File.WriteAllLines("table.txt", output);



        //Task 4

        List<int> nums = File.ReadAllText("numbers.txt").Split(new char[] { '\r', '\n', ' ', '\t' }).Select(n=> int.Parse(n)).ToList();

        
        Parallel.ForEach(nums, Factorial1);


        //Task 5


        int Sumn = nums.AsParallel().Sum();
        int Maxn = nums.AsParallel().Max();
        int Minn = nums.AsParallel().Min();

        Console.WriteLine(Sumn);
        Console.WriteLine(Maxn);
        Console.WriteLine(Minn);




    }

    static void Factorial(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }


            int number = result;
            int counter = 0;
            int sum = 0;
            while (number != 0)
            {
                counter++;
                sum += number % 10;
                number /= 10;
            }



            //Thread.Sleep(3000);
            Console.WriteLine($"Result {result}");
            Console.WriteLine($"Length {counter}");
            Console.WriteLine($"Sum {sum}");


        }
    static void Factorial1(int x)
    {
        long result = 1;

        for (int i = 1; i <= x; i++)
        {
            result *= i;
        }
        Console.WriteLine($"{x} Result {result}");

    }

    static string TableSub(int min)
        {
            string output = "";
            for (int j = 1; j <= 10; j++)
            {
                output += $"{min} * {j} = {min * j}\n";
            }
            return output;




        }
    
}