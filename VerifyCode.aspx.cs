using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationBibliothequeProjet
{
    public partial class VerifyCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vérifier si le code de vérification est présent dans la session
            if (Session["VerificationCode"] == null)
            {
                // Si le code n'est pas présent, rediriger vers la page SignUp
                Response.Redirect("SignUp.aspx");
            }
        }

        protected void btnVerifyCode_Click(object sender, EventArgs e)
        {
            // Récupérer le code de vérification de la session
            string storedVerificationCode = Session["VerificationCode"].ToString();

            // Récupérer le code saisi par l'utilisateur
            string userEnteredCode = txtVerificationCode.Text.Trim();

            // Comparer les codes
            if (userEnteredCode == storedVerificationCode)
            {
                // Les codes correspondent, afficher un message de succès
                string successScript = "swal('Success','Verification successful','success')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", successScript, true);
            }
            else
            {
                // Les codes ne correspondent pas, afficher un message d'erreur
                string errorScript = "swal('Error','Verification failed. Please try again.','error')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", errorScript, true);
            }
        }
    }
}