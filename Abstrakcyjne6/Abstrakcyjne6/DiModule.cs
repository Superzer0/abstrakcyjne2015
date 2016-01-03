using Autofac;
using Objects.Interfaces;
using Objects.Logger;
using ProductionLineMover;
using ProductionLineMover.Logger;
using ProductionLineMover.Services;

namespace Abstrakcyjne6
{
    internal class DiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultConstructionRecipeCreator>().As<IConstructionRecipeCreator>().SingleInstance();
            builder.RegisterType<DefaultObjectsConstructor>().As<IObjectsConstructor>();
            builder.RegisterType<DefaultProductionLineMover>().As<IProductionLineMover>();
            builder.RegisterType<DefaultLogger>().As<ILogger>();
            builder.RegisterType<ControllerWithDependencyInjection>().As<ControllerTemplateMethod>();
        }
    }
}
