using System;
using Palace.Hotel;

namespace Hotel
{
    public class Reservation
    {
        private String numero { get; set; }
        private Client client { get; set; }
        private Chambre chambre { get; set; }
        private double montant { get; set; }
        private String description { get; set; }
        private String arrive { get; set; }
        private String depart { get; set; }

        public Reservation(String numero, Client client, Chambre chambre, double montant, String description, String arrive, String depart)
        {
            this.numero = numero;
            this.client = client;
            this.chambre = chambre;
            this.montant = montant;
            this.description = description;
            this.arrive = arrive;
            this.depart = depart;
        }

        public override string ToString()
        {
            return "Numero de reservation: " + numero + "\n" +
                "Client information: " + "\n" + client.ToString() +
                "Chambre information: " + chambre.ToString() +
                "Cette pesronne paye: " + montant + "\n" +
                "Description: " + description + "\n" +
                "Date arrivé: " + arrive + "\n" +
                "Date de depart: " + depart + "\n";

        }

    }
}
