using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIB1_UE1_Gruppe05_AbgabeCP.Klassen
{

    [Serializable]
    public /*abstract*/ class Mobiliar
    {
        //----- Variablen
        protected string id;
        protected string bez;
        protected int anzahl;
        protected double aktuellerWert;
        protected string zustand;
        protected double kaufpreis;
        protected string typ;

        //----- Getter/Setter
        public string Id { get => id; set => id = value; }
        public string Bez { get => bez; set => bez = value; }
        public int Anzahl { get => anzahl; set => anzahl = value; }
        public double AktuellerWert { get => aktuellerWert; set => aktuellerWert = value; }
        public string Zustand { get => zustand; set => zustand = value; }
        public double Kaufpreis { get => kaufpreis; set => kaufpreis = value; }

        public string Typ { get => typ; set => typ = value; }

        //----- Konstruktoren
        public Mobiliar()
        {
            id = "";
            bez = "";
            anzahl = 0;
            aktuellerWert = 0.0;
            zustand = "";
            kaufpreis = 0.0;
            typ = "";
        }

        public Mobiliar(string id, string bez, int anzahl, string zustand, string typ/*, double aktuellerwert*/)
        {
            this.id = id;
            this.bez = bez;
            this.anzahl = anzahl;
            this.zustand = zustand;
            this.typ = typ;
            //this.aktuellerWert = PreisMobiliar(zustand);
        }

        //----- Methoden


        /* Theorethisch kann man auch hier kaufpreis als Übergabeparameter stehen haben
         * und in den Subklassen die Methode aufrufen und dort die spezifischen Preise eingeben*/
        //public double PreisMobiliar(string zustand)
        //{
        //    switch (zustand)
        //    {
        //        case "intakt":
        //            aktuellerWert = kaufpreis * 1;
        //            break;
        //        case "beschädigt":
        //            aktuellerWert = kaufpreis * 0.7;
        //            break;
        //        case "defekt":
        //            aktuellerWert = kaufpreis * 0.4;
        //            break;
        //    }

        //    return aktuellerWert;


        //    //if (zustand == "intakt")
        //    //{
        //    //    return aktuellerWert = kaufpreis * 1;
        //    //}
        //    //else if (zustand == "beschädigt" )
        //    //{
        //    //    return aktuellerWert = kaufpreis * 0.7;
        //    //}
        //    //else if (zustand == "defekt")
        //    //{
        //    //    return aktuellerWert = kaufpreis * 0.3;
        //    //}
        //    //else
        //    //{
        //    //    return aktuellerWert = 10000000000;
        //    //}
        //}



        public override string ToString()
        {
            string s;
            return s = id + " " + bez + " " + aktuellerWert + " " + zustand + " " + kaufpreis;

        }

    }
}
