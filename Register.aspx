<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="E_Commerce_Application.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="color: #222; margin-bottom: 24px;">Register</h1>
    <div style="background: #fafafa; border-radius: 8px; box-shadow: 0 1px 4px rgba(0,0,0,0.07); padding: 16px; max-width: 800px; margin: 0 auto;">
        
            <div style="margin-bottom: 16px;">
                <label for="username" style="display: block; color: #333; margin-bottom: 8px;">Username</label>
                <asp:TextBox ID="txtUsername" runat="server" style="width: 100%; padding: 8px; border-radius: 4px; border: 1px solid #ccc;"></asp:TextBox>
            </div>
            <div style="margin-bottom: 16px;">
                <label for="email" style="display: block; color: #333; margin-bottom: 8px;">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" style="width: 100%; padding: 8px; border-radius: 4px; border: 1px solid #ccc;"></asp:TextBox>
            </div>
            <div style="margin-bottom: 16px;">
                <label for="password" style="display: block; color: #333; margin-bottom: 8px;">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" style="width: 100%; padding: 8px; border-radius: 4px; border: 1px solid #ccc;"></asp:TextBox>
            </div>
            <div style="margin-bottom: 16px;">
                <label for="fullname" style="display: block; color: #333; margin-bottom: 8px;">Full Name</label>
                <asp:TextBox ID="txtFullName" runat="server" style="width: 100%; padding: 8px; border-radius: 4px; border: 1px solid #ccc;" required="true"></asp:TextBox>
            </div>
            <div style="margin-bottom: 16px;">
                <label for="address" style="display: block; color: #333; margin-bottom: 8px;">Address</label>
                <asp:TextBox ID="txtAddress" runat="server" style="width: 100%; padding: 8px; border-radius: 4px; border: 1px solid #ccc;"></asp:TextBox>
            </div>
            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" Style="background: #222; color: #fff; padding: 10px 20px; border-radius: 4px; border: none; font-size: 14px; font-weight: 500; cursor: pointer;" />
        
    </div>
</asp:Content>
