using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using System.Diagnostics;
namespace ApplicationBibliothequeProjet.UserScreen
{

    public partial class uViewBook : System.Web.UI.Page
    {
        DBConnect dbcon = new DBConnect();
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                BindGridData();
            }
        }
        private void BindGridData()
        {
            cmd = new SqlCommand("sp_Insert_Up_Del_BookInventory", dbcon.GetCon());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@StatementType", "Select");
            GridView1.DataSource = dbcon.Load_Data(cmd);
            GridView1.DataBind();
        }
        protected void btnReserve_Click(object sender, EventArgs e)
        {
            Button btnReserve = (Button)sender;
            string bookId = btnReserve.CommandArgument;

            // Retrieve user ID from the session
            string userId = Session["username"].ToString();
            Debug.WriteLine($"user id {userId}");
            if (!string.IsNullOrEmpty(userId))
            {
                // Retrieve book information
                string bookInfo = GetBookInfo(bookId);

                // Save reservation in the database
                SaveReservation(userId, bookId);

            }
            else
            {
                // Handle the case where user ID is not available (e.g., user not logged in)
                // You may redirect the user to a login page or display a message.
                
            }
        }

        private void SaveReservation(string userId, string bookId)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Reservation (BookId, UserId, ReservationDate) VALUES (@BookId, @UserId, GETDATE())", dbcon.GetCon()))
            {
                dbcon.OpenCon();
                cmd.Parameters.AddWithValue("@BookId", bookId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.ExecuteNonQuery();
            }
        }










        private string GetBookInfo(string bookId)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT book_name, author_name, genre FROM book_master_tbl WHERE book_id = @bookId", dbcon.GetCon()))
            {
                dbcon.OpenCon();
                cmd.Parameters.AddWithValue("@bookId", bookId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return $"Book Name: {reader["book_name"]}, Author: {reader["author_name"]}, Genre: {reader["genre"]}";
                    }
                }
            }

            return "Book Information Not Found";
        }





        private string GetUserInfo()
        {
            // Retrieve user ID from the session
            string userId = Session["UserId"] as string;

            if (userId != null)
            {
                using (SqlCommand cmd = new SqlCommand("SELECT full_name, email FROM member_master_tbl WHERE member_id = @userId", dbcon.GetCon()))
                {
                    dbcon.OpenCon();
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return $"User Name: {reader["full_name"]}, Email: {reader["email"]}";
                        }
                    }
                }
            }

            return "User Information Not Found";
        } 



        }
    }