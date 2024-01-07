using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using gestion_de_biblios.models;

namespace gestion_de_biblios
{
    public partial class AdminBooks : Window
    {
        private const string ConnectionString = "Data Source=DESKTOP-JTVS5VM\\SQLEXPRESS1;Initial Catalog=LibraryManagement;Integrated Security=True;";

        public AdminBooks()
        {
            InitializeComponent();
            RefreshBooksData();
        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            var addBookWindow = new AdminAddBook();
            addBookWindow.ShowDialog();
            RefreshBooksData();
        }

        private void RemoveBook_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = BooksDataGrid.SelectedItem as Book;
            if (selectedBook != null)
            {
                RemoveBookFromDatabase(selectedBook.BookId);
                RefreshBooksData();
            }
        }

        private void RemoveBookFromDatabase(int bookId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand($"DELETE FROM Book WHERE BookId = {bookId}", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing book: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshBooksData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT * FROM Book", connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        var books = new List<Book>();

                        while (reader.Read())
                        {
                            int BookId = reader.GetInt32(reader.GetOrdinal("BookId"));
                            string Title = reader.GetString(reader.GetOrdinal("Title"));
                            string Author = reader.GetString(reader.GetOrdinal("Author"));
                            int NbrPage =  reader.GetInt32(reader.GetOrdinal("NbrPage"));
                            DateTime PublicationDate = reader.GetDateTime(reader.GetOrdinal("PublicationDate"));
                            bool IsAvailable = reader.GetBoolean(reader.GetOrdinal("IsAvailable"));

                            var book = new Book(BookId, Title, Author, NbrPage, PublicationDate, IsAvailable);
                            books.Add(book);
                        }

                        BooksDataGrid.ItemsSource = books;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing books data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ModifyBook_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = BooksDataGrid.SelectedItem as Book;

            if (selectedBook != null)
            {
                var modifyBookWindow = new AdminUpdateBook(selectedBook.BookId);
                modifyBookWindow.ShowDialog();
                RefreshBooksData();
            }
            else
            {
                MessageBox.Show("Please select a book to modify");
            }
        }

        private void ReturnHome_Click(object sender, RoutedEventArgs e)
        {
            var adminhome = new AdminHome();
            adminhome.Show();
            Close();
        }
    }
}
