using System;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using WpfSampleApp.Models;

namespace WpfSampleApp.Modules.ViewModels
{
    internal sealed class AssignUserViewModel : BindableBase, IDisposable
    {
        private volatile bool isDisposed = default;

        private ObservableCollection<User> assigneUsers = new ObservableCollection<User>();
        private ObservableCollection<User> users = new ObservableCollection<User>();

        public ObservableCollection<User> AssignedUsers
        {
            get => this.assigneUsers;
            set => this.SetProperty(ref this.assigneUsers, value);
        }

        public ObservableCollection<User> Users
        {
            get => this.users;
            set => this.SetProperty(ref this.users, value);
        }

        public AssignUserViewModel()
        {
            this.AssignedUsers.Add(new User
            {
                FirstName = "John",
                Id = 1,
                LastName = "Doe",
            });

            this.AssignedUsers.Add(new User
            {
                FirstName = "Arnold",
                Id = 2,
                LastName = "Terminator",
            });

            this.Users.Add(new User
            {
                FirstName = "Luke",
                Id = 3,
                LastName = "Skywaker",
            });

            this.Users.Add(new User
            {
                FirstName = "Harry",
                Id = 4,
                LastName = "Potter",
            });
        }

        ~AssignUserViewModel()
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
