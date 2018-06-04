using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RefrigeratingChamberWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.ServiceFabric.Actors;
using ClimateActor.Interfaces;
using Microsoft.ServiceFabric.Actors.Client;
using Domain;

namespace RefrigeratingChamberWeb.Controllers
{
    public class DeviceController : Controller
    {
        static string URL = "https://jsonplaceholder.typicode.com/posts/";

        // GET: Device
        public ActionResult Index()
        {
            //recupera dados JSON
            WebClient client = new WebClient();
            string jsonData = client.DownloadString(URL + "1");
            Sample device = JsonConvert.DeserializeObject<Sample>(jsonData);

            return View(device);
        }

        // GET: Device/Details/5
        public ActionResult Details(int id)
        {
            //recupera dados JSON
            WebClient client = new WebClient();
            string jsonData = client.DownloadString(URL + id);
            Sample device = JsonConvert.DeserializeObject<Sample>(jsonData);

            //Salvar no Azure
            IClimateActor actor = ActorProxy.Create<IClimateActor>(new ActorId(1), new System.Uri("fabric:/ClimateStationApp/ClimaActorService"));
            actor.ActivateMe();
            SampleEntity dataEntity = new SampleEntity(device.userId, device.id, device.title, device.body);
            actor.UploadDeviceData(dataEntity);

            return View(device);
        }

        // GET: Device/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Device/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Device/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Device/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Device/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Device/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}