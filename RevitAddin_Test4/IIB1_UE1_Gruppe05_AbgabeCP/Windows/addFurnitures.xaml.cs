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

namespace IIB1_UE1_Gruppe05_AbgabeCP.Windows
{
    /// <summary>
    /// Interaktionslogik für addFurnitures.xaml
    /// </summary>
    public partial class addFurnitures : Window
    {
        public addFurnitures()
        {
            InitializeComponent();
        }

        private void Mobiliarabbrechen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
