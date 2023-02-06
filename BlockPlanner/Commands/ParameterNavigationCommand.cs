using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockPlanner.Services;

namespace BlockPlanner.Commands
{
    public class ParameterNavigationCommand<TParam> : CommandBase
    {
        private readonly ParameterNavigationService<TParam> _parameterNavigationService;

        public ParameterNavigationCommand(ParameterNavigationService<TParam> navigationService)
        {
            this._parameterNavigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            if (typeof(TParam) == typeof(int))
            {
                var result = parameter?.ToString();
                if (!string.IsNullOrEmpty(result))
                {
                    parameter = int.Parse(result);
                }
                else
                {
                    throw new Exception("Couldn't convert conversion");
                }
            }
            _parameterNavigationService.Navigate((TParam) parameter);
        }
    }
}
