﻿#pragma checksum "..\..\ModPackItems.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8EA7F3874D0899409853FA8700197CAF18E991525C30CFA22D146C956072BC75"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using Nexus;
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


namespace Nexus {
    
    
    /// <summary>
    /// ModPackItems
    /// </summary>
    public partial class ModPackItems : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\ModPackItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border template;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\ModPackItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ImageBrush imgShow;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\ModPackItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock title;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\ModPackItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border showdeitals;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\ModPackItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtmods;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\ModPackItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button install;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\ModPackItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border showversion;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\ModPackItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock versionstxt;
        
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
            System.Uri resourceLocater = new System.Uri("/Nexus;component/modpackitems.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ModPackItems.xaml"
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
            this.template = ((System.Windows.Controls.Border)(target));
            
            #line 7 "..\..\ModPackItems.xaml"
            this.template.Loaded += new System.Windows.RoutedEventHandler(this.template_Loaded);
            
            #line default
            #line hidden
            
            #line 7 "..\..\ModPackItems.xaml"
            this.template.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.template_MouseRightButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.imgShow = ((System.Windows.Media.ImageBrush)(target));
            return;
            case 3:
            this.title = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.showdeitals = ((System.Windows.Controls.Border)(target));
            return;
            case 5:
            
            #line 17 "..\..\ModPackItems.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtmods = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.install = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\ModPackItems.xaml"
            this.install.Click += new System.Windows.RoutedEventHandler(this.installbtn_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.showversion = ((System.Windows.Controls.Border)(target));
            return;
            case 9:
            this.versionstxt = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
