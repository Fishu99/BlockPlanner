using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using BlockPlanner.Models;
using BlockPlanner.Services;
using BlockPlanner.Stores;
using BlockPlanner.ViewModels;
using Application = System.Windows.Application;

namespace BlockPlanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Scheduler _scheduler;
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _scheduler = new Scheduler();
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            /* VIEW SELECTOR */
            // _navigationStore.CurrentViewModel = CreatePlanSettingsViewModel();
            // _navigationStore.CurrentViewModel = CreatePlanDetailsViewModel();
            _navigationStore.CurrentViewModel = CreateMainMenuViewModel();


            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_scheduler, _navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }

        private PlanDetailsViewModel CreatePlanDetailsViewModel()
        {
            return new PlanDetailsViewModel(new NavigationService(_navigationStore, CreateMainMenuViewModel), new NavigationService(_navigationStore, CreateModifyPlanSettingsViewModel));
        }

        private MainMenuViewModel CreateMainMenuViewModel()
        {
            return new MainMenuViewModel(new NavigationService(_navigationStore, CreateAddPlanSettingsViewModel), new NavigationService(_navigationStore, CreatePlanDetailsViewModel));
        }

        private PlanSettingsViewModel CreateAddPlanSettingsViewModel()
        {
            var testPlan = MainViewModel.SimulationInvoke();
            return new PlanSettingsViewModel(testPlan, PlanCreatorMode.Add, new NavigationService(_navigationStore, CreateMainMenuViewModel), new NavigationService(_navigationStore, CreatePlanDetailsViewModel));
        }

        private PlanSettingsViewModel CreateModifyPlanSettingsViewModel()
        {
            var testPlan = MainViewModel.SimulationInvoke();
            return new PlanSettingsViewModel(testPlan, PlanCreatorMode.Modify, new NavigationService(_navigationStore, CreateMainMenuViewModel), new NavigationService(_navigationStore, CreatePlanDetailsViewModel));
        }
    }
}
