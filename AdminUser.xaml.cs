using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using gestion_de_biblios.models;

namespace gestion_de_biblios
{
    public partial class AdminUser : Window
    {
        private const string ConnectionString = "Data Source=DESKTOP-JTVS5VM\\SQLEXPRESS1;Initial Catalog=LibraryManagement;Integrated Security=True;";

        public AdminUser()
        {
            InitializeComponent();
            RefreshUserData();
        }

        private void RemoveUser_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = UsersDataGrid.SelectedItem as Users;
            if (selectedUser != null)
            {
                RemoveUserFromDatabase(selectedUser.member_id);
                RefreshUserData();
            }
        }

        private void RefreshUserData()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Users", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    var users = new List<Users>();

                    while (reader.Read())
                    {
                        string full_name = reader.GetString(0);
                        string dob = reader.GetString(1);
                        string email = reader.GetString(2);
                        string state = reader.GetString(3);
                        string city = reader.GetString(4);
                        string pincode = reader.GetString(5);
                        string full_address = reader.GetString(6);
                        int member_id = reader.GetInt32(7);
                        string password = reader.GetString(8);
                        string account_status = reader.GetString(9);

                        var user = new Users(full_name, dob, email, state, city, pincode, full_address, member_id, password, account_status);
                        users.Add(user);
                    }

                    UsersDataGrid.ItemsSource = users;
                }
            }
        }

        private void RemoveUserFromDatabase(int memberId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand($"DELETE FROM Users WHERE member_id = {memberId}", connection))
                {
                    command.ExecuteNonQuery();
                }
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
