using Common.Enums;
using Data.Entities;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        Task<List<Service>> GetAllServiceOfType(ServiceType serviceType);
        Task<QuestsForQuote> GetServiceByNamesAndType(List<string> names, ServiceType serviceType);
    }
}
