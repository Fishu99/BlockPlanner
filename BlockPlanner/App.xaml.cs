using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BlockPlanner.Models;
using BlockPlanner.ViewModels;

namespace BlockPlanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Scheduler _scheduler;

        public App()
        {
            _scheduler = new Scheduler();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                //Should be MainViewModel even if checking other views
                DataContext = new MainViewModel(_scheduler)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
