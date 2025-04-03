using System.Diagnostics;
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

namespace _13_dz
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            int num = int.Parse(tb1.Text);
            try
            {
                list1.Items.Add(await Task.Run(() => Factorial(num)));


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }


        int Factorial(int num)
        {
            int result = 1;
            for (int i = 1; i <= num; i++)
            {
                result *= i; 
                
                
            }
            Thread.Sleep(2000);
            return result;
        }
    }
}