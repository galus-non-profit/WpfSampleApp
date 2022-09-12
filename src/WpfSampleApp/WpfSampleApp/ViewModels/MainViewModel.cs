namespace WpfSampleApp.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;
    using MvvmDialogs;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Regions;
    using WpfSampleApp.Modules.Views;
    using WpfSampleApp.Views;

    internal sealed class MainViewModel : BindableBase, IDisposable
    {
        private volatile bool isDisposed = default;

        private bool isClose = false;
        private string title = "Wpf Sample App";

        private readonly IDialogService dialogService;
        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;

        public bool IsClose
        {
            get => this.isClose;
            set => this.SetProperty(ref this.isClose, value);
        }

        public string Title
        {
            get => this.title;
            set => this.SetProperty(ref this.title, value);
        }

        public MainViewModel()
        {

        }

        public MainViewModel(IDialogService dialogService, IEventAggregator eventAggregator, IRegionManager regionManager)
            : this()
        {
            this.dialogService = dialogService;
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

        public ICommand FileExitCommand => new DelegateCommand(() => { this.IsClose = true; });

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

        public ICommand ShowEditUserViewCommand => new DelegateCommand<object>(o =>
        {
            using (var viewModel = new EditUserViewModel())
            {
                var result = this.dialogService.ShowDialog<EditUserView>(this, viewModel);
            }
        });

        public ICommand ShowYesNoDialogCommand => new DelegateCommand<object>(o =>
        {
            using (var viewModel = new YesNoViewModel())
            {
                viewModel.Question = "Czy jesteś pewien?";

                var result = this.dialogService.ShowDialog<YesNoView>(this, viewModel);

                var messageBoxText = $"Odpowiedź to: {result}";
                var caption = "Odpowiedź";

                _ = this.dialogService.ShowMessageBox(
                    this,
                    messageBoxText,
                    caption,
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        });

        public ICommand WindowClosedCommand => new DelegateCommand(this.Dispose);

        public ICommand WindowClosingCommand => new DelegateCommand<CancelEventArgs>(e =>
        {
            var messageBoxText = "Czy jesteś pewien?";
            var caption = "Zamykanie programu";

            var result = this.dialogService.ShowMessageBox(
                this,
                messageBoxText,
                caption,
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            e.Cancel = result != MessageBoxResult.Yes;
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
