namespace WpfSampleApp.Modules
{
    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Regions;
    using WpfSampleApp.Modules.Views;

    internal sealed class UserModule : IModule
    {
        private readonly IRegionViewRegistry regionViewRegistry;

        public UserModule(IRegionViewRegistry regionViewRegistry)
        {
            this.regionViewRegistry = regionViewRegistry;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<UserView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            this.regionViewRegistry.RegisterViewWithRegion("UserRegion", typeof(UserView));
        }
    }
}
