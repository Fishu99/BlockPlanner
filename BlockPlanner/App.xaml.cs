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
            _scheduler.Plans.Add(MainViewModel.SimulationInvoke());
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

        private PlanDetailsViewModel CreatePlanDetailsViewModel(int i)
        {
            return new PlanDetailsViewModel(_scheduler,
                i,
                new NavigationService(_navigationStore, CreateMainMenuViewModel),
                new ParameterNavigationService<int>(_navigationStore,
                    CreateModifyPlanSettingsViewModel));
        }

        private MainMenuViewModel CreateMainMenuViewModel()
        {
            return new MainMenuViewModel(_scheduler, new NavigationService(_navigationStore, CreateAddPlanSettingsViewModel), new ParameterNavigationService<int>(_navigationStore, CreatePlanDetailsViewModel));
        }

        private PlanSettingsViewModel CreateAddPlanSettingsViewModel()
        {
            // var testPlan = _scheduler.Plans.Count==0 ? MainViewModel.SimulationInvoke() : _scheduler.Plans[0]; //TODO delete
            return new PlanSettingsViewModel(_scheduler, null, PlanCreatorMode.Add, 0, new NavigationService(_navigationStore, CreateMainMenuViewModel), new ParameterNavigationService<int>(_navigationStore, CreatePlanDetailsViewModel));
        }

        private PlanSettingsViewModel CreateModifyPlanSettingsViewModel(int i)
        {
            var testPlan = _scheduler.Plans.Count == 0 ? MainViewModel.SimulationInvoke() : _scheduler.Plans[0];//TODO delete
            return new PlanSettingsViewModel(_scheduler, testPlan, PlanCreatorMode.Modify, i, new NavigationService(_navigationStore, CreateMainMenuViewModel), new ParameterNavigationService<int>(_navigationStore, CreatePlanDetailsViewModel));
        }
    }
}
