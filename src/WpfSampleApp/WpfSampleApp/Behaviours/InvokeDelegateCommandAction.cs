namespace WpfSampleApp.Behaviours
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Input;
    using Microsoft.Xaml.Behaviors;

    public sealed class InvokeDelegateCommandAction : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(InvokeDelegateCommandAction), null);

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(InvokeDelegateCommandAction),
            null);

        public static readonly DependencyProperty InvokeParameterProperty =
            DependencyProperty.Register("InvokeParameter", typeof(object), typeof(InvokeDelegateCommandAction), null);

        private string commandName;

        public object InvokeParameter
        {
            get => this.GetValue(InvokeParameterProperty);
            set => this.SetValue(InvokeParameterProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        public string CommandName
        {
            get => this.commandName;

            set
            {
                if (this.CommandName != value)
                {
                    this.commandName = value;
                }
            }
        }

        public object CommandParameter
        {
            get => this.GetValue(CommandParameterProperty);
            set => this.SetValue(CommandParameterProperty, value);
        }

        protected override void Invoke(object parameter)
        {
            this.InvokeParameter = parameter;

            if (this.AssociatedObject != null)
            {
                ICommand command = this.ResolveCommand();
                if ((command != null) && command.CanExecute(this.CommandParameter))
                {
                    command.Execute(this.CommandParameter);
                }
            }
        }

        private ICommand ResolveCommand()
        {
            ICommand command = null;

            if (this.Command != null)
            {
                return this.Command;
            }

            if (this.AssociatedObject is FrameworkElement frameworkElement)
            {
                object dataContext = frameworkElement.DataContext;

                if (dataContext != null)
                {
                    PropertyInfo commandPropertyInfo =
                        dataContext.GetType()
                            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .FirstOrDefault(
                                p =>
                                    typeof(ICommand).IsAssignableFrom(p.PropertyType)
                                    && string.Equals(p.Name, this.CommandName, StringComparison.Ordinal));

                    if (commandPropertyInfo != null)
                    {
                        command = (ICommand)commandPropertyInfo.GetValue(dataContext, null);
                    }
                }
            }

            return command;
        }
    }
}
