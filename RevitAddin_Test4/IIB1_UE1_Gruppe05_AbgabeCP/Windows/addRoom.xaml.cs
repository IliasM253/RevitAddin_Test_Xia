using System;
using IIB1_UE1_Gruppe05_AbgabeCP;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IIB1_UE1_Gruppe05_AbgabeCP.Klassen;
using IIB1_UE1_Gruppe05_AbgabeCP.Klassen.DIN277_1_NUF_Raumtypen;

namespace IIB1_UE1_Gruppe05_AbgabeCP.Windows
{
    /// <summary>
    /// Interaktionslogik für addRoom.xaml
    /// </summary>
    public partial class addRoom : Window
    {
        public addRoom()
        {
            InitializeComponent();
        }

        //Variablen

        private Raum raum;
        private List<Raum> raeume = new List<Raum>();
        private double floorqm = 100.0;
        public bool test = true;

        //private OeffentlichK ListRaeumeUebergabe = new OeffentlichK();


        private void RoomTyp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int typ = roomTyp.SelectedIndex;
            //string[] nutzungsarten;
            List<string> nutzungsarten = new List<string>();
            roomPlaces.IsEnabled = false;

            switch (typ)
            {
                case 0:
                    nutzungsarten = new List<string>{ "Wohnräume", "Schlafräume", "Beherbergungsräume", "Küchen in Wohnungen", "Gemeinschaftsräume",
                            "Aufenthaltsräume", "Bereitschaftsräume", "Pausenräume", "Teeküchen", "Ruheräume", "Warteräume", "Speiseräume", "Hafträume"};
                    break;
                case 1:
                    nutzungsarten = new List<string> { "Büroräume", "Großraumbüros", "Besprechungsräume", "Konstruktionsräume", "Zeichenräume", "Schalterräume", "Aufsichtsräume", "Bürogeräteräume" };
                    roomPlaces.IsEnabled = true;
                    break;
                case 2:
                    nutzungsarten = new List<string>{"Werkhallen", "Werkstätten", "Labors(technologische, physikalische, elektrotechnische, chemische, biologische usw.)",
                        "Räume für Tierhaltung", "Räume für Pflanzenzucht", "gewerbliche Küchen(einschließlich Aus - und Rückgaben)", "Sonderarbeitsräume(für Hauswirtschaft, Wäschepflege etc.)"};
                    break;
                case 3:
                    nutzungsarten = new List<string>{"Lager und Vorratsräume", "Lagerhallen", "Tresorräume", "Siloräume", "Archive", "Sammlungsräume", "Registraturen", "Kühlräume", "Annahme und Ausgaberäume",
                        "Packräume", "Versandräume", "Verkaufsräume", "Messeräume" };
                    break;
                case 4:
                    nutzungsarten = new List<string>{"Unterrichts- und Übungsräume", "Hörsäle", "Seminarräume", "Werkräume", "Praktikumsräume", "Bibliotheksräume", "Leseräume",
                        "Sporträume", "Gymnastikräume", "Zuschauerräume" , "Bühnenräume", "Studioräume", "Proberäume", "Ausstellungsräume", "Sakralräume"};
                    break;
                case 5:
                    nutzungsarten = new List<string>{ "allgemeine Untersuchung und Behandlung", "spezielle Untersuchung und Behandlung", "Operationsräume", "Entbindungsräume",
                        "für Strahlendiagnostik und Strahlentherapie", "für Physiotherapie und Rehabilitation", "Bettenräume", "Intensivpflegeräume" };
                    break;
                case 6:
                    nutzungsarten = new List<string> { "Abstellräume", "Fahrradräume", "Müllsammelräume", "Fahrzeugabstellflächen" };
                    break;
                case 7:
                    nutzungsarten = new List<string> { "Sanitärraum", "Umkleiderraum" };
                    break;
            }

            roomNutzungsart.Items.Clear();

            foreach (string i in nutzungsarten)
            {
                roomNutzungsart.Items.Add(i);
            }
        }


        private void RoomSafe_Click(object sender, RoutedEventArgs e)
        {
            //int typ = roomTyp.SelectedIndex;
            //Raum room = null;

            //switch (typ)
            //{
            //    case 0:
            //        room = new NUF1(roomBez.Text, roomTyp.SelectedItem.ToString(), roomNutzungsart.SelectedItem.ToString(), double.Parse(roomArea.Text));
            //        break;
            //    case 1:
            //        //muss noch in den Raum hinzugefügt werden.
            //        room = new NUF2(roomBez.Text, roomTyp.SelectedItem.ToString(), roomNutzungsart.SelectedItem.ToString(), double.Parse(roomArea.Text), int.Parse(roomPlaces.Text));
            //        break;
            //    case 2:
            //        room = new NUF3(roomBez.Text, roomTyp.SelectedItem.ToString(), roomNutzungsart.SelectedItem.ToString(), double.Parse(roomArea.Text));
            //        break;
            //    case 3:
            //        room = new NUF3(roomBez.Text, roomTyp.SelectedItem.ToString(), roomNutzungsart.SelectedItem.ToString(), double.Parse(roomArea.Text));
            //        break;
            //    case 4:
            //        room = new NUF4(roomBez.Text, roomTyp.SelectedItem.ToString(), roomNutzungsart.SelectedItem.ToString(), double.Parse(roomArea.Text));
            //        break;
            //    case 5:
            //        room = new NUF5(roomBez.Text, roomTyp.SelectedItem.ToString(), roomNutzungsart.SelectedItem.ToString(), double.Parse(roomArea.Text));
            //        break;
            //    case 6:
            //        room = new NUF6(roomBez.Text, roomTyp.SelectedItem.ToString(), roomNutzungsart.SelectedItem.ToString(), double.Parse(roomArea.Text));
            //        break;
            //    case 7:
            //        room = new NUF7(roomBez.Text, roomTyp.SelectedItem.ToString(), roomNutzungsart.SelectedItem.ToString(), double.Parse(roomArea.Text));
            //        break;
            //}

            //if (RaumAusgabe() == true)
            //{
            //    raeume.Add(room);
            //}


            this.Close();
        }

        private bool RaumAusgabe()
        {
            string s = "";
            string bez = roomBez.Text;
            //double floorqm = 100;
            double roomqm = Double.Parse(roomArea.Text);


            double rest = floorqm - roomqm;

            if (rest >= 0.0)
            {
                s = bez + " -- wurde hinzugefügt.\n " + "der Raum ist " + roomqm + "m² groß.\n" + rest + "m² stehen noch zur verfügung.\n";
                s += "----------------------\n";

                roomAusgabe.Text += s;
                floorqm = rest;

                return true;
            }
            else
            {
                s = "Raumfläche zu groß";

                roomAusgabe.Text += s;

                return false;
            }

        }



        private void RoomCancel_Click(object sender, RoutedEventArgs e)
        {

            ////NullReferenceException behoben.
            ////ListRaeumeUebergabe = new OeffentlichK(raeume);


            //foreach (Raum r in raeume)
            //{
            //    ListRaeumeUebergabe.OeRaeume.Add(r);
            //}

            this.Close();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            roomAusgabe.Text = "\n" + raeume.Count;
        }
    }
}
