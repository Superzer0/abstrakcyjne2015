using System;
using Abstrakcyjne1.Exporters;
using Abstrakcyjne1.Factory;
using Abstrakcyjne1.Importers;
using Abstrakcyjne1.Store;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestExporter()
        {
            string textToBeExported = "Ala ma kota";
            Exporter exporter = new TextExporter(textToBeExported);
            Data exportedData = exporter.ExportData;
            string exportedText = (exportedData as TextData).Text;
            Assert.AreEqual(textToBeExported, exportedText);
            exportedData = exporter.ExportData;
            exportedText = (exportedData as TextData).Text;
            textToBeExported = String.Empty;
            Assert.AreEqual(textToBeExported, exportedText);
        }

        [TestMethod]
        public void TestImporter()
        {
            string textToBeImported = "Ala zgubila dolara";
            Data dataToSendToImporter = new TextData(textToBeImported);
            Importer importer = new TextImporter();
            importer.ImportData(dataToSendToImporter);
            string dataSavedInImporter = (importer as TextImporter).ImportedText;
            Assert.AreEqual(textToBeImported, dataSavedInImporter);
        }

        [TestMethod]
        public void TestFactory()
        {
            const string textToForFactory = "Ali kot zjadl dolara";
            DistributedModuleFactory factory = new DistributedModuleTextFactory(textToForFactory);
            Data dataFromFactory = factory.CreateData();
            string textFromModule = (dataFromFactory as TextData).Text;
            Assert.AreEqual(textToForFactory, textFromModule);
            Exporter exporter = factory.CreateExporter();
            textFromModule = ((exporter as TextExporter).ExportData as TextData).Text;
            Assert.AreEqual(textToForFactory, textFromModule);
            Importer importer = factory.CreateImporter();
            Assert.IsTrue(importer is TextImporter);
        }
    }
}