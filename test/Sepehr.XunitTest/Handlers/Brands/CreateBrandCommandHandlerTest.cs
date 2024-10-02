using AutoMapper;
using Moq;
using Sepehr.Application.Features.Brands.Command.CreateBrand;
using Sepehr.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.XunitTest.Handlers.Brands
{
    public class CreateBrandCommandHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldCreateBrand()
        {
            var brandRepositoryMock = new Mock<IBrandRepositoryAsync>();
            var autoMapperMock=new Mock<IMapper>();
            var commandHandler = new CreateBrandCommandHandler(brandRepositoryMock.Object, autoMapperMock.Object);
        }
    }
}
