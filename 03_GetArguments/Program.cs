internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Кількість аргументів: " + args.Length);
        foreach (var item in args)
        {
            Console.WriteLine(item);
            Console.WriteLine(item[0] + item[1]);
        }
        Console.ReadKey();






    }
}