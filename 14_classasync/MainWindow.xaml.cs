using IOExtensions;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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

namespace _14_classasync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel model;
        int id = 0;
        public MainWindow()
        {

            InitializeComponent();
            model = new ViewModel()
            {
                Source = @"D:\Test\1GB.bin",
                Destination = @"C:\Users\Maksym\Desktop\TestCopy",
                Progress = 0,
            };
            srcTextBox.Text = model.Source;
            destTextBox.Text = model.Destination;
            this.DataContext = model;

        }

        private void OpenFileBtn(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                srcTextBox.Text = model.Source = dialog.FileName;
            }
        }

        private void OpenFolderBtn(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                destTextBox.Text = model.Destination = dialog.FileName;
            }
        }

        private async void CopyFile(object sender, RoutedEventArgs e)
        {
            string fileName = System.IO.Path.GetFileName(srcTextBox.Text);
            string destfileName = System.IO.Path.Combine(model.Destination,$"{fileName}({id++})");
            //File.Copy(Source, Targert1);
            CopyProcessInfo info = new CopyProcessInfo(fileName);
            model.AddProcess(info);
          
            await CopyFileAsync(model.Source, destfileName,info);
            info.Percentage = 100;
            MessageBox.Show("Complate");
        }
        private Task CopyFileAsync(string source, string dest, CopyProcessInfo info)
        {
            return FileTransferManager.CopyWithProgressAsync(source, dest,(progress) =>
            {
                model.Progress = progress.Percentage;
                info.Percentage = progress.Percentage;
                info.BytesPerSeconds = progress.BytesPerSecond;

            },false);
            //return Task.Run(() =>
            //{
            //    using (FileStream scrFile = new FileStream(source, FileMode.Open, FileAccess.Read))
            //    {
            //        using (FileStream destFile = new FileStream(dest, FileMode.Create, FileAccess.Write))
            //        {
            //            byte[] buffer = new byte[1024 * 8];
            //            int bytes = 0;
            //            do
            //            {
            //                bytes = scrFile.Read(buffer, 0, buffer.Length);
            //                destFile.Write(buffer, 0, bytes);
            //                float percentage = destFile.Length / (scrFile.Length / 100);
            //                model.Progress = percentage;


            //            } while (bytes > 0);
            //        }
            //    }
            //});


        }





    }
    [AddINotifyPropertyChangedInterface]
    public class ViewModel
    {
        private ObservableCollection<CopyProcessInfo> processes;
        public string Source { get; set; }
        public string Destination { get; set; }
        public double Progress { get; set; }
        public bool IsWaiting => Progress == 0;

        public ViewModel()
        {
            processes = new ObservableCollection<CopyProcessInfo>();
        }
        public IEnumerable<CopyProcessInfo> Processes => processes;


        public void AddProcess(CopyProcessInfo info)
        {
            processes.Add(info);
        }

    }
    [AddINotifyPropertyChangedInterface]
    public class CopyProcessInfo
    {
        
        public string FileName { get; set; }
        public double Percentage { get; set; }

        public int PercentageInt => (int)Percentage;
        public double BytesPerSeconds;
        public double MegaBytesPerSeconds => Math.Round(BytesPerSeconds / 1024 / 1024,1);


        public CopyProcessInfo(string fileName)
        {
            FileName = fileName;
        }
    }
}