using System;
using System.Threading.Tasks;

internal class Program
{
    private static void Main(string[] args)
    {
        Task task1 = new Task(() => Console.WriteLine($"#1 Date: {DateTime.Now.ToShortDateString()} Time: {DateTime.Now:HH:mm}"));
        task1.Start();

        
        Task task2 = Task.Factory.StartNew(() => Console.WriteLine($"#2 Date: {DateTime.Now.ToShortDateString()} Time: {DateTime.Now:HH:mm}"));

        Task task3 = Task.Run(() =>
        {
            Console.WriteLine($"#3 Date: {DateTime.Now.ToShortDateString()} Time: {DateTime.Now:HH:mm}");
        });

        Console.ReadLine();

       


        Task task = new Task(() => IsPrime(0,2000));
        task.Start();
        task.Wait();

        int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        Task<int> minTask = Task.Run(() => MinNum(arr));
        Task<int> maxTask = Task.Run(() => MaxNum(arr));
        Task<double> avgTask = Task.Run(() => AverageNum(arr));
        Task<int> sumTask = Task.Run(() => SumNum(arr));

        Task[] tasks = { minTask, maxTask, avgTask, sumTask };

        Task.WaitAll();

        Console.WriteLine($"Min: {minTask.Result}, Max: {maxTask.Result}, Avg: {avgTask.Result}, Sum: {sumTask.Result}");


        int[] arr2 = new int[] { 1, 2, 3, 4, 8, 6, 5, 7, 9, 10,9,10,1 };

  

    
        Task<int[]> DublicateTask = Task.Run(() => DublicateNum(arr2));
        int[] arr3 = DublicateTask.Result;


        foreach (int i in arr3)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();



        Task<int[]> SortedTask1 = DublicateTask.ContinueWith(x => SortedNum(arr3));
        int[] sortedArr3 = SortedTask1.Result;

        foreach (int i in arr3)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();


        Task<int> BinarySearchTask = SortedTask1.ContinueWith(task => BinarySearcher(task.Result, 5));
        //int num = BinarySearchTask.Result;
        //Console.WriteLine($"Результат пошуку: {num}");

        Console.WriteLine("End");
       

    }


    static void IsPrime(int min, int max)
    {
        
        for(int j = min; j <= max; j++)
        {
            bool IsPrime1 = true;
            if (j == 1 || j == 0)
            {
                continue;
            }
            for (int i = 2; i <= j / 2; i++)
            {
               
                if (j % i == 0)
                {
                    IsPrime1 = false;
                    break;
                }
            }
            if (IsPrime1)
            {
                Console.WriteLine("Prime number --> " + j);
            }
        }
        

    }
    static int MinNum(int[] nums)
    {
        return nums.Min();
    }
    static int MaxNum(int[] nums)
    {
        return nums.Max();
    }
    static double AverageNum(int[] nums)
    {
        return nums.Average();
    }
    static int SumNum(int[] nums)
    {
        return nums.Sum();
    }


    static int[] DublicateNum(int[] nums)
    {
        int length = 0;
        int[] temp = new int[nums.Length];
        foreach (int i in nums)
        {
            bool isTrue = true;
            for (int k = 0; k < length; k++)
            {
                if (temp[k] == i)
                {
                    isTrue = false;
                    break;
                }
            }
            if (isTrue)
            {
                temp[length] = i;
                length++;
            }
        }
        
        return temp.Take(length).ToArray();
    }
    static int[] SortedNum(int[] nums)
    {
        Array.Sort(nums);
        return nums;
    }
    static int BinarySearcher(int[] nums, int index)
    {
        int index1 = BinarySearcher(nums, index);
        return index1;
    }
}