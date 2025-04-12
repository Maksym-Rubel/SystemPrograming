using IOExtensions;
using Microsoft.WindowsAPICodePack.Dialogs;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.IO;
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
using static System.Net.WebRequestMethods;

namespace _18_Exam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        InformationFolder folder;
        List<InformationFolder> folders = new List<InformationFolder>();
        ViewModel model;
        public MainWindow()
        {
            InitializeComponent();
            model = new ViewModel()
            {
                Progress = 0,
            };
            this.DataContext = model;
        }

        private async void Start_Btn(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(srcTextBox.Text) && !string.IsNullOrWhiteSpace(WordTectBox.Text))
            {
                folders.Clear();
                lb1.Items.Clear();
                model.Progress = 0;
                
                try
                {
                    string[] files = Directory.GetFiles(srcTextBox.Text, "*", SearchOption.AllDirectories);
                    int totalFiles = files.Length;
                    for(int i = 0; i < totalFiles; i++) 
                    {
                        var item = files[i];

                        model.Progress = ((double)(i + 1) / totalFiles) * 100;
                        
                        folders.Add(new InformationFolder
                        {
                            fileName = System.IO.Path.GetFileName(item),
                            filePath = item,
                            countWord = await GetFiless(System.IO.Path.GetFullPath(item), WordTectBox.Text)
                        });


                    }
                    
                    foreach (var item in folders)
                    {
                        lb1.Items.Add(item);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("TextBlock is null");
            }

        }

        private void Browse_Btn(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                srcTextBox.Text = dialog.FileName;
            }
        }

        public async Task<int> GetFiless(string path, string word)
        {
            
            int counter = 0;
            await Task.Run(() =>
            {
                string[] alltext = System.IO.File.ReadAllText(path).Split(new[] {' ','\n','\t','\r'});
                foreach(var item in alltext)
                {
                    if (string.Equals(item, word))
                    {
                        counter++;
                    }
                    Thread.Sleep(100);
                }
               




            });
            return counter;
        }

        private void Save_Btn(object sender, RoutedEventArgs e)
        {
            if(lb1.Items.Count >= 1)
            {
                using (StreamWriter sw = new StreamWriter("save.txt"))
                {
                    foreach (var item in folders)
                    {
                        sw.WriteLine($"Назва файлу: {item.fileName} Шлях до файлу: {item.filePath} Кількість слів: {item.countWord}");
                    }
                }
            }
            
            
        }
    }
    public class InformationFolder()
    {
        public string fileName { get; set; }
        public string filePath { get; set; }
        public int countWord { get; set; }
    }
    [AddINotifyPropertyChangedInterface]
    public class ViewModel
    {
       
        public double Progress { get; set; }
        public bool IsWaiting => Progress == 0;

        


       
    }
    
}