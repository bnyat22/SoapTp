using System;
using System.Collections.Generic;
using Hotel;

namespace PalaceHotel
{
    public class Hotel
    {
        public long Id { get; set; }
        public String nom { get; set; }
        public int etoile { get; set; }
        public Adresse adresse { get; set; }
        public String description { get; set; }
        public List<Chambre> chambre { get; set; }
        public Hotel(String nom, int etoile, Adresse adresse, String description)
        {
            this.nom = nom;
            this.etoile = etoile;
            this.adresse = adresse;
            this.description = description;
            chambre = new List<Chambre>();
        }


    }
}
