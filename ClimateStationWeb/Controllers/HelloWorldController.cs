using ClimateActor.Interfaces;
using RefrigeratingChamberWeb.Models;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Newtonsoft.Json;
using System.Net;
using System.Text.Encodings.Web;

namespace RefrigeratingChamberWeb.Controllers
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

            //Salvar no Azure
            IClimateActor actor = ActorProxy.Create<IClimateActor>(new ActorId(1), new System.Uri("fabric:/ClimateStationApp/ClimaActorService"));
            DataEntity dataEntity = new DataEntity(dataAtual.data, dataAtual.time, dataAtual.miliseconds);
            actor.UploadClimateData(dataEntity);

            return "Upload completado com suceso. <br>Data e hora atual: " + dataAtual.data + ", "+ dataAtual.time;
        }

        // 
        // GET: /HelloWorld/Welcome/ 

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}