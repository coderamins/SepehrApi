using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.LogEntities;

namespace Sepehr.Application.Features.PurchaseOrders.Command.DeletePurchaseOrderById
{
    public class DeletePurchaseOrderByIdCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public class
        DeletePurchaseOrderByIdCommandHandler
        : IRequestHandler<DeletePurchaseOrderByIdCommand, Response<bool>>
        {
            private readonly IPurchaseOrderRepositoryAsync _PurchaseOrderRepository;
            private readonly ITableRecordRemovalRepositoryAsync _tableRecordRemoval;

            public DeletePurchaseOrderByIdCommandHandler(
                IPurchaseOrderRepositoryAsync PurchaseOrderRepository,
                ITableRecordRemovalRepositoryAsync tableRecordRemoval
            )
            {
                _PurchaseOrderRepository = PurchaseOrderRepository;
                _tableRecordRemoval = tableRecordRemoval;
            }

            public async Task<Response<bool>>
            Handle(
                DeletePurchaseOrderByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var PurchaseOrder = await _PurchaseOrderRepository.DeletePurchaseOrder(command.Id);

                //var PurchaseOrder = await _PurchaseOrderRepository.GetByIdAsync(command.Id);
                //if (PurchaseOrder == null)
                //    throw new ApiException($"سفارش یافت نشد !");

                //await _tableRecordRemoval.AddAsync(new TableRecordRemovalInfo { RemovedRecordId = PurchaseOrder.Id.ToString(), TableName = "purchaseOrder" });
                //await _PurchaseOrderRepository.DeleteAsync(PurchaseOrder);
                return new Response<bool>(true,"سفارش با موفقیت حذف شد .");
            }
        }
    }
}
