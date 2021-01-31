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
using System.Windows.Shapes;
using IIB1_UE1_Gruppe05_AbgabeCP.Klassen;
using IIB1_UE1_Gruppe05_AbgabeCP;

namespace IIB1_UE1_Gruppe05_AbgabeCP.Windows
{
    /// <summary>
    /// Interaktionslogik für addBuilding.xaml
    /// </summary>
    public partial class addBuilding : Window
    {

        //private List<Gebaeude> a = new List<Gebaeude>();

        public addBuilding()
        {
            InitializeComponent();
        }

        private void gebaeudeabbrechen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




        private void Gebaeudeok_Click(object sender, RoutedEventArgs e)
        {
            /* string bez = gebaeudeBez.Text;
             string add = gebaeudeadresse.Text;

             Gebaeude neuGebaeude = new Gebaeude(bez, add, new List<Geschoss>());



             a.Add(neuGebaeude);*/

        }
    }
}
