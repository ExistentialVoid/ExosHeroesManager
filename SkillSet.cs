using System.Text;

namespace ExosHeroesManager;

/// <summary>
/// All related skills; Hero will adjust for Fatecore
/// </summary>
public sealed class SkillSet
{
    public Skill? Active1 { get; private set; }
    public Skill? Active2 { get; private set; }
    public string FormattedPassiveText
    {
        get
        {
            StringBuilder textbuilder = new();

            Passives.ForEach(skill =>
            {
                if (!Passives[0].Name.Equals(skill.Name))
                {
                    textbuilder.AppendLine($"[{skill.Name}]");
                }
                textbuilder.AppendLine(skill.Effect.Description + '\n');
            });

            return textbuilder.ToString();
        }
    }
    public List<Skill> Passives { get; private set; } = new();


    public SkillSet(object skillHolder)
    {
        SetSkills(skillHolder);
    }


    private void SetSkills(object skillHolder)
    {
        if (skillHolder == null)
        {
            return;
        }

        string passiveString = string.Empty;

        if (skillHolder is Fatecore fatecore)
        {
            Active1 = new(Convert.ToString(fatecore.Data?[(int)FatecoreColumn.Skill1]) ?? string.Empty);
            Active2 = new(Convert.ToString(fatecore.Data?[(int)FatecoreColumn.Skill2]) ?? string.Empty);
            passiveString = Convert.ToString(fatecore.Data?[(int)FatecoreColumn.Passives]) ?? string.Empty;
            passiveString.Split(';').ToList().ForEach(passive => Passives.Add(new(passive)));
        }
        else if (skillHolder is Hero hero && hero.Fatecore != null)
        {
            Active1 = hero.Fatecore.Skills.Active1;
            Active2 = hero.Fatecore.Skills.Active2;
            passiveString = Convert.ToString(hero.Data?[(int)HeroColumn.Passive]) ?? string.Empty;
            passiveString.Split(';').ToList().ForEach(passive => Passives.Add(new(passive)));
            Passives.AddRange(hero.Fatecore.Skills.Passives);
        }
        else if (skillHolder is Hero)
        {
            hero = (Hero)skillHolder;
            Active1 = new(Convert.ToString(hero.Data?[(int)HeroColumn.Active1]) ?? string.Empty);
            Active2 = new(Convert.ToString(hero.Data?[(int)HeroColumn.Active2]) ?? string.Empty);
            passiveString = Convert.ToString(hero.Data?[(int)HeroColumn.Passive]) ?? string.Empty;
            passiveString.Split(';').ToList().ForEach(passive => Passives.Add(new(passive)));
        }
    }

    public enum SkillSetHolder { Hero, Fatecore}
}
