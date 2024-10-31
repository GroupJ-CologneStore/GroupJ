using CologneStore.Models;

namespace CologneStore.Repositories
{
    public interface ICompanyRepository
    {
        Task AddCompany(Company Company);
        Task DeleteCompany(Company Company);
        Task<Company?> GetCompanyById(int id);
        Task UpdateCompany(Company Company);
        Task<IEnumerable<Company>> GetCompanies();
    }
}
