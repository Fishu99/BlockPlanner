using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockPlanner.Stores;
using BlockPlanner.ViewModels;

namespace BlockPlanner.Services
{
    public class ParameterNavigationService<TParam>
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TParam, ViewModelBase> _createViewModel;

        public ParameterNavigationService(NavigationStore navigationStore, Func<TParam, ViewModelBase> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate(TParam parameter)
        {
            _navigationStore.CurrentViewModel = _createViewModel(parameter);
        }
    }
}
