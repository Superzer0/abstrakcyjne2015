using Abstrakcyjne1.Exporters;
using Abstrakcyjne1.Importers;
using Abstrakcyjne1.Store;

namespace Abstrakcyjne1.Factory
{
    public class DistributedModuleTextFactory : DistributedModuleFactory
    {
        private readonly string _text;

        public DistributedModuleTextFactory(string text)
        {
            _text = text;
        }

        public override Exporter CreateExporter()
        {
            return new TextExporter(_text);
        }

        public override Importer CreateImporter()
        {
            return new TextImporter();
        }

        public override Data CreateData()
        {
            return new TextData(_text);
        }
    }
}
