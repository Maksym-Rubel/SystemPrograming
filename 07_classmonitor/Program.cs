using System.Text;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    static int totalWordCount = 0;
    static int totalRiadCount = 0;
    static int totalRozdiloviCount = 0;

        class LetterCounter()
        {
            public void UpdateFields(object pathObj)
            {
                string path = pathObj.ToString();
                int wordCount = 0;
                int riadCount = 0;
                int rozdiloviCount = 0;

                using (StreamReader sw = new StreamReader(path))
                {
                    lock (typeof(LetterCounter))
                    {
                        riadCount = sw.ReadToEnd().Split('\n').Count();
                        string alltext = File.ReadAllText(path);
                        char[] rozdilovi = { '.', ',', ';', ':', '-', '—', '…', '!', '?', '"', '«', '»',
                     '(', ')', '{', '}', '[', ']', '<', '>', '/'};
                        rozdiloviCount = alltext.Count(c => rozdilovi.Contains(c));
                        wordCount = alltext.Split(new char[] { ' ', '\t', '\n' }).Count();

                        

                    }

                }

                
                lock (typeof(Program)) 
                {
                    totalRiadCount += riadCount;
                    totalRozdiloviCount += rozdiloviCount;
                    totalWordCount += wordCount;
                }
            }
        }

        private static void Main(string[] args)
        {


            string path = @"C:\\Users\\Maksym\\source\\repos\\01_processes\\07_classmonitor\\bin\\Debug\\net8.0";
            LetterCounter с = new LetterCounter();
            string[] files = Directory.GetFiles(path, "*.txt");
            
            Thread[] threads = new Thread[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                threads[i] = new Thread(с.UpdateFields);
                threads[i].Start(files[i]);
            }


            foreach (var thread in threads)
            {
                thread.Join();
            }
            Console.OutputEncoding = UTF8Encoding.UTF8;
            Console.InputEncoding = UTF8Encoding.UTF8;
            Console.WriteLine($"Загальна кількість файлів: {files.Count()}");
            Console.WriteLine($"Загальна кількість рядків: {totalRiadCount}");
            Console.WriteLine($"Загальна кількість розділових знаків: {totalRozdiloviCount}");
            Console.WriteLine($"Загальна кількість слів: {totalWordCount}");

        }
    }
