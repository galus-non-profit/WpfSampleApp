using System;
using MvvmDialogs;
using Prism.Mvvm;

namespace WpfSampleApp.ViewModels
{
    using System.Windows.Input;
    using Prism.Commands;

    internal sealed class YesNoViewModel : BindableBase, IDisposable, IModalDialogViewModel
    {
        private volatile bool isDisposed = default;

        private bool isClose = false;
        private string title = "Yes no view";
        private string question = string.Empty;

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

        public string Question
        {
            get => this.question;
            set => this.SetProperty(ref this.question, value);
        }

        public bool? DialogResult { get; private set; }

        ~YesNoViewModel()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ICommand ConfirmCommand => new DelegateCommand<object>(o =>
        {
            this.DialogResult = true;
            this.IsClose = true;

        });

        public ICommand CancelCommand => new DelegateCommand<object>(o =>
        {
            this.DialogResult = false;
            this.IsClose = true;
        });

        private void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (disposing)
            {
                //intentionally left empty
            }

            this.isDisposed = true;
        }


    }
}
