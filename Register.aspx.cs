using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace E_Commerce_Application
    {
        public partial class Register : System.Web.UI.Page
        {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Validate inputs
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string address = txtAddress.Text.Trim();
            
            // Check for missing required fields
            List<string> missingFields = new List<string>();
            
            if (string.IsNullOrEmpty(username))
                missingFields.Add("Username");
            
            if (string.IsNullOrEmpty(email))
                missingFields.Add("Email");
            
            if (string.IsNullOrEmpty(password))
                missingFields.Add("Password");
            
            if (string.IsNullOrEmpty(fullName))
                missingFields.Add("Full Name");
                
            if (string.IsNullOrEmpty(address))
                missingFields.Add("Address");
            
            if (missingFields.Count > 0)
            {
                string errorMessage = "Please fill in all required fields: " + string.Join(", ", missingFields);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + errorMessage + "');", true);
                return;
            }
            
            // Validate field formats
            if (username.Length < 4 || username.Length > 20)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Username must be between 4-20 characters.');", true);
                return;
            }
            
            if (!email.Contains("@") || !email.Contains("."))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter a valid email address.');", true);
                return;
            }
            
            if (password.Length < 8)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password must be at least 8 characters.');", true);
                return;
            }
            
            string connectionString = ConfigurationManager.ConnectionStrings["ECommerceDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                
                try
                {
                    // First check if username already exists
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username=@Username";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection, transaction);
                    checkCommand.Parameters.AddWithValue("@Username", username);
                    
                    int userCount = (int)checkCommand.ExecuteScalar();
                    
                    if (userCount > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Username already exists.');", true);
                        transaction.Rollback();
                        return;
                    }
                    
                    // Insert new user
                    string insertQuery = "INSERT INTO Users (Username, Email, Password, FullName, Address) VALUES (@Username, @Email, @Password, @FullName, @Address)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection, transaction);
                    insertCommand.Parameters.AddWithValue("@Username", username);
                    insertCommand.Parameters.AddWithValue("@Email", email);
                    // Hash password before storing
                    string hashedPassword = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));
                    insertCommand.Parameters.AddWithValue("@Password", hashedPassword);
                    insertCommand.Parameters.AddWithValue("@FullName", fullName);
                    insertCommand.Parameters.AddWithValue("@Address", address);
                    
                    insertCommand.ExecuteNonQuery();
                    transaction.Commit();
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Registration successful! Please login.');", true);
                    Response.Redirect("Login.aspx");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Log the error for debugging
                    System.Diagnostics.Trace.WriteLine(string.Format("Registration error: {0}", ex.Message));
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Registration failed. Please try again.');", true);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}