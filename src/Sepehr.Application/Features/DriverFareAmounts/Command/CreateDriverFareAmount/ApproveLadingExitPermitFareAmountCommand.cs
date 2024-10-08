﻿using System;
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
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.DriverFareAmounts
{
    public partial class ApproveLadingExitPermitFareAmountCommand : IRequest<Response<DriverFareAmountApprove>>
    {
        public Guid LadingExitPermitId { get; set; }
        public double FareAmount { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class ApproveLadingExitPermitFareAmountCommandHandler :
        IRequestHandler<ApproveLadingExitPermitFareAmountCommand, Response<DriverFareAmountApprove>>
    {
        private readonly ILadingExitPermitRepositoryAsync _ladingExitPermit;
        private readonly IDriverFareAmountApproveRepositoryAsync _driverFareAmountApprove;
        private readonly IMapper _mapper;
        public ApproveLadingExitPermitFareAmountCommandHandler(
            ILadingExitPermitRepositoryAsync ladingExitPermit,
            IDriverFareAmountApproveRepositoryAsync driverFareAmountApprove,
            IMapper mapper)
        {
            _ladingExitPermit = ladingExitPermit;
            _driverFareAmountApprove = driverFareAmountApprove;
            _mapper = mapper;
        }

        public async Task<Response<DriverFareAmountApprove>> Handle(ApproveLadingExitPermitFareAmountCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var ladingExitPermit = await _ladingExitPermit.GetByIdAsync(request.LadingExitPermitId);
                if (ladingExitPermit == null)
                {
                    throw new ApiException("مجوز تخلیه یافت نشد !");
                }

                var driverFareAmount = _mapper.Map<DriverFareAmountApprove>(request);

                driverFareAmount = await _driverFareAmountApprove.AddAsync(driverFareAmount);

                if (ladingExitPermit.FareAmountStatusId==(int)EFareAmountStatus.Approved)
                    throw new ApiException("کرایه قبلا تایید شده است !");

                ladingExitPermit.FareAmountStatusId = (int)EFareAmountStatus.Approved;
                ladingExitPermit.FareAmount = request.FareAmount > 0 ? (decimal)request.FareAmount : ladingExitPermit.FareAmount;

                await _ladingExitPermit.UpdateAsync(ladingExitPermit);

                return new Response<DriverFareAmountApprove>(driverFareAmount,
                    "تایید کرایه با موفقیت انجام شد !");
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }

}