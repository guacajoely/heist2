using System;
using System.Collections.Generic;

namespace Heist
{
    public class Muscle : IRobber
    {
        public string Name { get; set; }
        public int SkillLevel { get; set; }
        public int PercentageCut { get; set; }

        public void PerformSkill(Bank bank)
        {
            bank.SecurityGuardScore = (bank.SecurityGuardScore - 50);
            Console.WriteLine(
                $"{Name} is taking out the secuity guards. Decreased security 50 points"
            );

            if (bank.SecurityGuardScore < 1)
            {
                Console.WriteLine($"{Name} has neutralized all security guards!");
            }
        }

        public Muscle(string name, int skill, int cut)
        {
            Name = name;
            SkillLevel = skill;
            PercentageCut = cut;
        }
    }
}
