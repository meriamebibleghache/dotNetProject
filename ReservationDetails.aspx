<%@ Page Title="" Language="C#" MasterPageFile="~/UserScreen/User.Master" AutoEventWireup="true" CodeBehind="ReservationDetails.aspx.cs" Inherits="ApplicationBibliothequeProjet.UserScreen.ReservationDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<div>
        <form id="form1" runat="server">
            <asp:GridView ID="GridViewReservations" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewReservations_RowCommand">
             
                <Columns>
                    <asp:BoundField DataField="BookId" HeaderText="Book ID" SortExpression="BookId" />
                    <asp:BoundField DataField="ReservationDate" HeaderText="Reservation Date" SortExpression="ReservationDate" />
                    <asp:TemplateField HeaderText="Cancel Reservation">
                        <ItemTemplate>
                            <asp:Button runat="server" Text="Cancel" CommandName="CancelReservation" CommandArgument='<%# Eval("BookId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

         
            <asp:Button runat="server" Text="Another Button" OnClick="AnotherButton_Click" />

        </form>
    </div>
</asp:Content>