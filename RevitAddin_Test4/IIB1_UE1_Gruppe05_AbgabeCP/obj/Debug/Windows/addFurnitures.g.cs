﻿#pragma checksum "..\..\..\Windows\addFurnitures.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "903840AC4F1D53752999474038FBFD6EF63B417DDADA6EEB340A66EB1688130F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using IIB1_UE1_Gruppe05_AbgabeCP.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace IIB1_UE1_Gruppe05_AbgabeCP.Windows {
    
    
    /// <summary>
    /// addFurnitures
    /// </summary>
    public partial class addFurnitures : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 38 "..\..\..\Windows\addFurnitures.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Mobiliartyp;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Windows\addFurnitures.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox mobiliarBez;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Windows\addFurnitures.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox mobiliarAnz;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\Windows\addFurnitures.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox mobiliarWert;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\Windows\addFurnitures.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Mobiliarzustand;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\Windows\addFurnitures.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Mobiliarok;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\Windows\addFurnitures.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Mobiliarabbrechen;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/IIB1_UE1_Gruppe05_AbgabeCP;component/windows/addfurnitures.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\addFurnitures.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Mobiliartyp = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.mobiliarBez = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.mobiliarAnz = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.mobiliarWert = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.Mobiliarzustand = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.Mobiliarok = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.Mobiliarabbrechen = ((System.Windows.Controls.Button)(target));
            
            #line 100 "..\..\..\Windows\addFurnitures.xaml"
            this.Mobiliarabbrechen.Click += new System.Windows.RoutedEventHandler(this.Mobiliarabbrechen_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

