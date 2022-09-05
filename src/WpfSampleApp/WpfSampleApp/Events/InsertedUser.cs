using Prism.Events;

namespace WpfSampleApp.Events
{
    using WpfSampleApp.Models;

    internal sealed class InsertedUser : PubSubEvent<User>
    {
    }
}
