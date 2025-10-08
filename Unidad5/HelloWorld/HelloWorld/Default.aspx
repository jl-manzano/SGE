<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HelloWorld._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">ASP.NET</h1>
            <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
            <asp:Label ID="LabelNombre" runat="server" Text="Introduce tu nombre:" />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Pulsa" OnClick="Button1_Click" />
            <asp:Label ID="LabelError" runat="server" ForeColor="Red" Text="" />
            <asp:Label ID="Label1" runat="server" Text="Hola"></asp:Label>
            <asp:Label ID="Label2" runat="server" Text="Hola"></asp:Label>

        </section>
    </main>

</asp:Content>
