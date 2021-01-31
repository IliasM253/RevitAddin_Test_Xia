using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIB1_UE1_Gruppe05_AbgabeCP.Klassen
{
    [Serializable]
    class Tisch : Mobiliar
    {
        public Tisch() : base()
        {
            bez = "Default Bezeichnung";
            typ = "Tisch";
            kaufpreis = 45.0;
        }

        public Tisch(string id, string bez, int anzahl, string zustand, string typ) : base(id, bez, anzahl, zustand, typ)
        {
            bez = "Default Bezeichnung";
            typ = "Tisch";
            kaufpreis = 45.0;
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
