using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace IIB1_UE1_Gruppe05_AbgabeCP.Klassen
{
    [Serializable]
    public class Geschoss
    {
        //----- Variablen
        string bez;
        double flaeche;
        BindingList<Raum> listRaeume;
        string id;

        //----- Getter/Setter
        public string Bez { get => bez; set => bez = value; }
        public double Flaeche { get => flaeche; set => flaeche = value; }
        public BindingList<Raum> ListRaeume { get => listRaeume; set => listRaeume = value; }
        public string Id { get => id; set => id = value; }

        //----- Konstruktoren
        public Geschoss()
        {
            bez = null;
            flaeche = 0.0;
            listRaeume = null;
        }

        public Geschoss(string bez, double flaeche, BindingList<Raum> listRaeume)
        {
            this.bez = bez;
            this.flaeche = flaeche;
            this.listRaeume = listRaeume;
        }

        public Geschoss(string bez, BindingList<Raum> listRaeume)
        {
            this.bez = bez;
            this.flaeche = berechneFlaeche();
            this.listRaeume = listRaeume;
            this.Id = Id;
        }

        private double berechneFlaeche()
        {
            double flaeche = 0.0;
            foreach (Raum r in ListRaeume)
            {
                flaeche += r.Flaeche;
            }
            return flaeche;
        }

        public override string ToString()
        {
            return bez;
        }
    }
}
