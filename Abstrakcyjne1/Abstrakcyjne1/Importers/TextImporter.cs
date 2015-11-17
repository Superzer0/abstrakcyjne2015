using Abstrakcyjne1.Store;

namespace Abstrakcyjne1.Importers
{
    public class TextImporter : Importer
    {
        private string _importerData;
        public override void ImportData(Data dataToSendToImporter)
        {
            _importerData = ((TextData) dataToSendToImporter).Text;
        }

        public string ImportedText { get { return _importerData; } }
    }
}
