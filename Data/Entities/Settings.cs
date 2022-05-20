using Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Settings : BaseEntity
    {
        public string SettingName { get; set; } = default!;
        public string SettingValue { get; set; } = default!;
    }
}
