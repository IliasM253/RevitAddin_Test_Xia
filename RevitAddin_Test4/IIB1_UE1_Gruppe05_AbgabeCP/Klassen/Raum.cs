using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;

namespace IIB1_UE1_Gruppe05_AbgabeCP.Klassen
{
    [Serializable]
    public /*abstract */class Raum
    {
        //----- Variablen
        protected string id;
        protected string bez;
        protected string typ;
        protected string nutzungsart;
        protected double flaeche;
        protected bool mietzustand;
        protected BindingList<Mobiliar> invMoebel;
        //Neu
        protected double flaechenpreis;
        protected double gesamtmoebelpreis;
        protected double gesamtmiete;

        public string Id { get => id; set => id = value; }
        public string Bez { get => bez; set => bez = value; }
        public string Typ { get => typ; set => typ = value; }
        public string Nutzungsart { get => nutzungsart; set => nutzungsart = value; }
        public double Flaeche { get => flaeche; set => flaeche = value; }
        public BindingList<Mobiliar> InvMoebel { get => invMoebel; set => invMoebel = value; }
        public bool Mietzustand { get => mietzustand; set => mietzustand = value; }
        //Neu
        public double Flaechenpreis { get => flaechenpreis; set => flaechenpreis = value; }
        public double Gesamtmoebelpreis { get => gesamtmoebelpreis; set => gesamtmoebelpreis = value; }
        public double Gesamtmiete { get => gesamtmiete; set => gesamtmiete = value; }

        public Raum()
        {
            this.id = "";
            this.bez = "";
            this.typ = "";
            this.nutzungsart = "";
            this.flaeche = 0.0;
            this.mietzustand = false;
            invMoebel = new BindingList<Mobiliar>();

            flaechenpreis = 0.0;
            gesamtmoebelpreis = 0.0;
            gesamtmiete = 0.0;
        }

        public Raum(string id, string bez, string typ, string nutzungsart, double flaeche, bool mietzustand)
        {
            this.id = id;
            this.bez = bez;
            this.typ = typ;
            this.nutzungsart = nutzungsart;
            this.flaeche = flaeche;
            this.mietzustand = mietzustand;
            invMoebel = new BindingList<Mobiliar>();

            //Die neuen Variablen werden in den Konstruktoren der Unterklassen hinzugefügt
        }

        //public double PreisRaumFlaeche()
        //{
        //    return 0;
        //}

        //public double PreisRaumMoebel()
        //{
        //    double preis = 0.0;

        //    foreach(Mobiliar m in invMoebel)
        //    {
        //        preis += m.AktuellerWert;
        //    }

        //    return preis;
        //}

        //public double PreisRaumGes()
        //{
        //    double gesP = 0.0;
        //    double moebelP = PreisRaumMoebel();
        //    double flaecheP = PreisRaumFlaeche();

        //    gesP = moebelP + flaecheP;

        //    return gesP;
        //}

        // public abstract bool DINerfüllt();

        //public override string ToString()
        //{
        //    string s;
        //    return s = typ + "\n" + nutzungsart + "\n" + bez + "\n" + "--------------------";

        //}

        public override string ToString()
        {
            string s;
            return s = id + " " + bez;

        }


    }
}
