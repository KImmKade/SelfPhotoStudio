﻿#pragma checksum "..\..\..\View\ChoicePayment.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "35F6B6A69F90C06D23E3E90DD834CC75B69D68F6DCAB522D0E59704DEA498EC5"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
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
using wpfTest.View;


namespace wpfTest.View {
    
    
    /// <summary>
    /// ChoicePayment
    /// </summary>
    public partial class ChoicePayment : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\View\ChoicePayment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image backimg;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\View\ChoicePayment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image cardimg;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\View\ChoicePayment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image cashimg;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\View\ChoicePayment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image BackImg;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\View\ChoicePayment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image NextImg;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\View\ChoicePayment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Timer;
        
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
            System.Uri resourceLocater = new System.Uri("/Onecut;component/view/choicepayment.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\ChoicePayment.xaml"
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
            
            #line 11 "..\..\..\View\ChoicePayment.xaml"
            ((System.Windows.Controls.Grid)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Grid_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.backimg = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.cardimg = ((System.Windows.Controls.Image)(target));
            
            #line 13 "..\..\..\View\ChoicePayment.xaml"
            this.cardimg.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.cardimg_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cashimg = ((System.Windows.Controls.Image)(target));
            
            #line 14 "..\..\..\View\ChoicePayment.xaml"
            this.cashimg.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.cashimg_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BackImg = ((System.Windows.Controls.Image)(target));
            
            #line 15 "..\..\..\View\ChoicePayment.xaml"
            this.BackImg.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.BackImg_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.NextImg = ((System.Windows.Controls.Image)(target));
            
            #line 16 "..\..\..\View\ChoicePayment.xaml"
            this.NextImg.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.NextImg_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Timer = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

