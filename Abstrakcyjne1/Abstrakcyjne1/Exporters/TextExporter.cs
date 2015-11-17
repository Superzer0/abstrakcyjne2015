using Abstrakcyjne1.Store;

namespace Abstrakcyjne1.Exporters
{
    public class TextExporter : Exporter
    {
        private string _textToBeExported;

        public TextExporter(string textToBeExported)
        {
            _textToBeExported = textToBeExported;
        }

        public override Data ExportData
        {
            get
            {
                var value = _textToBeExported;
                _textToBeExported = string.Empty;
                return new TextData(value);
            }
        }
    }
}
