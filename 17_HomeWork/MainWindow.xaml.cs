using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Diagnostics.Metrics;
using System.IO;
using System.Text;
using System.Text.Json;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _17_HomeWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ResumeJson> Resumes = new List<ResumeJson>();

        public MainWindow()
        {
            InitializeComponent();
            

        }

        private async Task OneFile(string path)
        {
            string json = File.ReadAllText(path);
            var obj = JsonSerializer.Deserialize<ResumeJson>(json);
            

            Resumes.Add(obj);
            lb1.Items.Clear();
            lb1.Items.Add(obj);
        }

        private void Browse_Btn(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                srcTextBox.Text = dialog.FileName;
            }
        }

        private async void Start_Btn(object sender, RoutedEventArgs e)
        {
            

            if (!string.IsNullOrWhiteSpace(srcTextBox.Text))
            {
                if (File.Exists(srcTextBox.Text))
                {
                    await OneFile(srcTextBox.Text);
                }
                else if (Directory.Exists(srcTextBox.Text))
                {
                    await LoadResumesAsync(srcTextBox.Text);
                }
                
                
            }
        }

        private void Browse_FLR_Btn(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                srcTextBox.Text = dialog.FileName;
            }


            
        }

        private async void MExpir_Btn(object sender, RoutedEventArgs e)
        {
            lb1.Items.Clear();
            var orderedRes = await Task.Run(() => Resumes.OrderByDescending(n => n.ExpriencYear).First());

            
            lb1.Items.Add(orderedRes);
        }







        private async Task LoadResumesAsync(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath, "*.json");

            Resumes.Clear();

            foreach (var file in files)
            {
                string json = await File.ReadAllTextAsync(file);
                var obj = JsonSerializer.Deserialize<ResumeJson>(json);
                if (obj != null)
                    Resumes.Add(obj);
            }
            lb1.Items.Clear();
            foreach(var file in Resumes)
            {
                lb1.Items.Add(file);
            }
            
        }

        private async void  LExp_Btn(object sender, RoutedEventArgs e)
        {
            try
            {
                var orderedRes = await Task.Run(() => Resumes.OrderBy(n => n.ExpriencYear).First());


                lb1.Items.Clear();

                lb1.Items.Add(orderedRes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private async void OrdCountrt_Btn(object sender, RoutedEventArgs e)
        {
            var Countries = await Task.Run(()=>Resumes.OrderBy(n => n.Country).ToList());

            lb1.Items.Clear();
            foreach( var country in Countries)
            {
               lb1.Items.Add(country);
            } 
        }

        private async void LSal_Btn(object sender, RoutedEventArgs e)
        {
            var lsal = await Task.Run(() => Resumes.OrderBy(n => n.Salary).First());

            lb1.Items.Clear();
            lb1.Items.Add(lsal);
        }

        private async void MSal_Btn(object sender, RoutedEventArgs e)
        {
            var lsal = await Task.Run(() => Resumes.OrderByDescending(n => n.Salary).First());

            lb1.Items.Clear();
            lb1.Items.Add(lsal);
        }
    }
}