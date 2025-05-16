<%@ Page Language="C#" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="E_Commerce_Application.About" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>About Us</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            line-height: 1.6;
            color: #333;
            max-width: 1200px;
            margin: 0 auto;
            padding: 20px;
        }
        .about-container {
            display: flex;
            flex-direction: column;
            gap: 40px;
        }
        .section {
            background: #f8f9fa;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        h1, h2 {
            color: #222;
            margin-bottom: 20px;
        }
        .contact-form {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 20px;
        }
        .form-group {
            margin-bottom: 15px;
        }
        label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }
        input, textarea {
            width: 100%;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 4px;
        }
        button {
            background: #28a745;
            color: #fff;
            padding: 10px 24px;
            border-radius: 4px;
            border: none;
            font-size: 16px;
            cursor: pointer;
        }
        .contact-info {
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="about-container">
            <div class="section">
                <h1>About Our Platform</h1>
                <p>Welcome to our e-commerce platform, where we bring you the best products at competitive prices. Our mission is to provide a seamless shopping experience with quality products and excellent customer service.</p>
                <p>Founded in 2023, we've grown from a small startup to a trusted online marketplace serving thousands of customers nationwide.</p>
            </div>
            
            <div class="section">
                <h2>Contact Us</h2>
                <div class="contact-form">
                    <div class="form-group">
                        <label for="txtName">Name</label>
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtEmail">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
                    </div>
                    <div class="form-group" style="grid-column: span 2;">
                        <label for="txtSubject">Subject</label>
                        <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group" style="grid-column: span 2;">
                        <label for="txtMessage">Message</label>
                        <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    </div>
                    <div class="form-group" style="grid-column: span 2;">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                    </div>
                </div>
                
                <div class="contact-info">
                    <h3>Other Ways to Reach Us</h3>
                    <p><strong>Email:</strong> support@ecommerceplatform.com</p>
                    <p><strong>Phone:</strong> (123) 456-7890</p>
                    <p><strong>Address:</strong> 123 Commerce Street, Business City, BC 12345</p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>