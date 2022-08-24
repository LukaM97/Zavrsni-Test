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
    public class OglasiControllerTest
    {
        [Fact]
        public void GetOglas_ValidId_ReturnsObject()
        {
            // Arrange
            Oglas oglas = new Oglas()
            {
                Id = 1,
                Naslov = "Naj Nekretnine",
                Tip = "Kuca",
                GodinaIzgradnje = 1987,
                CenaNekretnine = 110000,
                AgencijaId = 3,
                Agencija = new Agencija { Id = 3, Naziv = "Fast Nekretnine", GodinaOsnivanja = 2005 }

            };


            var mockRepository = new Mock<IOglasRepository>();
            mockRepository.Setup(x => x.GetOne(1)).Returns(oglas);



            var controller = new OglasiController(mockRepository.Object);

            // Act
            var actionResult = controller.GetOne(1) as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);
            Assert.Equal(oglas, actionResult.Value);
        }

        [Fact]
        public void GetOglas_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IOglasRepository>();


            var controller = new OglasiController(mockRepository.Object);

            // Act
            var actionResult = controller.GetOne(12) as NotFoundResult;

            // Assert
            Assert.NotNull(actionResult);
        }

        [Fact]
        public void GetOglasi_ReturnsCollection()
        {
            // Arrange
            List<Oglas> oglasi = new List<Oglas>() {
                new Oglas()  {
                    Id = 1,
                    Naslov = "Naj Nekretnine",
                    Tip = "Kuca",
                    GodinaIzgradnje = 1987,
                    CenaNekretnine = 110000,
                    AgencijaId = 3,
                    Agencija = new Agencija{ Id = 3, Naziv = "Fast Nekretnine", GodinaOsnivanja = 2005}
                    
                },
                new Oglas()  {
                    Id = 3,
                    Naslov = "Moderan dupleks",
                    Tip = "Dupleks stan",
                    GodinaIzgradnje = 1979,
                    CenaNekretnine = 80000,
                    AgencijaId = 1,
                    Agencija = new Agencija{Id = 1, Naziv = "Naj Nekretnine", GodinaOsnivanja = 2005}
                }
            };

            var mockRepository = new Mock<IOglasRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(oglasi.AsQueryable());



            var controller = new OglasiController(mockRepository.Object);

            // Act
            var actionResult = controller.GetAll() as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            List<Oglas> listResult = (List<Oglas>)actionResult.Value;

            for (int i = 0; i < listResult.Count; i++)
            {
                Assert.Equal(oglasi[i], listResult[i]);
            }
        }

        [Fact]
        public void PutOglas_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            Oglas oglas = new Oglas()
            {
                Id = 1,
                Naslov = "Naj Nekretnine",
                Tip = "Kuca",
                GodinaIzgradnje = 1987,
                CenaNekretnine = 110000,
                AgencijaId = 3,
                Agencija = new Agencija { Id = 3, Naziv = "Fast Nekretnine", GodinaOsnivanja = 2005 }
            };

            var mockRepository = new Mock<IOglasRepository>();

            

            var controller = new OglasiController(mockRepository.Object);

            // Act
            var actionResult = controller.Update(24, oglas) as BadRequestResult;

            // Assert
            Assert.NotNull(actionResult);
        }

        [Fact]
        public void PutOglas_ValidRequest_ReturnsObject()
        {
            // Arrange
            Oglas oglas = new Oglas()
            {
                Id = 1,
                Naslov = "Naj Nekretnine",
                Tip = "Kuca",
                GodinaIzgradnje = 1987,
                CenaNekretnine = 110000,
                AgencijaId = 3,
                Agencija = new Agencija { Id = 3, Naziv = "Fast Nekretnine", GodinaOsnivanja = 2005 }
            };

            var mockRepository = new Mock<IOglasRepository>();
            mockRepository.Setup(x => x.GetOne(1)).Returns(oglas);

       

            var controller = new OglasiController(mockRepository.Object);

            // Act
            var actionResult = controller.Update(1, oglas) as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            Assert.Equal(oglas, actionResult.Value);
        }

        [Fact]
        public void PostOglas_ValidRequest_SetsLocationHeader()
        {
            // Arrange
            Oglas oglas = new Oglas()
            {
                Id = 1,
                Naslov = "Naj Nekretnine",
                Tip = "Kuca",
                GodinaIzgradnje = 1987,
                CenaNekretnine = 110000,
                AgencijaId = 3,
                Agencija = new Agencija { Id = 3, Naziv = "Fast Nekretnine", GodinaOsnivanja = 2005 }
            };

            var mockRepository = new Mock<IOglasRepository>();
            mockRepository.Setup(x => x.GetOne(1)).Returns(oglas);

           

            var controller = new OglasiController(mockRepository.Object);
            // Act
            var actionResult = controller.Kreiraj(oglas) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(actionResult);

            Assert.Equal("GetOne", actionResult.ActionName);
            Assert.Equal(1, actionResult.RouteValues["id"]);

            Assert.NotNull(actionResult.Value);
            Assert.Equal(oglas, actionResult.Value);
        }

        [Fact]
        public void DeleteOglas_ValidId_ReturnsNoContent()
        {
            // Arrange
            Oglas oglas = new Oglas()
            {
                Id = 1,
                Naslov = "Naj Nekretnine",
                Tip = "Kuca",
                GodinaIzgradnje = 1987,
                CenaNekretnine = 110000,
                AgencijaId = 3,
                Agencija = new Agencija { Id = 3, Naziv = "Fast Nekretnine", GodinaOsnivanja = 2005 }
            };

            var mockRepository = new Mock<IOglasRepository>();
            mockRepository.Setup(x => x.GetOne(1)).Returns(oglas);

            

            var controller = new OglasiController(mockRepository.Object);
            // Act
            var actionResult = controller.Delete(1) as NoContentResult;

            // Assert
            Assert.NotNull(actionResult);
        }

        [Fact]
        public void DeleteCountry_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IOglasRepository>();

           

            var controller = new OglasiController(mockRepository.Object);
            // Act
            var actionResult = controller.Delete(12) as NotFoundResult;

            // Assert
            Assert.NotNull(actionResult);
        }
    }
}
