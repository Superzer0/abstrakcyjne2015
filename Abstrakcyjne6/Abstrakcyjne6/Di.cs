using System.Reflection;
using Autofac;

namespace Abstrakcyjne6
{
    internal static class Di
    {
        private static volatile object _lock = new object();
        private static volatile IContainer _autoFacContainer;

        private static IContainer Container
        {
            get
            {
                //concurrent singleton
                if (_autoFacContainer == null)
                {
                    lock (_lock)
                    {
                        if (_autoFacContainer == null)
                        {
                            _autoFacContainer = BuildDi();
                        } 
                    }
                }

                return _autoFacContainer;
            }
        }


        private static IContainer BuildDi()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DiModule>();
            return builder.Build();
        }

        public static ILifetimeScope GetContainerScope
        {
            get { return Container.BeginLifetimeScope(); }
        }
    }
}
