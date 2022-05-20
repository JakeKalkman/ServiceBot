using Common.Enums;
using Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class SkillingMethod : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public double Amount { get; set; }
        public int? StartLevel { get; set; }
        public int? EndLevel { get; set; }
        public SkillType SkillType { get; set; }

        public ICollection<SkillingMethodUpcharge>? SkillingMethodUpcharges { get; set; }

    }
}
