﻿#pragma checksum "..\..\..\Windows\addTenant.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E362C750DB0340203459543913C68628D21F4FE529F68EC642D5544C34FA8E45"
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
    /// addTenant
    /// </summary>
    public partial class addTenant : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\..\Windows\addTenant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox geschlecht;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Windows\addTenant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox mietername;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Windows\addTenant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox telefon;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Windows\addTenant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox email;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\Windows\addTenant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Mieterok;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\Windows\addTenant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Mieterabbrechen;
        
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
            System.Uri resourceLocater = new System.Uri("/IIB1_UE1_Gruppe05_AbgabeCP;component/windows/addtenant.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\addTenant.xaml"
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
            this.geschlecht = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.mietername = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.telefon = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.email = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.Mieterok = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.Mieterabbrechen = ((System.Windows.Controls.Button)(target));
            
            #line 83 "..\..\..\Windows\addTenant.xaml"
            this.Mieterabbrechen.Click += new System.Windows.RoutedEventHandler(this.Mieterabbrechen_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

