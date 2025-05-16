using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Application
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // Basic validation
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text) || 
                    string.IsNullOrEmpty(txtSubject.Text) || string.IsNullOrEmpty(txtMessage.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please fill all fields.');", true);
                    return;
                }

                // Save to database
                string connectionString = ConfigurationManager.ConnectionStrings["ECommerceDB"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO ContactMessages (Name, Email, Subject, Message, SubmissionDate) " +
                                  "VALUES (@Name, @Email, @Subject, @Message, @SubmissionDate)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", txtName.Text);
                    command.Parameters.AddWithValue("@Email", txtEmail.Text);
                    command.Parameters.AddWithValue("@Subject", txtSubject.Text);
                    command.Parameters.AddWithValue("@Message", txtMessage.Text);
                    command.Parameters.AddWithValue("@SubmissionDate", DateTime.Now);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                // Clear form
                txtName.Text = "";
                txtEmail.Text = "";
                txtSubject.Text = "";
                txtMessage.Text = "";

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Thank you for your message! We will get back to you soon.');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error submitting your message. Please try again later.');", true);
            }
        }
    }
}