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
    public class SingleComponentTester
    {
        [TestMethod]
        public void TestILogger()
        {
            var mock = new Mock<ILogger>();
            var loggedErrors = 0;
            mock.Setup(foo => foo.Log(LoggingType.Error, It.IsAny<string>())).Callback(() => loggedErrors++);
            var loggedWanings = 0;
            mock.Setup(foo => foo.Log(LoggingType.Warning, It.IsAny<string>())).Callback(() => loggedWanings++);
            var loggedInformations = 0;
            mock.Setup(foo => foo.Log(LoggingType.Info, It.IsAny<string>())).Callback(() => loggedInformations++);
            var instance = mock.Object;

            instance.Log(LoggingType.Error, "seriousError");
            instance.Log(LoggingType.Error, "seriousError2");
            instance.Log(LoggingType.Warning, "wrn");
            instance.Log(LoggingType.Info, "succesfully done");
            instance.Log(LoggingType.Info, "done");
            instance.Log(LoggingType.Info, "done again");

            Assert.AreEqual(2, loggedErrors);
            Assert.AreEqual(1, loggedWanings);
            Assert.AreEqual(3, loggedInformations);
            mock.Verify(foo => foo.Log(LoggingType.Error, It.IsAny<string>()), Times.AtLeastOnce);
            mock.Verify(foo => foo.Log(LoggingType.Warning, It.IsAny<string>()), Times.Once);
            mock.Verify(foo => foo.Log(LoggingType.Info, It.IsAny<string>()), Times.Exactly(3));
        }

        [TestMethod]
        public void TestProductionLineMover()
        {
            var mock = new Mock<IProductionLineMover>();
            mock.Setup(foo => foo.MoveProductionLine(MovingDirection.Forward));
            mock.Setup(foo => foo.MoveProductionLine(MovingDirection.ToScran));

            var instance = mock.Object;
            instance.MoveProductionLine(MovingDirection.Forward);
            instance.MoveProductionLine(MovingDirection.ToScran);
            instance.MoveProductionLine(MovingDirection.Forward);
            instance.MoveProductionLine(MovingDirection.Forward);

            mock.Verify(foo => foo.MoveProductionLine(MovingDirection.Forward), Times.Exactly(3));
            mock.Verify(foo => foo.MoveProductionLine(MovingDirection.ToScran), Times.Once);
            mock.Verify(foo => foo.MoveProductionLine(MovingDirection.Back), Times.Never);
        }

        [TestMethod]
        public void TestConstructionRecipe()
        {
            var mock = new Mock<ConstructionRecipe>();
            const string nameOfObject = "Fiat 126P";
            mock.Setup(foo => foo.NameOfObject).Returns(nameOfObject);

            Assert.AreEqual(nameOfObject, mock.Object.NameOfObject);
        }

        [TestMethod]
        public void TestIConstructionRecipeCreator()
        {
            var mock = new Mock<ConstructionRecipe>();
            const string nameOfObject = "Fiat 126P";
            mock.Setup(foo => foo.NameOfObject).Returns(nameOfObject);

            const uint numberOfObjectsToProduce = 10;
            var mockCreator = new Mock<IConstructionRecipeCreator>();
            mockCreator.Setup(foo => foo.ConstructionRecipe).Returns(mock.Object);
            mockCreator.Setup(foo => foo.NumberOfElementsToProduce).Returns(numberOfObjectsToProduce);

            IConstructionRecipeCreator mockCreatorInstance = mockCreator.Object;
            Assert.AreEqual(numberOfObjectsToProduce, mockCreatorInstance.NumberOfElementsToProduce);
            Assert.AreEqual(nameOfObject, mockCreatorInstance.ConstructionRecipe.NameOfObject);
        }

        [TestMethod]
        public void TestObjectConstructor()
        {
            var mock = new Mock<ConstructionRecipe>();
            var mockObjectConstructor = new Mock<IObjectsConstructor>();
            mockObjectConstructor.Setup(foo => foo.ConstructObjectFromRecipe(mock.Object)).Returns(true);

            Assert.AreEqual(true, mockObjectConstructor.Object.ConstructObjectFromRecipe(mock.Object));
            mockObjectConstructor.Verify(foo => foo.ConstructObjectFromRecipe(mock.Object), Times.Once);
        }
    }
}
