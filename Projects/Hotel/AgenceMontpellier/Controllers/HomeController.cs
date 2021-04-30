using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AgenceMontpellier.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Hotel;
using Hotel.Models;

namespace AgenceParis.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(RequestViewModel request)
        {
            List<Reponse> reponsesIbiza = new List<Reponse>();
            List<Reponse> reponsesPalace = new List<Reponse>();
            List<Reponse> all = new List<Reponse>();
            request.id = "1034A";
            using (var client = new HttpClient())
            {


                client.BaseAddress = new Uri("https://localhost:46767/api/Home/");
                var postJob = client.PostAsJsonAsync<RequestViewModel>("getChambres", request);
                postJob.Wait();
                var reponse = postJob.Result;

                if (reponse.IsSuccessStatusCode)
                {


                    Console.WriteLine("deta era");
                    var readTask = reponse.Content.ReadFromJsonAsync<IList<Reponse>>();
                    readTask.Wait();

                    reponsesPalace = (List<Reponse>)readTask.Result;
                    foreach (Reponse monPrix in reponsesPalace)
                    {
                        monPrix.prix = monPrix.prix + 6;
                    }


                }

                else { Console.WriteLine("dew"); }


            }
            using (var clientIbiza = new HttpClient())
            {


                clientIbiza.BaseAddress = new Uri("https://localhost:31627/api/Home/getChambres");
                var postJob = clientIbiza.PostAsJsonAsync<RequestViewModel>("getChambres", request);
                postJob.Wait();
                var reponse = postJob.Result;

                if (reponse.IsSuccessStatusCode)
                {


                    Console.WriteLine("deta era");
                    var readTask = reponse.Content.ReadFromJsonAsync<IList<Reponse>>();
                    readTask.Wait();

                    reponsesIbiza = (List<Reponse>)readTask.Result;
                    foreach (Reponse monPrix in reponsesIbiza)
                    {
                        monPrix.prix = monPrix.prix + 6;
                    }


                }

                else { Console.WriteLine("dew"); }


            }
            if (reponsesPalace.Any())
                all.AddRange(reponsesPalace);
            if (reponsesPalace.Any())
                all.AddRange(reponsesIbiza);
            ViewBag.reponses = all;
            return View("Chambres");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Reserver(ReservationRequest request)
        {



            using (var client = new HttpClient())
            {
                request.agenceId = "1034A";
                request.password = "1234";
                client.BaseAddress = new Uri("https://localhost:46767/api/Home/");
                var postJob = client.PostAsJsonAsync<ReservationRequest>("reserver", request);
                postJob.Wait();
                var reponse = postJob.Result;

                if (reponse.IsSuccessStatusCode)
                {


                    Console.WriteLine("deta era");
                    var readTask = reponse.Content.ReadFromJsonAsync<ReservationReponse>();
                    readTask.Wait();

                    ReservationReponse reponses = readTask.Result;
                    ViewBag.reservation = reponses;
                    return View("Result");
                }

                else { Console.WriteLine("dew"); }


            }
            using (var clientIbiza = new HttpClient())
            {
                request.agenceId = "1034A";
                request.password = "113355";
                clientIbiza.BaseAddress = new Uri("https://localhost:31627/api/Home/");
                var postJob = clientIbiza.PostAsJsonAsync<ReservationRequest>("reserver", request);
                postJob.Wait();
                var reponse = postJob.Result;

                if (reponse.IsSuccessStatusCode)
                {


                    Console.WriteLine("deta era");
                    var readTask = reponse.Content.ReadFromJsonAsync<ReservationReponse>();
                    readTask.Wait();

                    ReservationReponse reponses = readTask.Result;
                    ViewBag.reservation = reponses;
                    return View("Result");
                }

                else { Console.WriteLine("dew"); }


            }

            ModelState.AddModelError(string.Empty, "il y a eu une errur de capter le api");
            return View(request);
        }
    }
}



