using AppProdutoCategoria.AppCliente.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Net;
using System.Text;


namespace AppProdutoCategoria.AppCliente.Util
{
    public class WebAPI
    {
        private static string _URL = "http://ugssrv04.southcentralus.cloudapp.azure.com/AppProdutoCategoriaWebAPI/v1/";

        //public WebAPI()
        //{
        //    _URL = _configuration.GetSection("AppSettings").GetSection("URLWebAPI").ToString() ;
        //}


        public static string RequestGET(string Entidade, string metodo, string parametro)
        {
            return RequesteGET_DELETE(Entidade, metodo, parametro, "GET");
        }

        public static string RequestDELETE(string Entidade, string metodo, string parametro)
        {
            return RequesteGET_DELETE(Entidade, metodo, parametro, "DELETE");
        }

        private static string RequesteGET_DELETE(string Entidade, string metodo, string parametro, string tipo)
        {
            if (metodo != string.Empty)
            {
                metodo = metodo + "/";
            }
            var request = (HttpWebRequest)WebRequest.Create(_URL + Entidade + "/" + metodo + parametro);
            request.Method = tipo;
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }


        public static string RequestPOST(string Entidade, string metodo, string jsonData)
        {
            var request = (HttpWebRequest)WebRequest.Create(_URL + Entidade + "/" + metodo);
            var data = Encoding.ASCII.GetBytes(jsonData);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return responseString;
        }

        public static string RequestPUT(string Entidade, string metodo, string jsonData)
        {
            var request = (HttpWebRequest)WebRequest.Create(_URL + Entidade + "/" + metodo);
            var data = Encoding.ASCII.GetBytes(jsonData);
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return responseString;
        }

    }
}
