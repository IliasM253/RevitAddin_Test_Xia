using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIB1_UE1_Gruppe05_AbgabeCP.Klassen.DIN277_1_NUF_Raumtypen
{
    [Serializable]
    class NUF3 : Raum
    {
        //Klassifizierung nach DIN 277-1
        //Produktion, Hand- und Maschinenarbeit Forschung und Entwicklung
        //---- Konstruktoren
        public NUF3() : base() { }

        public NUF3(string id, string bez, string typ, string nutzungsart, double flaeche, bool mietzustand) : base(id, bez, typ, nutzungsart, flaeche, mietzustand)
        {
            flaechenpreis = PreisRaumFlaeche();
            gesamtmoebelpreis = PreisRaumMoebel();
            gesamtmiete = PreisRaumGes();
        }

        //---- Methoden

        public double PreisRaumFlaeche()
        {
            double preis = flaeche * 30;

            return preis;
        }

        public double PreisRaumMoebel()
        {
            double preis = 0.0;

            foreach (Mobiliar m in invMoebel)
            {
                preis += m.AktuellerWert;
            }

            return preis;
        }

        public double PreisRaumGes()
        {
            double gesP = 0.0;
            double moebelP = PreisRaumMoebel();
            double flaecheP = PreisRaumFlaeche();

            gesP = moebelP + flaecheP;

            return gesP;
        }

        public /*override*/ bool DINerfüllt()
        {
            return true;

        }
    }
}
