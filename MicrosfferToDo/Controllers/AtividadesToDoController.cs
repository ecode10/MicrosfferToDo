using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MicrosfferToDo.DAL;
using MicrosfferToDo.Models;
using System.Text;
using System.Data.SqlClient;

namespace MicrosfferToDo.Controllers
{
    /// <summary>
    /// Classe responsável pela comunicação com a tabela de Atividades ToDo do projeto
    /// <author>Mauricio Junior</author>
    /// </summary>
    public class AtividadesToDoController : ApiController
    {
        private DBContextToDo db = new DBContextToDo();

        /// <summary>
        /// Método que verifica a autorização do token
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        private IHttpActionResult checkAutenticacao()
        {
            AuthHeader _auth = new AuthHeader();
            if (_auth.autorizarToken(Request.Headers) == HttpStatusCode.Unauthorized)
            {
                throw new Exception("Error: Your are not unauthorized - Code: " + HttpStatusCode.Unauthorized);
            }
            else
                return null;
        }

        /// <summary>
        /// Método que busca todas as atividades do ToDo
        /// </summary>
        /// <returns>IQueryable</returns>
        // GET: api/AtividadesToDo
        public IQueryable<AtividadesToDo> GetAtividadesToDo()
        {
            //método que verifica a autorizacao do sistema
            checkAutenticacao();

            return db.AtividadesToDo;
        }

        /// <summary>
        /// Método que busca as atividades pelo Id
        /// </summary>
        /// <param name="id">long</param>
        /// <returns>IHttpActionResult</returns>
        // GET: api/AtividadesToDo/5
        [ResponseType(typeof(AtividadesToDo))]
        public IHttpActionResult GetAtividadesToDo(long id)
        {
            //método que verifica a autorizacao do sistema
            checkAutenticacao();

            AtividadesToDo atividadesToDo = db.AtividadesToDo.Find(id);
            if (atividadesToDo == null)
            {
                return NotFound();
            }

            return Ok(atividadesToDo);
        }


        /// <summary>
        /// Método que atualiza as atividades do ToDo
        /// </summary>
        /// <param name="id">long</param>
        /// <param name="atividadesToDo"></param>
        /// <returns>AtividadesToDo</returns>
        // PUT: api/AtividadesToDo/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAtividadesToDo(long id, AtividadesToDo atividadesToDo)
        {
            //método que verifica a autorizacao do sistema
            checkAutenticacao();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != atividadesToDo.IdTodo)
            {
                return BadRequest();
            }

            db.Entry(atividadesToDo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AtividadesToDoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Método que insere as atividades do ToDo
        /// </summary>
        /// <param name="atividadesToDo"></param>
        /// <returns>AtividadesToDo</returns>
        // POST: api/AtividadesToDo
        [ResponseType(typeof(AtividadesToDo))]
        public IHttpActionResult PostAtividadesToDo(AtividadesToDo atividadesToDo)
        {
            //método que verifica a autorizacao do sistema
            checkAutenticacao();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AtividadesToDo.Add(atividadesToDo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = atividadesToDo.IdTodo }, atividadesToDo);
        }

        /// <summary>
        /// Método que deleta as atividades do ToDo
        /// </summary>
        /// <param name="id">long</param>
        /// <returns>IHttpActionResult</returns>
        // DELETE: api/AtividadesToDo/5
        [ResponseType(typeof(AtividadesToDo))]
        public IHttpActionResult DeleteAtividadesToDo(long id)
        {
            //método que verifica a autorizacao do sistema
            checkAutenticacao();

            AtividadesToDo atividadesToDo = db.AtividadesToDo.Find(id);
            if (atividadesToDo == null)
            {
                return NotFound();
            }

            db.AtividadesToDo.Remove(atividadesToDo);
            db.SaveChanges();

            return Ok(atividadesToDo);
        }


        /// <summary>
        /// Método que busca as atividades pelo status
        /// Completo: 1
        /// Não completo: 0
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>IEnumerable</returns>
        [Route("api/AtividadesToDo/Status/{id}")]
        public IEnumerable<AtividadesToDo> GetAtividadesByStatus(string id)
        {
            //método que verifica a autorizacao do sistema
            checkAutenticacao();

            StringBuilder str = new StringBuilder();
            str.Append(@"SELECT
                            IdToDo, NomeToDo, DescricaoToDo, DataToDo, HoraToDo, CompletoToDo
                         FROM
                            AtividadesToDo
                        WHERE CompletoToDo = @id ");

            IDataParameter _completoParameter = new SqlParameter();
            _completoParameter.DbType = DbType.Int32;
            _completoParameter.ParameterName = "@id";
            _completoParameter.Value = int.Parse(id);
            _completoParameter.SourceColumn = "CompletoToDo";

            var resultado = db.Database.SqlQuery<AtividadesToDo>(str.ToString(),
                _completoParameter).AsEnumerable();

            if (resultado == null)
                return null;

            return resultado;
        }

        /// <summary>
        /// Método que faz o dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AtividadesToDoExists(long id)
        {
            return db.AtividadesToDo.Count(e => e.IdTodo == id) > 0;
        }
    }
}