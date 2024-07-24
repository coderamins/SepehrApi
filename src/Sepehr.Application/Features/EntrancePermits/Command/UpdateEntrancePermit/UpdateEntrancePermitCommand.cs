using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.Order;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.EntrancePermits.Command.UpdateEntrancePermit
{
    public class UpdateEntrancePermitCommand : IRequest<Response<EntrancePermit>>
    {
        public Guid Id { get; set; }
        public required Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Description { get; set; }
        public ExitType ExitType { get; set; }
        public int EntrancePermitSendTypeId { get; set; }
        public required int ProductBrandId { get; set; }
        public int OriginWarehouseId { get; set; }
        public int DestinationWarehouseId { get; set; }
        public int PaymentTypeId { get; set; }
        public int InvoiceTypeId { get; set; }
        public bool IsTemporary { get; set; }
        public int? CustomerOfficialCompanyId { get; set; }


        public virtual List<UpdateEntrancePermitDetailRequest> Details { get; set; }
        public virtual List<EntrancePermitPaymentDto>? OrderPayments { get; set; }
        public virtual List<OrderServiceDto>? OrderServices { get; set; }

        public class UpdateEntrancePermitCommandHandler : IRequestHandler<UpdateEntrancePermitCommand, Response<EntrancePermit>>
        {
            private readonly IEntrancePermitRepositoryAsync __entrancePermitRepository;
            private readonly IMapper _mapper;

            public UpdateEntrancePermitCommandHandler(IEntrancePermitRepositoryAsync _entrancePermitRepository, IMapper mapper)
            {
                __entrancePermitRepository = _entrancePermitRepository;
                _mapper = mapper;
            }
            public async Task<Response<EntrancePermit>> Handle(UpdateEntrancePermitCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    //var _entrancePermit = await __entrancePermitRepository.GetEntrancePermitById(command.Id);
                    var _entrancePermit = await __entrancePermitRepository.GetByIdAsync(command.Id);

                    if (_entrancePermit == null)
                    {
                        throw new ApiException($"مجوز ورود یاقت نشد !");
                    }
                    else
                    {
                        _entrancePermit = _mapper.Map(command, _entrancePermit);
                        _entrancePermit = await __entrancePermitRepository.UpdateEntrancePermit(_entrancePermit);

                        //await __entrancePermitRepository.UpdateAsync(_entrancePermit);
                        return new Response<EntrancePermit>(_entrancePermit, "");
                    }
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
    }
}