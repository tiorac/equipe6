﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Equipe6.Truco
{
    /// <summary>
    /// Lógica interna para Window1.xaml
    /// </summary>
    public partial class JogoTruco : Window
    {
        public JogoTruco()
        {
            InitializeComponent();
        }

        public Mesa MestaAtual
        {
            get
            {
                return (Mesa)this.DataContext;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}