using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Autofac;
using Caliburn.Micro;
using Poncho.ViewModels;
using Poncho.ViewModels.Interfaces;
using Poncho.Views;
using IContainer = Autofac.IContainer;

namespace Poncho
{
    public class Bootstrapper : Bootstrapper<LoginViewModel>
    {
        private ContainerBuilder _builder;
        private IContainer _container;


        protected void ConfigureContainer(ContainerBuilder builder)
        {
        }

        protected override void Configure()
        { //  configure container     var builder = new ContainerBuilder();

            //  register view models
            //_builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
            //    //  must be a type that ends with ViewModel
            //    .Where(type => type.Name.EndsWith("ViewModel"))
            //    //  must be in a namespace ending with ViewModels
            //    .Where(type => !(string.IsNullOrWhiteSpace(type.Namespace)) && type.Namespace.EndsWith("ViewModels"))
            //    //  must implement INotifyPropertyChanged (deriving from PropertyChangedBase will statisfy this)
            //    .Where(type => type.GetInterface(typeof (INotifyPropertyChanged).Name) != null)
            //    //  registered as self
            //    .AsSelf()
            //    //  always create a new one
            //    .InstancePerDependency();

            ////  register views
            //_builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
            //    //  must be a type that ends with View
            //  .Where(type => type.Name.EndsWith("View"))
            //    //  must be in a namespace that ends in Views
            //  .Where(type => !(string.IsNullOrWhiteSpace(type.Namespace)) && type.Namespace.EndsWith("Views"))
            //    //  registered as self
            //  .AsSelf()
            //    //  always create a new one
            //  .InstancePerDependency();

            _builder.RegisterType<LoginViewModel>().As<ILoginViewModel>();
            _builder.RegisterType<LoginView>().AsSelf();

            //  register the single window manager for this container
            _builder.Register<IWindowManager>(c => new WindowManager()).InstancePerLifetimeScope();
            //  register the single event aggregator for this container
            _builder.Register<IEventAggregator>(c => new EventAggregator()).InstancePerLifetimeScope();

            ConfigureContainer(_builder);

            _container = _builder.Build();
        }
    }
}
