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
using System.Windows.Shapes;
using PetShop.Views.GoodsView;

namespace PetShop.Views.Temp
{
    /// <summary>
    /// Interaction logic for CountForm.xaml
    /// </summary>
    public partial class CountForm : Window
    {

        public CountForm()
        {
            InitializeComponent();

        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        void PassInt(Object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int iValue = -1;

            if (Int32.TryParse(textBox.Text, out iValue) == false)
            {
                TextChange textChange = e.Changes.ElementAt<TextChange>(0);
                int iAddedLength = textChange.AddedLength;
                int iOffset = textChange.Offset;
                textBox.Text = textBox.Text.Remove(iOffset, iAddedLength);
            }
        }

        private void txtCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            PassInt(sender, e);
        }
        public string Count
        {
            get { return txtCount.Text; }
        }
    }
}
