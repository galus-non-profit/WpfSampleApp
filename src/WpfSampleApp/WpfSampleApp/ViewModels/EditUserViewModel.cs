namespace WpfSampleApp.ViewModels
{
    using System;
    using MvvmDialogs;
    using Prism.Mvvm;

    internal sealed class EditUserViewModel : BindableBase, IDisposable, IModalDialogViewModel
    {
        private volatile bool isDisposed = default;

        private string title = "Edit user view";

        public string Title
        {
            get => this.title;
            set => this.SetProperty(ref this.title, value);
        }

        ~EditUserViewModel()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

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

        public bool? DialogResult { get; }
    }
}
