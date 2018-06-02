using ClimateStationWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text.Encodings.Web;

namespace ClimateStationWeb.Controllers
{
    public class HelloWorldController : Controller
    {
        static string URL = "http://date.jsontest.com/";
        // 
        // GET: /HelloWorld/

        public string Index()
        {
            WebClient client = new WebClient();
            string jsonData = client.DownloadString(URL);
            Data dataAtual = JsonConvert.DeserializeObject<Data>(jsonData);
            return "Data e hora atual: " + dataAtual.data + dataAtual.time;
        }

        // 
        // GET: /HelloWorld/Welcome/ 

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}