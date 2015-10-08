﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TH_WPF
{
    /// <summary>
    /// Interaction logic for ListButton.xaml
    /// </summary>
    public partial class ListButton : UserControl
    {
        public ListButton()
        {
            InitializeComponent();
            DataContext = this;
        }

        public object DataObject;

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ListButton), new PropertyMetadata(null));


        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(ListButton), new PropertyMetadata(null));


        public delegate void Selected_Handler(ListButton LB);
        public event Selected_Handler Selected;
        public event Selected_Handler MultiSelected;
        public event Selected_Handler MultiUnselected;

        private void Main_GRID_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (MultiSelected != null) MultiSelected(this);

                Selected_Handler handler = Selected;
                if (handler != null) Selected(this);
            }
        }

        public bool IsSelected
        {
            get { return (bool)this.GetValue(IsSelectedProperty); }
            set { this.SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.RegisterAttached(
            "IsSelected", typeof(bool), typeof(ListButton), new PropertyMetadata(false));


        public bool MouseOver
        {
            get { return (bool)GetValue(MouseOverProperty); }
            set { SetValue(MouseOverProperty, value); }
        }

        public static readonly DependencyProperty MouseOverProperty =
            DependencyProperty.Register("MouseOver", typeof(bool), typeof(ListButton), new PropertyMetadata(false));


        private void Main_GRID_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!IsSelected)
                {
                    if (MultiSelected != null) MultiSelected(this);
                }
                else
                {
                    if (MultiUnselected != null) MultiUnselected(this);
                }
            }
                
            MouseOver = true;
        }

        private void Main_GRID_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseOver = false;
        }

    }
}