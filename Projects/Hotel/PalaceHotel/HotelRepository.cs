using System;
using System.Collections.Generic;
using Hotel;
using Hotel.Models;
using PalaceHotel.Models;

namespace PalaceHotel
{
    public class HotelRepository
    {
       public Hotel hotel { get; set; }
        public List<Chambre> chambres { get; set; }
        public List<Reponse> reponses { get; set; }
        public Dictionary<String, String> dateDepart;
        public Dictionary<String, String> chambreMap;
        public Dictionary<String, float> agences;


        public HotelRepository()
        {
            hotel = new Hotel("plaza", 4, new Adresse("France", "Paris", "105 rue claude", "67500"), "hotel prés de la riviére");

            chambres = new List<Chambre>();
            Chambre c1 = new Chambre("P1", "normale", "une chambre un lit dedans", 45, 1);
            c1.image = "https://media-cdn.tripadvisor.com/media/photo-s/07/60/35/a8/hotel-des-pavillons.jpg";
            Chambre c2 = new Chambre("P2", "vip", "une chambre un grand lit dedans", 65, 1);
            c2.image = "https://lh3.googleusercontent.com/proxy/n3gAX1-HYRo3qlK9YpVqo2c48mfw1QQCqHmvYXjO73reQLduMou-k4BGMXRe6b61OimmKZeRjsRjOwb0maZEUpV-L9f6Ln3L3EtCvLPwIieX720nUWOcclzTsZGscuXYAJEOqw2Z4AQJCDNTdb10E3SK";
            Chambre c3 = new Chambre("P3", "normale", "une chambre deux lit dedans", 60, 2);
            c3.image = "https://media-cdn.tripadvisor.com/media/photo-s/01/3a/4d/09/two-beds-in-the-smart.jpg";
            Chambre c4 = new Chambre("P4", "vip", "une chambre deux grand lit dedans", 105, 2);
            c4.image = "https://www.seaportboston.com/resourcefiles/mainimages/seaport-hotel-world-trade-center-boston-deluxe-room-two-double-beds.jpg";
            chambres.Add(c1);
            chambres.Add(c2);
            chambres.Add(c3);
            chambres.Add(c4);
            hotel.chambre = chambres;
         
    }
        public List<Reponse> FindChambres(Request request)
        {
            dateDepart = new Dictionary<string, String>();
            chambreMap = new Dictionary<string, String>();
            agences = new Dictionary<String, float>();
            reponses = new List<Reponse>();
          //  dateDepart = new Dictionary<string, DateTime>();
           // chambreMap = new Dictionary<string, string>();
            int i = 0;
            foreach (Chambre chambre in hotel.chambre)
            {
               
                if (request.numPersonne == chambre.personnes)
                {
                   
                    i++;
                    Reponse reponse = new Reponse("PL" + i, chambre.type, chambre.prix);
                    if (request.numPersonne == 2)
                    {
                        reponse.offreId = "PL" + i + 1;
                    }
                    chambreMap.Add(reponse.offreId, chambre.numero);
                    String numeroDeChambre = chambreMap[reponse.offreId];
                    dateDepart.Add(reponse.offreId, request.depart);
                    if (chambre.disponible)
                    {
                        reponse.disponibilite = DateTime.Today.ToString("dd/MM/yyyy");
                    }
                    else
                        reponse.disponibilite = chambre.dateDispo;
                    reponse.image = chambre.image;
                    reponses.Add(reponse);
                }
            }

            return reponses;
        }


        public List<ReponseTrivago> FindChambresTrivago(RequestTrivago request)
        {
            
          List<ReponseTrivago>  reponses = new List<ReponseTrivago>();

            if(request.ville == hotel.adresse.ville && request.etoile == hotel.etoile)
            foreach (Chambre chambre in hotel.chambre)
            {

                if (request.numPersonne == chambre.personnes)
                {

                   
                    ReponseTrivago reponse = new ReponseTrivago(hotel.nom, hotel.etoile,
                        chambre.description,hotel.adresse.rue, chambre.prix);
                   
                   
                    
                   
                   
                  
                    reponses.Add(reponse);
                }
            }

            return reponses;
        }
        public ReservationReponse reserver(ReservationRequest request)
        {
        
            ReservationReponse reservationReponse = new ReservationReponse();
            String numeroDeChambre = chambreMap[request.offreId];
            String dateDep = dateDepart[request.offreId];
            foreach(Reponse reponse in reponses)
            {
                if (reponse.offreId.CompareTo(request.offreId) == 0)
                {
                    foreach (Chambre c in hotel.chambre)
                    {
                        if (c.numero.CompareTo(numeroDeChambre) == 0)
                        {
                            if (!c.disponible)
                            {
                                reservationReponse.confirmation ="désolé le chambre n'est pas disponible pour l'instant veuillez regardé " +
                                        "la date disponible de cette offre ou choisir un autre offre";
                                return reservationReponse;
                            }
                            c.disponible = false;
                            c.dateDispo = dateDep;
                            double percentage = c.prix * 0.1;
                            agences.Add(request.agenceId, (float)(percentage));
                            Console.WriteLine("Agence numero " + request.agenceId + "vous avez gagné  " + agences[request.agenceId] + " Euros de cette reservation");
                        }
                    }
                    reservationReponse.confirmation = "Vous avez réservé votre chambre chez Place hotel!";
                    return reservationReponse;
                }
            }

             reservationReponse.confirmation = "Désole il y a eu une erreur ";
            return reservationReponse;
        }
    }
}