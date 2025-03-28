using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    class LetterCounter()
    {
        int wordCount;
        int riadCount;
        int rozdiloviCount;
        public int WordCount { get { return wordCount; } }
        public int RiadCount { get { return riadCount; } }
        public int RozdiloviCount { get { return rozdiloviCount; }  }

        public void UpdateFields(object a)
        {
            string path = a.ToString();
            using (StreamReader sw = new StreamReader(path))
            {
                lock (this)
                {
                    riadCount = sw.ReadToEnd().Split('\n').Count();
                    string alltext = File.ReadAllText(path);
                    char[] rozdilovi = { '.', ',', ';', ':', '-', '—', '…', '!', '?', '"', '«', '»',
                     '(', ')', '{', '}', '[', ']', '<', '>', '/'};
                    rozdiloviCount = alltext.Count(c => rozdilovi.Contains(c));
                    wordCount = alltext.Split(new char[] { ' ', '\t', '\n' }).Count();
                }
                

            }

        }
    }


    private static void Main(string[] args)
    {
        LetterCounter c = new LetterCounter();
        string path = "new.txt";
        Thread threads = new Thread(c.UpdateFields);
        threads.Start(path);
        threads.Join();
        Console.WriteLine(c.RiadCount);
        Console.WriteLine(c.RozdiloviCount);
        Console.WriteLine(c.WordCount);



    
    }
}