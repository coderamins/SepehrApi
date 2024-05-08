using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.DTOs.Order;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.LogEntities;

namespace Sepehr.Application.Features.Orders.Command.DeleteOrderById
{
    public class DeleteOrderByIdCommand : IRequest<Response<long>>
    {
        public Guid Id { get; set; }

        public class
        DeleteOrderByIdCommandHandler
        : IRequestHandler<DeleteOrderByIdCommand, Response<long>>
        {
            private readonly IOrderRepositoryAsync _OrderRepository;
            private readonly ITableRecordRemovalRepositoryAsync _tableRecordRemoval;
            private readonly IProductInventoryRepositoryAsync _productInventory;

            public DeleteOrderByIdCommandHandler(
                IOrderRepositoryAsync OrderRepository,
                IProductInventoryRepositoryAsync productInventory,
                ITableRecordRemovalRepositoryAsync tableRecordRemoval
            )
            {
                _OrderRepository = OrderRepository;
                _productInventory = productInventory;
                _tableRecordRemoval = tableRecordRemoval;
            }

            public async Task<Response<long>>
            Handle(
                DeleteOrderByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var order = await _OrderRepository.GetByIdAsync(command.Id);
                await _productInventory.UpdateProductInventory(order.Id,
                    new List<OrderDetailRequest>(), Domain.Enums.InventoryActionType.DeleteOrder);

                if (order == null)
                    throw new ApiException($"سفارش یافت نشد !");

                await _tableRecordRemoval.AddAsync(new TableRecordRemovalInfo { RemovedRecordId = order.Id.ToString(), TableName = "order" });
                await _OrderRepository.DeleteAsync(order);
                return new Response<long>("سفارش با موفقیت حذف شد .");
            }
        }
    }
}
