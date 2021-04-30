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
            hotel = new Hotel("Ibiza", 3, new Adresse("France", "Strasbourg", "16 rue de macon", "67100"), "hotel prés de place de clibére");

            chambres = new List<Chambre>();
            Chambre c1 = new Chambre("P1", "normale", "une chambre un lit dedans", 40, 1);
            c1.image = "https://medias.bestwestern.fr/props_iceportal/85483/64474158_XL.jpg?frz-v=22";
            Chambre c2 = new Chambre("P2", "vip", "une chambre vip un grand lit dedans", 75, 1);
            c2.image = "https://gevorahotels.com/wp-content/uploads/2020/06/MYP_0336-HDR-scaled.jpg";
               
            Chambre c3 = new Chambre("P3", "normale", "une chambre deux lit dedans", 70, 2);
            c3.image = "https://www.ahstatic.com/photos/1871_roskb_00_p_1024x768.jpg";
            Chambre c4 = new Chambre("P4", "vip", "une chambre vip deux grand lit dedans", 155, 2);
            c4.image = "";
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
                    Reponse reponse = new Reponse("IB" + i, chambre.type, chambre.prix);
                    if (request.numPersonne == 2)
                    {
                        reponse.offreId = "IB" + i + 1;
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
                            double percentage = c.prix * 0.2;
                            agences.Add(request.agenceId, (float)(percentage));
                            Console.WriteLine("Agence numero " + request.agenceId + "vous avez gagné  " + agences[request.agenceId] + " Euros de cette reservation");
                        }
                    }
                    reservationReponse.confirmation = "Vous avez réservé votre chambre chez Ibiza hotel!";
                    return reservationReponse;
                }
            }

             reservationReponse.confirmation = "Désole il y a eu une erreur ";
            return reservationReponse;
        }
        public List<ReponseTrivago> FindChambresTrivago(RequestTrivago request)
        {

            List<ReponseTrivago> reponses = new List<ReponseTrivago>();

            if (request.ville == hotel.adresse.ville && request.etoile == hotel.etoile)
                foreach (Chambre chambre in hotel.chambre)
                {

                    if (request.numPersonne == chambre.personnes)
                    {


                        ReponseTrivago reponse = new ReponseTrivago(hotel.nom, hotel.etoile,
                            chambre.description, hotel.adresse.rue, chambre.prix);






                        reponses.Add(reponse);
                    }
                }

            return reponses;
        }
      
    }
}
