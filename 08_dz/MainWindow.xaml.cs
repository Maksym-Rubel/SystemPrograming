using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _08_dz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int number = 2;
        int number2 = 2;
        int num1 = 1;
        int num2 = 2;

        Thread thread = null;
        Thread thread1 = null;


        bool isStopped = false;
        bool isStopped1 = false;

        public MainWindow()
        {
            InitializeComponent();
            this.Closing += Main_Close;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (tb1.Text == "" && tb2.Text == "")
            {
                thread = new Thread(PrintNumber);
                thread.Start();
                isStopped = false;

            }
            else if (tb1.Text != "" && tb2.Text == "")
            {
                
                number = int.Parse(tb1.Text);
                thread = new Thread(PrintNumber);
                thread.Start();
                isStopped = false;
                
            }
            else if (tb1.Text == "" && tb2.Text != "")
            {


                thread = new Thread(PrintNumberCount);
                thread.Start();
                isStopped = false;
               


            }
            else if (tb1.Text != "" && tb2.Text != "")
            {

                number = int.Parse(tb1.Text);
                thread = new Thread(PrintNumberCount);
                thread.Start();
                isStopped = false;
                

            }
            Thread thread1 = new Thread(IsFibonacci);
            thread1.Start();
            isStopped1 = false;
            

        }

        private bool IsPrime(int num)
        {
           
            for (int i = 2; i <= num/2; i++)
            {
                if(num % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        private void IsFibonacci()
        {

            
                while (true) 
                { 
                    if (!isStopped1)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            int temp = number2;
                            number2 = num1 + num2; // 1 + 2 = 3 // 2 + 3 = 5 // 3 + 5 // 8 + 4
                            lb2.Items.Add($"Element: {number2}");
                            
                            num1 = temp;
                            num2 = number2;

                        });
                        Thread.Sleep(100);
                        
                    }
                    
                }
                
            


        }
        private void PrintNumber()
        {
            
            while (true) 
            {
                if(!isStopped)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (IsPrime(number))
                        {
                            lb1.Items.Add($"Element: {number}");
                        }
                        ++number;
                    });
                    Thread.Sleep(100);
                }
            }
        }
        private void PrintNumberCount()
        {
            int count = 0;
            Application.Current.Dispatcher.Invoke(() =>
            {
                count = int.Parse(tb2.Text.ToString());
            });
            for (int i = number; i <= count; i++)
            {
                if (!isStopped)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (IsPrime(number))
                        {
                            lb1.Items.Add($"Element: {number}");
                        }
                        ++number;

                    });
                    
                    Thread.Sleep(100);
                }
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {
                isStopped = true;
                thread.Abort();
            }
            catch(Exception ex) 
            {
                
            }
       
        }

     
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                isStopped1 = true;
                thread1.Abort();
            }
            catch (Exception ex)
            {
                
            }
        }

        private void Main_Close(object sender, CancelEventArgs e)
        {
            isStopped = true;
            isStopped1 = true;
            thread.Abort();
            thread1.Abort();
        }
        
    }
}