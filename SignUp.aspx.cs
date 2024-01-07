using ApplicationBibliothequeProjet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationBibliothequeProjet
{
    public partial class SignUp : System.Web.UI.Page
    {
        DBConnect dbcon = new DBConnect();
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Autogenrate();
            }
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            if (checkDuplicationMemberExist())
            {
                Response.Write("<script>alert('Member already exists with this ID and email');</script>");
            }
            else
            {
                // Si l'envoi du code de vérification réussit, alors enregistrez le compte
                CreateAccount();
            }
               
        }

        private string GenerateVerificationCode()
        {
            // Logique de génération de code (peut-être utiliser une bibliothèque de génération de code aléatoire)
            return "123456"; // Exemple, remplacez par votre propre logique
        }
        private bool SendVerificationCodeEmail(string userEmail, string verificationCode)
        {
            try
            {
                // Replace these values with your Mailtrap SMTP details
                string smtpServer = "sandbox.smtp.mailtrap.io";
                int smtpPort = 2525;
                string smtpUsername = "de1a5f109783d1";
                string smtpPassword = "********03ce"; // Replace with your actual Mailtrap password

                // Create an instance of SmtpClient
                var smtpClient = new SmtpClient(smtpServer, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                    EnableSsl = true
                };

                // Replace these values with your actual email addresses, subject, and body
                string fromEmail = "meriamebibelghache@gmail.com"; // Replace with your actual email address
                string subject = "Code de vérification pour votre inscription";
                string body = $"Votre code de vérification est : {verificationCode}";

                // Create a MailMessage
                var mailMessage = new MailMessage(fromEmail, userEmail)
                {
                    Subject = subject,
                    Body = body
                };

                // Send the email
                smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                // Handle email sending errors here
                Debug.WriteLine($"Erreur d'envoi d'e-mail : {ex.Message}");
                return false;
            }
        }

        private void CreateAccount()
        {
            try
            {
                dbcon.OpenCon();       
                using (SqlCommand cmd = new SqlCommand("sp_InsertSignup", dbcon.GetCon()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@full_name", txtFullName.Text);
                    cmd.Parameters.AddWithValue("@dob", txtDOB.Text);
                    cmd.Parameters.AddWithValue("@contact_no", txtContactNO.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@state", ddlState.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@city", txtCity.Text);
                    cmd.Parameters.AddWithValue("@pincode", txtPIN.Text);
                    cmd.Parameters.AddWithValue("@full_address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@member_id", txtMemberID.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@account_status", "pending");

                    int rowsAffected = cmd.ExecuteNonQuery();
                    Debug.WriteLine($"Rows affected: {rowsAffected}");
                    if (rowsAffected < 0)
                    {
                        // Rows were affected (inserted)
                        string successScript = "swal('Success','Account created successfully','success')";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", successScript, true);
                        clrcontrol();
                        Autogenrate();

                    }
                    else
                    {
                        // Rows were not affected (no records inserted)
                        string errorScript = "swal('Error','Error! Record not inserted here . Please try again.','error')";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", errorScript, true);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                string errorScript = $"swal('Error','An error occurred: {ex.Message}','error')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", errorScript, true);
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                dbcon.CloseCon();
            }
        }


        protected bool checkDuplicationMemberExist()
        {
            cmd = new SqlCommand("sp_CheckDuplicateMember", dbcon.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", txtMemberID.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            dbcon.OpenCon();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void Autogenrate()
        {
            try
            {
                int newMemberId;

                using (SqlCommand cmd = new SqlCommand("SELECT MAX(member_id) AS ID FROM member_master_tbl", dbcon.GetCon()))
                {
                    dbcon.OpenCon();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        // Si une ligne est lue, récupérer la valeur maximale de member_id et l'incrémenter de 1
                        if (int.TryParse(dr["ID"].ToString(), out int maxMemberId))
                        {
                            newMemberId = maxMemberId + 1;
                        }
                        else
                        {
                            // En cas d'erreur de conversion, utiliser une valeur par défaut
                            newMemberId = 1;
                        }
                    }
                    else
                    {
                        // Si aucune ligne n'est lue, utiliser une valeur par défaut
                        newMemberId = 1;
                    }
                }

                // Affecter le nouvel identifiant de membre au contrôle TextBox
                txtMemberID.Text = newMemberId.ToString();
            }
            catch (Exception ex)
            {
                // Gérer les exceptions ici
                Response.Write("<script>alert('An error occurred: " + ex.Message + "');</script>");
            }
            finally
            {
                dbcon.CloseCon();
            }
        }


        private void clrcontrol()
        {
            txtFullName.Text = txtAddress.Text = txtCity.Text = txtContactNO.Text = txtDOB.Text = txtEmail.Text = txtFullName.Text = txtPassword.Text = txtPIN.Text = String.Empty;
            ddlState.SelectedIndex = 0;
            ddlState.SelectedItem.Text = "Select";
            txtFullName.Focus();

        }
    }
}