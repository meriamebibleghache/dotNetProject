using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationBibliothequeProjet.UserScreen
{
    public partial class uPayment : System.Web.UI.Page
    {
        DBConnect dbcon = new DBConnect();
        SqlCommand cmd;
        decimal total;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null || string.IsNullOrEmpty(Session["username"].ToString()))
            {
                Response.Redirect("~/signout.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    BindGridData();
                }
            }
        }



        private void BindGridData()
        {
            cmd = new SqlCommand("sp_FineDetails", dbcon.GetCon());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@member_id", Session["mid"].ToString());
            GridView1.DataSource = dbcon.Load_Data(cmd);

            Debug.WriteLine($"GridView data count: {GridView1.Rows.Count}");  // Add this line

            GridView1.DataBind();

            // Calculate the total price and display it
            CalculateAndDisplayTotalPrice();
        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string bookId = GridView1.DataKeys[e.Row.RowIndex].Values["book_id"].ToString();
                Debug.WriteLine(bookId);
                decimal bookPrice = GetBookPrice(bookId);
                total += bookPrice;
                e.Row.Cells[3].Text = "Total Price: $" + total.ToString();
                Session["OriginalTotalPrice"] = total;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                // Display the total in the Footer row
                e.Row.Cells[3].Text = "Total Price: $" + total.ToString("0.00");
                Session["OriginalTotalPrice"] = total; // Store the total in a session variable for later use
            }
        }





        private void CalculateAndDisplayTotalPrice()
        {
            total = 200;

            if (GridView1.Rows.Count > 0)
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    string bookId = GridView1.DataKeys[row.RowIndex].Values["book_id"].ToString();
                    Debug.WriteLine($"book id : {bookId}");
                    decimal bookPrice = GetBookPrice(bookId);
                    total += bookPrice;
                }
            }

            Debug.WriteLine($"Total Price: {total}");  // Check this in the debugger output window

            lblTotalPrice.Text = $"Total Price: ${total.ToString("0.00")}";
            Session["OriginalTotalPrice"] = total;
        }






        private decimal GetBookPrice(string bookId)
        {
            decimal price = 0;

            using (SqlCommand cmd = new SqlCommand("SELECT price FROM book_master_tbl WHERE book_id = @bookId", dbcon.GetCon()))
            {
                dbcon.OpenCon();
                cmd.Parameters.AddWithValue("@bookId", bookId);
                object result = cmd.ExecuteScalar();

                if (result != null && decimal.TryParse(result.ToString(), out decimal bookPrice))
                {
                    price = bookPrice;
                }
                else
                {
                    // Failed to retrieve the price. Handle the error or set a default value.
                    // For now, let's set a default value of 0
                    price = 0;
                }
            }

            return price;
        }


        protected void btnApplyPromo_Click(object sender, EventArgs e)
        {
            // Implement your logic to check and apply the promo code
            // Update lblDiscountedTotalPrice.Text accordingly based on the applied promo code
            // For now, let's assume a fixed discount value for demonstration purposes
            decimal discount = 10;
            decimal discountedTotalPrice = CalculateDiscountedTotalPrice(discount);
            lblTotalPrice.Text = $"Discounted Total Price: {discountedTotalPrice:C}";
            lblPromoMessage.Text = "Promo code applied successfully!";
        }

        private decimal CalculateDiscountedTotalPrice(decimal discount)
        {
            // Retrieve the original total price from the session variable
            if (Session["OriginalTotalPrice"] != null && decimal.TryParse(Session["OriginalTotalPrice"].ToString(), out decimal originalTotalPrice))
            {
                decimal discountedTotalPrice = originalTotalPrice - discount;
                return discountedTotalPrice < 0 ? 0 : discountedTotalPrice;
            }

            // Return 0 if the original total price is not available
            return 0;
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            Button btnPay = (Button)sender;
            string bookId = btnPay.CommandArgument;

            // Implement logic to process the payment
            // This may involve collecting credit card information, using a secure payment gateway, etc.
            // Display a success message or redirect to a success page after successful payment
            string successScript = "swal('Success','Account created successfully','success')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", successScript, true);
            
        }
    }
}
