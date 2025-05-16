<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="E_Commerce_Application.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="display: flex; flex-direction: column; align-items: center; justify-content: center; min-height: 400px;">
        <h1 style="font-size: 32px; margin-bottom: 16px; color: #222;">Welcome to E-Shop!</h1>
        <p style="font-size: 16px; color: #555; margin-bottom: 24px;">Your one-stop shop for the latest products and best deals.</p>
        <a href="Products.aspx" style="background: #222; color: #fff; padding: 10px 20px; border-radius: 4px; text-decoration: none; font-size: 14px; font-weight: 500; transition: background 0.2s;">Shop Now</a>
    </div>
    <div style="margin-top: 24px;">
        <h2 style="color: #222;">Featured Products</h2>
        <div style="display: flex; gap: 16px; flex-wrap: wrap; margin-top: 16px; justify-content: center;">
            <div style="background: #fafafa; border-radius: 8px; box-shadow: 0 1px 4px rgba(0,0,0,0.07); padding: 16px; width: 300px; text-align: center;">
                <img src="imgs/shoes4.png" alt="Product 1" style="width: 100%; border-radius: 6px; margin-bottom: 8px;" />
                <h3 style="font-size: 18px; color: #333;">Product 1</h3>
                <p style="color: #888;">$49.99</p>
                <a href="Products.aspx" style="background: #222; color: #fff; padding: 8px 16px; border-radius: 4px; text-decoration: none; font-size: 14px;">View</a>
            </div>
            <div style="background: #fafafa; border-radius: 8px; box-shadow: 0 1px 4px rgba(0,0,0,0.07); padding: 16px; width: 300px; text-align: center;">
                <img src="imgs/cloths2.jpg" alt="Product 2" style="width: 100%; border-radius: 6px; margin-bottom: 8px;" />
                <h3 style="font-size: 18px; color: #333;">Product 2</h3>
                <p style="color: #888;">$29.99</p>
                <a href="Products.aspx" style="background: #222; color: #fff; padding: 8px 16px; border-radius: 4px; text-decoration: none; font-size: 14px;">View</a>
            </div>
            <div style="background: #fafafa; border-radius: 8px; box-shadow: 0 1px 4px rgba(0,0,0,0.07); padding: 16px; width: 300px; text-align: center;">
                <img src="imgs/watch1.jpg" alt="Product 3" style="width: 100%; border-radius: 6px; margin-bottom: 8px;" />
                <h3 style="font-size: 18px; color: #333;">Product 3</h3>
                <p style="color: #888;">$19.99</p>
                <a href="Products.aspx" style="background: #222; color: #fff; padding: 8px 16px; border-radius: 4px; text-decoration: none; font-size: 14px;">View</a>
            </div>
        </div>
    </div>
</asp:Content>
