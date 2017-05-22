// --------------------------------
// <copyright file="App.xaml.cs" company="MLRTech Ltd.">
//     (c) Copyright 2013, MLRTech Ltd, Budapest, HU. All rights reserved.
// </copyright>
// <author>pczegledi</author>
// <date>2015-11-18</date>
// ---------------------------------
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace WPFApplication1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        internal static void Main()
        {
            var application = new App();
            application.Init();
            application.Run();
        }

        internal void Init()
        {
            this.InitializeComponent();
        }
    }
}
