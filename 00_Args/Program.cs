internal class Program
{
    private static void Main(string[] args)
    {
        foreach (var item in args)
        {
            Console.WriteLine(item);
            
        }
        int num1 = int.Parse(args[0]);
        int num2 = int.Parse(args[1]);
        char diya = char.Parse(args[2]);

        if(diya == '+')
        {
            Console.WriteLine(num1 + num2); 
        }
        else if(diya == '-')
        {
            Console.WriteLine(num1 - num2);

        }
        else if (diya == '*')
        {
            Console.WriteLine(num1 * num2);

        }
        else if (diya == '/')
        {
            Console.WriteLine(num1 / num2);

        }
    }
}