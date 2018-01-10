using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MicrosfferToDo.WebSite.wpf
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string _path = "https://microsffertodoaspnet.azurewebsites.net/wpf/MicrosfferToDo.WPF.zip";
            WebClient _client = new WebClient();
            Byte[] _buffer = _client.DownloadData(_path);
            Response.ContentType = "application/zip";
            Response.AddHeader("content-length", _buffer.Length.ToString());
            Response.BinaryWrite(_buffer);
        }
    }
}