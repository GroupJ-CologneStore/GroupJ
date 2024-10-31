using CologneStore.Data;
using CologneStore.Models;
using Microsoft.EntityFrameworkCore;

namespace CologneStore.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task AddCompany(Company Company)
        {
            _context.Companies.Add(Company);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCompany(Company Company)
        {
            _context.Companies.Remove(Company);
            await _context.SaveChangesAsync();
        }

        public async Task<Company?> GetCompanyById(int id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task UpdateCompany(Company Company)
        {
            _context.Companies.Update(Company);
            await _context.SaveChangesAsync();
        }
    }
}
