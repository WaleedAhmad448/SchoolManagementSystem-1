using SchoolManagementSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repositories
{
    public interface IAlertRepository : IGenericRepository<Alert>
    {
        Task<IEnumerable<Alert>> GetActiveAlertsAsync();
    }
}
