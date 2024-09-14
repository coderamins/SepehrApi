using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.DraftOrders.Command.UpdateDraftOrder
{
    public class UpdateDraftOrderCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        public class UpdateDraftOrderCommandHandler : IRequestHandler<UpdateDraftOrderCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IDraftOrderRepositoryAsync _draftOrderRepository;
            public UpdateDraftOrderCommandHandler(IDraftOrderRepositoryAsync draftOrderRepository, IMapper mapper)
            {
                _draftOrderRepository = draftOrderRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateDraftOrderCommand command, CancellationToken cancellationToken)
            {
                var draftOrder = await _draftOrderRepository.GetByIdAsync(command.Id);

                draftOrder = _mapper.Map<UpdateDraftOrderCommand, DraftOrder>(command, draftOrder);

                if (draftOrder == null)
                {
                    throw new ApiException($"پیش نویس سفارش یافت نشد !");
                }
                else
                {
                    await _draftOrderRepository.UpdateAsync(draftOrder);
                    return new Response<string>(draftOrder.Id.ToString(), "");
                }
            }
        }
    }
}