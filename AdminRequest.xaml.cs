using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Windows;
using gestion_de_biblios.models;

namespace gestion_de_biblios
{
    public partial class AdminRequest : Window
    {
        private const string ConnectionString = "Data Source=DESKTOP-JTVS5VM\\SQLEXPRESS1;Initial Catalog=LibraryManagement;Integrated Security=True;";

        public AdminRequest()
        {
            InitializeComponent();
            RefreshRequestsData();
        }

        private void RefreshRequestsData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT * FROM BookRequest", connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        var requests = new List<BookRequest>();

                        while (reader.Read())
                        {
                            int requestId = reader.GetInt32(reader.GetOrdinal("RequestID"));
                            int bookId = reader.GetInt32(reader.GetOrdinal("BookId"));
                            int memberId = reader.GetInt32(reader.GetOrdinal("member_id"));
                            DateTime requestDate = reader.GetDateTime(reader.GetOrdinal("RequestDate"));
                            bool isAccepted = reader.GetBoolean(reader.GetOrdinal("IsAccepted"));

                            var request = new BookRequest(requestId, bookId, memberId, requestDate, isAccepted);
                            requests.Add(request);
                        }

                        RequestsDataGrid.ItemsSource = requests;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing requests data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void AcceptRequest(int requestId)
        {
            try
            {
                int bookId = -1; 

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand($"SELECT * FROM BookRequest WHERE RequestId = {requestId}", connection))
                    {
                        SqlDataReader detailsReader = command.ExecuteReader();

                        // Boucle while pour traiter les résultats
                        while (detailsReader.Read())
                        {
                            bookId = detailsReader.GetInt32(detailsReader.GetOrdinal("BookId"));
                            int member_id = detailsReader.GetInt32(detailsReader.GetOrdinal("member_id"));

                            // Mise à jour de la table Loan
                            string insertLoanQuery = $"INSERT INTO Loan (BookId, member_id, LoanDate) VALUES ({bookId}, {member_id}, GETDATE())";
                            using (SqlCommand insertLoanCommand = new SqlCommand(insertLoanQuery, connection))
                            {
                                insertLoanCommand.ExecuteNonQuery();
                            }

                            // Mise à jour de la table BookRequest
                            string updateRequestQuery = $"UPDATE BookRequest SET IsAccepted = 1 WHERE RequestId  = {requestId}";
                            using (SqlCommand updateRequestCommand = new SqlCommand(updateRequestQuery, connection))
                            {
                                updateRequestCommand.ExecuteNonQuery();
                            }

                            // Mise à jour de la table Book
                            string updateBookQuery = $"UPDATE Book SET IsAvailable = 0 WHERE BookId = {bookId}";
                            using (SqlCommand updateBookCommand = new SqlCommand(updateBookQuery, connection))
                            {
                                updateBookCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }

                RefreshRequestsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error accepting request: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }






        private void RejectRequest(int requestId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string updateRequestQuery = $"UPDATE BookRequest SET IsAccepted = 0 WHERE RequestId = {requestId}";
                    using (SqlCommand updateRequestCommand = new SqlCommand(updateRequestQuery, connection))
                    {
                        updateRequestCommand.ExecuteNonQuery();
                    }
                }

                RefreshRequestsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error rejecting request: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AcceptRequest_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = RequestsDataGrid.SelectedItem as BookRequest;

            if (selectedRequest != null)
            {
                AcceptRequest(selectedRequest.RequestId);
            }
            else
            {
                MessageBox.Show("Please select a loan request");
            }
        }

        private void RejectRequest_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = RequestsDataGrid.SelectedItem as BookRequest;

            if (selectedRequest != null)
            {
                RejectRequest(selectedRequest.RequestId);
            }
            else
            {
                MessageBox.Show("Please select a loan request");
            }
        }
        private void ViewLoan(object sender,RoutedEventArgs e)
        {
            var adminloan = new AdminLoan();
            adminloan.Show();
            Close();
        }
        private void ReturnToHome_Click(object sender, RoutedEventArgs e)
        {
            var adminhome = new AdminHome();
            adminhome.Show();

            Close();
        }

    }

}
