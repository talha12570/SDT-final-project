<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="E_Commerce_Application.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="color: #222; margin-bottom: 24px;">Login</h1>
    <div style="background: #fafafa; border-radius: 8px; box-shadow: 0 1px 4px rgba(0,0,0,0.07); padding: 32px; max-width: 600px; margin: 0 auto;">
        <form>
            <div style="margin-bottom: 20px;">
                <label for="username" style="display: block; color: #333; margin-bottom: 8px;">Username</label>
                <input type="text" id="username" name="username" style="width: 100%; padding: 12px; border-radius: 4px; border: 1px solid #ccc; font-size: 16px;" />
            </div>
            <div style="margin-bottom: 20px;">
                <label for="password" style="display: block; color: #333; margin-bottom: 8px;">Password</label>
                <input type="password" id="password" name="password" style="width: 100%; padding: 12px; border-radius: 4px; border: 1px solid #ccc; font-size: 16px;" />
            </div>
            <button type="submit" style="background: #222; color: #fff; padding: 12px 24px; border-radius: 4px; border: none; font-size: 16px; font-weight: 500; cursor: pointer;">Login</button>
        </form>
    </div>
</asp:Content>
