using IIB1_UE1_Gruppe05_AbgabeCP.Klassen;
using IIB1_UE1_Gruppe05_AbgabeCP.Windows;
using IIB1_UE1_Gruppe05_AbgabeCP.Klassen.DIN277_1_NUF_Raumtypen;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using Autodesk.Revit.UI;

namespace IIB1_UE1_Gruppe05_AbgabeCP  //IIB1_UE1_Gruppe05
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Beispiele();
            if (comboboxmieter.Items.Count == 0)
            {
                MieterBSP();
            }
        }


        //Variablen
        Gebaeude mainGebaeude;
        Geschoss mainGeschoss;
        Raum mainRaum = new Raum();
        bool manuellePlatzierung = false;
        ExternalEvent eventUpdater, placementEvent;

        private BindingList<Gebaeude> gebaeudeListe = new BindingList<Gebaeude>();
        private BindingList<Geschoss> geschossListe = new BindingList<Geschoss>();
        private BindingList<Raum> raumListe = new BindingList<Raum>();
        private BindingList<Mieter> mieterListe = new BindingList<Mieter>();

        private bool loadWasClicked = false;
        
        
        
        public MainWindow( BindingList<Gebaeude> gebaeude, ExternalEvent eventUpdater, ExternalEvent placementEvent)
        {
            InitializeComponent();
            gebaeudeListe = gebaeude;
            EventUpdater = eventUpdater;
            PlacementEvent = placementEvent;

            MieterBSP();

            foreach (Gebaeude g in gebaeudeListe)
            {
                comboboxgebaeude.Items.Add(g).ToString();
            }

        }

        //-----------------------------------------------------------------------------------------------------------
        //Getter und Setter
        internal Gebaeude MainGebaeude { get => mainGebaeude; set => mainGebaeude = value; }
        internal Geschoss MainGeschoss { get => mainGeschoss; set => mainGeschoss = value; }
        internal BindingList<Gebaeude> GebaeudeListe { get => gebaeudeListe; set => gebaeudeListe = value; }
        public Raum MainRaum { get => mainRaum; set => mainRaum = value; }
        public bool ManuellePlatzierung { get => manuellePlatzierung; set => manuellePlatzierung = value; }
        public ExternalEvent EventUpdater { get => eventUpdater; set => eventUpdater = value; }
        public ExternalEvent PlacementEvent { get => placementEvent; set => placementEvent = value; }


        //-----------------------------------------------------------------------------------------------------------
        //ButtonClick-Methods
        private void Buttonspeichern_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                save();

                tbSpeichern.Text = "//Speichern erfolgreich.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: \n" + ex.Message, "Speicherfehler");
            }
        }

        private void Buttonladen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                load();

                comboboxmieter.Items.Clear();

                foreach (Mieter m in mieterListe)
                {
                    comboboxmieter.Items.Add(m);
                }

                comboboxgebaeude.Items.Clear();

                foreach (Gebaeude g in gebaeudeListe)
                {
                    comboboxgebaeude.Items.Add(g);

                }



                comboboxstockwerk.Items.Clear();
                LVraeume.Items.Clear();

                comboboxlistraum1.Items.Clear();
                comboboxlistraum.Items.Clear();
                listviewmobiliar.Items.Clear();
                LVbilanz.Items.Clear();

                tbSpeichern.Text = "//Laden erfolgreich.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: \n" + ex.Message, "Ladefehler");
            }

            loadWasClicked = true;

        }

        private void BtBilanzRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LVbilanz.Items.Clear();

                //Lokale Variablen

                Mieter mieter = (Mieter)comboboxmieter.SelectedItem;
                double gesamtmiete = 0.0;

                //Methoden-Körper

                for (int k = 0; k < mieter.GemieteteRaeume.Count; k++)
                {
                    Raum r = mieter.GemieteteRaeume[k];
                    LVbilanz.Items.Add(new Raum { Id = r.Id, Nutzungsart = r.Nutzungsart, Flaeche = r.Flaeche, Flaechenpreis = r.Flaechenpreis, Gesamtmoebelpreis = PreisRaumMoebel(k), Gesamtmiete = PreisRaumGes(k) });
                    gesamtmiete += PreisRaumGes(k);
                }

                lGesamtmiete.Content = "Die Gesamtmiete beträgt: " + gesamtmiete + "€";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: \n" + ex.Message, "Refreshfehler");
            }

        }

        private void Buttonrefreshraum_Click(object sender, RoutedEventArgs e)
        {
            //Gebaeude g =(Gebaeude) comboboxgebaeude.SelectedItem;
            //Geschoss ges = g.ListGeschosse[comboboxstockwerk.SelectedIndex];
            //ges.ListRaeume = ListRaeumeUebergabe.OeRaeume;

            /*
             * BOAHHHHH!!!!!!!!!!!!!!!!!!!!!!
             * Ich ficke mein Leben!!!!
             * Keine Ahnung warum das nicht funktioniert!!!!!!
             * Anscheinend ist die Liste in der öffentlichen Klasse leer aber nicht null.
             * Der Beispielraum, den ich hinzugefügt habe, wirft irgendeinen Fehler auf...
             * Wahrscheinlich ist es irgendein kleiner Fehler, den ich gerade nicht finde...
             */

            //comboboxraum.Items.Clear();

            //if (ListRaeumeUebergabe.OeRaeume == null)
            //{
            //    textboxausgaberaum.Text = "Liste ist leer!!!";
            //}
            //else {

            //    textboxausgaberaum.Text = "Liste ist nicht leer!!\n" + ges.ListRaeume.Count + "\n" + ListRaeumeUebergabe.OeRaeume.Count + "\n";



            //    foreach (Raum r2 in ges.ListRaeume)
            //    {
            //        comboboxraum.Items.Add(r2.ToString());

            //        //textboxausgaberaum.Text += r2.ToString() + "/n";
            //    }
            //Why you not work????????????!!!!!!!!!!!!
            // Die RTäume-Liste in der Klasse1 ist definitiv nicht null!!! Aber dennoch iwie leer....WARUM???????????????


            //}
        }

        private void Buttonmieter_Click(object sender, RoutedEventArgs e)
        {
            //    Mieter m = (Mieter) comboboxmieter.SelectedItem;

            //    textboxausgabespeichernladengebaeude.Clear();
            //    foreach(Raum r in m.GemieteteRaeume)
            //    {
            //        textboxausgabespeichernladengebaeude.Text += r.ToString() + "\n";
            //    }

        }

        private void Raumupdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Raum rm = (Raum)comboboxlistraum1.SelectedItem;
                int i = raumtyp.SelectedIndex;

                if (raumtyp.SelectedItem != null)
                {
                    rm.Typ = RaumTypBestimmen(i);
                }

                if (raumnutzungsart.SelectedItem != null)
                {
                    rm.Nutzungsart = (string)raumnutzungsart.SelectedItem;
                }

                if (raumtyp.SelectedIndex != -1 && raumnutzungsart.SelectedIndex == -1)
                {
                    MessageBox.Show("Fehler: \n Nutzungsart auswählen", "Updatefehler");
                }


                rm.Bez = raumbez.Text;
                Filltextboxraum(rm);

                foreach (Gebaeude g in gebaeudeListe)
                {
                    foreach (Geschoss ges in g.ListGeschosse)
                    {
                        for (int k = 0; k < ges.ListRaeume.Count; k++)
                        {
                            if (ges.ListRaeume[k].Id == rm.Id)
                            {
                                Raum r = new Raum(rm.Id, rm.Bez, rm.Typ, rm.Nutzungsart, rm.Flaeche, rm.Mietzustand);

                                ges.ListRaeume.Remove(ges.ListRaeume[k]);
                                ges.ListRaeume[k] = r;
                                //ges.Add(r);

                            }
                        }
                    }
                }
                ComboBoxRaumRefresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: \n" + ex.Message, "Updatefehler");
            }

        }

        private void Buttonmobiliar_Click(object sender, RoutedEventArgs e)
        {

            Mobiliar mob = (Mobiliar)listviewmobiliar.SelectedItem;

            if (Mobiliartyp.SelectedItem != null /*&& mob.Typ != null*/)
            {
                if (Mobiliartyp.SelectedIndex == 0)
                {
                    mob.Typ = "Stuhl";
                }
                else if (Mobiliartyp.SelectedIndex == 1)
                {
                    mob.Typ = "Tisch";
                }
                else if (Mobiliartyp.SelectedIndex == 2)
                {
                    mob.Typ = "Konferenztisch";
                }
            }

            if (Mobiliarzustand.SelectedItem != null)
            {
                if (Mobiliarzustand.SelectedIndex == 0)
                {
                    mob.Zustand = "intakt";
                }
                else if (Mobiliarzustand.SelectedIndex == 1)
                {
                    mob.Zustand = "beschädigt";
                }
                else if (Mobiliarzustand.SelectedIndex == 2)
                {
                    mob.Zustand = "defekt";
                }
            }

            //if (Mobiliarzustand.SelectedItem != null && Mobiliartyp.SelectedItem != null)
            //{
            //    string m1 = Mobiliartyp.SelectedItem.ToString();
            //    string m2 = Mobiliarzustand.SelectedItem.ToString();
            //    mobiliaraendern(mob, m1, m2, mob.Bez);
            //}

            mob.Bez = mobiliarBez.Text;


            foreach (Gebaeude g in gebaeudeListe)
            {
                foreach (Geschoss ges in g.ListGeschosse)
                {
                    foreach (Raum r in ges.ListRaeume)
                    {
                        for (int m = 0; m < r.InvMoebel.Count; m++)
                        {
                            if (r.InvMoebel[m].Id == mob.Id)
                            {
                                if (mob.Typ == "Stuhl")
                                {
                                    Stuhl newStuhl = new Stuhl(mob.Id, mob.Bez, mob.Anzahl, mob.Zustand, mob.Typ);
                                    r.InvMoebel[m] = newStuhl;
                                }
                                else if (mob.Typ == "Tisch")
                                {
                                    Tisch newTisch = new Tisch(mob.Id, mob.Bez, mob.Anzahl, mob.Zustand, mob.Typ);
                                    r.InvMoebel[m] = newTisch;
                                }
                                else if (mob.Typ == "Konferenztisch")
                                {
                                    Konferenztisch newKonferenztisch = new Konferenztisch(mob.Id, mob.Bez, mob.Anzahl, mob.Zustand, mob.Typ);
                                    r.InvMoebel[m] = newKonferenztisch;
                                }

                            }
                        }
                    }
                }
            }

            if (comboboxlistraum.SelectedItem != null)
            {
                Filltextboxmobiliar(mob);
            }

            Raum ro = (Raum)comboboxlistraum.SelectedItem;
            listviewmobiliar.Items.Clear();

            foreach (Mobiliar mobs in ro.InvMoebel)
            {
                listviewmobiliar.Items.Add(mobs);
                //textboxausgabebilanz.Text += mobs.ToString() + "\n";
            }

            mobiliartextTyp.Clear();
            Mobiliartyp.SelectedIndex = -1;
            mobiliarBez.Clear();
            mobiliarAnz.Clear();
            mobiliarWert.Clear();
            mobiliartextzustand.Clear();
            Mobiliarzustand.SelectedIndex = -1;
        }

        private void Buttonneuemobiliar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mobiliar mob1 = (Mobiliar)listviewmobiliar.SelectedItem;

                if (Mobiliartyp.SelectedItem != null)
                {
                    if (Mobiliartyp.SelectedIndex == 0)
                    {
                        mob1.Typ = "Stuhl";
                    }
                    else if (Mobiliartyp.SelectedIndex == 1)
                    {
                        mob1.Typ = "Tisch";
                    }
                    else if (Mobiliartyp.SelectedIndex == 2)
                    {
                        mob1.Typ = "Konferenztisch";
                    }
                }

                if (Mobiliarzustand.SelectedItem != null)
                {
                    if (Mobiliarzustand.SelectedIndex == 0)
                    {
                        mob1.Zustand = "intakt";
                    }
                    else if (Mobiliarzustand.SelectedIndex == 1)
                    {
                        mob1.Zustand = "beschädigt";
                    }
                    else if (Mobiliarzustand.SelectedIndex == 2)
                    {
                        mob1.Zustand = "defekt";
                    }
                }



                Raum r = (Raum)comboboxlistraum.SelectedItem;

                int Anzmob = r.InvMoebel.Count;

                if (mob1.Typ == "Stuhl")
                {
                    Stuhl newStuhl = new Stuhl { Id = r.Id + "." + Anzmob, Bez = mobiliarBez.Text, Anzahl = 1, Zustand = Mobiliarzustand.Text, Typ = Mobiliartyp.Text };
                    Stuhl newStuhl1 = new Stuhl(newStuhl.Id, newStuhl.Bez, newStuhl.Anzahl, newStuhl.Zustand, newStuhl.Typ);
                    r.InvMoebel.Add(newStuhl1);
                }
                else if (mob1.Typ == "Tisch")
                {
                    Tisch newTisch = new Tisch { Id = r.Id + "." + Anzmob, Bez = mobiliarBez.Text, Anzahl = 1, Zustand = Mobiliarzustand.Text, Typ = Mobiliartyp.Text };
                    Tisch newTisch1 = new Tisch(newTisch.Id, newTisch.Bez, newTisch.Anzahl, newTisch.Zustand, newTisch.Typ);
                    r.InvMoebel.Add(newTisch1);
                }
                else if (mob1.Typ == "Konferenztisch")
                {
                    Konferenztisch newKonferenztisch = new Konferenztisch { Id = r.Id + "." + Anzmob, Bez = mobiliarBez.Text, Anzahl = 1, Zustand = Mobiliarzustand.Text, Typ = Mobiliartyp.Text };
                    Konferenztisch newKonferenztisch1 = new Konferenztisch(newKonferenztisch.Id, newKonferenztisch.Bez, newKonferenztisch.Anzahl, newKonferenztisch.Zustand, newKonferenztisch.Typ);
                    r.InvMoebel.Add(newKonferenztisch1);
                }

                //Mobiliar mob2 = new Mobiliar { Id = r.Id + "." + Anzmob, Bez = mobiliarBez.Text, Anzahl = 1, Zustand = mobiliartextzustand.Text, Typ = mobiliartextTyp.Text };

                //r.InvMoebel.Add(mob2);

                Raum ro = (Raum)comboboxlistraum.SelectedItem;

                listviewmobiliar.Items.Clear();

                foreach (Mobiliar mobs in ro.InvMoebel)
                {
                    listviewmobiliar.Items.Add(mobs);
                    //textboxausgabebilanz.Text += mobs.ToString() + "\n";
                }

                //--- Clear-Befehle:
                mobiliartextTyp.Clear();
                Mobiliartyp.SelectedIndex = -1;
                mobiliarBez.Clear();
                mobiliarAnz.Clear();
                mobiliarWert.Clear();
                mobiliartextzustand.Clear();
                Mobiliarzustand.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: \n" + ex.Message, "Fehler beim Hinzufügen");
            }

        }

        private void mobiliaraendern(Mobiliar mob, string m1, string m2, string bez)
        {
            if (m1 == "Stuhl")
            {
                //if (m2 == "intakt")
                //{

                //} else if (m2 == "beschädigt")
                //{

                //} else if (m2 == "defekt")
                //{

                //}
                mob = new Stuhl { Id = mob.Id, Bez = bez, Typ = "Stuhl", Zustand = m2 };

            }
            else if (m1 == "Tisch")
            {
                mob = new Tisch { Id = mob.Id, Bez = bez, Typ = "Tisch", Zustand = m2 };

            }
            else if (m1 == "Konferenztisch")
            {
                mob = new Konferenztisch { Id = mob.Id, Bez = bez, Typ = "Konferenztisch", Zustand = m2 };
            }
        }



        //-----------------------------------------------------------------------------------------------------------
        //ComboBox-SelectionChanged-Methods
        private void comboboxgebaeude_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LVraeume.Items.Clear();



            if (loadWasClicked == true)
            {


                if (comboboxgebaeude.SelectedItem is Gebaeude)
                {
                    //Iwie hat hier wegen der -1 das Speichern und Laden nicht funktioniert.
                    //Beobachten, ob sich die Funktion des Programms iwie deswegen verändert.

                    MainGebaeude = gebaeudeListe[comboboxgebaeude.SelectedIndex];
                    textboxausgabespeichernladengebaeude.Text = MainGebaeude.ToString() + " ist selektiert.";

                    comboboxstockwerk.Items.Clear();
                    foreach (Geschoss gs in MainGebaeude.ListGeschosse)
                    {
                        comboboxstockwerk.Items.Add(gs).ToString();
                    }
                }

            }
            else if (loadWasClicked == false)
            {
                if (comboboxgebaeude.SelectedItem is Gebaeude)
                {
                    MainGebaeude = gebaeudeListe[comboboxgebaeude.SelectedIndex - 1];
                    textboxausgabespeichernladengebaeude.Text = MainGebaeude.ToString() + " ist selektiert.";

                    comboboxstockwerk.Items.Clear();
                    foreach (Geschoss gs in MainGebaeude.ListGeschosse)
                    {
                        comboboxstockwerk.Items.Add(gs).ToString();
                    }
                }
            }
        }

        private void comboboxstockwerk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboboxstockwerk.SelectedItem is Geschoss)
            {
                //wieso hier nicht die -1 ??
                MainGeschoss = MainGebaeude.ListGeschosse[comboboxstockwerk.SelectedIndex];
                textboxausgabespeichernstockwerk.Text = MainGeschoss.ToString() + " ist selektiert.";
                //Console.WriteLine(mainGeschoss.ToString());

                //comboboxraum.Items.Clear();

                RaeumeRefresh();

            }
        }

        private void Comboboxlistraum1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboboxlistraum1.SelectedItem != null)
            {
                Raum rm = (Raum)comboboxlistraum1.SelectedItem;
                Filltextboxraum(rm);

                mainRaum= (Raum)comboboxlistraum1.SelectedItem;
            }
        }

        private void Raumtyp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            raumnutzungsart.SelectedItem = null;
            raumnutzungsart.IsEnabled = true;
            int typ = raumtyp.SelectedIndex;
            List<string> nutzungsarten = new List<string>();
            raumsitzplaetze.IsEnabled = false;

            switch (typ)
            {
                case 0:
                    nutzungsarten = new List<string>{ "Wohnräume", "Schlafräume", "Beherbergungsräume", "Küchen in Wohnungen", "Gemeinschaftsräume",
                            "Aufenthaltsräume", "Bereitschaftsräume", "Pausenräume", "Teeküchen", "Ruheräume", "Warteräume", "Speiseräume", "Hafträume"};
                    break;
                case 1:
                    nutzungsarten = new List<string> { "Büroräume", "Großraumbüros", "Besprechungsräume", "Konstruktionsräume", "Zeichenräume", "Schalterräume", "Aufsichtsräume", "Bürogeräteräume" };
                    raumsitzplaetze.IsEnabled = true;
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

            raumnutzungsart.Items.Clear();

            foreach (string i in nutzungsarten)
            {
                raumnutzungsart.Items.Add(i);
            }


        }

        private void Comboboxlistraum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboboxlistraum.SelectedItem != null)
            {
                //listviewmobiliar.Items.Clear();
                Mobiliarlistfuellen();
                mobiliartextTyp.Clear();
                mobiliartextzustand.Clear();
                mobiliarBez.Clear();
                mobiliarAnz.Clear();
                mobiliarWert.Clear();

                mainRaum = (Raum)comboboxlistraum.SelectedItem;
            }
            
        }

        private void Comboboxmieter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tabbilanz.IsEnabled = true;
            tabgebauede.IsEnabled = true;
            tabraeume.IsEnabled = true;
            tabmobiliar.IsEnabled = true;
            gridgebauede.IsEnabled = true;
            Mieter m = (Mieter)comboboxmieter.SelectedItem;

            //---Reiter: Gebäude
            comboboxgebaeude.SelectedIndex = 0;
            textboxausgabespeichernladengebaeude.Clear();
            comboboxstockwerk.Items.Clear();
            textboxausgabespeichernstockwerk.Clear();
            LVraeume.Items.Clear();

            //---Reiter: Räume
            comboboxlistraum1.Items.Clear();
            raumid.Clear();
            raumtexttyp.Clear();
            //raumtyp.Items.Clear();
            raumtyp.SelectedIndex = -1;
            raumtextnutzungsart.Clear();
            raumnutzungsart.Items.Clear();
            raumbez.Clear();
            raumflaeche.Clear();
            raumsitzplaetze.Clear();

            foreach (Raum r in m.GemieteteRaeume)
            {
                comboboxlistraum1.Items.Add(r);
            }

            //---Reiter: Mobiliar
            comboboxlistraum.Items.Clear();
            listviewmobiliar.Items.Clear();
            mobiliartextTyp.Clear();
            //Mobiliartyp.Items.Clear();
            mobiliarBez.Clear();
            mobiliarAnz.Clear();
            mobiliarWert.Clear();
            mobiliartextzustand.Clear();
            //Mobiliarzustand.Items.Clear();

            foreach (Raum r in m.GemieteteRaeume)
            {
                comboboxlistraum.Items.Add(r);
            }


            //---Reiter: Blanz
            LVbilanz.Items.Clear();
            lGesamtmiete.Content = "#######";
        }


        //-----------------------------------------------------------------------------------------------------------
        //ListView-Methods
        private void Listviewmobiliar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Mobiliartyp.IsEnabled = true;
            mobiliarBez.IsEnabled = true;
            Mobiliarzustand.IsEnabled = true;
            if (listviewmobiliar.SelectedItem != null)
            {
                Mobiliar mob = (Mobiliar)listviewmobiliar.SelectedItem;
                //Filltextboxmobiliar(mob);
                mobiliarBez.Text = mob.Bez;
                mobiliarAnz.Text = mob.Anzahl.ToString();
                mobiliartextTyp.Text = mob.Typ;
                mobiliartextzustand.Text = mob.Zustand;
                mobiliarWert.Text = mob.AktuellerWert.ToString();
            }
        }

        private void ListView_DoubleClick_Event(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Mieter m = (Mieter)comboboxmieter.SelectedItem;
                int i = LVraeume.SelectedIndex;
                Geschoss geschoss = (Geschoss)comboboxstockwerk.SelectedItem;

                if (geschoss.ListRaeume[i].Mietzustand == false)
                {
                    geschoss.ListRaeume[i].Mietzustand = true;
                    m.GemieteteRaeume.Add(geschoss.ListRaeume[i]);
                }
                else if (geschoss.ListRaeume[i].Mietzustand == true)
                {
                    geschoss.ListRaeume[i].Mietzustand = false;
                    for (int s = 0; s < m.GemieteteRaeume.Count; s++)
                    {
                        m.GemieteteRaeume.Remove(m.GemieteteRaeume[s]);
                    }
                }

                RaeumeRefresh();
                ComboBoxRaumRefresh();
            }
            catch
            {
                MessageBox.Show("Bitte genau auf die Räume klicken", "Fehler!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }


        //-----------------------------------------------------------------------------------------------------------
        //Methods
        public void RaeumeRefresh()
        {
            LVraeume.Items.Clear();
            Geschoss g = (Geschoss)comboboxstockwerk.SelectedItem;

            foreach (Raum raum in g.ListRaeume)
            {
                //comboboxraum.Items.Add(raum).ToString();
                if (raum.Typ == "NUF1")
                {
                    LVraeume.Items.Add(new NUF1 { Id = raum.Id, Typ = raum.Typ, Nutzungsart = raum.Nutzungsart, Flaeche = raum.Flaeche, Mietzustand = raum.Mietzustand });
                }
                else if (raum.Typ == "NUF2")
                {
                    LVraeume.Items.Add(new NUF2 { Id = raum.Id, Typ = raum.Typ, Nutzungsart = raum.Nutzungsart, Flaeche = raum.Flaeche, Mietzustand = raum.Mietzustand });
                }
                else if (raum.Typ == "NUF3")
                {
                    LVraeume.Items.Add(new NUF3 { Id = raum.Id, Typ = raum.Typ, Nutzungsart = raum.Nutzungsart, Flaeche = raum.Flaeche, Mietzustand = raum.Mietzustand });
                }
                else if (raum.Typ == "NUF4")
                {
                    LVraeume.Items.Add(new NUF4 { Id = raum.Id, Typ = raum.Typ, Nutzungsart = raum.Nutzungsart, Flaeche = raum.Flaeche, Mietzustand = raum.Mietzustand });
                }
                else if (raum.Typ == "NUF5")
                {
                    LVraeume.Items.Add(new NUF5 { Id = raum.Id, Typ = raum.Typ, Nutzungsart = raum.Nutzungsart, Flaeche = raum.Flaeche, Mietzustand = raum.Mietzustand });
                }
                else if (raum.Typ == "NUF6")
                {
                    LVraeume.Items.Add(new NUF6 { Id = raum.Id, Typ = raum.Typ, Nutzungsart = raum.Nutzungsart, Flaeche = raum.Flaeche, Mietzustand = raum.Mietzustand });
                }
                else if (raum.Typ == "NUF7")
                {
                    LVraeume.Items.Add(new NUF7 { Id = raum.Id, Typ = raum.Typ, Nutzungsart = raum.Nutzungsart, Flaeche = raum.Flaeche, Mietzustand = raum.Mietzustand });
                }

            }

        }

        private void MieterBSP()
        {
            //Erstellen von Mietern
            mieterListe.Add(new Mieter("Youssef", "y@gmail.com", "0157 12345678"));
            mieterListe.Add(new Mieter("Ilias", "i@gmail.com", "0157 23456789"));
            mieterListe.Add(new Mieter("Ole", "o.gmail.com", "0157 34567890"));
            mieterListe.Add(new Mieter("Patrick", "p.gmail.com", "0157 22332233"));

            comboboxmieter.Items.Add(mieterListe[0]).ToString();
            comboboxmieter.Items.Add(mieterListe[1]).ToString();
            comboboxmieter.Items.Add(mieterListe[2]).ToString();
            comboboxmieter.Items.Add(mieterListe[3]).ToString();
        }

        private void Beispiele()
        {
            Random zufallsNr = new Random();

            MieterBSP();


            //Zufällige Anzahl an Gebäuden
            for (int i = 0; i < 5; i++)
            {

                string bez = "Beispiel " + i;
                string add = "Straße " + i;

                BindingList<Geschoss> geschosseBsp = new BindingList<Geschoss>();

                //Zufällige Anzahl Geschosse

                int anzahlGeschosse = zufallsNr.Next(1, 20);
                //textboxausgabespeichernstockwerk.Text += "Geschosse: " + anzahlGeschosse + "\n";

                for (int j = 0; j < anzahlGeschosse; j++)
                {
                    int GeschossNr = j;
                    string bezG = "Geschoss Nummer " + j;
                    double flaecheG = 100;

                    BindingList<Raum> raeumeBsp = new BindingList<Raum>();

                    //Zufällige Anzahl Räume

                    int anzahlRaeume = zufallsNr.Next(4, 10);
                    //textboxausgabespeichernstockwerk.Text += "Räume: " + anzahlRaeume + "\n";

                    for (int k = 0; k < anzahlRaeume; k++)
                    {
                        string idR = i + "." + j + "." + k;
                        string typR = RaumTypBestimmen(zufallsNr.Next(0, 7));
                        string nutzungsartR = RaumNutzungBestimmen(typR, zufallsNr);
                        string bezR = "Default Bezeichnung";
                        double flaecheR;

                        //Einfache Variante:
                        flaecheR = 100 / anzahlRaeume;
                        // flaecheR = RaumFlaecheBestimmen(anzahlRaeume, flaecheG);


                        Raum raum;
                        if (typR == "NUF1")
                        {
                            raum = new NUF1(idR, bezR, typR, nutzungsartR, flaecheR, false);
                            mobiliargenerieren(i, j, k, raum, zufallsNr);
                            raeumeBsp.Add(raum);
                        }
                        else if (typR == "NUF2")
                        {
                            raum = new NUF2(idR, bezR, typR, nutzungsartR, flaecheR, 42, false);
                            mobiliargenerieren(i, j, k, raum, zufallsNr);
                            raeumeBsp.Add(raum);
                        }
                        else if (typR == "NUF3")
                        {
                            raum = new NUF3(idR, bezR, typR, nutzungsartR, flaecheR, false);
                            mobiliargenerieren(i, j, k, raum, zufallsNr);
                            raeumeBsp.Add(raum);
                        }
                        else if (typR == "NUF4")
                        {
                            raum = new NUF4(idR, bezR, typR, nutzungsartR, flaecheR, false);
                            mobiliargenerieren(i, j, k, raum, zufallsNr);
                            raeumeBsp.Add(raum);
                        }
                        else if (typR == "NUF5")
                        {
                            raum = new NUF5(idR, bezR, typR, nutzungsartR, flaecheR, false);
                            mobiliargenerieren(i, j, k, raum, zufallsNr);
                            raeumeBsp.Add(raum);
                        }
                        else if (typR == "NUF6")
                        {
                            raum = new NUF6(idR, bezR, typR, nutzungsartR, flaecheR, false);
                            mobiliargenerieren(i, j, k, raum, zufallsNr);
                            raeumeBsp.Add(raum);
                        }
                        else if (typR == "NUF7")
                        {
                            raum = new NUF7(idR, bezR, typR, nutzungsartR, flaecheR, false);
                            mobiliargenerieren(i, j, k, raum, zufallsNr);
                            raeumeBsp.Add(raum);
                        }
                        else
                        {
                            MessageBox.Show("Der Fehler ist bei dem \nHinzufügen in die RaumBspListe!!", "Fehler Liste!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        //zufällige Anzahl Mobiliar
                        //List<Mobiliar> listmob = new List<Mobiliar>();
                        //Mobiliar mob;
                        //int AnzMobliar = zufallsNr.Next(1, 10);
                        //int zustandzufall = zufallsNr.Next(1, 3);
                        //for (int m = 0; m < AnzMobliar; m++)
                        //{
                        //    int Mobiliartyprandom = zufallsNr.Next(1, 3);
                        //    if (Mobiliartyprandom == 1)
                        //    {
                        //        mob = new Stuhl (bez, 1, MobiliarzustandBestimmen(zustandzufall));
                        //        raum.InvMoebel.Add(mob);

                        //    } else if (Mobiliartyprandom ==2) {
                        //        mob = new Tisch (bez, 1, MobiliarzustandBestimmen(zustandzufall));
                        //        raum.InvMoebel.Add(mob);

                        //    } else if (Mobiliartyprandom == 3) {
                        //        mob = new Konferenztisch(bez, 1, MobiliarzustandBestimmen(zustandzufall));
                        //        raum.InvMoebel.Add(mob);
                        //    }
                        //}
                    }


                    Geschoss geschoss = new Geschoss(bezG, flaecheG, raeumeBsp);

                    geschosseBsp.Add(geschoss);

                }

                Gebaeude gebaeudeBsp = new Gebaeude(bez, add, geschosseBsp);
                gebaeudeListe.Add(gebaeudeBsp);
            }


            foreach (Gebaeude g in gebaeudeListe)
            {
                comboboxgebaeude.Items.Add(g).ToString();

                //foreach (Geschoss gs in g.ListGeschosse1)t
                //{
                //    comboboxstockwerk.Items.Add(gs).ToString();
                //}

                //if(g is Gebaeude){
                //    Console.WriteLine(g.ToString() + " passt\n");
                //}
            }


        }

        private string mobiliaridgenerator(int g, int ges, int r, int m)
        {
            return g + "." + ges + "." + r + "." + m;
        }

        private void mobiliargenerieren(int g, int ges, int ro, Raum r, Random zufallsNr)
        {
            //Random zufallsNr = new Random();
            Mobiliar mob;
            int anzMobiliarGes = zufallsNr.Next(3, 10);

            string[] zustand = { "intakt", "beschädigt", "defekt" };

            for (int m = 0; m < anzMobiliarGes; m++)
            {
                int zustandzufall = zufallsNr.Next(0, 3);

                int Mobiliartyprandom = zufallsNr.Next(1, 3);
                if (Mobiliartyprandom == 1)
                {
                    mob = new Stuhl(mobiliaridgenerator(g, ges, ro, m), "Default Bezeichnung", 1, zustand[zustandzufall], "Stuhl");
                    r.InvMoebel.Add(mob);

                }
                else if (Mobiliartyprandom == 2)
                {
                    mob = new Tisch(mobiliaridgenerator(g, ges, ro, m), "Default Bezeichnung", 1, zustand[zustandzufall], "Tisch");
                    r.InvMoebel.Add(mob);

                }
                else if (Mobiliartyprandom == 3)
                {
                    mob = new Konferenztisch(mobiliaridgenerator(g, ges, ro, m), "Default Bezeichnung", 1, zustand[zustandzufall], "Konferenztisch");
                    r.InvMoebel.Add(mob);
                }
            }
        }

        public void Mobiliarlistfuellen()
        {

            Raum r = (Raum)comboboxlistraum.SelectedItem;
            listviewmobiliar.Items.Clear();
            foreach (Mobiliar mob in r.InvMoebel)
            {
                if (mob is Stuhl)
                {
                    listviewmobiliar.Items.Add(new Stuhl { Id = mob.Id, Bez = mob.Bez, Anzahl = mob.Anzahl, Zustand = mob.Zustand, AktuellerWert = mob.AktuellerWert });
                }
                else if (mob is Tisch)
                {
                    listviewmobiliar.Items.Add(new Tisch { Id = mob.Id, Bez = mob.Bez, Anzahl = mob.Anzahl, Zustand = mob.Zustand, AktuellerWert = mob.AktuellerWert });
                }
                else if (mob is Konferenztisch)
                {
                    listviewmobiliar.Items.Add(new Konferenztisch { Id = mob.Id, Bez = mob.Bez, Anzahl = mob.Anzahl, Zustand = mob.Zustand, AktuellerWert = mob.AktuellerWert });
                }
            }
        }

        public string RaumTypBestimmen(int raumTypNr)
        {
            //Random rmd = new Random();
            //int raumTypNr = rmd.Next(0, 7);
            string s = "";

            //Bestimmung des Raumtyps
            switch (raumTypNr)
            {
                case 0:
                    s = "NUF1";

                    break;
                case 1:
                    s = "NUF2";
                    break;
                case 2:
                    s = "NUF3";
                    break;
                case 3:
                    s = "NUF4";
                    break;
                case 4:
                    s = "NUF5";
                    break;
                case 5:
                    s = "NUF6";
                    break;
                case 6:
                    s = "NUF7";
                    break;
                default:
                    MessageBox.Show("Es wird keine NUF ausgewählt...", "Fehler du Pisser!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }

            return s;
        }

        public string RaumNutzungBestimmen(string typRaum, Random rmd)
        {
            string nutzungsart = "";
            //Random rmd = new Random();
            int nutzungsartLaenge;

            string[] nuf1 = {"Wohnräume", "Schlafräume", "Beherbergungsräume", "Küchen in Wohnungen", "Gemeinschaftsräume",
                             "Aufenthaltsräume", "Bereitschaftsräume", "Pausenräume", "Teeküchen", "Ruheräume", "Warteräume", "Speiseräume", "Hafträume" };

            string[] nuf2 = { "Büroräume", "Großraumbüros", "Besprechungsräume", "Konstruktionsräume", "Zeichenräume", "Schalterräume", "Aufsichtsräume", "Bürogeräteräume" };

            string[] nuf3 = {"Werkhallen", "Werkstätten", "Labors(technologische, physikalische, elektrotechnische, chemische, biologische usw.)",
                             "Räume für Tierhaltung", "Räume für Pflanzenzucht", "gewerbliche Küchen(einschließlich Aus - und Rückgaben)", "Sonderarbeitsräume(für Hauswirtschaft, Wäschepflege etc.)" };

            string[] nuf4 = {"Lager und Vorratsräume", "Lagerhallen", "Tresorräume", "Siloräume", "Archive", "Sammlungsräume", "Registraturen", "Kühlräume", "Annahme und Ausgaberäume",
                        "Packräume", "Versandräume", "Verkaufsräume", "Messeräume" };

            string[] nuf5 = {"Unterrichts- und Übungsräume", "Hörsäle", "Seminarräume", "Werkräume", "Praktikumsräume", "Bibliotheksräume", "Leseräume",
                        "Sporträume", "Gymnastikräume", "Zuschauerräume" , "Bühnenräume", "Studioräume", "Proberäume", "Ausstellungsräume", "Sakralräume" };

            string[] nuf6 = { "allgemeine Untersuchung und Behandlung", "spezielle Untersuchung und Behandlung", "Operationsräume", "Entbindungsräume",
                        "für Strahlendiagnostik und Strahlentherapie", "für Physiotherapie und Rehabilitation", "Bettenräume", "Intensivpflegeräume" };

            string[] nuf7 = { "Abstellräume", "Fahrradräume", "Müllsammelräume", "Fahrzeugabstellflächen" };

            //Bestimmung der Raumnutzung

            if (typRaum == "NUF1")
            {
                nutzungsartLaenge = rmd.Next(nuf1.Length - 1);
                nutzungsart = nuf1[nutzungsartLaenge];
            }
            else if (typRaum == "NUF2")
            {
                nutzungsartLaenge = rmd.Next(nuf2.Length - 1);
                nutzungsart = nuf2[nutzungsartLaenge];
            }
            else if (typRaum == "NUF3")
            {
                nutzungsartLaenge = rmd.Next(nuf3.Length - 1);
                nutzungsart = nuf3[nutzungsartLaenge];
            }
            else if (typRaum == "NUF4")
            {
                nutzungsartLaenge = rmd.Next(nuf4.Length - 1);
                nutzungsart = nuf4[nutzungsartLaenge];
            }
            else if (typRaum == "NUF5")
            {
                nutzungsartLaenge = rmd.Next(nuf5.Length - 1);
                nutzungsart = nuf5[nutzungsartLaenge];
            }
            else if (typRaum == "NUF6")
            {
                nutzungsartLaenge = rmd.Next(nuf6.Length - 1);
                nutzungsart = nuf6[nutzungsartLaenge];
            }
            else if (typRaum == "NUF7")
            {
                nutzungsartLaenge = rmd.Next(nuf7.Length - 1);
                nutzungsart = nuf7[nutzungsartLaenge];
            }
            else
            {
                MessageBox.Show("Fehler bei der Nutzungsart-Methode", "Fehler!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return nutzungsart;
        }

        public double RaumFlaecheBestimmen(int raumAnzahl, double geschossFlaeche)
        {
            //Es wird davon ausgegangenm,dass jedes Geschoss 100qm hat.
            //Das kann im nachhinein noch geändert werden.

            Random rmd = new Random();
            //double geschossFlaeche = 100;
            double raumFlaeche = rmd.Next(3, 50);
            double b = geschossFlaeche;


            if (b == 100)
            {
                b = b - raumFlaeche;
                textboxausgaberaum.Text += "Text1";
                return b;

            }
            else if (0 <= b && b <= 97)
            {
                if (raumFlaeche <= b)
                {
                    b = b - raumFlaeche;
                    textboxausgaberaum.Text += "Text2";
                    return b;
                }
                else
                {
                    int a = (int)Math.Round(b) - 1;
                    raumFlaeche = rmd.Next(3, a);
                    b = b - raumFlaeche;
                    textboxausgaberaum.Text += "Text3";
                    return b;
                }

            }
            else if (b == 0)
            {
                raumAnzahl--;
                textboxausgaberaum.Text += "Text4";
                return 0;

            }

            return b;
        }

        public void ComboBoxRaumRefresh()
        {
            Mieter m = (Mieter)comboboxmieter.SelectedItem;


            comboboxlistraum1.Items.Clear();
            comboboxlistraum.Items.Clear();



            foreach (Raum r in m.GemieteteRaeume)
            {

                comboboxlistraum1.Items.Add(r).ToString();
                comboboxlistraum.Items.Add(r).ToString();
            }
        }

        private void Filltextboxraum(Raum rm)
        {
            raumflaeche.Text = rm.Flaeche.ToString();
            raumid.Text = rm.Id;
            raumtextnutzungsart.Text = rm.Nutzungsart;
            raumtexttyp.Text = rm.Typ;
            raumnutzungsart.Text = rm.Nutzungsart;
            raumbez.Text = rm.Bez;
        }

        private void Filltextboxmobiliar(Mobiliar mob)
        {

            mobiliartextTyp.Text = mob.Typ;
            mobiliartextzustand.Text = mob.Zustand;
            mobiliarBez.Text = mob.Bez;
            mobiliarAnz.Text = mob.Anzahl.ToString();
            mobiliarWert.Text = mob.AktuellerWert.ToString();
        }

        public double PreisRaumMoebel(int k)
        {
            Mieter mieter = (Mieter)comboboxmieter.SelectedItem;
            double preis = 0.0;

            for (int i = 0; i < mieter.GemieteteRaeume[k].InvMoebel.Count; i++)
            {
                //tbSpeichern.Text += "Diese: " + mieter.GemieteteRaeume[k].InvMoebel[i].AktuellerWert + "\n";
                preis += mieter.GemieteteRaeume[k].InvMoebel[i].AktuellerWert;
            }

            return preis;
        }

        public double PreisRaumGes(int k)
        {
            Mieter mieter = (Mieter)comboboxmieter.SelectedItem;
            double preisF = mieter.GemieteteRaeume[k].Flaechenpreis;
            double preisM = PreisRaumMoebel(k);
            double preisGes;

            preisGes = preisF + preisM;

            return preisGes;
        }

        private void save()
        {

            FileStream fs = new FileStream("..\\..\\..\\Gebaeude.bin", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, gebaeudeListe);
            fs.Close();

            fs = new FileStream("..\\..\\..\\Mieter.bin", FileMode.Create);
            bf.Serialize(fs, mieterListe);
            fs.Close();
        }

        private void load()
        {
            FileStream fs = new FileStream("..\\..\\..\\Gebaeude.bin", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            gebaeudeListe = (BindingList<Gebaeude>)bf.Deserialize(fs);
            fs.Close();
            fs = new FileStream("..\\..\\..\\Mieter.bin", FileMode.Open);
            mieterListe = (BindingList<Mieter>)bf.Deserialize(fs);
            fs.Close();
        }


        //-----------------------------------------------------------------------------------------------------------
        //Extentional Methods
        private void Buttonneuraum_Click(object sender, RoutedEventArgs e)
        {
            new addRoom().ShowDialog();
        }

        private void Buttonneumieter_Click(object sender, RoutedEventArgs e)
        {
            new addTenant().ShowDialog();
        }

        private void Buttonneugebaeude_Click(object sender, RoutedEventArgs e)
        {
            new addBuilding().ShowDialog();
        }

        private void Buttonneustockwerk_Click(object sender, RoutedEventArgs e)
        {
            new addFloor().ShowDialog();
        }

        private void LVraeume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void Application_StartUP(Object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }




    }
}
