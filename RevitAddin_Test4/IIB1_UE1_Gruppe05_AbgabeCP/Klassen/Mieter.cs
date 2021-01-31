using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIB1_UE1_Gruppe05_AbgabeCP.Klassen
{
    [Serializable]
    class Mieter
    {
        //----- Variablen
        private string name;
        private string mail;
        private string tel;
        private double aktuelleKosten;
        private BindingList<Raum> gemieteteRaeume;

        //----- Getter/Setter
        public string Name { get => name; set => name = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Tel { get => tel; set => tel = value; }
        public double AktuelleKosten { get => aktuelleKosten; set => aktuelleKosten = value; }
        public BindingList<Raum> GemieteteRaeume { get => gemieteteRaeume; set => gemieteteRaeume = value; }

        //---Konstruktoren

        public Mieter()
        {
            name = "";
            mail = "";
            tel = "";
            aktuelleKosten = 0.0;
            gemieteteRaeume = new BindingList<Raum>();
        }

        public Mieter(string name, string mail, string tel, BindingList<Raum> gemieteteRaeume)
        {
            this.name = name;
            this.mail = mail;
            this.tel = tel;
            aktuelleKosten = berechneKosten();
            this.gemieteteRaeume = gemieteteRaeume;
        }

        public Mieter(string name, string mail, string tel)
        {
            this.name = name;
            this.mail = mail;
            this.tel = tel;
            aktuelleKosten = berechneKosten();
            this.gemieteteRaeume = new BindingList<Raum>();
        }

        //----- Meethoden
        public double berechneKosten()
        {
            return 0.0;
        }

        public override string ToString()
        {
            return name;
        }


        public void Sortieren()
        {



        }
    }
}
