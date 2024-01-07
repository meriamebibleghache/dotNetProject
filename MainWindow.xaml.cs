using System.Windows;

namespace gestion_de_biblios
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminLogin adminLoginWindow = new AdminLogin();
            adminLoginWindow.Show();

            this.Close();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
             AdminRegister adminRegister= new AdminRegister();
            adminRegister.Show();
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
