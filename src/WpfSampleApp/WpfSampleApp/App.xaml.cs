namespace WpfSampleApp
{
    using System.Windows;
    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Unity;
    using WpfSampleApp.Interfaces;
    using WpfSampleApp.Modules;
    using WpfSampleApp.Services;
    using WpfSampleApp.Views;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule(typeof(CustomerModule));
            moduleCatalog.AddModule(typeof(UserModule));
        }

        protected override Window CreateShell()
            => this.Container.Resolve<MainView>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IShellService>(this.Container.Resolve<ShellService>());
        }
    }
}
