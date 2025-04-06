using _14_Library;
using _14_Library.Entities;

internal class Program
{
    private static void Main(string[] args)
    {
        
        DbLibrary dbLibrary = new DbLibrary();
        dbLibrary.Authors.Add(new Author { Name = "test", Surname = "test" });
        dbLibrary.SaveChanges();



    }
}