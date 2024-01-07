using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using gestion_de_biblios.models;

namespace gestion_de_biblios
{
    public partial class AdminMembers : Window
    {
        private const string ConnectionString = "Data Source=DESKTOP-JTVS5VM\\SQLEXPRESS1;Initial Catalog=LibraryManagement;Integrated Security=True;";

        public AdminMembers()
        {
            InitializeComponent();
            RefreshAdminsData();
        }

        private void AddAdmin_Click(object sender, RoutedEventArgs e)
        {
            var addAdminWindow = new AdminAddAdmin();
            addAdminWindow.ShowDialog();
            RefreshAdminsData();
        }

        private void RemoveAdmin_Click(object sender, RoutedEventArgs e)
        {
            var selectedAdmin = AdminDataGrid.SelectedItem as Admin;
            if (selectedAdmin != null)
            {
                RemoveAdminFromDatabase(selectedAdmin.Id);
                RefreshAdminsData();
            }
        }

        private void RemoveAdminFromDatabase(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand($"DELETE FROM Admin WHERE Id = {Id}", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing admin: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshAdminsData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT * FROM Admin", connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        var admins = new List<Admin>();

                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("Id"));
                            string username = reader.GetString(reader.GetOrdinal("UserName"));
                            string email = reader.GetString(reader.GetOrdinal("Email"));
                            string password = reader.GetString(reader.GetOrdinal("Email"));
                            string job = reader.GetString(reader.GetOrdinal("Job"));

                            var admin = new Admin(id, username, email, password, job);
                            admins.Add(admin);
                        }

                        AdminDataGrid.ItemsSource = admins;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing admins data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ModifyAdmin_Click(object sender, RoutedEventArgs e)
        {
            var selectedAdmin = AdminDataGrid.SelectedItem as Admin;

            if (selectedAdmin != null)
            {
                var modifyAdminWindow = new AdminUpdateAdmin(selectedAdmin.Id);
                modifyAdminWindow.ShowDialog();
                RefreshAdminsData();
            }
            else
            {
                MessageBox.Show("Please select an Admin to modify");
            }
        }
      
        private void ReturnHome_Click(object sender, RoutedEventArgs e)
        {
            var adminHome = new AdminHome();
            adminHome.Show();
            Close();
        }
    }
}
