using gestion_de_biblios;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace gestion_de_biblios
{
    public partial class AdminLogin : Window
    {
        private const string ConnectionString = "Data Source=DESKTOP-JTVS5VM\\SQLEXPRESS1;Initial Catalog=LibraryManagement;Integrated Security=True;";

        public AdminLogin()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string enteredUsername = UsernameTextBox.Text;
                string enteredPassword = PasswordBox.Password;

                if (IsValidUser(connection, enteredUsername, enteredPassword))
                {
                    MessageBox.Show("Successful connection");


                    
                    AdminHome adminHome = new AdminHome();
                    adminHome.Show();
                    Close();

                }
                else
                {
                    MessageBox.Show("Incorrect credentials Please try again");
                }
            }
        }


        private bool IsValidUser(SqlConnection connection, string username, string password)
        {
            using (SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM Admin WHERE Username = @Username AND Password = @Password", connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            AdminRegister adminRegister = new AdminRegister();
            adminRegister.Show();
            Close();
        }

       

    }
}
