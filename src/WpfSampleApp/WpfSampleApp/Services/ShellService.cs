using System;
using WpfSampleApp.Interfaces;

namespace WpfSampleApp.Services
{
    using Prism.Events;
    using TableDependency.SqlClient;
    using WpfSampleApp.Models;

    internal sealed class ShellService : IShellService
    {
        private volatile bool isDisposed = default;

        private readonly string connectionString = "data source=(local)\\SQLExpress; initial catalog=WpfSampleApp; integrated security=True";
        private readonly SqlTableDependency<User> users;

        private readonly IEventAggregator eventAggregator;

        ~ShellService()
        {
            this.Dispose(false);
        }

        public ShellService(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            //this.users = new SqlTableDependency<User>(this.connectionString, "User", "dbo");
            //this.users.OnChanged += (sender, args) =>
            //{
            //    switch (args.ChangeType)
            //    {
            //        case ChangeType.Delete:
            //            break;
            //        case ChangeType.Insert:
            //            var user = new User
            //            {
            //                Id = args.Entity.Id,
            //                FirstName = args.Entity.FirstName,
            //                LastName = args.Entity.LastName,
            //            };
            //            this.eventAggregator.GetEvent<InsertedUser>().Publish(user);
            //            break;
            //        case ChangeType.Update:
            //            //var student = this.GetStudentByStudentId(args.Entity.StudentId);
            //            //this.eventAggregator.GetEvent<UpdateStudentClassRequest>().Publish(student);
            //            break;
            //    }
            //};

            //this.users.Start();
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
                this.users.Stop();
                this.users.Dispose();

                //this.eventAggregator.GetEvent<NewApplicationFormsCountEvent>()
                //    .Unsubscribe(this.newApplicationFormsToken);
            }

            this.isDisposed = true;
        }
    }
}
