using CityDataUpdater.Services;
using CityDataUpdater.ViewModels;
using CityDataUpdater.Views;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CityDataUpdater
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            // Register services
            Locator.CurrentMutable.RegisterConstant<ICityProvider>(new CityProvider());

            // Register viewmodel
            Locator.CurrentMutable.RegisterConstant<ICardListVM>(new CardListVM());
            Locator.CurrentMutable.RegisterConstant<IToggleVM>(new ToggleViewModel());

            // Register viewmodel with view
            Locator.CurrentMutable.Register<IViewFor<ICardVM>>(() => new CityCard());

            var windows = new MainWindow();
            windows.Show();
        }
    }
}
