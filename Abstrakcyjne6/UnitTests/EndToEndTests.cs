using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Objects.Interfaces;
using Objects.Logger;
using Objects.ProductionMover;
using ProductionLineMover;

namespace UnitTests
{
    [TestClass]
    public class EndToEndTests
    {
        Mock<IObjectsConstructor> ObjectsConstructor { get; set; }
        Mock<IConstructionRecipeCreator> ConstructionRecipeCreator { get; set; }
        Mock<ILogger> Logger { get; set; }
        Mock<IProductionLineMover> ProductionLineMover { get; set; }
        Mock<ConstructionRecipe> ConstructionRecipe { get; set; }
        ControllerTemplateMethod Controller { get; set; }

        const string NameOfObject = "UJ's first car";
        private const uint NumberOfObjectsToConstruct = 1;

        public EndToEndTests()
        {
            ObjectsConstructor = new Mock<IObjectsConstructor>(MockBehavior.Strict);
            ConstructionRecipeCreator = new Mock<IConstructionRecipeCreator>(MockBehavior.Strict);
            Logger = new Mock<ILogger>(MockBehavior.Strict);
            ProductionLineMover = new Mock<IProductionLineMover>(MockBehavior.Strict);
            ConstructionRecipe = new Mock<ConstructionRecipe>(MockBehavior.Strict);
            Controller = new ControllerWithDependencyInjection(ObjectsConstructor.Object, ConstructionRecipeCreator.Object, Logger.Object, ProductionLineMover.Object);
        }

        [TestMethod]
        public void TestConstructionOfSingleObject()
        {
            const bool wasSuccessfull = true;
            ConstructionRecipe.Setup(foo => foo.NameOfObject).Returns(NameOfObject);
            ConstructionRecipeCreator.Setup(foo => foo.ConstructionRecipe).Returns(ConstructionRecipe.Object);
            ConstructionRecipeCreator.Setup(foo => foo.NumberOfElementsToProduce).Returns(NumberOfObjectsToConstruct);
            ProductionLineMover.Setup(foo => foo.MoveProductionLine(MovingDirection.Forward)).Returns(wasSuccessfull);
            ObjectsConstructor.Setup(foo => foo.ConstructObjectFromRecipe(ConstructionRecipe.Object)).Returns(wasSuccessfull);
            Logger.Setup(foo => foo.Log(LoggingType.Info, It.IsAny<string>()));

            Controller.Execute();

            ConstructionRecipe.Verify(foo => foo.NameOfObject);
            ConstructionRecipeCreator.Verify(foo => foo.ConstructionRecipe, Times.Once);
            ConstructionRecipeCreator.Verify(foo => foo.ConstructionRecipe, Times.Once);
            ObjectsConstructor.Verify(foo => foo.ConstructObjectFromRecipe(ConstructionRecipe.Object), Times.Once);
            Logger.Verify(foo => foo.Log(LoggingType.Info, It.IsAny<string>()), Times.Once);
            ProductionLineMover.Verify(foo => foo.MoveProductionLine(MovingDirection.Forward), Times.Exactly(2));
        }

        [TestMethod]
        public void TestConstructionFailure_MovingProductionLineFailed()
        {
            ConstructionRecipe.Setup(foo => foo.NameOfObject).Returns(NameOfObject);
            ConstructionRecipeCreator.Setup(foo => foo.ConstructionRecipe).Returns(ConstructionRecipe.Object);
            ConstructionRecipeCreator.Setup(foo => foo.NumberOfElementsToProduce).Returns(NumberOfObjectsToConstruct);
            ProductionLineMover.Setup(foo => foo.MoveProductionLine(MovingDirection.Forward)).Throws(new Exception("Can't move ProductionLine!"));

            Logger.Setup(foo => foo.Log(LoggingType.Error, It.IsAny<string>()));

            Controller.Execute();

            ConstructionRecipe.Verify(foo => foo.NameOfObject);
            ConstructionRecipeCreator.Verify(foo => foo.ConstructionRecipe, Times.Once);
            ConstructionRecipeCreator.Verify(foo => foo.ConstructionRecipe, Times.Once);
            ObjectsConstructor.Verify(foo => foo.ConstructObjectFromRecipe(ConstructionRecipe.Object), Times.Never);
            Logger.Verify(foo => foo.Log(LoggingType.Info, It.IsAny<string>()), Times.Never);
            ProductionLineMover.Verify(foo => foo.MoveProductionLine(MovingDirection.Forward), Times.Once);
        }

