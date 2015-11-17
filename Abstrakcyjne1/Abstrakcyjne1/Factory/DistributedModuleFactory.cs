using Abstrakcyjne1.Exporters;
using Abstrakcyjne1.Importers;

namespace Abstrakcyjne1.Factory
{
    public abstract class DistributedModuleFactory
    {
        public abstract Exporter CreateExporter();

        public abstract Importer CreateImporter();

        public abstract Store.Data CreateData();
    }
}
