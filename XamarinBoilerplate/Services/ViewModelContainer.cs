using Autofac;
using System;
using Xamarin.Forms;
using XamarinBoilerplate.ViewModels;

namespace XamarinBoilerplate.Services
{
    public class ViewModelContainer
    {
        private IContainer _container;
        private ContainerBuilder _builder;
        private bool _isInitialized = false;
        private bool isBuilt = false;
        private static ViewModelContainer _instance;
        public static ViewModelContainer Current => _instance ?? (_instance = new ViewModelContainer());

        protected ViewModelContainer()
        {
        }

        public void BuildContainer()
        {
            if (isBuilt)
            {
                return;
            }

            if (_builder == null)
            {
                return;
            }

            _container = _builder.Build();
            isBuilt = true;
        }

        public void Initialize<TVM, TPage>()
        where TVM : BaseViewModel
            where TPage : Page
        {
            if (_isInitialized)
            {
                RegisterRenderViewModel<TVM, TPage>();
                return;
            }

            _builder = new ContainerBuilder();

            RegisterRenderViewModel<TVM, TPage>();

            _isInitialized = true;
        }

        public T Resolve<T>()
        {
            if (_container == null)
            {
                return default(T);
            }

            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            if (_container == null)
            {
                throw new InvalidOperationException("APP CONTAINER IS NOT INITIALIZED");
            }
            return _container.Resolve(type);
        }

        public void RegisterRenderViewModel<TVM, TPage>()
        where TVM : BaseViewModel
            where TPage : Page
        {
            _builder.RegisterType<TVM>().AsSelf();
        }
    }

}
