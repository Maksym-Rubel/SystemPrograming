using _14_Library;
using _14_Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace _14_Dz_Librarry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DbLibrary context = new DbLibrary();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Search_Btn(object sender, RoutedEventArgs e)
        {
            await ReturnBooks();
        }

        private async void Update_Btn(object sender, RoutedEventArgs e)
        {
           await ReturnAuthors();
        }
        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = TBox.Text;
            await ReturnSearchBooks(text);
        }
        void Authors_an_Books()
        {
            context.Authors.AddRange(
                new Author { Name = "Тарас", Surname = "Шевченко" },
                new Author { Name = "Леся", Surname = "Українка" },
                new Author { Name = "Іван", Surname = "Франко" },
                new Author { Name = "Михайло", Surname = "Коцюбинський" },
                new Author { Name = "Ольга", Surname = "Кобилянська" },
                new Author { Name = "Василь", Surname = "Стефаник" },
                new Author { Name = "Пантелеймон", Surname = "Куліш" },
                new Author { Name = "Марко", Surname = "Вовчок" },
                new Author { Name = "Григорій", Surname = "Сковорода" },
                new Author { Name = "Юрій", Surname = "Андрухович" }
            );
            context.SaveChanges();
            context.Books.AddRange(
                new Book { Title = "Кобзар", AuthorId = 1 },
                new Book { Title = "Поезії", AuthorId = 2 },
                new Book { Title = "Захар Беркут", AuthorId = 3 },
                new Book { Title = "Тіні забутих предків", AuthorId = 4 },
                new Book { Title = "Царівна", AuthorId = 5 },
                new Book { Title = "Камінний хрест", AuthorId = 6 },
                new Book { Title = "Чорна рада", AuthorId = 7 },
                new Book { Title = "Інститутка", AuthorId = 8 },
                new Book { Title = "Байки Харківські", AuthorId = 9 },
                new Book { Title = "Московіада", AuthorId = 10 },
                new Book { Title = "Перехресні стежки", AuthorId = 3 },
                new Book { Title = "Лісова пісня", AuthorId = 2 },
                new Book { Title = "І мертвим, і живим...", AuthorId = 1 },
                new Book { Title = "Intermezzo", AuthorId = 4 },
                new Book { Title = "На полі крові", AuthorId = 2 },
                new Book { Title = "Блуд", AuthorId = 5 },
                new Book { Title = "Дорогою ціною", AuthorId = 4 },
                new Book { Title = "Професор Підопригора", AuthorId = 10 },
                new Book { Title = "Сад божественних пісень", AuthorId = 9 },
                new Book { Title = "Amore", AuthorId = 10 }
            );
            context.SaveChanges();
        }

        private Task ReturnBooks()
        {
            return Task.Run(() =>
            {
                string A_Name = "";
                try
                {
                    Dispatcher.Invoke(() =>
                    {
                        if (ComboBox1.SelectedItem != null)
                        {
                            A_Name = ComboBox1.SelectedItem.ToString();
                        }
                        else
                        {
                            A_Name = ""; 
                        }
                    });
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }


                if(A_Name != "")
                {
                    string[] A_NSurname = A_Name.Split(" ");

                    var books = context.Books.Include(a => a.Author).Where(a => a.Author.Name == A_NSurname[0] && a.Author.Surname == A_NSurname[1]).ToList();
                    Dispatcher.Invoke(() =>
                    {
                        ListBox1.ItemsSource = books;
                    });
                }
               
            });
        }

        private Task ReturnAuthors()
        {
            return Task.Run(() =>
            {
                var authors = context.Authors.ToList();
                Dispatcher.Invoke(() =>
                {
                    ComboBox1.Items.Clear();
                    foreach (var author in authors)
                    {
                        var fullName = $"{author.Name} {author.Surname}";
                        ComboBox1.Items.Add(fullName);

                    }
                });
            });
            
        }
        private async Task ReturnSearchBooks(string text)
        {
            await Task.Run(() =>
            {
                if(text.Length >= 3)
                {
                    var books = context.Books.Where(a => a.Title.Contains(text)).ToList();
                    Dispatcher.Invoke(() =>
                    {
                        ListBox1.ItemsSource = books;
                    });

                }


            });
        }
        
    }
}