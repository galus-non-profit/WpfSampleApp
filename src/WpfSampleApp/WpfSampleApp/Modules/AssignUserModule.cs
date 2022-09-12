namespace WpfSampleApp.Modules
{
    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Regions;
    using WpfSampleApp.Modules.Views;

    internal sealed class AssignUserModule : IModule
    {
        private readonly IRegionViewRegistry regionViewRegistry;

        public AssignUserModule(IRegionViewRegistry regionViewRegistry)
        {
            this.regionViewRegistry = regionViewRegistry;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<AssignUserView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            this.regionViewRegistry.RegisterViewWithRegion("UserRegion", typeof(AssignUserView));
        }
    }
}
