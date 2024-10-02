using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Sepehr.Application.DTOs.Customer;
using Sepehr.Application.DTOs.Phonebook;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.Customers.Command.CreateCustomer
{
    public partial class ImportCustomersCommand : IRequest<Response<bool>>
    {
        public IFormFile FileData { get; set; }
    }
    public class ImportCustomersCommandHandler : IRequestHandler<ImportCustomersCommand, Response<bool>>
    {
        private readonly ICustomerRepositoryAsync _customerRepository;
        private readonly IMapper _mapper;
        public ImportCustomersCommandHandler(ICustomerRepositoryAsync customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(ImportCustomersCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<Customer> customers = new List<Customer>();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var stream = new MemoryStream())
                    await NewMethod(request, stream, cancellationToken); return new Response<bool>(true, "اطلاعات مشتریان با موفقیت ایجاد گردید .");
            }
            catch (Exception e)
            {

                throw;
            }
        }

        private async Task NewMethod(ImportCustomersCommand request, MemoryStream stream, CancellationToken cancellationToken)
        {
            await request.FileData.CopyToAsync(stream, cancellationToken);

            using (var package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;
                List<CustomerOfficialCompany> _comps = new List<CustomerOfficialCompany>();
                for (int row = 2; row <= rowCount; row++)
                {
                    //CustomerDto newCust = new CustomerDto();
                    var customer = await _customerRepository.GetCustomerInfoByName((worksheet.Cells[row, 1]?.Value ?? "").ToString());
                    if (customer != null)
                    {
                        for (int y = 1; y <= 6; y++)
                        {
                            string custCompany = (worksheet.Cells[row, y]?.Value ?? "").ToString();
                            if (!string.IsNullOrEmpty(custCompany))
                            {
                                _comps.Add(new CustomerOfficialCompany
                                {
                                    CompanyName = custCompany,
                                    CustomerId = customer.Id,
                                    //Customer = customer,
                                });
                            }
                        }
                    }
                    //string _custValidity = (worksheet.Cells[row, 3]?.Value ?? "").ToString();
                    //string _isSupplier = (worksheet.Cells[row, 4]?.Value ?? "").ToString();
                    //string _settleType = (worksheet.Cells[row, 5]?.Value ?? "").ToString();
                    //string _customerCharacteristics =(worksheet.Cells[row, 7]?.Value ?? "").ToString();
                    //string[] mobiles= (worksheet.Cells[row, 9]?.Value ?? "").ToString().Split('-');
                    //string[] phones= (worksheet.Cells[row, 8]?.Value ?? "").ToString().Split('-');

                    //List<CreatePhonebookRequest> phonebooks = new List<CreatePhonebookRequest>();
                    //foreach (var item in mobiles)
                    //{
                    //    if (!string.IsNullOrEmpty(item) && !phonebooks.Any(x=>x.PhoneNumber==item.Trim()))
                    //    {
                    //        phonebooks.Add(new CreatePhonebookRequest
                    //        {
                    //            PhoneNumber = item.Trim(),
                    //            PhoneNumberTypeId = (int)EPhoneNoType.Mobile
                    //        });
                    //    }
                    //}

                    //foreach (var item in phones )
                    //{
                    //    if (!string.IsNullOrEmpty(item) && item.Length>=4 && !phonebooks.Any(x => x.PhoneNumber == item.Trim()))
                    //    {
                    //        phonebooks.Add(new CreatePhonebookRequest
                    //        {
                    //            PhoneNumber = item.Trim(),
                    //            PhoneNumberTypeId = (int)EPhoneNoType.Office
                    //        });
                    //    }
                    //}


                    //newCust.FirstName=string.Empty;
                    //newCust.LastName=string.Empty;
                    //newCust.OfficialName =(worksheet.Cells[row, 2].Value ?? "").ToString()?.Trim();
                    //newCust.Address1 = (worksheet.Cells[row, 11].Value ?? "").ToString();
                    //newCust.CustomerValidityId = _custValidity == "VIP" ? (int)ECustomerValidity.VIP:
                    //                _custValidity == "نامطلوب" ? (int)ECustomerValidity.undesirable:(int)ECustomerValidity.Usual;
                    //newCust.Address2=string.Empty;
                    //newCust.Representative= (worksheet.Cells[row, 10].Value ?? "").ToString();
                    //newCust.CustomerCharacteristics = _customerCharacteristics;
                    //newCust.IsSupplier = _isSupplier=="+" ? true:false;
                    //SettlementType settlementType=
                    //    _settleType.Contains("خروج") ? SettlementType.BeforeExit: SettlementType.AfterExit;

                    //newCust.Phonebook=phonebooks;

                    //int settlementDay = 0;
                    //int.TryParse(_settleType,out settlementDay);// ? int.Parse(_settleType) : 0;


                    //customers.Add(_mapper.Map<Customer>(newCust));
                }

                _customerRepository.AddCustomerCompany(_comps);
            }


            //await _customerRepository.AddAsync(customers);

        }
    }
}