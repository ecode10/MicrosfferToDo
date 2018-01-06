using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MicrosfferToDo.Library.Commum;
using MicrosfferToDo.Library.Util;
using MicrosfferToDo.WebSite.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace MicrosfferToDo.WebSite
{
    public partial class _Default : Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                preencherGridCompleto();
                preencherGridNaoCompleto();
            }
        }

        private void preencherGridCompleto()
        {
            var client = HttpClientRequest.getClient();
            
            HttpResponseMessage response = client.GetAsync(EnderecosWebAPI._getByStatus + 1).Result;
            Uri envioUri = response.Headers.Location;

            if (response.IsSuccessStatusCode)
            {
                var _contas = response.Content.ReadAsAsync<IEnumerable<AtividadesToDo>>().Result;

                grdToDoCompleto.DataSource = _contas;
                grdToDoCompleto.DataBind();
            }
        }

        private void preencherGridNaoCompleto()
        {
            var client = HttpClientRequest.getClient();

            HttpResponseMessage response = client.GetAsync(EnderecosWebAPI._getByStatus + 0).Result;
            Uri envioUri = response.Headers.Location;

            if (response.IsSuccessStatusCode)
            {
                var _contas = response.Content.ReadAsAsync<IEnumerable<AtividadesToDo>>().Result;

                grdToDo.DataSource = _contas;
                grdToDo.DataBind();
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var client = HttpClientRequest.getClient();

                //preenche os dados
                var _atividades = new AtividadesToDo()
                {
                    NomeTodo = txtTituloToDo.Text,
                    CompletoTodo = 0
                };

                HttpResponseMessage response = client.PostAsJsonAsync(EnderecosWebAPI._post, _atividades).Result;//client.GetAsync(EnderecosWebAPI._enderecoBase + EnderecosWebAPI._post).Result;
                Uri envioUri = response.Headers.Location;

                if (response.IsSuccessStatusCode)
                {
                    Response.Redirect("Default?guid=" + Guid.NewGuid() + "&id=sucesso");
                }
                else
                    Response.Write(response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString());
            }
        }

        protected void grdToDo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //pegando o valor selecionado no grid
            string _IdToDo = this.grdToDo.DataKeys[e.RowIndex]["IdToDo"].ToString();

            var client = HttpClientRequest.getClient();

            HttpResponseMessage response = client.DeleteAsync(EnderecosWebAPI._delete + _IdToDo).Result;
            Uri envioUri = response.Headers.Location;

            if (response.IsSuccessStatusCode)
            {
                Response.Redirect("Default?guid=" + Guid.NewGuid() + "&id=deleteSucesso");
            }
        }

        protected void grdToDo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Feito")
            {
                int index = int.Parse((string)e.CommandArgument);
                string chave = grdToDo.DataKeys[index]["IdToDo"].ToString();

                var client = HttpClientRequest.getClient();

                //preenche os dados
                var _atividades = new AtividadesToDo()
                {
                    NomeTodo = HTMLToText.StripHTML(grdToDo.Rows[index].Cells[1].Text, true),
                    CompletoTodo = 1,
                    IdTodo = int.Parse(chave)
                };

                HttpResponseMessage response = client.PutAsJsonAsync(EnderecosWebAPI._put + chave, _atividades).Result;
                Uri envioUri = response.Headers.Location;

                if (response.IsSuccessStatusCode)
                    Response.Redirect("Default?guid=" + Guid.NewGuid() + "&id=AtualizadoSucesso");
                else
                    Response.Write(response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString());
                

            }else if (e.CommandName == "Editar")
            {
                int index = int.Parse((string)e.CommandArgument);
                string chave = grdToDo.DataKeys[index]["IdToDo"].ToString();
                
                hdIdToDo.Value = chave.ToString();
                txtTituloToDo.Text = HTMLToText.StripHTML(grdToDo.Rows[index].Cells[1].Text, true);

                btnEditar.Visible = true;
                btnEnviar.Visible = false;
            }
        }

        protected void grdToDoCompleto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DesFeito")
            {
                int index = int.Parse((string)e.CommandArgument);
                string chave = grdToDoCompleto.DataKeys[index]["IdToDo"].ToString();

                var client = HttpClientRequest.getClient();

                //preenche os dados
                var _atividades = new AtividadesToDo()
                {
                    NomeTodo = HTMLToText.StripHTML(grdToDoCompleto.Rows[index].Cells[1].Text, true),
                    CompletoTodo = 0,
                    IdTodo = int.Parse(chave)
                };

                HttpResponseMessage response = client.PutAsJsonAsync(EnderecosWebAPI._put + chave, _atividades).Result;
                Uri envioUri = response.Headers.Location;

                if (response.IsSuccessStatusCode)
                    Response.Redirect("Default?guid=" + Guid.NewGuid() + "&id=AtualizadoSucesso");
                else
                    Response.Write(response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString());

            }
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var client = HttpClientRequest.getClient();

                //preenche os dados
                var _atividades = new AtividadesToDo()
                {
                    NomeTodo = HTMLToText.StripHTML(txtTituloToDo.Text, true),
                    CompletoTodo = 0,
                    IdTodo = int.Parse(hdIdToDo.Value)
                };

                HttpResponseMessage response = client.PutAsJsonAsync(EnderecosWebAPI._put + hdIdToDo.Value.ToString(), _atividades).Result;
                Uri envioUri = response.Headers.Location;

                if (response.IsSuccessStatusCode)
                    Response.Redirect("Default?guid=" + Guid.NewGuid() + "&id=AtualizadoSucesso");
                else
                    Response.Write(response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString());
            }

        }
    }
}