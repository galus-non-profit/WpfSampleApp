namespace WpfSampleApp.Modules
{
    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Regions;
    using WpfSampleApp.Modules.Views;

    internal sealed class CustomerModule : IModule
    {
        private readonly IRegionViewRegistry regionViewRegistry;

        public CustomerModule(IRegionViewRegistry regionViewRegistry)
        {
            this.regionViewRegistry = regionViewRegistry;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CustomerView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            this.regionViewRegistry.RegisterViewWithRegion("UserRegion", typeof(CustomerView));
        }
    }
}
