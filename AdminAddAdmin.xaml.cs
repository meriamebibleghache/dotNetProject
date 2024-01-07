using System;
using System.Data.SqlClient;
using System.Windows;

namespace gestion_de_biblios
{
    public partial class AdminAddAdmin : Window
    {
        private const string ConnectionString = "Data Source=DESKTOP-JTVS5VM\\SQLEXPRESS1;Initial Catalog=LibraryManagement;Integrated Security=True;";

        public AdminAddAdmin()
        {
            InitializeComponent();
        }

        private void AddAdmin_Click(object sender, RoutedEventArgs e)
        {
            string username = UserNameTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordTextBox.Password;
            string job = JobTextBox.Text;

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(job))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        string insertAdminQuery = $"INSERT INTO Admin (UserName, Email, Password, Job) VALUES ('{username}', '{email}', '{password}', '{job}')";

                        using (SqlCommand insertAdminCommand = new SqlCommand(insertAdminQuery, connection))
                        {
                            insertAdminCommand.ExecuteNonQuery();
                            MessageBox.Show("Admin added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding admin: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelAdd_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
