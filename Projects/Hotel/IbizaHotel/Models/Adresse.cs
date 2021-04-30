using System;
namespace PalaceHotel
{
    public class Adresse
    {
        public long Id { set; get; }
        public String pays { set; get; }
        public String ville { set; get; }
        public String rue { set; get; }
  
        public String codePostal { set; get; }
        public Adresse(String pays, String ville, String rue, String codePostal)
        {
            this.Id = Id;
            this.pays = pays;
            this.ville = ville;
            this.rue = rue;
       
            this.codePostal = codePostal;

        }
    }
}
