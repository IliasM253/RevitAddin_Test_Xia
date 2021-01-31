using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace IIB1_UE1_Gruppe05_AbgabeCP.Klassen
{
    [Serializable]
    public class Gebaeude
    {
        //----- Variablen
        string bez;
        string adresse;
        BindingList<Geschoss> listGeschosse;
        string id;

        //----- Getter/Setter
        public string Bez { get => bez; set => bez = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public BindingList<Geschoss> ListGeschosse { get => listGeschosse; set => listGeschosse = value; }
        public string Id { get => id; set => id = value; }

        //----- Konstruktoren
        public Gebaeude()
        {
            bez = null;
            adresse = null;
            listGeschosse = null;
        }

        public Gebaeude(string bez, string adresse, BindingList<Geschoss> listGeschosse)
        {
            this.bez = bez;
            this.adresse = adresse;
            this.listGeschosse = listGeschosse;
        }

        public Gebaeude(string bez)
        {
            this.bez = bez;
            adresse = "Hier!!!!";
            listGeschosse = new BindingList<Geschoss>();
            this.id = "0";
        }

        public override string ToString()
        {
            return bez;
        }

    }
}
