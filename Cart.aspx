<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="E_Commerce_Application.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="color: #222; margin-bottom: 24px;">Your Cart</h1>
    <div style="background: #fafafa; border-radius: 8px; box-shadow: 0 1px 4px rgba(0,0,0,0.07); padding: 16px; max-width: 800px; margin: 0 auto;">
        <asp:Repeater ID="CartRepeater" runat="server" OnItemCommand="CartRepeater_ItemCommand">
            <HeaderTemplate>
                <table style="width: 100%; border-collapse: collapse;">
                    <thead>
                        <tr style="background: #f5f5f5;">
                            <th style="padding: 16px; text-align: left; color: #333; font-size: 16px;">Product</th>
                            <th style="padding: 16px; text-align: right; color: #333; font-size: 16px;">Price</th>
                            <th style="padding: 16px; text-align: right; color: #333; font-size: 16px;">Quantity</th>
                            <th style="padding: 16px; text-align: right; color: #333; font-size: 16px;">Total</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="border-bottom: 1px solid #eee;">
                    <td style="padding: 16px; font-size: 16px;"><%# Eval("Name") %></td>
                    <td style="padding: 16px; text-align: right; font-size: 16px;">$<%# Eval("Price", "{0:0.00}") %></td>
                    <td style="padding: 16px; text-align: right; font-size: 16px;">
                        <%# Eval("Quantity") %>
                        <asp:Button ID="btnIncrease" runat="server" Text="+" CommandName="Increase" CommandArgument='<%# Eval("ProductID") %>' style="margin-left:5px;" />
                        <asp:Button ID="btnDecrease" runat="server" Text="-" CommandName="Decrease" CommandArgument='<%# Eval("ProductID") %>' style="margin-left:5px;" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ProductID") %>' style="margin-left:5px; color:red;" />
                    </td>
                    <td style="padding: 16px; text-align: right; font-size: 16px;">$<%# (Convert.ToDecimal(Eval("Price")) * Convert.ToInt32(Eval("Quantity"))).ToString("0.00") %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                    </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        
        <div style="text-align: right; margin-top: 1.5rem;">
            <asp:Label ID="lblGrandTotal" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
            <asp:Button ID="btnCheckout" runat="server" Text="Checkout" OnClick="btnCheckout_Click" style="background: #28a745; color: #fff; padding: 10px 24px; border-radius: 4px; border: none; font-size: 18px; margin-left: 24px; cursor: pointer;" />
        </div>
        <div id="orderSummary" runat="server" visible="false" style="margin-top: 24px; padding: 16px; background: #f8f9fa; border-radius: 8px;">
            <h2 style="color: #222; margin-bottom: 16px;">Order Summary</h2>
            <asp:Repeater ID="SummaryRepeater" runat="server">
                <HeaderTemplate>
                    <table style="width: 100%; border-collapse: collapse;">
                        <thead>
                            <tr style="background: #f5f5f5;">
                                <th style="padding: 16px; text-align: left; color: #333; font-size: 16px;">Product</th>
                                <th style="padding: 16px; text-align: right; color: #333; font-size: 16px;">Price</th>
                                <th style="padding: 16px; text-align: right; color: #333; font-size: 16px;">Quantity</th>
                                <th style="padding: 16px; text-align: right; color: #333; font-size: 16px;">Total</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="border-bottom: 1px solid #eee;">
                        <td style="padding: 16px; font-size: 16px;"><%# Eval("Name") %></td>
                        <td style="padding: 16px; text-align: right; font-size: 16px;">$<%# Eval("Price", "{0:0.00}") %></td>
                        <td style="padding: 16px; text-align: right; font-size: 16px;"><%# Eval("Quantity") %></td>
                        <td style="padding: 16px; text-align: right; font-size: 16px;">$<%# (Convert.ToDecimal(Eval("Price")) * Convert.ToInt32(Eval("Quantity"))).ToString("0.00") %></td>
                    </tr>
                        <span>$<%# Eval("Price", "{0:0.00}") %></span>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div style="border-top: 1px solid #ddd; margin: 16px 0; padding-top: 8px;"></div>
            <div style="display: flex; justify-content: space-between; font-weight: bold; font-size: 18px;">
                <span>Total:</span>
                <asp:Label ID="lblOrderTotal" runat="server" Text="0.00"></asp:Label>
            </div>
            <asp:Button ID="btnConfirmCheckout" runat="server" Text="Confirm & Pay" OnClick="btnConfirmCheckout_Click" 
                style="background: #28a745; color: #fff; padding: 10px 20px; border-radius: 4px; border: none; font-size: 16px; cursor: pointer; margin-top: 16px;" />
            <asp:Button ID="btnBackToCart" runat="server" Text="Back to Cart" OnClick="btnBackToCart_Click" 
                style="background: #6c757d; color: #fff; padding: 10px 20px; border-radius: 4px; border: none; font-size: 16px; cursor: pointer; margin-top: 16px; margin-left: 12px;" />
        </div>
    </div>
</asp:Content>
