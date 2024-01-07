using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ApplicationBibliothequeProjet.UserScreen
{
    public partial class ReservationDetails : System.Web.UI.Page
    {
        DBConnect dbcon = new DBConnect();
        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUserReservations();
            }
        }

        private void BindUserReservations()
        {
            // Retrieve user ID from the session
            string userId = Session["username"]?.ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                cmd = new SqlCommand("SELECT BookId, ReservationDate FROM Reservation WHERE UserId = @UserId", dbcon.GetCon());
                cmd.Parameters.AddWithValue("@UserId", userId);

                GridViewReservations.DataSource = dbcon.Load_Data(cmd);
                GridViewReservations.DataBind();
            }
            else
            {
                // Handle the case where user ID is not available (e.g., user not logged in)
                // You may redirect the user to a login page or display a message.
            }
        }

        protected void GridViewReservations_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CancelReservation")
            {
                // Get the BookId from the CommandArgument
                string bookId = e.CommandArgument.ToString();

                // Implement the logic to cancel the reservation (delete from the Reservation table)
                CancelReservation(bookId);

                // Rebind the GridView to reflect the updated data
                BindUserReservations();
            }
        }

        private void CancelReservation(string bookId)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Reservation WHERE BookId = @BookId AND UserId = @UserId", dbcon.GetCon()))
            {
                // Retrieve user ID from the session
                string userId = Session["username"]?.ToString();

                if (!string.IsNullOrEmpty(userId))
                {
                    dbcon.OpenCon();
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    // Handle the case where user ID is not available (e.g., user not logged in)
                    // You may redirect the user to a login page or display a message.
                }
            }
        }

        protected void AnotherButton_Click(object sender, EventArgs e)
        {
            // Your logic for AnotherButton_Click event
            // Implement the functionality you want for this button click.
            // For example, you can display a message or perform some other action.
        }
    }
}
