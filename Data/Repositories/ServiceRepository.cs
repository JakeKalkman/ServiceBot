using Common.Enums;
using Data.Entities;
using Data.Models;
using Data.Repositories.Interfaces;
using FuzzySharp;
using FuzzySharp.SimilarityRatio.Scorer.Composite;
using FuzzySharp.SimilarityRatio.Scorer.StrategySensitive;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ServiceBotContext _db;

        public ServiceRepository(ServiceBotContext db)
        {
            _db = db;
        }

        public async Task<QuestsForQuote> GetServiceByNamesAndType(List<string> names, ServiceType serviceType)
        {
            var loweredNames = names.Select(x => x.ToLower()).ToList();
            var services = await _db.Service.Where(x => x.ServiceType == serviceType).ToListAsync();
            var serviceNames = services.Select(x => x.Name.ToLower()).ToList();
            var correctedNames = new List<string>();
            var unfoundNames = new List<string>();

            foreach(var name in loweredNames)
            {
                var process = Process.ExtractTop(name, serviceNames, cutoff: 75)
                    .OrderByDescending(x => x.Score)
                    .ThenByDescending(x => x.Index)
                    .FirstOrDefault();

                if(process != null)
                {
                    correctedNames.Add(process.Value);
                    serviceNames.Remove(process.Value);
                }
                else
                {
                    var properNameIndex = loweredNames.IndexOf(name);
                    unfoundNames.Add(names[properNameIndex]);
                }
            }

            var foundServices = services.Where(x => correctedNames.Contains(x.Name.ToLower())).ToList();
            
            return new QuestsForQuote(foundServices, unfoundNames);
        }

        public async Task<List<Service>> GetAllServiceOfType(ServiceType serviceType)
        {
            var services = await _db.Service.Where(x => x.ServiceType == serviceType).ToListAsync();

            return services;
        }
    }
}
