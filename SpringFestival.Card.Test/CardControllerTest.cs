using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SpringFestival.Card.API.Controllers;
using SpringFestival.Card.Common.Enums;
using SpringFestival.Card.Service;
using SpringFestival.Card.UICommand;
using SpringFestival.Card.ViewModel;

namespace SpringFestival.Card.Test
{
    [TestFixture]
    public class CardControllerTest
    {
        private CardController cardController;
        private Mock<ICardService> cardServiceMock;
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            cardServiceMock = new Mock<ICardService>();
            cardController = new CardController(cardServiceMock.Object);
            mapper = AutoMap.Get();
        }

        [Test]
        public void CardController_GetCard_Should_Correct()
        {
            // arrange
            var testCard = new Entity.Card
            {
                Id = Guid.NewGuid(),
                Name = "test",
                CardType = CardType.Creative
            };

            cardServiceMock.Setup(x => x.Get(testCard.Id))
                .Returns(Task.FromResult(mapper.Map<CardViewModel>(testCard)));

            //act
            var result = cardController.GetCard(testCard.Id).Result;
            var resultId = ((CardViewModel) ((OkObjectResult) result.Result).Value).Id;

            Assert.AreEqual(testCard.Id.ToString(), resultId);
        }

        [Test]
        public void CardController_PostCard_Should_Return_BadRequest_When_ModelInValid()
        {
            //arrange
            var testCard = new CardAddUICommand
            {
                Name = "test",
                CardType = CardType.kungFu
            };

            cardController.ControllerContext = new ControllerContext();
            cardController.ControllerContext.ModelState.AddModelError("default", "mocked");

            //act
            var result = cardController.PostCard(testCard).Result as BadRequestObjectResult;
            
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }
        
        [Test]
        public void CardController_PostCard_Should_Correct()
        {
            //arrange
            var testCard = new CardAddUICommand
            {
                Name = "test",
                CardType = CardType.kungFu
            };

            cardServiceMock.Setup(x => x.Add(testCard))
                .Returns(Task.CompletedTask);
            cardController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    Request = { Path = new PathString("/mocked")}
                }
            };

            //act
            var result = cardController.PostCard(testCard).Result as CreatedResult;
            
            Assert.IsNotNull(result);
            Assert.AreEqual("/mocked", result.Location);
        }
    }
}