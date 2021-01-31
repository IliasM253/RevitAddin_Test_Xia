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

namespace IIB1_UE1_Gruppe05_AbgabeCP.Windows
{
    /// <summary>
    /// Interaktionslogik für addTenant.xaml
    /// </summary>
    public partial class addTenant : Window
    {
        public addTenant()
        {
            InitializeComponent();
        }

        private void Mobiliarok_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Mieterabbrechen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
