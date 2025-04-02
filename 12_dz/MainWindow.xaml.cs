using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace _12_dz
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
        //"C:\Users\Maksym\source\repos\01_processes\12_dz\bin\Debug\net8.0-windows\ForCopy.txt"
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string sourceFile = tb1.Text;
            string destinationFolder = tb2.Text;
            int Count = int.Parse(tb3.Text);



            try
            {
                await Task.Run(() => CopyToA(sourceFile,destinationFolder,Count));
                

            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        void CopyToA(string sourceFile, string destinationFolder, int Count)
        {
            bool flag = false;
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (Pg1.Value > 0)
                    Pg1.Value = Pg1.Minimum;
                flag = Pg1.Value < Pg1.Maximum;
            });
            
            for (int i = 1; i <= Count; i++)
            {

                string destFileName = Path.Combine(destinationFolder, $"{Path.GetFileNameWithoutExtension(sourceFile)}_{i}{Path.GetExtension(sourceFile)}");
                try
                {
                    File.Copy(sourceFile, destFileName, false);
                }
                catch (Exception ex) 
                {
                    string destFileName1 = Path.Combine(destinationFolder, $"{Path.GetFileNameWithoutExtension(destFileName)}_{i}{Path.GetExtension(sourceFile)}");

                    File.Copy(sourceFile, destFileName1, false);
                }
                
            for (int j = 0; j < 100/Count + 1; j++)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Pg1.Value++;
                        flag = Pg1.Value < Pg1.Maximum;
                    });
                    Thread.Sleep(50);
                }
                
            }
            Thread.Sleep(3000);

        }
      


    }
}