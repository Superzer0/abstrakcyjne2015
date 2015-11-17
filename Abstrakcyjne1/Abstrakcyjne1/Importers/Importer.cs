using Abstrakcyjne1.Store;

namespace Abstrakcyjne1.Importers
{
    public abstract class Importer
    {
        public abstract void ImportData(Data dataToSendToImporter);
    }
}
