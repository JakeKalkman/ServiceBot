using Common.Enums;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class SkillingRepository : ISkillingRepository
    {
        private readonly ServiceBotContext _db;
        public SkillingRepository(ServiceBotContext db)
        {
            _db = db;
        }

        public async Task<List<SkillingMethod>> GetSkillingMethodBySkillType(SkillType skillType)
        {
            var skillingMethods = await _db.SkillingMethod
                .Include(x => x.SkillingMethodUpcharges)
                .Where(x => x.SkillType == skillType)
                .ToListAsync();

            return skillingMethods;
        }

        public async Task<SkillingMethodUpcharge> GetSkillingUpchargeBySkillingMethodId(int id)
        {
            var methodUpcharge = await _db.SkillingMethodUpcharge.FirstAsync(x => x.SkillingMethodId == id);

            return methodUpcharge;
        }

        public async Task<bool> DeleteSkillingMethod(SkillType skillType, string methodname)
        {
            var skillingMethod = _db.SkillingMethod
                .FirstOrDefault(x => x.Name.ToLower() == methodname.ToLower() && x.SkillType == skillType);

            if(skillingMethod == null)
            {
                return false;
            }

            _db.Remove(skillingMethod);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddSkillingMethod(string methodname, string description, string amount, SkillType skillType, int startLevel, int? endLevel)
        {
            try
            {
                var amountParsed = double.Parse(amount);
                var skillingMethod = new SkillingMethod()
                {
                    Name = methodname,
                    Description = description,
                    Amount = amountParsed,
                    SkillType = skillType,
                    StartLevel = startLevel,
                    EndLevel = endLevel ?? 120
                };

                _db.Add(skillingMethod);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateSkillingMethod(string methodName, SkillType skillType,string? newName = null, string? amount = null, string? description =null, int? startLevel = null, int? endLevel = null)
        {
            try
            {
                var skillingMethod = _db.SkillingMethod
                .FirstOrDefault(x => x.Name.ToLower() == methodName.ToLower() && x.SkillType == skillType);

                if (skillingMethod == null)
                {
                    return false;
                }

                if(newName != null)
                {
                    skillingMethod.Name = newName;
                }

                if(amount != null)
                {
                    skillingMethod.Amount = double.Parse(amount);
                }
                if(description != null)
                {
                    skillingMethod.Description = description;
                }
                if(startLevel != null)
                {
                    skillingMethod.StartLevel = startLevel;
                }
                if(endLevel != null)
                {
                    skillingMethod.EndLevel = endLevel;
                }

                _db.Update(skillingMethod);
                await _db.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }           
        }

    }
}
