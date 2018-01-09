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
        /// Métodos de load da página
        /// Load grid view
        /// </summary>
        #region "#### Métodos Load da página ####"

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
                preencherGridCompleto(); //grid com atividades não completas
                preencherGridNaoCompleto(); //grid com a atividades completas
            }
        }

        /// <summary>
        /// Preenche o grid com as atividades completas
        /// </summary>
        private void preencherGridCompleto()
        {
            HttpResponseMessage response = WebApiController.ConsultaWebApiByStatus(1);

            if (response.IsSuccessStatusCode)
            {
                var _contas = response.Content.ReadAsAsync<IEnumerable<AtividadesToDo>>().Result;

                grdToDoCompleto.DataSource = _contas;
                grdToDoCompleto.DataBind();
            }
        }

        
        /// <summary>
        /// Preenche o grid com as atividades não completas
        /// </summary>
        private void preencherGridNaoCompleto()
        {
            HttpResponseMessage response = WebApiController.ConsultaWebApiByStatus(0);

            if (response.IsSuccessStatusCode)
            {
                var _contas = response.Content.ReadAsAsync<IEnumerable<AtividadesToDo>>().Result;

                grdToDo.DataSource = _contas;
                grdToDo.DataBind();
            }
        }

        #endregion

        /// <summary>
        /// Métodos específicos dos botões (click)
        /// </summary>
        #region "#### Métodos específicos para os botões ####"

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
                var _atividades = new AtividadesToDo()
                {
                    NomeTodo = HTMLToText.StripHTML(txtTituloToDo.Text, true),
                    CompletoTodo = 0,
                    IdTodo = int.Parse(hdIdToDo.Value)
                };

                //atualiza os dados na web api
                HttpResponseMessage response = WebApiController.AtualizarDadosWebApi(hdIdToDo.Value.ToString(), _atividades);

                if (response.IsSuccessStatusCode)
                    Response.Redirect("Default?guid=" + Guid.NewGuid() + "&id=AtualizadoSucesso");
                else
                    Response.Write(response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString());
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
                var _atividades = new AtividadesToDo()
                {
                    NomeTodo = txtTituloToDo.Text,
                    CompletoTodo = 0
                };

                //inserindo dados na Web Api
                HttpResponseMessage response = WebApiController.InserirDadosWebApi(_atividades);

                if (response.IsSuccessStatusCode)
                {
                    Response.Redirect("Default?guid=" + Guid.NewGuid() + "&id=sucesso");
                }
                else
                    Response.Write(response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString());
            }
        }
        #endregion


        /// <summary>
        /// Métodos específicos do GridView
        /// </summary>
        #region "#### Grid View ####"
        /// <summary>
        /// Deletando dados na Web Api
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GridViewDeleteEventArgs</param>
        protected void grdToDo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //pegando o valor selecionado no grid
            string _IdToDo = this.grdToDo.DataKeys[e.RowIndex]["IdToDo"].ToString();

            //deleta os dados pela Web API
            HttpResponseMessage response = WebApiController.DeletarDadosWebApi(_IdToDo);

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
                string chave = grdToDo.DataKeys[index]["IdToDo"].ToString();

                //preenche os dados
                var _atividades = new AtividadesToDo()
                {
                    NomeTodo = HTMLToText.StripHTML(grdToDo.Rows[index].Cells[1].Text, true),
                    CompletoTodo = 1,
                    IdTodo = int.Parse(chave)
                };

                //atualiza os dados preenchidos da classe Atividades no Web Api
                HttpResponseMessage response = WebApiController.AtualizarDadosWebApi(chave, _atividades);

                if (response.IsSuccessStatusCode)
                    Response.Redirect("Default?guid=" + Guid.NewGuid() + "&id=AtualizadoSucesso");
                else
                    Response.Write(response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString());


            }
            //click no botão editar dentro do grid
            else if (e.CommandName == "Editar")
            {
                int index = int.Parse((string)e.CommandArgument);
                string chave = grdToDo.DataKeys[index]["IdToDo"].ToString();
                
                hdIdToDo.Value = chave.ToString();
                txtTituloToDo.Text = HTMLToText.StripHTML(grdToDo.Rows[index].Cells[1].Text, true);

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
                string chave = grdToDoCompleto.DataKeys[index]["IdToDo"].ToString();

                //preenche os dados
                var _atividades = new AtividadesToDo()
                {
                    NomeTodo = HTMLToText.StripHTML(grdToDoCompleto.Rows[index].Cells[1].Text, true),
                    CompletoTodo = 0,
                    IdTodo = int.Parse(chave)
                };

                //atualiza os dados no web api
                HttpResponseMessage response = WebApiController.AtualizarDadosWebApi(chave, _atividades);

                if (response.IsSuccessStatusCode)
                    Response.Redirect("Default?guid=" + Guid.NewGuid() + "&id=AtualizadoSucesso");
                else
                    Response.Write(response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString());

            }
        }

        #endregion


    }
}