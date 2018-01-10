<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MicrosfferToDo.WebSite._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        /*
            Função local para abrir e fechar as atividades realizadas
            A função também muda o texto do link de + para -
        */
        function abrir() {
            
            var doc = document.getElementById("abrirGrid"); //div
            var lnk = document.getElementById("lnkAtividadesRealizadas"); //link href

            if (lnk.innerText == "+") {
                doc.style.display = '';
                lnk.innerText = "-";
            } else {
                doc.style.display = 'none';
                lnk.innerText = "+";
            }

            
        }
    </script>


    <asp:HiddenField ID="hdIdToDo" runat="server" />
    <br />
    <div class="well">
        <h1>Microsffer ToDo</h1> Feito em ASP.NET
    </div>
    <div class="input-group">
        <asp:TextBox runat="server" ValidationGroup="atividade" ID="txtTituloToDo" MaxLength="50" CssClass="form-control" placeholder="Digite a atividade" />
        <asp:RequiredFieldValidator ValidationGroup="atividade" Display="Dynamic" ForeColor="Red" ErrorMessage="Por favor, digite a atividade." ControlToValidate="txtTituloToDo" runat="server" />

        &nbsp;<asp:Button Text="Enviar" ValidationGroup="atividade" CssClass="btn btn-primary btn-block" OnClick="btnEnviar_Click" ID="btnEnviar" runat="server" />
        <asp:Button Text="Editar" ValidationGroup="atividade" Visible="false" CssClass="btn btn-primary btn-block" OnClick="btnEditar_Click" ID="btnEditar" runat="server" />
    </div>
    

    
    <div class="row">
        <div class="col-md-12">
            <p>
                <asp:GridView runat="server" ID="grdToDo" DataKeyNames="IdToDo" AllowPaging="true" 
                        Width="100%" PageSize="50" GridLines="None" AutoGenerateColumns="false" 
                    OnRowDeleting="grdToDo_RowDeleting" OnRowCommand="grdToDo_RowCommand" CssClass="table table-striped">
                    <RowStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    <EmptyDataTemplate>
                        Nenhum valor adicionado.
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Images/checkbox_vazio.png" CommandName="Feito" HeaderText="" Text="" />
                        <asp:BoundField DataField="NomeToDo" HeaderText="Nome da(s) Atividade(s) não Realizada(s)" />
                        <asp:ButtonField ButtonType="Button" CommandName="Editar" ControlStyle-CssClass="btn btn-primary" HeaderText="Editar" Text="Editar" />
                        <asp:CommandField ButtonType="Button" CancelText="Cancelar" ControlStyle-CssClass="btn btn-danger" DeleteText="Deletar" ShowDeleteButton="true" HeaderText="Deletar" />
                        
                    </Columns>
                </asp:GridView>
            </p>
        </div>
    </div>



    <div class="row">
        <div class="col-md-12">
            <p>
                Atividades Realizadas <a href="#" id="lnkAtividadesRealizadas" onclick="javascript:abrir();">-</a>

                <div id="abrirGrid" style="display:'';">
                    <asp:GridView runat="server" ID="grdToDoCompleto" DataKeyNames="IdToDo" AllowPaging="true"
                        Width="100%" PageSize="50" GridLines="None" AutoGenerateColumns="false"
                        OnRowCommand="grdToDoCompleto_RowCommand" CssClass="table table-striped">                        
                        <RowStyle VerticalAlign="Middle" HorizontalAlign="Left" Font-Strikeout="true" />
                        <EmptyDataTemplate>
                            Nenhum valor adicionado.
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Images/checkbox_preenchido.png" CommandName="DesFeito" HeaderText="" Text="" />
                            <asp:BoundField DataField="NomeToDo" HeaderText="Nome da(s) Atividade(s) Realizada(s)" />
                        </Columns>
                    </asp:GridView>
                </div>
            </p>
        </div>
    </div>
    
</asp:Content>
