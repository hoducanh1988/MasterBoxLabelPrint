﻿#pragma checksum "..\..\..\MyUserControl\ucRunAll.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "63493FF3645FFE8D3348DEA3AD83D7057FD04579"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MasterBoxLabelPrint_Ver1.MyUserControl;
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


namespace MasterBoxLabelPrint_Ver1.MyUserControl {
    
    
    /// <summary>
    /// ucRunAll
    /// </summary>
    public partial class ucRunAll : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 52 "..\..\..\MyUserControl\ucRunAll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid _grid_testing;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\MyUserControl\ucRunAll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_SN;
        
        #line default
        #line hidden
        
        
        #line 192 "..\..\..\MyUserControl\ucRunAll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid _grid_setting;
        
        #line default
        #line hidden
        
        
        #line 293 "..\..\..\MyUserControl\ucRunAll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_lot_name;
        
        #line default
        #line hidden
        
        
        #line 295 "..\..\..\MyUserControl\ucRunAll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_search_datalog_sn;
        
        #line default
        #line hidden
        
        
        #line 297 "..\..\..\MyUserControl\ucRunAll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbb_list_result;
        
        #line default
        #line hidden
        
        
        #line 301 "..\..\..\MyUserControl\ucRunAll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid datagrid_recentdatalog;
        
        #line default
        #line hidden
        
        
        #line 331 "..\..\..\MyUserControl\ucRunAll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_printed_lot;
        
        #line default
        #line hidden
        
        
        #line 333 "..\..\..\MyUserControl\ucRunAll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_search_printed_sn;
        
        #line default
        #line hidden
        
        
        #line 339 "..\..\..\MyUserControl\ucRunAll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid datagrid_recentproduct;
        
        #line default
        #line hidden
        
        
        #line 367 "..\..\..\MyUserControl\ucRunAll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid datagrid_recentlot;
        
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
            System.Uri resourceLocater = new System.Uri("/MasterBoxLabelPrint_Ver1;component/myusercontrol/ucrunall.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MyUserControl\ucRunAll.xaml"
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
            this._grid_testing = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.txt_SN = ((System.Windows.Controls.TextBox)(target));
            
            #line 69 "..\..\..\MyUserControl\ucRunAll.xaml"
            this.txt_SN.KeyDown += new System.Windows.Input.KeyEventHandler(this.Txt_SN_KeyDown);
            
            #line default
            #line hidden
            
            #line 70 "..\..\..\MyUserControl\ucRunAll.xaml"
            this.txt_SN.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Txt_SN_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this._grid_setting = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            
            #line 290 "..\..\..\MyUserControl\ucRunAll.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txt_lot_name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txt_search_datalog_sn = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.cbb_list_result = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.datagrid_recentdatalog = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 9:
            
            #line 329 "..\..\..\MyUserControl\ucRunAll.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.txt_printed_lot = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.txt_search_printed_sn = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            this.datagrid_recentproduct = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 13:
            
            #line 359 "..\..\..\MyUserControl\ucRunAll.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.datagrid_recentlot = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 15:
            
            #line 370 "..\..\..\MyUserControl\ucRunAll.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

