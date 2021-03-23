﻿#pragma checksum "..\..\..\..\ShoppingList\View\ShoppingListView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "68958D002A74AE42C95095E9FEBDE613200C45F18621DE40CD323BF2BEC5B458"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
using Nourriture.Inventory.ViewModel;
using Nourriture.ShoppingList.View;
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


namespace Nourriture.ShoppingList.View {
    
    
    /// <summary>
    /// ShoppingListView
    /// </summary>
    public partial class ShoppingListView : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\..\ShoppingList\View\ShoppingListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView mealsList;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\ShoppingList\View\ShoppingListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView productsList;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\ShoppingList\View\ShoppingListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock status;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\ShoppingList\View\ShoppingListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addProduct;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\ShoppingList\View\ShoppingListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button removeProduct;
        
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
            System.Uri resourceLocater = new System.Uri("/Nourriture;component/shoppinglist/view/shoppinglistview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\ShoppingList\View\ShoppingListView.xaml"
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
            this.mealsList = ((System.Windows.Controls.ListView)(target));
            
            #line 22 "..\..\..\..\ShoppingList\View\ShoppingListView.xaml"
            this.mealsList.SizeChanged += new System.Windows.SizeChangedEventHandler(this.mealsList_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.productsList = ((System.Windows.Controls.ListView)(target));
            
            #line 44 "..\..\..\..\ShoppingList\View\ShoppingListView.xaml"
            this.productsList.SizeChanged += new System.Windows.SizeChangedEventHandler(this.productsList_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.status = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.addProduct = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.removeProduct = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

