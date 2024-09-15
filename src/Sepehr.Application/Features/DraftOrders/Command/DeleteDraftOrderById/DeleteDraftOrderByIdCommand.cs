using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;

namespace Sepehr.Application.Features.DraftOrders.Command.DeleteDraftOrderById
{
    public class DeleteDraftOrderByIdCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public class
        DeleteDraftOrderByIdCommandHandler
        : IRequestHandler<DeleteDraftOrderByIdCommand, Response<bool>>
        {
            private readonly IDraftOrderRepositoryAsync _draftOrderRepository;

            public DeleteDraftOrderByIdCommandHandler(
                IDraftOrderRepositoryAsync draftOrderRepository
            )
            {
                _draftOrderRepository = draftOrderRepository;
            }

            public async Task<Response<bool>>
            Handle(
                DeleteDraftOrderByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var draftOrder = await _draftOrderRepository.GetByIdAsync(command.Id);
                if (draftOrder == null)
                    throw new ApiException($"پیش نویس سفارش یافت نشد !");
                 await _draftOrderRepository.DeleteAsync(draftOrder);
                return new Response<bool>(true, "پیش نویس سفارش با موفقیت حذف شد .");
            }
        }
    }
}
