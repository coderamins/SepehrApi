using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.DraftOrders.Command.CreateDraftOrder
{
    public partial class CreateDraftOrderCommand : IRequest<Response<DraftOrder>>
    {
        public List<AttachmentDto>? Attachments { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class CreateDraftOrderCommandHandler : IRequestHandler<CreateDraftOrderCommand, Response<DraftOrder>>
    {
        private readonly IDraftOrderRepositoryAsync _draftOrderRepository;
        private readonly IMapper _mapper;
        public CreateDraftOrderCommandHandler(IDraftOrderRepositoryAsync draftOrderRepository, IMapper mapper)
        {
            _draftOrderRepository = draftOrderRepository;
            _mapper = mapper;
        }

        public async Task<Response<DraftOrder>> Handle(CreateDraftOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var draftOrder = _mapper.Map<DraftOrder>(request);
                await _draftOrderRepository.AddAsync(draftOrder);
                return new Response<DraftOrder>(draftOrder, "پیش نویس سفارش با موفقیت ایجاد گردید .");
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}