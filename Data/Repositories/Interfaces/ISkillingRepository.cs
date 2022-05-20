using Common.Enums;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface ISkillingRepository
    {
        Task<bool> AddSkillingMethod(string methodname, string description, string amount, SkillType skillType, int startLevel, int? endLevel);
        Task<bool> DeleteSkillingMethod(SkillType skillType, string methodname);
        Task<List<SkillingMethod>> GetSkillingMethodBySkillType(SkillType skillType);
        Task<SkillingMethodUpcharge> GetSkillingUpchargeBySkillingMethodId(int id);
        Task<bool> UpdateSkillingMethod(string methodName, SkillType skillType, string? newName = null, string? amount = null, string? description = null, int? startLevel = null, int? endLevel = null);
    }
}
