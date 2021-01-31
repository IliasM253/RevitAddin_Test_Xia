using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIB1_UE1_Gruppe05_AbgabeCP.Klassen
{
    [Serializable]
    class Konferenztisch : Mobiliar
    {
        //double aktuellerWert;
        public Konferenztisch() : base()
        {
            bez = "Default Bezeichnung";
            kaufpreis = 150.0;
            Typ = "Konferenztisch";
        }

        public Konferenztisch(string id, string bez, int anzahl, string zustand, string typ) : base(id, bez, anzahl, zustand, typ)
        {
            bez = "Default Bezeichnung";
            typ = "Konferenztisch";
            kaufpreis = 150.0;
            this.aktuellerWert = PreisMobiliar(zustand);
        }

        public double PreisMobiliar(string zustand)
        {
            switch (zustand)
            {
                case "intakt":
                    aktuellerWert = kaufpreis * 1;
                    break;
                case "beschädigt":
                    aktuellerWert = kaufpreis * 0.7;
                    break;
                case "defekt":
                    aktuellerWert = kaufpreis * 0.4;
                    break;
            }

            return aktuellerWert;

        }
    }
}
