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
using System.Windows.Threading;

namespace _02_TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer _timer = null;
        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 20);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }
        private void _timer_Tick(object? sender, EventArgs e)
        {
            Refresh(sender, null);
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            grid.ItemsSource = Process.GetProcesses();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process pr = grid.SelectedItem as Process;
            if(pr != null)
            {
                pr.Kill();
            }
           

        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            string rbutton = (sender as RadioButton).Content.ToString();
               
            if(rbutton != null)
            {
                _timer.Interval = new TimeSpan(0, 0,int.Parse(rbutton));
            }
            
        }

        private void ShowDetails(object sender, RoutedEventArgs e)
        {
            try 
            {
                Process pr = grid.SelectedItem as Process;
                if (pr != null)
                {
                    MessageBox.Show($"BasePriority --> {pr.BasePriority.ToString()}\nStartTime --> {pr.StartTime.ToString()} ", "Details");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("You dont can see because you not admin","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }

           
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter && TextBoox.Text != null)
                {
                    Process pr = Process.Start(@$"{TextBoox.Text}.exe");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Process not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                TextBoox.Text = "";
            }
            
        }
    }
}