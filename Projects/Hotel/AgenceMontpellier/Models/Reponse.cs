using System;
namespace Hotel.Models
{
    public class Reponse
    {
        public String offreId { set; get; }
        public String type { set; get; }
        public String disponibilite { set; get; }
        public String image { set; get; }

        public double prix { set; get; }


        public Reponse(String offreId, String type, double prix , String image)
        {
            this.offreId = offreId;

            this.type = type;
         
            this.prix = prix;
            this.image = image;

        }
    }
}
