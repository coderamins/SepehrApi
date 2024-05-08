using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.ShareHolders.Queries.GetAllShareHolders;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.ShareHolders.Queries.GetAllShareHolders
{
    public class GetAllShareHoldersQuery : IRequest<PagedResponse<IEnumerable<ShareHolderViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? ShareHolderCode { get; set; }
    }
    public class GetAllShareHoldersQueryHandler :
         IRequestHandler<GetAllShareHoldersQuery, PagedResponse<IEnumerable<ShareHolderViewModel>>>
    {
        private readonly IShareHolderRepositoryAsync _shareHolderRepository;
        private readonly IMapper _mapper;
        public GetAllShareHoldersQueryHandler(IShareHolderRepositoryAsync shareHolderRepository, IMapper mapper)
        {
            _shareHolderRepository = shareHolderRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<ShareHolderViewModel>>> Handle(
            GetAllShareHoldersQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllShareHoldersParameter>(request);
                var shareHolder = await _shareHolderRepository.GetAllAsync();

                var shareHolders = shareHolder
                    .Where((b => b.ShareHolderCode == validFilter.ShareHolderCode || validFilter.ShareHolderCode == null))
                    .AsEnumerable();

                var shareHolderViewModel = _mapper.Map<IEnumerable<ShareHolderViewModel>>(
                    shareHolders.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize).ToList());


                return new PagedResponse<IEnumerable<ShareHolderViewModel>>(
                    shareHolderViewModel.OrderByDescending(p=>p.Id),
                    validFilter.PageNumber,
                    validFilter.PageSize, shareHolders.Count());
            }
            catch (Exception e) 
            {

                throw;
            }
        }
    }
}