using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ZavrsniTest.Controllers;
using ZavrsniTest.Models;
using ZavrsniTest.Repository.Interfaces;

namespace ZavrsniTestTest.Controllers
{
    public class AgencijeControllerTest
    {
        [Fact]
        public void GetAgencija_ValidId_ReturnsObject()
        {
            // Arrange
            Agencija agencija = new Agencija()
            {
                Id = 1,
                Naziv = "Naj Nekretnine",
                GodinaOsnivanja = 2005

            };


            var mockRepository = new Mock<IAgencijaRepository>();
            mockRepository.Setup(x => x.GetOne(1)).Returns(agencija);



            var controller = new AgencijeController(mockRepository.Object);

            // Act
            var actionResult = controller.GetOne(1) as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);
            Assert.Equal(agencija, actionResult.Value);
        }

        [Fact]
        public void GetAgencija_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IAgencijaRepository>();


            var controller = new AgencijeController(mockRepository.Object);

            // Act
            var actionResult = controller.GetOne(12) as NotFoundResult;

            // Assert
            Assert.NotNull(actionResult);
        }

        [Fact]
        public void GetAgencije_ReturnsCollection()
        {
            // Arrange
            List<Agencija> agencije = new List<Agencija>() {
                new Agencija()  {
                    Id = 1,
                    Naziv = "Naj Nekretnine",
                    GodinaOsnivanja = 2005
                },
                new Agencija()  {
                    Id = 3,
                    Naziv = "Fast Nekretnine",
                    GodinaOsnivanja = 2005
                }
            };

            var mockRepository = new Mock<IAgencijaRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(agencije.AsQueryable());



            var controller = new AgencijeController(mockRepository.Object);

            // Act
            var actionResult = controller.GetAll() as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            List<Agencija> listResult = (List<Agencija>)actionResult.Value;

            for (int i = 0; i < listResult.Count; i++)
            {
                Assert.Equal(agencije[i], listResult[i]);
            }
        }
    }
}
