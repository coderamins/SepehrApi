using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.ShareHolders.Command.UpdateShareHolder
{
    public class UpdateShareHolderCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        [StringLength(150)]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(150)]
        public string LastName { get; set; } = string.Empty;
        [StringLength(150)]
        public string FatherName { get; set; } = string.Empty;
        [StringLength(11)]
        public string MobileNo { get; set; } = string.Empty;

        public class UpdateShareHolderCommandHandler : IRequestHandler<UpdateShareHolderCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IShareHolderRepositoryAsync _shareHolderRepository;
            public UpdateShareHolderCommandHandler(IShareHolderRepositoryAsync shareHolderRepository, IMapper mapper)
            {
                _shareHolderRepository = shareHolderRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateShareHolderCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var shareHolders = _shareHolderRepository.GetAllAsQueryable();

                    if (shareHolders.Any(s => s.MobileNo == command.MobileNo && command.Id != s.Id))
                        throw new ApiException("سهامدار با این شماره موبایل قبلا ایجاد شده است !");

                    var shareHolder = await _shareHolderRepository.GetByIdAsync(command.Id);
                    shareHolder = _mapper.Map<UpdateShareHolderCommand, ShareHolder>(command, shareHolder);

                    if (shareHolder == null)
                        throw new ApiException(new ErrorMessageFactory().MakeError("سهامدار", ErrorType.NotFound));
                    else
                    {
                        await _shareHolderRepository.UpdateAsync(shareHolder);
                        return new Response<string>(shareHolder.Id.ToString(), new ErrorMessageFactory().MakeError("سهامدار", ErrorType.UpdatedSuccess));
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