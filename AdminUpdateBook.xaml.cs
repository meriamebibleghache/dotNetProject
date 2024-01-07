using Microsoft.OData.Edm;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace gestion_de_biblios
{
    public partial class AdminUpdateBook : Window
    {
        private const string ConnectionString = "Data Source=DESKTOP-JTVS5VM\\SQLEXPRESS1;Initial Catalog=LibraryManagement;Integrated Security=True;";

        private int BookIDToUpdate;

        public AdminUpdateBook(int bookId)
        {
            InitializeComponent();
            BookIDToUpdate = bookId;

            LoadBookInformation();
        }

        private void UpdateBook_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text;
            string author = AuthorTextBox.Text;

            if (int.TryParse(PagesTextBox.Text, out int pages) && DateTime.TryParse(PublicationDate.Text, out DateTime publicationdate))
            {
                UpdateBookInDatabase(title, author, pages, publicationdate);
                MessageBox.Show("Book Update successfully");
                Close();
            }
            else
            {
                MessageBox.Show("Please enter valid values for the fields");
            }
        }

        private void LoadBookInformation()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand($"SELECT * FROM Book WHERE BookId = {BookIDToUpdate}", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        TitleTextBox.Text = reader.GetString(1);
                        AuthorTextBox.Text = reader.GetString(2);
                        PagesTextBox.Text = reader.GetInt32(3).ToString();
                        PublicationDate.SelectedDate = reader.GetDateTime(4);
                    }
                }
            }
        }

        private void UpdateBookInDatabase(string title, string author, int pages, DateTime publicationdate)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand($"UPDATE Book SET Title = '{title}', Author = '{author}', NbrPage = {pages}, PublicationDate = '{publicationdate}' WHERE BookId = {BookIDToUpdate}", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
