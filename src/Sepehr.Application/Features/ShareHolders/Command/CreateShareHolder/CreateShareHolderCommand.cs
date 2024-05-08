using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

namespace Sepehr.Application.Features.ShareHolders.Command.CreateShareHolder
{
    public partial class CreateShareHolderCommand : IRequest<Response<ShareHolder>>
    {
        [StringLength(150)]
        public required string FirstName { get; set; } 
        [StringLength(150)]
        public required string LastName { get; set; }
        [StringLength(150)]
        public string FatherName { get; set; } = string.Empty;
        [StringLength(11)]
        public string MobileNo { get; set; } = string.Empty;
    }
    public class CreateShareHolderCommandHandler : IRequestHandler<CreateShareHolderCommand, Response<ShareHolder>>
    {
        private readonly IShareHolderRepositoryAsync _shareHolderRepository;
        private readonly IMapper _mapper;
        public CreateShareHolderCommandHandler(
            IShareHolderRepositoryAsync shareHolderRepository, 
            IMapper mapper)
        {
            _shareHolderRepository = shareHolderRepository;
            _mapper = mapper;
        }

        public async Task<Response<ShareHolder>> Handle(CreateShareHolderCommand request, CancellationToken cancellationToken)
        {
            var shareHolders = _shareHolderRepository.GetAllAsQueryable();

            if (shareHolders.Any(s=>s.MobileNo==request.MobileNo))
                throw new ApiException(new ErrorMessageFactory().MakeError("سهامدار", ErrorType.DuplicateForCreate));

            var pbrand = _mapper.Map<ShareHolder>(request);
            await _shareHolderRepository.AddAsync(pbrand);

            return new Response<ShareHolder>(pbrand, new ErrorMessageFactory().MakeError("سهامدار", ErrorType.CreatedSuccess));
        }

    }
}