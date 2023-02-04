using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using BlockPlanner.Models;
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
            return new PlanDetailsViewModel(_navigationStore, CreateMainMenuViewModel, CreateModifyPlanSettingsViewModel);
        }

        private MainMenuViewModel CreateMainMenuViewModel()
        {
            return new MainMenuViewModel(_navigationStore, CreatePlanDetailsViewModel, CreateAddPlanSettingsViewModel);
        }

        private PlanSettingsViewModel CreateAddPlanSettingsViewModel()
        {
            var testPlan = MainViewModel.SimulationInvoke();
            return new PlanSettingsViewModel(_navigationStore, testPlan, PlanCreatorMode.Add, CreateMainMenuViewModel, CreatePlanDetailsViewModel);
        }

        private PlanSettingsViewModel CreateModifyPlanSettingsViewModel()
        {
            var testPlan = MainViewModel.SimulationInvoke();
            return new PlanSettingsViewModel(_navigationStore, testPlan, PlanCreatorMode.Modify, CreateMainMenuViewModel, CreatePlanDetailsViewModel);
        }
    }
}
