using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace gestion_de_biblios
{
    public partial class AdminUpdateAdmin : Window
    {
        private const string ConnectionString = "Data Source=DESKTOP-JTVS5VM\\SQLEXPRESS1;Initial Catalog=LibraryManagement;Integrated Security=True;";
        private readonly int adminId;

        public AdminUpdateAdmin(int adminId)
        {
            InitializeComponent();
            this.adminId = adminId;
            LoadAdminData();
        }

        private void LoadAdminData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand($"SELECT * FROM Admin WHERE Id = {adminId}", connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            string username = reader.GetString(reader.GetOrdinal("UserName"));
                            string email = reader.GetString(reader.GetOrdinal("Email"));
                            string password = reader.GetString(reader.GetOrdinal("Password"));
                            string job = reader.GetString(reader.GetOrdinal("Job"));

                            UserNameTextBox.Text = username;
                            EmailTextBox.Text = email;
                            PasswordTextBox.Password = password;
                            JobTextBox.Text = job;
                        }
                        else
                        {
                            MessageBox.Show("Admin not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading admin data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void UpdateAdmin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Update the Admin record in the database
                    using (SqlCommand command = new SqlCommand($"UPDATE Admin SET UserName = @UserName, Email = @Email, Password = @Password, Job = @Job WHERE Id = {adminId}", connection))
                    {
                        command.Parameters.AddWithValue("@UserName", UserNameTextBox.Text);
                        command.Parameters.AddWithValue("@Email", EmailTextBox.Text);
                        command.Parameters.AddWithValue("@Password", PasswordTextBox.Password);
                        command.Parameters.AddWithValue("@Job", JobTextBox.Text);

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Admin updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating admin: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelUpdate_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
