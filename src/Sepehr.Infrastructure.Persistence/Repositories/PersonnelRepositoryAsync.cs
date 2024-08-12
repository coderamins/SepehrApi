using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Features.Personnels.Queries.GetAllPersonnels;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class PersonnelRepositoryAsync : GenericRepositoryAsync<Personnel>, IPersonnelRepositoryAsync
    {
        private readonly DbSet<Personnel> _personnels;
        private readonly DbSet<OrderDetail> _orderDetail;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public PersonnelRepositoryAsync(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _personnels = dbContext.Set<Personnel>();
            _orderDetail = dbContext.Set<OrderDetail>();
            _dbContext = dbContext;
            _mapper = mapper;
        }
 
        public async Task<List<Personnel>> GetAllPersonnels(GetAllPersonnelsParameter filter)
        {
            return await _personnels
                .Include(c => c.ApplicationUser)
                .Include(c => c.Phonebook).ThenInclude(p => p.PhoneNumberType)
                .Where(c =>
                        (c.NationalCode == filter.NationalCode || string.IsNullOrEmpty(filter.NationalCode)) &&
                        (c.PersonnelCode == filter.PersonnelCode || filter.PersonnelCode == null) &&
                        (string.Concat(c.FirstName, " ", c.LastName).Contains(filter.PersonnelName) || string.IsNullOrEmpty(filter.PersonnelName)) &&
                        (c.Phonebook != null && c.Phonebook.Any(p => p.PhoneNumber.Contains(filter.PhoneNumber)) || filter.PersonnelCode == null)
                        )
                .OrderByDescending(p => p.Created).ToListAsync();
        }

        public async Task<Personnel?> GetPersonnelInfo(string nationalId)
        {
            return await _personnels
                .Include(c => c.Phonebook).ThenInclude(p => p.PhoneNumberType)
                .FirstOrDefaultAsync(p => p.NationalId == nationalId);
        }

        public async Task<Personnel?> GetPersonnelInfo(Guid Id)
        {
            return await _personnels
                .Include(c => c.Phonebook).ThenInclude(p => p.PhoneNumberType)
                .FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<Personnel> UpdatePersonnel(Personnel personnel)
        {
            var pers_phones = _dbContext.Phonebook
                .Where(p => p.PersonnelId == personnel.Id);
            if (pers_phones != null)
                _dbContext.Phonebook.RemoveRange(pers_phones);

            _personnels.Update(personnel);
            await _dbContext.SaveChangesAsync();

            return personnel;
        }

    }
}