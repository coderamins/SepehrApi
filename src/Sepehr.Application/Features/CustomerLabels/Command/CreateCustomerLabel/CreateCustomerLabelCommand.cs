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

namespace Sepehr.Application.Features.CustomerLabels.Command.CreateCustomerLabel
{
    public partial class CreateCustomerLabelCommand : IRequest<Response<CustomerLabel>>
    {
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
    }
    public class CreateCustomerLabelCommandHandler : IRequestHandler<CreateCustomerLabelCommand, Response<CustomerLabel>>
    {
        private readonly ICustomerLabelRepositoryAsync _customerLabelRepository;
        private readonly IMapper _mapper;
        public CreateCustomerLabelCommandHandler(ICustomerLabelRepositoryAsync customerLabelRepository, IMapper mapper)
        {
            _customerLabelRepository = customerLabelRepository;
            _mapper = mapper;
        }

        public async Task<Response<CustomerLabel>> Handle(CreateCustomerLabelCommand request, CancellationToken cancellationToken)
        {
            var checkDuplicate = await _customerLabelRepository.GetCustomerLabelInfo(request);
            if (checkDuplicate != null)
                throw new ApiException(new ErrorMessageFactory().MakeError("برچسب مشتری", ErrorType.DuplicateForCreate));

            var customerLabel = _mapper.Map<CustomerLabel>(request);
            await _customerLabelRepository.AddAsync(customerLabel);

            return new Response<CustomerLabel>(customerLabel, new ErrorMessageFactory().MakeError("برچسب مشتری", ErrorType.CreatedSuccess));
        }

    }
}