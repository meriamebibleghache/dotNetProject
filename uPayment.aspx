<%@ Page Language="C#" MasterPageFile="~/UserScreen/User.Master" AutoEventWireup="true" CodeBehind="uPayment.aspx.cs" Inherits="ApplicationBibliothequeProjet.UserScreen.uPayment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../datatable/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h2>Fine Payment Details</h2>
                <hr />
                <asp:GridView class="table table-striped table-bordered" ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound">
                     <Columns>
        <asp:BoundField DataField="book_id" HeaderText="Book ID" SortExpression="book_id" />
        <asp:BoundField DataField="book_name" HeaderText="Book Name" SortExpression="book_name" />
        <asp:BoundField DataField="author_name" HeaderText="Author" SortExpression="author_name" />
        <asp:BoundField DataField="book_cost" HeaderText="Book Cost" SortExpression="book_cost" />
    </Columns>
                </asp:GridView>

              
                <div>
<h3>Total Price: $<asp:Label ID="lblTotalPrice" runat="server"></asp:Label></h3>
                    <asp:TextBox ID="txtPromoCode" runat="server" placeholder="Enter Promo Code"></asp:TextBox>
                    <asp:Button ID="btnApplyPromo" runat="server" Text="Apply Promo" OnClick="btnApplyPromo_Click" />
                    <asp:Button ID="btnPay" runat="server" Text="Pay" OnClick="btnPay_Click" />
                    <asp:Label ID="lblPromoMessage" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