        [TestMethod]
        public void TestConstructionFailure_ObjectConstructionFailed()
        {
            bool wasSuccessfull = true;
            ConstructionRecipe.Setup(foo => foo.NameOfObject).Returns(NameOfObject);
            ConstructionRecipeCreator.Setup(foo => foo.ConstructionRecipe).Returns(ConstructionRecipe.Object);
            ConstructionRecipeCreator.Setup(foo => foo.NumberOfElementsToProduce).Returns(NumberOfObjectsToConstruct);
            ProductionLineMover.Setup(foo => foo.MoveProductionLine(MovingDirection.Forward)).Returns(wasSuccessfull);
            ObjectsConstructor.Setup(foo => foo.ConstructObjectFromRecipe(ConstructionRecipe.Object)).Returns(wasSuccessfull = false);
            Logger.Setup(foo => foo.Log(LoggingType.Warning, It.IsAny<string>()));
            ProductionLineMover.Setup(foo => foo.MoveProductionLine(MovingDirection.ToScran)).Returns(wasSuccessfull = true);

            Controller.Execute();

            ConstructionRecipe.Verify(foo => foo.NameOfObject);
            ConstructionRecipeCreator.Verify(foo => foo.ConstructionRecipe, Times.Once);
            ConstructionRecipeCreator.Verify(foo => foo.ConstructionRecipe, Times.Once);
            ObjectsConstructor.Verify(foo => foo.ConstructObjectFromRecipe(ConstructionRecipe.Object), Times.Once);
            Logger.Verify(foo => foo.Log(LoggingType.Warning, It.IsAny<string>()), Times.Once);
            ProductionLineMover.Verify(foo => foo.MoveProductionLine(MovingDirection.Forward), Times.Once);
            ProductionLineMover.Verify(foo => foo.MoveProductionLine(MovingDirection.ToScran), Times.Once);
        }

        [TestMethod]
        public void TestConstructionFailure_ObjectConstructionFailedThenMovingToScanAlsoFailed()
        {
            bool wasSuccessfull = true;
            ConstructionRecipe.Setup(foo => foo.NameOfObject).Returns(NameOfObject);
            ConstructionRecipeCreator.Setup(foo => foo.ConstructionRecipe).Returns(ConstructionRecipe.Object);
            ConstructionRecipeCreator.Setup(foo => foo.NumberOfElementsToProduce).Returns(NumberOfObjectsToConstruct);
            ProductionLineMover.Setup(foo => foo.MoveProductionLine(MovingDirection.Forward)).Returns(wasSuccessfull);
            ObjectsConstructor.Setup(foo => foo.ConstructObjectFromRecipe(ConstructionRecipe.Object)).Returns(wasSuccessfull = false);
            Logger.Setup(foo => foo.Log(LoggingType.Warning, It.IsAny<string>()));
            ProductionLineMover.Setup(foo => foo.MoveProductionLine(MovingDirection.ToScran)).Throws(new Exception("Can't move not constructed car to scan, scan is full!"));
            Logger.Setup(foo => foo.Log(LoggingType.Error, It.IsAny<string>()));

            Controller.Execute();

            ConstructionRecipe.Verify(foo => foo.NameOfObject);
            ConstructionRecipeCreator.Verify(foo => foo.ConstructionRecipe, Times.Once);
            ConstructionRecipeCreator.Verify(foo => foo.ConstructionRecipe, Times.Once);
            ObjectsConstructor.Verify(foo => foo.ConstructObjectFromRecipe(ConstructionRecipe.Object), Times.Once);
            Logger.Verify(foo => foo.Log(LoggingType.Warning, It.IsAny<string>()), Times.Once);
            Logger.Verify(foo => foo.Log(LoggingType.Error, It.IsAny<string>()), Times.Once);
            ProductionLineMover.Verify(foo => foo.MoveProductionLine(MovingDirection.Forward), Times.Once);
            ProductionLineMover.Verify(foo => foo.MoveProductionLine(MovingDirection.ToScran), Times.Once);
        }
    }
}
