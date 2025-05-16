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
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCartItems();
            }
        }

        private void BindCartItems()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ECommerceDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT c.CartID, p.ProductID, p.Name, p.Price, c.Quantity 
                                FROM Cart c 
                                JOIN Products p ON c.ProductID = p.ProductID 
                                WHERE c.UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                string userId = Session["UserID"] != null ? Session["UserID"].ToString() : "1"; // fallback for demo
                command.Parameters.AddWithValue("@UserID", userId);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable cartItems = new DataTable();
                adapter.Fill(cartItems);
                
                CartRepeater.DataSource = cartItems;
                CartRepeater.DataBind();

                // Calculate total
                decimal grandTotal = 0;
                foreach (DataRow row in cartItems.Rows)
                {
                    decimal price = Convert.ToDecimal(row["Price"]);
                    int quantity = Convert.ToInt32(row["Quantity"]);
                    grandTotal += price * quantity;
                }
                // Store total in ViewState for access in .aspx
                ViewState["GrandTotal"] = grandTotal;
                // Display total
                lblGrandTotal.Text = grandTotal.ToString("0.00");
            }
        }

        protected void CartRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string userId = Session["UserID"] != null ? Session["UserID"].ToString() : "1"; // fallback for demo
            string productId = e.CommandArgument.ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["ECommerceDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (e.CommandName == "Delete")
                {
                    string deleteQuery = "DELETE FROM Cart WHERE UserID=@UserID AND ProductID=@ProductID";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection);
                    deleteCmd.Parameters.AddWithValue("@UserID", userId);
                    deleteCmd.Parameters.AddWithValue("@ProductID", productId);
                    deleteCmd.ExecuteNonQuery();
                }
                else if (e.CommandName == "Increase")
                {
                    string updateQuery = "UPDATE Cart SET Quantity = Quantity + 1 WHERE UserID=@UserID AND ProductID=@ProductID";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
                    updateCmd.Parameters.AddWithValue("@UserID", userId);
                    updateCmd.Parameters.AddWithValue("@ProductID", productId);
                    updateCmd.ExecuteNonQuery();
                }
                else if (e.CommandName == "Decrease")
                {
                    // First, get current quantity
                    string selectQuery = "SELECT Quantity FROM Cart WHERE UserID=@UserID AND ProductID=@ProductID";
                    SqlCommand selectCmd = new SqlCommand(selectQuery, connection);
                    selectCmd.Parameters.AddWithValue("@UserID", userId);
                    selectCmd.Parameters.AddWithValue("@ProductID", productId);
                    int quantity = (int)(selectCmd.ExecuteScalar() ?? 0);
                    if (quantity > 1)
                    {
                        string updateQuery = "UPDATE Cart SET Quantity = Quantity - 1 WHERE UserID=@UserID AND ProductID=@ProductID";
                        SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
                        updateCmd.Parameters.AddWithValue("@UserID", userId);
                        updateCmd.Parameters.AddWithValue("@ProductID", productId);
                        updateCmd.ExecuteNonQuery();
                    }
                    else if (quantity == 1)
                    {
                        string deleteQuery = "DELETE FROM Cart WHERE UserID=@UserID AND ProductID=@ProductID";
                        SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection);
                        deleteCmd.Parameters.AddWithValue("@UserID", userId);
                        deleteCmd.Parameters.AddWithValue("@ProductID", productId);
                        deleteCmd.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
            BindCartItems();
        }

        private decimal GetProductPrice(string productId, SqlConnection connection, SqlTransaction transaction)
        {
            string query = "SELECT Price FROM Products WHERE ProductID = @ProductID";
            SqlCommand command = new SqlCommand(query, connection, transaction);
            command.Parameters.AddWithValue("@ProductID", productId);
            return Convert.ToDecimal(command.ExecuteScalar());
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            // Skip order summary and proceed directly to checkout
            btnConfirmCheckout_Click(sender, e);
        }

        protected void btnBackToCart_Click(object sender, EventArgs e)
        {
            // Hide order summary and show cart contents
            orderSummary.Visible = false;
            BindCartItems();
        }

        

        protected void btnConfirmCheckout_Click(object sender, EventArgs e)
        {
            string userId = Session["UserID"] != null ? Session["UserID"].ToString() : "1";
            string connectionString = ConfigurationManager.ConnectionStrings["ECommerceDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    // 1. Create new order
                    string insertOrderQuery = "INSERT INTO Orders (UserID, OrderDate, TotalAmount) OUTPUT INSERTED.OrderID VALUES (@UserID, @OrderDate, @TotalAmount)";
                    SqlCommand orderCmd = new SqlCommand(insertOrderQuery, connection, transaction);
                    orderCmd.Parameters.AddWithValue("@UserID", userId);
                    orderCmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                    orderCmd.Parameters.AddWithValue("@TotalAmount", ViewState["GrandTotal"] ?? 0);
                    int orderId = (int)orderCmd.ExecuteScalar();

                    // 2. Get cart items
                    string selectCartQuery = "SELECT ProductID, Quantity FROM Cart WHERE UserID=@UserID";
                    SqlCommand selectCartCmd = new SqlCommand(selectCartQuery, connection, transaction);
                    selectCartCmd.Parameters.AddWithValue("@UserID", userId);
                    SqlDataAdapter adapter = new SqlDataAdapter(selectCartCmd);
                    DataTable cartItems = new DataTable();
                    adapter.Fill(cartItems);

                    // 3. Insert into OrderItems
                    foreach (DataRow row in cartItems.Rows)
                    {
                        string insertOrderItemQuery = "INSERT INTO OrderItems (OrderID, ProductID, Quantity, Price) VALUES (@OrderID, @ProductID, @Quantity, @Price)";
                        SqlCommand orderItemCmd = new SqlCommand(insertOrderItemQuery, connection, transaction);
                        orderItemCmd.Parameters.AddWithValue("@OrderID", orderId);
                        orderItemCmd.Parameters.AddWithValue("@ProductID", row["ProductID"]);
                        orderItemCmd.Parameters.AddWithValue("@Quantity", row["Quantity"]);
                        orderItemCmd.Parameters.AddWithValue("@Price", GetProductPrice(row["ProductID"].ToString(), connection, transaction));
                        orderItemCmd.ExecuteNonQuery();
                    }

                    // 4. Clear cart
                    string clearCartQuery = "DELETE FROM Cart WHERE UserID=@UserID";
                    SqlCommand clearCartCmd = new SqlCommand(clearCartQuery, connection, transaction);
                    clearCartCmd.Parameters.AddWithValue("@UserID", userId);
                    clearCartCmd.ExecuteNonQuery();

                    transaction.Commit();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Order placed successfully!');", true);
                    lblGrandTotal.Text = "Order placed successfully!";
                    BindCartItems(); // Refresh cart to show it's empty
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    lblGrandTotal.Text = "Checkout failed: " + ex.Message;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}