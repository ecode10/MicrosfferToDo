<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MicrosfferToDo.WebSite.wpf.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="well">
        <h1>Microsffer ToDo</h1> Feito em WPF
    </div>
    <div class="input-group">
        Faça download do arquivo .zip para execução no computador local
        <asp:Button Text="Download" CssClass="btn btn-primary" OnClick="btnDownload_Click" ID="btnDownload" runat="server" />
    </div>


</asp:Content>
