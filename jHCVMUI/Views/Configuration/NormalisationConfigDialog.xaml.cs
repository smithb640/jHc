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

namespace jHCVMUI.Views.Configuration
{
  /// <summary>
  /// Interaction logic for NormalisationConfigDialog.xaml
  /// </summary>
  public partial class NormalisationConfigDialog : Window
  {
    public NormalisationConfigDialog()
    {
      InitializeComponent();
    }

    private void OkClick(object sender, RoutedEventArgs e)
    {
      this.DialogResult = true;
    }
  }
}
