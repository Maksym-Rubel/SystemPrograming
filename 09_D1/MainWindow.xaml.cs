using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _09_D1
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
       
        int number = 1;
        int num1 = 0;
        int num2 = 1;
        int number_2 = 1;
        int num_3 = 0;
        int num_4 = 1;
        int count = 0;

        private void Oppderation1()
        {


            for(int i = 0; i < 10; i++) 
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    int temp = number;
                    lb1.Items.Add($"Element: {number}");
                    number = num1 + num2;
                    
                    num1 = temp;
                    num2 = number;
                });
                Thread.Sleep(100);
            }
        }
        private void Oppderation2()
        {


            for (int i = 0; i < 10; i++)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    int temp = number_2;

                    number_2 = num_3 + num_4;
                    lb2.Items.Add($"Element: {number_2}");
                    num_3 = temp;
                    num_4 = number_2;
                });
                Thread.Sleep(100);
            }
        }
        private void Oppderation3()
        {

            
            for (int i = 0; i < 10; i++)
            {
                int temp = 0;
                
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if(number % 2 == 0 && number != temp)
                    {
                        temp = number;
                        count++;
                    }
                    if(number_2 % 2 == 0 && number_2 != temp && number != number_2)
                    {
                        temp = number;
                        count++;
                    }
                    lb3.Items.Add("Even number: " + count);
                });
                Thread.Sleep(100);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread[] threads =
            {
                new Thread(Oppderation1),
                new Thread(Oppderation2),
                new Thread(Oppderation3),

            };

            foreach (var t in threads)
                t.Start();
        }
    }
}