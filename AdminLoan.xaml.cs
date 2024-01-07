using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Windows;
using ClosedXML.Excel;
using gestion_de_biblios.models;

namespace gestion_de_biblios
{
    public partial class AdminLoan : Window
    {
        private const string ConnectionString = "Data Source=DESKTOP-JTVS5VM\\SQLEXPRESS1;Initial Catalog=LibraryManagement;Integrated Security=True;";

        public AdminLoan()
        {
            InitializeComponent();
            RefreshLoansData();
        }

        private void RefreshLoansData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT * FROM Loan", connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        var loans = new List<Loan>();

                       
                            while (reader.Read())
                            {
                                int LoanId = reader.GetInt32(0);
                                int BookId = reader.GetInt32(1);
                                int member_id = reader.GetInt32(2);
                            DateTime LoanDate = reader.GetDateTime(3);

                                var loan = new Loan(LoanId, BookId, member_id, LoanDate);
                            loans.Add(loan);
                        }

                        LoansDataGrid.ItemsSource = loans;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing loans data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    
        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Loans");

                worksheet.Cell(1, 1).Value = "LoanId";
                worksheet.Cell(1, 2).Value = "BookId";
                worksheet.Cell(1, 3).Value = "member_id";
                worksheet.Cell(1, 4).Value = "LoanDate";

                var loans = (List<Loan>)LoansDataGrid.ItemsSource;

                for (int i = 0; i < loans.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = loans[i].LoanId;
                    worksheet.Cell(i + 2, 2).Value = loans[i].BookId;
                    worksheet.Cell(i + 2, 3).Value = loans[i].member_id; 
                    worksheet.Cell(i + 2, 4).Value = loans[i].LoanDate;
                }

                var dialog = new Microsoft.Win32.SaveFileDialog
                {
                    DefaultExt = ".xlsx",
                    Filter = "Excel Workbook (*.xlsx)|*.xlsx"
                };

                if (dialog.ShowDialog() == true)
                {
                    workbook.SaveAs(dialog.FileName);
                    MessageBox.Show("Export successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to Excel: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ImportFromExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new Microsoft.Win32.OpenFileDialog
                {
                    DefaultExt = ".xlsx",
                    Filter = "Excel Workbook (*.xlsx)|*.xlsx"
                };

                if (dialog.ShowDialog() == true)
                {
                    using (var workbook = new XLWorkbook(dialog.FileName))
                    {
                        var worksheet = workbook.Worksheet(1);
                        var rows = worksheet.RowsUsed();

                        foreach (var row in rows.Skip(1))
                        {
                            int empruntId = Convert.ToInt32(row.Cell(1).Value);
                            int bookId = Convert.ToInt32(row.Cell(2).Value);
                            int member_id = Convert.ToInt32(row.Cell(3).Value);
                            DateTime loanDate = Convert.ToDateTime(row.Cell(4).Value);

                        }

                        RefreshLoansData();
                        MessageBox.Show("Import successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error importing from Excel: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void RefreshLoanData_Click(object sender, RoutedEventArgs e)
        {
            RefreshLoansData();
        }
        private void ReturnToHome_Click(object sender, RoutedEventArgs e)
        {
            var adminHome = new AdminHome();
            adminHome.Show();
            Close();
        }
    }
}
