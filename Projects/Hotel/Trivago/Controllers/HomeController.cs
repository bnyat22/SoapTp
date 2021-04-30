using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Trivago.Models;

namespace Trivago.Controllers
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
        public IActionResult Index(RequestTrivago request)
        {
            List<ReponseTrivago> reponsesAgenceParis = new List<ReponseTrivago>();
            List<ReponseTrivago> reponsesAgenceMontpellier = new List<ReponseTrivago>();
            List<ReponseTrivago> all = new List<ReponseTrivago>();
           
            using (var client = new HttpClient())
            {


                client.BaseAddress = new Uri("https://localhost:55206/api/Triv/");
                var postJob = client.PostAsJsonAsync<RequestTrivago>("trivago", request);
                postJob.Wait();
                var reponse = postJob.Result;

                if (reponse.IsSuccessStatusCode)
                {


                    Console.WriteLine("deta era");
                    var readTask = reponse.Content.ReadFromJsonAsync<IList<ReponseTrivago>>();
                    readTask.Wait();

                    reponsesAgenceParis = (List<ReponseTrivago>)readTask.Result;
                   


                }

                else { Console.WriteLine("dew"); }


            }
            using (var clientIbiza = new HttpClient())
            {


                clientIbiza.BaseAddress = new Uri("https://localhost:48820/api/Triv/");
                var postJob = clientIbiza.PostAsJsonAsync<RequestTrivago>("trivago", request);
                postJob.Wait();
                var reponse = postJob.Result;

                if (reponse.IsSuccessStatusCode)
                {


                    Console.WriteLine("deta era");
                    var readTask = reponse.Content.ReadFromJsonAsync<IList<ReponseTrivago>>();
                    readTask.Wait();

                    reponsesAgenceMontpellier = (List<ReponseTrivago>)readTask.Result;
                   


                }

                else { Console.WriteLine("dew"); }


            }
            if (reponsesAgenceParis.Any())
                all.AddRange(reponsesAgenceParis);
            if (reponsesAgenceMontpellier.Any())
            {
                all.AddRange(reponsesAgenceMontpellier);
                Console.WriteLine("deta era Mont");
            }
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
    }
}
