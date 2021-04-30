using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Models;
using Microsoft.AspNetCore.Mvc;
using PalaceHotel;
using PalaceHotel.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {

      public static  HotelRepository hotelRepository = new HotelRepository();

        [HttpPost("getChambres")]
        public List<Reponse> GetChambres(Request request)
        {
            Console.WriteLine("hat");
            return hotelRepository.FindChambres(request);
        }


        [HttpPost("reserver")]
        public ReservationReponse Reserver(ReservationRequest request)
        {

            return hotelRepository.reserver(request);
        }

        [HttpPost("trivago")]
        public List<ReponseTrivago> FindForTrivago(RequestTrivago requestTrivago)
        {

            Console.WriteLine("dem" + requestTrivago.ville);
            return hotelRepository.FindChambresTrivago(requestTrivago);
        }
    }
}
