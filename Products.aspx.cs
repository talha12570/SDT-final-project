using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Application
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProducts();
            }
        }

        private void BindProducts()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ECommerceDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductID, Name, Description, Price, ImageURL FROM Products";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable products = new DataTable();
                adapter.Fill(products);
                
                ProductsRepeater.DataSource = products;
                ProductsRepeater.DataBind();
            }
        }

        protected void AddToCart_Click(object sender, EventArgs e)
        {
            string productId = ((Button)sender).CommandArgument;
            string userId = Session["UserID"] != null ? Session["UserID"].ToString() : "1"; // fallback for demo
            string connectionString = ConfigurationManager.ConnectionStrings["ECommerceDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Check if product already in cart
                string checkQuery = "SELECT Quantity FROM Cart WHERE UserID=@UserID AND ProductID=@ProductID";
                SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                checkCmd.Parameters.AddWithValue("@UserID", userId);
                checkCmd.Parameters.AddWithValue("@ProductID", productId);
                object result = checkCmd.ExecuteScalar();
                if (result != null)
                {
                    // Update quantity
                    string updateQuery = "UPDATE Cart SET Quantity = Quantity + 1 WHERE UserID=@UserID AND ProductID=@ProductID";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
                    updateCmd.Parameters.AddWithValue("@UserID", userId);
                    updateCmd.Parameters.AddWithValue("@ProductID", productId);
                    updateCmd.ExecuteNonQuery();
                }
                else
                {
                    // Add new item
                    string insertQuery = "INSERT INTO Cart (UserID, ProductID, Quantity) VALUES (@UserID, @ProductID, 1)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, connection);
                    insertCmd.Parameters.AddWithValue("@UserID", userId);
                    insertCmd.Parameters.AddWithValue("@ProductID", productId);
                    insertCmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            Response.Redirect("Cart.aspx");
        }
    }
}