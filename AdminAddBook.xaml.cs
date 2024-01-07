using System;
using System.Data.SqlClient;
using System.Windows;
using gestion_de_biblios;
using gestion_de_biblios.models;
namespace gestion_de_biblios
{
    public partial class AdminAddBook : Window
    {
        private const string ConnectionString = "Data Source=DESKTOP-JTVS5VM\\SQLEXPRESS1;Initial Catalog=LibraryManagement;Integrated Security=True;";

        public AdminAddBook()
        {
            InitializeComponent();
        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text;
            string author = AuthorTextBox.Text;

            if (int.TryParse(PagesTextBox.Text, out int pages) && DateTime.TryParse(PublicationDate.Text, out DateTime publicationDate))
            {
                AddBookToDatabase(title, author, pages, publicationDate,true);
                MessageBox.Show("Book added successfully!");

                AdminBooks adminBooks = new AdminBooks();
                adminBooks.Show();

                Close();
            }
            else
            {
                MessageBox.Show("Please enter valid values for the fields");
            }
        }

        private void AddBookToDatabase(string title, string author, int pages, DateTime publicationDate, bool IsAvailable)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand($"INSERT INTO Book (Title, Author, NbrPage, PublicationDate, IsAvailable) VALUES ('{title}', '{author}',{pages}, '{publicationDate.ToString("yyyy-MM-dd")}',1)", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
