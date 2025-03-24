using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        //Process pr = Process.Start(@"mspaint.exe");
        //pr.WaitForExit();
        //Console.WriteLine("ExitCode: " + pr.ExitCode);



        #region Start Process
        Console.WriteLine("1 - Paint, 2 - notepad, 3 - google ");

        Console.Write("Enter choice --> ");
        Process pr1 = new Process();
        int choice = int.Parse(Console.ReadLine());
        switch(choice)
        {
            case 1:
                pr1  = Process.Start(@"mspaint.exe");
                break;
            case 2:
                pr1 =  Process.Start(@"notepad.exe");
                break;
            case 3:
                pr1 = Process.Start(@"C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe");
                break;
                
        }
        pr1.WaitForExit();
        Console.WriteLine("ExitCode: " + pr1.ExitCode);
        #endregion

        #region GetArguments
        Process.Start(@"C:\Users\Maksym\source\repos\01_processes\00_Args\bin\Debug\net8.0\00_Args.exe", "7 3 +");
        Console.WriteLine("Press key to do operation...");
        Console.ReadKey();

        #endregion

    }
}