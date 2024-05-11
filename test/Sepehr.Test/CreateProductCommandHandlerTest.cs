using AutoFixture;
using AutoMapper;
using Moq;
using Sepehr.Application.Features.Orders.Command.CreateOrder;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;

namespace Sepehr.Test
{
    public class CreateOrderCommandHandlerTest
    {
        private readonly Mock<IOrderRepositoryAsync> _orderRepoMock;
        private readonly Mock<IPurchaseOrderRepositoryAsync> _purOrderRepoMock;
        private readonly Mock<IProductRepositoryAsync> _productRepoMock;
        private readonly Mock<IProductInventoryRepositoryAsync> _prodInventoryRepoMock;
        private readonly Mock<ISmsService> _smsServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        

        public CreateOrderCommandHandlerTest()
        {
            _orderRepoMock = new();
            _purOrderRepoMock = new();
            _productRepoMock=new(); 
            _prodInventoryRepoMock=new();
            _mapperMock = new();
            _smsServiceMock= new();
        }

        [Fact]
        public async Task Handle_Should_ReturnOk()
        {
            Fixture fixutre = new Fixture();
            //Arrange
            var command = fixutre.Create<CreateOrderCommand>();

            var handler = new CreateOrderCommandHandler(
                _orderRepoMock.Object,
                _purOrderRepoMock.Object,
                _productRepoMock.Object,
                _prodInventoryRepoMock.Object,
                _mapperMock.Object,
                _smsServiceMock.Object
                );

            //Act
           var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Succeeded);
            //Assert.Equal();

        }
    }
}
