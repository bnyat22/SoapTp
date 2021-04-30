using System;
namespace Hotel
{
    public class Request
    {

        public String id { set; get; }
        public String password { set; get; }
        public String arrive { set; get; }
        public String depart { set; get; }

        public int numPersonne { set; get; }


        public Request(String id, String arrive, String depart, int numPersonne)
        {
            this.id = id;
   
            this.arrive = arrive;
            this.depart = depart;
     
            this.numPersonne = numPersonne;
          
        }
    }
}
