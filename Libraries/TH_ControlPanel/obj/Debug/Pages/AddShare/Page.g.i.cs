﻿#pragma checksum "..\..\..\..\Pages\AddShare\Page.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "22E9F72A0ABDC93E8762EA9CC0DC4F09"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using TH_WPF;
using TH_WPF.LoadingAnimation;


namespace TH_DeviceManager.Pages.AddShare {
    
    
    /// <summary>
    /// Page
    /// </summary>
    public partial class Page : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 129 "..\..\..\..\Pages\AddShare\Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox description_TXT;
        
        #line default
        #line hidden
        
        
        #line 226 "..\..\..\..\Pages\AddShare\Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox devicetype_TXT;
        
        #line default
        #line hidden
        
        
        #line 323 "..\..\..\..\Pages\AddShare\Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox manufacturer_TXT;
        
        #line default
        #line hidden
        
        
        #line 419 "..\..\..\..\Pages\AddShare\Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox model_TXT;
        
        #line default
        #line hidden
        
        
        #line 516 "..\..\..\..\Pages\AddShare\Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox controller_TXT;
        
        #line default
        #line hidden
        
        
        #line 612 "..\..\..\..\Pages\AddShare\Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox author_TXT;
        
        #line default
        #line hidden
        
        
        #line 708 "..\..\..\..\Pages\AddShare\Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox version_TXT;
        
        #line default
        #line hidden
        
        
        #line 804 "..\..\..\..\Pages\AddShare\Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tags_TXT;
        
        #line default
        #line hidden
        
        
        #line 900 "..\..\..\..\Pages\AddShare\Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox dependencies_TXT;
        
        #line default
        #line hidden
        
        
        #line 997 "..\..\..\..\Pages\AddShare\Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border image_BD;
        
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
            System.Uri resourceLocater = new System.Uri("/TH_DeviceManager;component/pages/addshare/page.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\AddShare\Page.xaml"
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
            
            #line 53 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Help_MouseDown);
            
            #line default
            #line hidden
            
            #line 53 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.Help_MouseEnter);
            
            #line default
            #line hidden
            
            #line 53 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.Help_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 2:
            this.description_TXT = ((System.Windows.Controls.TextBox)(target));
            
            #line 129 "..\..\..\..\Pages\AddShare\Page.xaml"
            this.description_TXT.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.description_TXT_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 149 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Help_MouseDown);
            
            #line default
            #line hidden
            
            #line 149 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.Help_MouseEnter);
            
            #line default
            #line hidden
            
            #line 149 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.Help_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 4:
            this.devicetype_TXT = ((System.Windows.Controls.TextBox)(target));
            
            #line 226 "..\..\..\..\Pages\AddShare\Page.xaml"
            this.devicetype_TXT.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.devicetype_TXT_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 246 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Help_MouseDown);
            
            #line default
            #line hidden
            
            #line 246 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.Help_MouseEnter);
            
            #line default
            #line hidden
            
            #line 246 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.Help_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 6:
            this.manufacturer_TXT = ((System.Windows.Controls.TextBox)(target));
            
            #line 323 "..\..\..\..\Pages\AddShare\Page.xaml"
            this.manufacturer_TXT.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.manufacturer_TXT_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 343 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Help_MouseDown);
            
            #line default
            #line hidden
            
            #line 343 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.Help_MouseEnter);
            
            #line default
            #line hidden
            
            #line 343 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.Help_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 8:
            this.model_TXT = ((System.Windows.Controls.TextBox)(target));
            
            #line 419 "..\..\..\..\Pages\AddShare\Page.xaml"
            this.model_TXT.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.model_TXT_TextChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 439 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Help_MouseDown);
            
            #line default
            #line hidden
            
            #line 439 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.Help_MouseEnter);
            
            #line default
            #line hidden
            
            #line 439 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.Help_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 10:
            this.controller_TXT = ((System.Windows.Controls.TextBox)(target));
            
            #line 516 "..\..\..\..\Pages\AddShare\Page.xaml"
            this.controller_TXT.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.controller_TXT_TextChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 536 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Help_MouseDown);
            
            #line default
            #line hidden
            
            #line 536 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.Help_MouseEnter);
            
            #line default
            #line hidden
            
            #line 536 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.Help_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 12:
            this.author_TXT = ((System.Windows.Controls.TextBox)(target));
            
            #line 612 "..\..\..\..\Pages\AddShare\Page.xaml"
            this.author_TXT.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.author_TXT_TextChanged);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 632 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Help_MouseDown);
            
            #line default
            #line hidden
            
            #line 632 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.Help_MouseEnter);
            
            #line default
            #line hidden
            
            #line 632 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.Help_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 14:
            this.version_TXT = ((System.Windows.Controls.TextBox)(target));
            
            #line 708 "..\..\..\..\Pages\AddShare\Page.xaml"
            this.version_TXT.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.version_TXT_TextChanged);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 728 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Help_MouseDown);
            
            #line default
            #line hidden
            
            #line 728 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.Help_MouseEnter);
            
            #line default
            #line hidden
            
            #line 728 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.Help_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 16:
            this.tags_TXT = ((System.Windows.Controls.TextBox)(target));
            
            #line 804 "..\..\..\..\Pages\AddShare\Page.xaml"
            this.tags_TXT.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tags_TXT_TextChanged);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 824 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Help_MouseDown);
            
            #line default
            #line hidden
            
            #line 824 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.Help_MouseEnter);
            
            #line default
            #line hidden
            
            #line 824 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.Help_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 18:
            this.dependencies_TXT = ((System.Windows.Controls.TextBox)(target));
            
            #line 900 "..\..\..\..\Pages\AddShare\Page.xaml"
            this.dependencies_TXT.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.dependencies_TXT_TextChanged);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 910 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((TH_WPF.Button_01)(target)).Clicked += new TH_WPF.Button_01.Clicked_Handler(this.Share_Clicked);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 924 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Help_MouseDown);
            
            #line default
            #line hidden
            
            #line 924 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.Help_MouseEnter);
            
            #line default
            #line hidden
            
            #line 924 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.Help_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 21:
            this.image_BD = ((System.Windows.Controls.Border)(target));
            return;
            case 22:
            
            #line 1003 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Controls.Border)(target)).PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Upload_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            case 23:
            
            #line 1158 "..\..\..\..\Pages\AddShare\Page.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Clear_MouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

