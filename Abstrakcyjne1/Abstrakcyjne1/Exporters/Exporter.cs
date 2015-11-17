namespace Abstrakcyjne1.Exporters
{
    public abstract class Exporter
    {
        public abstract Store.Data ExportData { get; }
    }
}
