using System;
using System.Collections.Generic;

namespace Trivago.Models
{
    public class ReponseTrivago
    {
        public String nom { set; get; }
        public int etoile { set; get; }
        public String nombreDeLit { set; get; }
       
        public String adresse { set; get; }
        public double prix { set; get; }
        public String nomDeAgence { set; get; }
        public ReponseTrivago(String nom, int etoile , String nombreDeLit,String adresse, double prix)
        {
            this.nom = nom;
            this.nombreDeLit = nombreDeLit;
            this.adresse = adresse;
            this.etoile = etoile;

            this.prix = prix;

        }

    }
}
