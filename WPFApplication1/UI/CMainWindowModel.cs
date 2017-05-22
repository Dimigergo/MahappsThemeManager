// --------------------------------
// <copyright file="CWPFApplication1WindowModel.cs" company="MLRTech Ltd.">
//     (c) Copyright 2013, MLRTech Ltd, Budapest, HU. All rights reserved.
// </copyright>
// <author>pczegledi</author>
// <date>2015-11-18</date>
// ---------------------------------
using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace WPFApplication1.UI
{
    internal class CMainWindowModel : DependencyObject, IDisposable
    {
        #region Types

        public enum EThemeAccents
        {
            Red,
            Amber,
            Emerald,
        }

        #endregion

        #region Private Members

        private int _i;

        #endregion

        #region Private Methods

        #region Logs

        private void Info(string info)
        {
            CLog.Log(DateTime.Now, info);
        }

        #endregion
        
        private void ThreadProc()
        {
            if (this._i > 2)
                this._i = 0;

            EThemeAccents newTheme = (EThemeAccents)this._i;

            this.Info("Changing theme to " + newTheme.ToString());
            this.ChangeTheme(newTheme);

            this._i++;
        }

        private void ChangeTheme(EThemeAccents themeAccent)
        {
            if (!Dispatcher.CheckAccess())
                Dispatcher.BeginInvoke(new Action<EThemeAccents>(this.ChangeTheme), new object[] { themeAccent });
            else
            {
                Accent newAccent = ThemeManager.GetAccent(themeAccent.ToString());
                if (newAccent != null)
                {
                    var theme = ThemeManager.DetectAppStyle(Application.Current);
                    ThemeManager.ChangeAppStyle(Application.Current, newAccent, theme.Item1);
                    this.Info("Change theme: NewTheme: " + newAccent.Name + " Theme changed.");
                }
                else
                    this.Info("Change theme: Theme not found: " + themeAccent.ToString() + ".");
            }
        }

        private void ThemeManagerIsThemeChangedHandler(object sender, OnThemeChangedEventArgs e)
        {
            try
            {
                var theme = ThemeManager.DetectAppStyle(Application.Current);
                this.Info("Current theme from args: " + e.Accent.Name);
                this.Info("Current theme from detection: " + theme.Item2.Name);
            }
            catch(Exception ex)
            {
                this.Info(ex.Message);
            }
        }

        #endregion

        #region Constructor & Destructor

        internal CMainWindowModel()
        {
            try
            {
                if (!DesignerProperties.GetIsInDesignMode(this))
                {
                    ThemeManager.IsThemeChanged += this.ThemeManagerIsThemeChangedHandler;

                    Task.Run(() => {

                        while (true)
                        {
                            Thread.Sleep(5000);
                            this.ThreadProc();
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                this.Info(ex.Message);
            }
        }        

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
