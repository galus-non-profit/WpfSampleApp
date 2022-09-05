using System;
using Prism.Mvvm;

namespace WpfSampleApp.Modules.ViewModels
{
    using System.Collections.ObjectModel;
    using Prism.Events;
    using WpfSampleApp.Events;
    using WpfSampleApp.Models;

    internal sealed class UserViewModel : BindableBase, IDisposable
    {
        private volatile bool isDisposed = default;

        private string title = "To jest user";

        private ObservableCollection<User> users = new ObservableCollection<User>();

        private readonly IEventAggregator eventAggregator;

        public string Title
        {
            get => this.title;
            set => this.SetProperty(ref this.title, value);
        }

        public ObservableCollection<User> Users
        {
            get => this.users;
            set => this.SetProperty(ref this.users, value);
        }

        public UserViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            this.eventAggregator.GetEvent<InsertedUser>().Subscribe(user =>
            {
                this.Users.Add(user);
            }, ThreadOption.UIThread);
        }

        ~UserViewModel()
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
                //this.eventAggregator.GetEvent<NewApplicationFormsCountEvent>()
                //    .Unsubscribe(this.newApplicationFormsToken);
            }

            this.isDisposed = true;
        }
    }
}
