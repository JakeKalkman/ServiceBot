using Common.Enums;
using Data.Repositories.Interfaces;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public  class AdminService : IAdminService
    {
        private readonly ISkillingRepository _skillingRepository;
        public AdminService(ISkillingRepository skillingRepository)
        {
            _skillingRepository = skillingRepository;
        }

        public async Task<bool> AddSkillingMethod(string methodname, string description, string amount, SkillType skillType, int startLevel, int? endLevel)
        {
            var addResult = await _skillingRepository.AddSkillingMethod(methodname, description, amount, skillType, startLevel, endLevel);

            return addResult;
        }

        public async Task<bool> UpdateSkillingMethod(string methodName, SkillType skillType, string? newName = null, string? amount = null, string? description = null, int? startLevel = null, int? endLevel = null)
        {
            var updateResult = await _skillingRepository.UpdateSkillingMethod(methodName, skillType, newName, amount, description, startLevel, endLevel);

            return updateResult;
        }

        public async Task<bool> DeleteSkillingMethod(SkillType skillType, string methodname)
        {
            var deleteResult = await _skillingRepository.DeleteSkillingMethod(skillType, methodname);

            return deleteResult;
        }
    }
}
