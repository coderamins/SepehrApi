using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.CustomerLabels.Command.UpdateCustomerLabel
{
    public class UpdateCustomerLabelCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        /// <summary>
        /// کد نوع برچسب
        /// </summary>
        public int CustomerLabelTypeId { get; set; }
        /// <summary>
        /// کد محصول
        /// </summary>
        public Guid? ProductId { get; set; }
        /// <summary>
        /// کد نوع محصول
        /// </summary>
        public int? ProductTypeId { get; set; }
        /// <summary>
        /// کد برند
        /// </summary>
        public int? BrandId { get; set; }
        /// <summary>
        /// کد کالابرند
        /// </summary>
        public int? ProductBrandId { get; set; }

        /// <summary>
        /// نام برچسب
        /// </summary>
        public string LabelName { get; set; } = string.Empty;

        public class UpdateCustomerLabelCommandHandler : IRequestHandler<UpdateCustomerLabelCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly ICustomerLabelRepositoryAsync _customerLabelRepository;
            public UpdateCustomerLabelCommandHandler(ICustomerLabelRepositoryAsync customerLabelRepository, IMapper mapper)
            {
                _customerLabelRepository = customerLabelRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateCustomerLabelCommand command, CancellationToken cancellationToken)
            {
                var customerLabel = await _customerLabelRepository.GetByIdAsync(command.Id);
                customerLabel = _mapper.Map<UpdateCustomerLabelCommand, CustomerLabel>(command, customerLabel);

                if (customerLabel == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("برچسب مشتری", ErrorType.NotFound));
                else
                {
                    await _customerLabelRepository.UpdateAsync(customerLabel);
                    return new Response<string>(customerLabel.Id.ToString(), new ErrorMessageFactory().MakeError("برچسب مشتری", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}