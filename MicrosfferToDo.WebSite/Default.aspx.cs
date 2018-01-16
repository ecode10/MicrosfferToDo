using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using MicrosfferToDo.Library.Util;
using MicrosfferToDo.WebSite.Models;
using System.Net.Http;
using MicrosfferToDo.WebSite.Controllers;

namespace MicrosfferToDo.WebSite
{
    /// <summary>
    /// Classe principal Web Site
    /// <author>Mauricio Junior</author>
    /// </summary>
    public partial class _Default : Page
    {
       
        /// <summary>
        /// Método load da página
        /// Carrega o grid com as atividades completas e não completas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PreencherGridCompleto(); //grid com atividades não completas
                PreencherGridNaoCompleto(); //grid com a atividades completas
            }
        }

        /// <summary>
        /// Preenche o grid com as atividades completas
        /// </summary>
        private void PreencherGridCompleto()
        {
            HttpResponseMessage response = WebApiController.ConsultaWebApiByStatus(1);

            if (response.IsSuccessStatusCode)
            {
                var contas = response.Content.ReadAsAsync<IEnumerable<AtividadesToDo>>().Result;

                grdToDoCompleto.DataSource = contas;
                grdToDoCompleto.DataBind();
            }
        }

        
        /// <summary>
        /// Preenche o grid com as atividades não completas
        /// </summary>
        private void PreencherGridNaoCompleto()
        {
            HttpResponseMessage response = WebApiController.ConsultaWebApiByStatus(0);

            if (response.IsSuccessStatusCode)
            {
                var contas = response.Content.ReadAsAsync<IEnumerable<AtividadesToDo>>().Result;

                grdToDo.DataSource = contas;
                grdToDo.DataBind();
            }
        }

        
        /// <summary>
        /// Clicando no botão editar do formulário para atualizar o nome da atividade
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //preenche os dados
                var atividades = new AtividadesToDo()
                {
                    NomeTodo = HtmlToText.StripHtml(txtTituloToDo.Text),
                    CompletoTodo = 0,
                    IdTodo = int.Parse(hdIdToDo.Value)
                };

                //atualiza os dados na web api
                HttpResponseMessage response = WebApiController.AtualizarDadosWebApi(hdIdToDo.Value, atividades);

                if (response.IsSuccessStatusCode)
                    Response.Redirect("Default?guid=" + Guid.NewGuid() + "&id=AtualizadoSucesso");
                else
                    Response.Write(response.StatusCode + " - " + response.ReasonPhrase);
            }

        }

        /// <summary>
        /// Inserindo os dados no Web Api via POST
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //preenche os dados
                var atividades = new AtividadesToDo()
                {
                    NomeTodo = txtTituloToDo.Text,
                    CompletoTodo = 0
                };

                //inserindo dados na Web Api
                HttpResponseMessage response = WebApiController.InserirDadosWebApi(atividades);

                if (response.IsSuccessStatusCode)
                {
                    Response.Redirect("Default?guid=" + Guid.NewGuid() + "&id=sucesso");
                }
                else
                    Response.Write($"{response.StatusCode} - {response.ReasonPhrase}");
            }
        }
       
        
        /// <summary>
        /// Deletando dados na Web Api
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GridViewDeleteEventArgs</param>
        protected void grdToDo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //pegando o valor selecionado no grid
            string idToDo = grdToDo.DataKeys[e.RowIndex]?["IdToDo"].ToString();

            //deleta os dados pela Web API
            HttpResponseMessage response = WebApiController.DeletarDadosWebApi(idToDo);

            if (response.IsSuccessStatusCode)
            {
                Response.Redirect("Default?guid=" + Guid.NewGuid() + "&id=deleteSucesso");
            }
        }


        /// <summary>
        /// Atualiza os dados pel Web Api
        /// Ou Joga os dados para a tela
        /// Apenas o status que altera
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GridViewCommandEventArgs</param>
        protected void grdToDo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //clicar no botão feito (já foi realizado essa atividade)
            if (e.CommandName == "Feito")
            {
                int index = int.Parse((string)e.CommandArgument);
                string chave = grdToDo.DataKeys[index]?["IdToDo"].ToString();

                //preenche os dados
                var atividades = new AtividadesToDo()
                {
                    NomeTodo = HtmlToText.StripHtml(grdToDo.Rows[index].Cells[1].Text),
                    CompletoTodo = 1,
                    IdTodo = int.Parse(chave ?? throw new Exception())
                };

                //atualiza os dados preenchidos da classe Atividades no Web Api
                HttpResponseMessage response = WebApiController.AtualizarDadosWebApi(chave, atividades);

                if (!response.IsSuccessStatusCode)
                    Response.Write(response.StatusCode + " - " + response.ReasonPhrase);
                else
                    Response.Redirect("Default?guid=" + Guid.NewGuid() + "&id=AtualizadoSucesso");
            }
            //click no botão editar dentro do grid
            else if (e.CommandName == "Editar")
            {
                int index = int.Parse((string)e.CommandArgument);
                string chave = grdToDo.DataKeys[index]?["IdToDo"].ToString();

                if (chave != null)
                {
                    hdIdToDo.Value = chave;
                }

                txtTituloToDo.Text = HtmlToText.StripHtml(grdToDo.Rows[index].Cells[1].Text);

                btnEditar.Visible = true;
                btnEnviar.Visible = false;
            }
        }

        /// <summary>
        /// Segunda grid com as atividades já realizadas
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GridViewCommandEventArgs</param>
        protected void grdToDoCompleto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DesFeito")
            {
                int index = int.Parse((string)e.CommandArgument);
                string chave = grdToDoCompleto.DataKeys[index]?["IdToDo"].ToString();

                //preenche os dados
                var atividades = new AtividadesToDo()
                {
                    NomeTodo = HtmlToText.StripHtml(grdToDoCompleto.Rows[index].Cells[1].Text),
                    CompletoTodo = 0,
                    IdTodo = int.Parse(chave ?? throw new InvalidOperationException())
                };

                //atualiza os dados no web api
                HttpResponseMessage response = WebApiController.AtualizarDadosWebApi(chave, atividades);

                if (response.IsSuccessStatusCode)
                    Response.Redirect("Default?guid=" + Guid.NewGuid() + "&id=AtualizadoSucesso");
                else
                    Response.Write(response.StatusCode + " - " + response.ReasonPhrase);

            }
        }
        
    }
}