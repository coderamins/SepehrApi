using AutoFixture;
using AutoMapper;
using Moq;
using Sepehr.Application.Features.Products.Command.CreateProduct;
using Sepehr.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.TestService
{
    public class CreateProductCommandTest
    {
        private readonly Mock<IProductRepositoryAsync> _productRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;


        public CreateProductCommandTest()
        {
            _productRepositoryMock = new();
            _mapperMock = new();
        }

        [Fact]
        public async Task Handle_Should_ReturnOk()
        {
            Fixture fixutre = new Fixture();
            //Arrange
            var command = fixutre.Create<CreateProductCommand>();

            var handler = new CreateProductCommandHandler(
                _productRepositoryMock.Object,
                _mapperMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Succeeded);
            //Assert.Equal();

        }
    }
}
