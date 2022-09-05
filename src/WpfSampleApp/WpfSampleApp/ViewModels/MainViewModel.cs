namespace WpfSampleApp.ViewModels
{
    using System;
    using System.Windows.Input;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Regions;
    using WpfSampleApp.Modules.Views;

    internal sealed class MainViewModel : BindableBase, IDisposable
    {
        private volatile bool isDisposed = default;
        private string title = "Wpf Sample App";

        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;

        public string Title
        {
            get => this.title;
            set => this.SetProperty(ref this.title, value);
        }

        public MainViewModel()
        {

        }

        public MainViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
            : this()
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
        }

        ~MainViewModel()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ICommand ShowUserViewCommand =>
            new DelegateCommand<object>(o =>
            {
                this.regionManager.RequestNavigate("UserRegion", new Uri(nameof(UserView), UriKind.RelativeOrAbsolute));

            });

        public ICommand ShowCustomerViewCommand =>
            new DelegateCommand<object>(o =>
            {
                this.regionManager.RequestNavigate("UserRegion", new Uri(nameof(CustomerView), UriKind.RelativeOrAbsolute));

            });

        private void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (disposing)
            {
                //this.eventAggregator.GetEvent<NewApplicationFormsCountEvent>()
                //    .Unsubscribe(this.newApplicationFormsToken);
            }

            this.isDisposed = true;
        }
    }
}
