using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace _11_home_work
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        static int all_symbols = 0;
        Task<int> task1;
        Task<int> task2;
        Task<int> task3;
        Task<int> task5;
        Task<int> task4;

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if(tb1.Text != null)
            {
                string text = tb1.Text; 

                task1 = Task.Run(() => AllSymbols(text));
                task2 = Task.Run(() => AllMathes(text));
                task3 = Task.Run(() => AllWords(text));
                task4 = Task.Run(() => Allquiez(text));
                task5 = Task.Run(() => AllHappy(text));


            }
        }
        //Hello World! My name is Maks. How old are you?
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (task1 == null)
            {
                MessageBox.Show("Завдання ще не запущено!");
                return;
            }

            if (!task1.IsCompleted)
            {
                MessageBox.Show("Завдання ще виконується...");
                return;
            }

            try
            {
                int Symbols = task1.Result;
                int Rechen = task2.Result;
                int Words_Count = task3.Result;
                int Quiz_Count = task4.Result;
                int Happy_Count = task5.Result;

                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"Кількість всіх символів: {Symbols}\nКількість речень у тексті: {Rechen}" +
                        $"\nКількість слів у тексті: {Words_Count}\nКількість питальних речень у тексті: {Quiz_Count}" +
                        $"\nКількість окличних речень у тексті: {Happy_Count}");

                });
            }
            catch (AggregateException ex)
            {
                MessageBox.Show($"Помилка: {ex.InnerException.Message}");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (task1 == null)
            {
                MessageBox.Show("Завдання ще не запущено!");
                return;
            }
            if (!task1.IsCompleted)
            {
                MessageBox.Show("Завдання ще виконується...");
                return;
            }
            try
            {
                string path = "output.txt";
                using (StreamWriter sw = new StreamWriter(path))
                {
                    int Symbols = task1.Result;
                    int Rechen = task2.Result;
                    int Words_Count = task3.Result;
                    int Quiz_Count = task4.Result;
                    int Happy_Count = task5.Result;
                    sw.Write(tb1.Text + "\n");
                    sw.Write($"Кількість всіх символів: {Symbols}\nКількість речень у тексті: {Rechen}" +
                            $"\nКількість слів у тексті: {Words_Count}\nКількість питальних речень у тексті: {Quiz_Count}" +
                            $"\nКількість окличних речень у тексті: {Happy_Count}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        private int AllSymbols(string text)
        {
            return text.Length;
        }
        private int AllMathes(string text)
        {
            string pattern = @"[.!?]";
            int matches = Regex.Matches(text, pattern).Count();
            return matches;
        }
        private int AllWords(string text)
        {
           
            int wordCount = text.Split(new char[] { ' ', '\t', '\n' }).Count();
            return wordCount;
        }
        private int Allquiez(string text)
        {

            string pattern = @"[?]";
            int matches = Regex.Matches(text, pattern).Count();
            return matches;
            
        }
        private int AllHappy(string text)
        {

            string pattern = @"[!]";
            int matches = Regex.Matches(text, pattern).Count();
            return matches;

        }



    }
}