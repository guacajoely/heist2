using System;
using System.Collections.Generic;

namespace Heist
{
    public class Hacker : IRobber
    {
        public string Name { get; set; }
        public int SkillLevel { get; set; }
        public int PercentageCut { get; set; }

        public void PerformSkill(Bank bank)
        {
            bank.AlarmScore = (bank.AlarmScore - 50);
            Console.WriteLine($"{Name} is hacking the alarm system. Decreased security 50 points");

            if (bank.AlarmScore < 1)
            {
                Console.WriteLine($"{Name} has disabled the alarm system!");
            }
        }

        public Hacker(string name, int skill, int cut)
        {
            Name = name;
            SkillLevel = skill;
            PercentageCut = cut;
        }
    }
}
