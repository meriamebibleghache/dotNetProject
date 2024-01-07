using System.Windows;

namespace gestion_de_biblios
{
    public partial class AdminHome : Window
    {
        private const string ConnectionString = "Data Source=DESKTOP-JTVS5VM\\SQLEXPRESS1;Initial Catalog=LibraryManagement;Integrated Security=True;";

        public AdminHome()
        {
            InitializeComponent();
        }

       public void OpenAdminBooks_Click(object sender, RoutedEventArgs e)
        {
            AdminBooks adminBooks = new AdminBooks();
            adminBooks.Show();
            Close();

        } 

        public void OpenAdminUsers_Click(object sender, RoutedEventArgs e)
        {
            AdminUser adminUser = new AdminUser();
            adminUser.Show();
            Close();

        }
        public void OpenAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminMembers adminMember = new AdminMembers();
            adminMember.Show();
            Close();

        }

        public  void OpenAdminRequests_Click(object sender, RoutedEventArgs e)
        {
            AdminRequest adminRequest = new AdminRequest();
            adminRequest.Show();
            Close();

        }
    }
}
