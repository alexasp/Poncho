using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Autofac;
using Caliburn.Micro;
using System.Windows;
using System.Windows.Controls;
using Poncho.ViewModels;
using Poncho.ViewModels.Interfaces;
using Poncho.Views;
using SpotifyService;
using SpotifyService.Interfaces;
using SpotifyService.Model;
using SpotifyService.Model.Interfaces;
using IContainer = Autofac.IContainer;

namespace Poncho
{
    public class Bootstrapper : Bootstrapper<ILoginViewModel>
    {
        private readonly ILog _logger = LogManager.GetLog(typeof(Bootstrapper));
        private IContainer _container;

        protected void ConfigureContainer(ContainerBuilder builder)
        {
            _logger.Info("Configuring Container.");

            //  configure container     var builder = new ContainerBuilder();

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
            _logger.Info("Registering ViewModels.");
            builder.RegisterType<LoginViewModel>().As<ILoginViewModel>();
            builder.RegisterType<MainViewModel>().As<IMainViewModel>();
            _logger.Info("Registering Views.");
            builder.RegisterType<LoginView>().AsSelf();
            builder.RegisterType<ShellView>().AsSelf();

            builder.RegisterType<LoginManager>().As<ILoginManager>();
            builder.RegisterType<UserFeedbackHandler>().As<IUserFeedbackHandler>();
            builder.RegisterType<MusicServices>().As<IMusicServices>().InstancePerLifetimeScope();
            builder.RegisterType<SpotifyWrapper>().As<ISpotifyWrapper>().InstancePerLifetimeScope();
            builder.RegisterType<SearchManager>().As<ISearchManager>();
            builder.RegisterType<TrackHandler>().As<ITrackHandler>();
            builder.RegisterType<PlaylistManager>().As<IPlaylistManager>();
        }

        
        protected override void Configure()
        { 
            var builder = new ContainerBuilder();

            //  register the single window manager for this container
            builder.Register<IWindowManager>(c => new WindowManager()).InstancePerLifetimeScope();
            //  register the single event aggregator for this container
            builder.Register<IEventAggregator>(c => new EventAggregator()).InstancePerLifetimeScope();

            ConfigureContainer(builder);

            _container = builder.Build();
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            object instance;
            if (string.IsNullOrWhiteSpace(key))
            {
                if (_container.TryResolve(serviceType, out instance))
                {
                    return instance;
                }
            }
            else
            {
                if (_container.TryResolveNamed(key, serviceType, out instance))
                {
                    return instance;
                }
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", key ?? serviceType.Name));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.Resolve(typeof(IEnumerable<>).MakeGenericType(serviceType)) as IEnumerable<object>;
        }
        protected override void BuildUp(object instance)
        {
            _container.InjectProperties(instance);
        }
    }
}
