using Common.Enums;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public static class UtilityService
    {
        private static readonly int[] experiences = {
            0, 0, 83, 174, 276, 388, 512, 650, 801, 969, 1154, 1358, 1584, 1833, 2107, 2411, 2746, 3115, 3523, 3973,
            4470, 5018, 5624, 6291, 7028, 7842, 8740, 9730, 10824, 12031, 13363, 14833, 16456, 18247, 20224, 22406,
            24815, 27473, 30408, 33648, 37224, 41171, 45529, 50339, 55649, 61512, 67983, 75127, 83014, 91721, 101333,
            111945, 123660, 136594, 150872, 166636, 184040, 203254, 224466, 247886, 273742, 302288, 333804, 368599,
            407015, 449428, 496254, 547953, 605032, 668051, 737627, 814445, 899257, 992895, 1096278, 1210421, 1336443,
            1475581, 1629200, 1798808, 1986068, 2192818, 2421087, 2673114, 2951373, 3258594, 3597792, 3972294, 4385776,
            4842295, 5346332, 5902831, 6517253, 7195629, 7944614, 8771558, 9684577, 10692629, 11805606, 13034431 };

        public static int GetLevelAtExperience(int experience)
        {
            int index;

            for (index = 0; index < experiences.Length; index++)
            {
                if (experiences[index + 1] > experience)
                    break;
            }

            return index;
        }

        public static int GetExperienceAtLevel(int level)
        {
            double total = 0;
            for (int i = 1; i < level; i++)
            {
                total += Math.Floor(i + 300 * Math.Pow(2, i / 7.0));
            }

            return (int)Math.Floor(total / 4);
        }

        public static int GetExperienceDifferenceBetweenLevels(int startLevel, int endLevel)
        {
            if(startLevel > endLevel || startLevel < 1 || endLevel > 120)
            {
                return 0;
            }

            var total = GetExperienceAtLevel(endLevel) - GetExperienceAtLevel(startLevel);

            return total;
        }

        public static string GetReadableGPPrice(long gpPrice)
        {
            if(gpPrice < 1000000 && gpPrice >= 1000)
            {
                return $"{gpPrice / 1000}K";
            }
            if(gpPrice >= 1000000)
            {
                return $"{gpPrice / 1000000}M";
            }

            return $"{gpPrice}";
        }

        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
        
        public static string GetSkillIcon(SkillType skillType)
        {
            switch (skillType)
            {
                case SkillType.Attack:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657808526524416/Attack_icon.png";
                case SkillType.Strength:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973658692274778132/Strength_icon.png";
                case SkillType.Defense:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657777710972968/Defence_icon.png";
                case SkillType.Magic:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657776981168168/Magic_icon.png";
                case SkillType.Ranged:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657777505468446/Ranged_icon.png";
                case SkillType.Prayer:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657777274777610/Prayer_icon.png";
                case SkillType.Mining:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657776347820032/Mining_icon.png";
                case SkillType.Fishing:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657775764807720/Fishing_icon.png";
                case SkillType.Woodcutting:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657721285017680/Woodcutting_icon.png";
                case SkillType.Hunter:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657719514992720/Hunter_icon.png";
                case SkillType.Farming:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657719976378438/Farming_icon.png";
                case SkillType.Cooking:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657775425065091/Cooking_icon.png";
                case SkillType.Smithing:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657776146501672/Smithing_icon.png";
                case SkillType.Fletching:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657720395812884/Fletching_icon.png";
                case SkillType.Firemaking:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657721515671572/Firemaking_icon.png";
                case SkillType.Herbalore:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657720832016384/Herblore_icon.png";
                case SkillType.Crafting:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657776549138526/Crafting_icon.png";
                case SkillType.Runecrafting:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657776775651418/Runecraft_icon.png";
                case SkillType.Construction:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657719733117058/Construction_icon.png";
                case SkillType.Agility:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657721075298324/Agility_icon.png";
                case SkillType.Thieving:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657720618094662/Thieving_icon.png";
                case SkillType.Slayer:
                    return "https://cdn.discordapp.com/attachments/971492312309985340/973657720173502554/Slayer_icon.png";
                default:
                    return "";
            }
        }
        //public int GetLevelAtExperience(int experience)
        //{
        //    int index;

        //    for (index = 0; index < 120; index++)
        //    {
        //        if (GetExperienceAtLevel(index + 1) > experience)
        //            break;
        //    }

        //    return index;
        //}
    }
}
