<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="E_Commerce_Application.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="color: #222; margin-bottom: 24px;">Products</h1>
    <asp:Repeater ID="ProductsRepeater" runat="server">
    <ItemTemplate>
        <div style="background: #fafafa; border-radius: 8px; box-shadow: 0 1px 4px rgba(0,0,0,0.07); padding: 16px; width: 300px; text-align: center; margin: 16px;">
            <img src="<%# Eval("ImageURL") %>" alt="<%# Eval("Name") %>" style="width: 100%; border-radius: 6px; margin-bottom: 8px;" />
            <h3 style="font-size: 18px; color: #333;"><%# Eval("Name") %></h3>
            <p style="color: #666; margin-bottom: 8px;"><%# Eval("Description") %></p>
            <p style="color: #888; font-weight: bold;">$<%# Eval("Price", "{0:0.00}") %></p>
            <asp:Button ID="btnAddToCart" runat="server" style="background: #222; color: #fff; padding: 8px 16px; border-radius: 4px; border: none; font-size: 16px; cursor: pointer;" Text="Add to Cart" CommandArgument='<%# Eval("ProductID") %>' OnClick="AddToCart_Click" />
        </div>
    </ItemTemplate>
</asp:Repeater>
</asp:Content>
