using System;
namespace Hotel
{
    public class Chambre
    {
        public long Id { get; set; }
        public String numero { get; set; }
        public String type { get; set; }
        public String description { get; set; }
        public Boolean disponible { get; set; }
        public double prix { get; set; }
        public int personnes { get; set; }
        public String dateDispo { get; set; }
        public String image { get; set; }

        public Chambre(String numero, String type, String description, double prix, int p)
        {
            this.numero = numero;
            this.type = type;
            this.description = description;
            disponible = true;
            this.prix = prix;
            personnes = p;
        }
    }
}



    
