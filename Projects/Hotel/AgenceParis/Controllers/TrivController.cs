using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PalaceHotel.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgenceParis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrivController : Controller
    {

        [HttpPost("trivago")]
        public List<ReponseTrivago> ChambreTrivago(RequestTrivago request)
        {
            Console.WriteLine("deta " + request.ville);
            List<ReponseTrivago> reponsesIbiza = new List<ReponseTrivago>();
            List<ReponseTrivago> reponsesPalace = new List<ReponseTrivago>();
            List<ReponseTrivago> all = new List<ReponseTrivago>();

            using (var client = new HttpClient())
            {


                client.BaseAddress = new Uri("https://localhost:46767/api/Home/trivago");
                var postJob = client.PostAsJsonAsync<RequestTrivago>("trivago", request);
                postJob.Wait();
                var reponse = postJob.Result;

                if (reponse.IsSuccessStatusCode)
                {


                    Console.WriteLine("deta era");
                    var readTask = reponse.Content.ReadFromJsonAsync<IList<ReponseTrivago>>();
                    readTask.Wait();

                    reponsesPalace = (List<ReponseTrivago>)readTask.Result;
                    foreach (ReponseTrivago reponseTrivago in readTask.Result)
                    {

                        reponseTrivago.prix = reponseTrivago.prix + 3;
                        reponseTrivago.nomDeAgence = "Agence de Paris";
                    }


                }
            }
            using (var clientIbiza = new HttpClient())
            {


                clientIbiza.BaseAddress = new Uri("https://localhost:31627/api/Home/trivago");
                var postJob = clientIbiza.PostAsJsonAsync<RequestTrivago>("trivago", request);
                postJob.Wait();
                var reponse = postJob.Result;

                if (reponse.IsSuccessStatusCode)
                {


                    Console.WriteLine("deta era");
                    var readTask = reponse.Content.ReadFromJsonAsync<IList<ReponseTrivago>>();
                    readTask.Wait();

                    reponsesIbiza = (List<ReponseTrivago>)readTask.Result;
                    foreach (ReponseTrivago reponseTrivago in reponsesIbiza)
                    {
                        Console.WriteLine(reponseTrivago.nom);
                        reponseTrivago.prix = reponseTrivago.prix + 3;
                        reponseTrivago.nomDeAgence = "Agence de Paris";
                    }


                }

                else { Console.WriteLine("dew"); }


            }




            if (reponsesPalace.Any())
            {
                all.AddRange(reponsesPalace);
            }
            else
            {
                Console.WriteLine("hich");
            }
            if (reponsesIbiza.Any())
                all.AddRange(reponsesIbiza);
            else
                Console.WriteLine("hich");


            return all;
        }
    }
}
