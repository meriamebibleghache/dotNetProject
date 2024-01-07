using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace gestion_de_biblios
{
    public partial class AdminRegister : Window
    {
        private const string ConnectionString = "Data Source=DESKTOP-JTVS5VM\\SQLEXPRESS1;Initial Catalog=LibraryManagement;Integrated Security=True;";

        public AdminRegister()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            string job = JobTextBox.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(job))
            {
                MessageBox.Show("Please fill in all fields");
                return;
            }

            if (IsAdminExists(username))
            {
                MessageBox.Show("This user already exists");
                return;
            }

            if (AddAdminToDatabase(username, email, password,job))
            {
                MessageBox.Show("Registration successful!");
                Close();
            }
            else
            {
                MessageBox.Show("Registration failed Please try again");
            }
        }

        private bool IsAdminExists(string username)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM Admin WHERE Username = '{username}'", connection))
                {
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private bool AddAdminToDatabase(string username, string email, string password, string job)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand($"INSERT INTO Admin (Username, Email, Password ,Job) VALUES ('{username}', '{email}', '{password}', '{job}')", connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        private void AdminRegister_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
