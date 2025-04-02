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

namespace _12_classAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //Task<int> task = Task.Run(GenerateValue);
            //await task;
            //int value = task.Result;
            //list.Items.Add(value);

            list.Items.Add(await GenerateValueAsync());

        }

        int GenerateValue()
        {
            Thread.Sleep(rnd.Next(5000));
            return rnd.Next(1000);
        }


        Task<int> GenerateValueAsync()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(rnd.Next(5000));
                return rnd.Next(1000);
            });
            
        }
    }
}