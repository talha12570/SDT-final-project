using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace E_Commerce_Application
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string username = Request.Form["username"];
                string password = Request.Form["password"];
                
                string connectionString = ConfigurationManager.ConnectionStrings["ECommerceDB"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT UserID FROM Users WHERE Username=@Username AND Password=@Password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    
                    connection.Open();
                    object result = command.ExecuteScalar();
                    connection.Close();
                    
                    if (result != null)
                    {
                        Session["UserID"] = result.ToString();
                        Response.Redirect("Products.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid username or password.');", true);
                    }
                }
            }
        }
    }
}