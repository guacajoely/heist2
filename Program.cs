using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Heist
{
    class Program
    {
        static void Main(string[] args)
        {
            Heist();
        }

        static void Heist()
        {
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine($"                   Plan your Heist                  ");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine();

            AddAnotherCandidate();
        }

        // STORE ALL ROBBERS
        static public List<IRobber> rolodex = new List<IRobber>()
        {
            new Hacker("Henry", 50, 10),
            new Hacker("Harriet", 50, 10),
            new LockSpecialist("Luke", 50, 10),
            new LockSpecialist("Lucy", 50, 10),
            new Muscle("Mark", 50, 10),
            new Muscle("Mary", 50, 10)
        };

        // STORE BANK INFO
        static public Bank theBank = new Bank()
        {
            AlarmScore = new Random().Next(1, 100),
            VaultScore = new Random().Next(1, 100),
            SecurityGuardScore = new Random().Next(1, 100),
            CashOnHand = new Random().Next(50000, 1000000)
        };

        // STORE ALL CREW MEMBERS
        static public List<IRobber> crew = new List<IRobber>();

        static void AddCandidate()
        {
            string name;
            string specialty;
            string skill;
            string cut;
            int parsedSkill;
            int parsedCut;
            int parsedSpecialty;

            try
            {
                // GET NEW ROBBER'S NAME FROM USER
                Console.WriteLine();
                Console.Write("What is the candidate's NAME?: ");
                name = Console.ReadLine();

                // GET NEW ROBBER'S SPECIALTY FROM USER
                Console.WriteLine();
                Console.WriteLine("What is the candidate's SPECIALTY?");
                Console.WriteLine("1.Hacker   2.Muscle   3.Lock Specialist");
                Console.Write("Select one: ");

                specialty = Console.ReadLine();
                parsedSpecialty = int.Parse(specialty);

                // CHECK THAT USER ENTERED SPECIALTY 1, 2 or 3
                if (parsedSpecialty < 1 || parsedSpecialty > 3)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a valid response");
                    AddCandidate();
                }

                // GET NEW ROBBER'S SKILL LEVEL FROM USER
                Console.WriteLine();
                Console.WriteLine("What is the candidate's SKILL LEVEL?");
                Console.Write("Enter a number between 1 and 100: ");

                skill = Console.ReadLine();
                parsedSkill = int.Parse(skill);

                // CHECK THAT USER ENTERED SKILL LEVEL BETWEEN 1 AND 100
                if (parsedSkill < 0 || parsedSkill > 100)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a valid response");
                    AddCandidate();
                }

                // GET NEW ROBBER'S CUT PERCENTAGE FROM USER
                Console.WriteLine();
                Console.WriteLine("What cut would the new candidate receive?");
                Console.Write("Enter a reasonable percentage: ");

                cut = Console.ReadLine();
                parsedCut = int.Parse(cut);

                // CHECK THAT USER ENTERED CUT BETWEEN 0 AND 100
                if (parsedCut < 0 || parsedCut > 100)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a valid response");
                    AddCandidate();
                }

                // CREATE A NEW Robber OBJECT USING THE MATCHING CLASS (hacker, muscle, or lockspecialist) and ADD IT TO THE ROLODEX
                if (parsedSpecialty == 1)
                {
                    var newRecruit = new Hacker(name, parsedSkill, parsedCut);
                    rolodex.Add(newRecruit);
                }
                else if (parsedSpecialty == 2)
                {
                    var newRecruit = new Muscle(name, parsedSkill, parsedCut);
                    rolodex.Add(newRecruit);
                }
                else
                {
                    var newRecruit = new LockSpecialist(name, parsedSkill, parsedCut);
                    rolodex.Add(newRecruit);
                }

                Console.WriteLine();
                ;

                // ASK USER IF THEY'D LIKE TO ADD ANOTHER ROBBER
                AddAnotherCandidate();
            }
            catch
            {
                // PROMPT USER TO ENTER VALID RESPONSE IF CAN'T PARSE
                Console.WriteLine();
                Console.WriteLine("Please enter a valid response");

                AddCandidate();
            }
        }

        static void AddAnotherCandidate()
        {
            Console.WriteLine(
                $"There are currently {rolodex.Count} candidates to choose from for your heist"
            );

            PrintRolo();

            Console.WriteLine();
            Console.Write($"Would you like to add another candidate? (Y/N): ");
            string answer = Console.ReadLine().ToLower();

            while (answer != "y" && answer != "n")
            {
                Console.Write($"Would you like to add another candidate? (Y/N): ");
                answer = Console.ReadLine().ToLower();
            }

            if (answer == "y")
            {
                AddCandidate();
            }
            else
            {
                ReconReport();
                AddCandidateToCrew();
            }
        }

        static void PrintRolo()
        {
            int index = 0;

            foreach (IRobber robber in rolodex)
            {
                int currentDistro = crew.Sum(x => x.PercentageCut);
                int maxCut = 100 - currentDistro;

                //get specialty from class type
                string specialty = robber.GetType().ToString();
                string specialtyString;
                if (specialty == "Heist.Hacker")
                {
                    specialtyString = "Hacker";
                }
                else if (specialty == "Heist.Muscle")
                {
                    specialtyString = "Muscle";
                }
                else
                {
                    specialtyString = "Lock Specialist";
                }

                if (robber.PercentageCut < maxCut && !crew.Contains(robber))
                    Console.WriteLine(
                        $"{index}. {robber.Name}({specialtyString}) Skill:{robber.SkillLevel} Cut:{robber.PercentageCut}%"
                    );

                index++;
            }
        }

        static void ReconReport()
        {
            string mostSecure;
            string leastSecure;

            //find most secure
            if (
                theBank.AlarmScore > theBank.VaultScore
                && theBank.AlarmScore > theBank.SecurityGuardScore
            )
            {
                mostSecure = "Alarm";
            }
            else if (
                theBank.VaultScore > theBank.AlarmScore
                && theBank.VaultScore > theBank.SecurityGuardScore
            )
            {
                mostSecure = "Vault";
            }
            else
            {
                mostSecure = "Guards";
            }

            //find least secure
            if (
                theBank.AlarmScore < theBank.VaultScore
                && theBank.AlarmScore < theBank.SecurityGuardScore
            )
            {
                leastSecure = "Alarm";
            }
            else if (
                theBank.VaultScore < theBank.AlarmScore
                && theBank.VaultScore < theBank.SecurityGuardScore
            )
            {
                leastSecure = "Vault";
            }
            else
            {
                leastSecure = "Guards";
            }

            Console.WriteLine();
            Console.WriteLine("RANDOMLY GENERATING TARGET BANK");
            Console.WriteLine();
            Console.WriteLine("RECON REPORT");
            Console.WriteLine("-----------------");
            Console.WriteLine($"Most Secure: {mostSecure}");
            Console.WriteLine($"Least Secure: {leastSecure}");
            Console.WriteLine();
            Console.WriteLine($"Now let's assemble our crew for the heist...");
            Console.WriteLine();
        }

        static void AddCandidateToCrew()
        {
            string newCandidateIndex;
            int parsedIndex;

            try
            {
                PrintRolo();
                // GET NEW CREW MEMBER'S INDEX FROM USER
                Console.WriteLine();
                Console.Write("Enter the Index of the candidate you'd like to add to the crew: ");

                newCandidateIndex = Console.ReadLine();
                int.TryParse(newCandidateIndex, out parsedIndex);

                IRobber matchingCandidate = rolodex[parsedIndex];
                crew.Add(matchingCandidate);

                // ASK USER IF THEY'D LIKE TO ADD ANOTHER TEAM MEMBER
                AddAnotherCandidateToCrew();
            }
            catch
            {
                // PROMPT USER TO ENTER VALID RESPONSE IF CAN'T PARSE
                Console.WriteLine();
                Console.WriteLine("Please enter a valid response");

                AddCandidateToCrew();
            }
        }

        static void AddAnotherCandidateToCrew()
        {
            Console.WriteLine();
            Console.Write($"Would you like to add another crew member? (Y/N): ");
            string answer = Console.ReadLine().ToLower();

            while (answer != "y" && answer != "n")
            {
                Console.Write($"Would you like to add another crew member? (Y/N): ");
                answer = Console.ReadLine().ToLower();
            }

            if (answer == "y")
            {
                AddCandidateToCrew();
            }
            else
            {
                AttemptHeist();
            }
        }

        static void AttemptHeist()
        {
            foreach (IRobber member in crew)
            {
                member.PerformSkill(theBank);
            }

            if(theBank.IsSecure()){
                Console.WriteLine("Sorry, your crew failed the heist.");
                Replay();
            }

            else{
                Console.WriteLine("Congratulations! Your crew successfully pulled off the heist.");
                Console.WriteLine("Here was everyone's take:");

                int overallTake = theBank.CashOnHand;
                int alreadyDist = 0;

                foreach (IRobber member in crew)
                {
                    int take = (member.PercentageCut * theBank.CashOnHand) / 100;
                    Console.WriteLine($"{member.Name}'s cut was ${take}");
                    alreadyDist = alreadyDist + take;
                }

                int userTake = overallTake - alreadyDist;
                Console.WriteLine($"After paying your crew, you stashed the remaining ${userTake} for yourself!");

                Replay();
            }



        }

        // ONCE GAME ENDS PROMPT PLAY AGAIN QUESTION
        static void Replay()
        {
            Console.WriteLine();
            Console.Write($"Play Again? (Y/N):");
            string answer = Console.ReadLine().ToLower();

            while (answer != "y" && answer != "n")
            {
                Console.Write($"Play Again? (Y/N):");
                answer = Console.ReadLine().ToLower();
            }

            if (answer == "y")
            {
                //RESET GLOBAL VARIABLES FROM PREVIOUS HEIST
                // successfulHeists = 0;
                crew = new List<IRobber>();
                rolodex = new List<IRobber>()
                {
                    new Hacker("Henry", 50, 10),
                    new Hacker("Harriet", 50, 10),
                    new LockSpecialist("Luke", 50, 10),
                    new LockSpecialist("Lucy", 50, 10),
                    new Muscle("Mark", 50, 10),
                    new Muscle("Mary", 50, 10)
                };
                theBank = new Bank()
                {
                    AlarmScore = new Random().Next(1, 100),
                    VaultScore = new Random().Next(1, 100),
                    SecurityGuardScore = new Random().Next(1, 100),
                    CashOnHand = new Random().Next(50000, 1000000)
                };

                Heist();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
