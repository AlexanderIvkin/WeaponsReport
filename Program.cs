using System;
using System.Collections.Generic;
using System.Linq;

namespace WeaponsReport
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int soldiersCount = 20;
            List<string> names = new List<string>
            {
                "Виктор", "Алексей", "Дмитрий",
                "Александр", "Олег", "Андрей",
                "Геннадий", "Антон", "Роман",
                "Валерий", "Инокентий", "Глеб"
            };
            List<string> weapons = new List<string>
            {
                "Нож", "Лопата", "Пистолет",
                "Автомат", "Рогатка", "Кулаки",
                "Когти", "Разум", "Воля"
            };
            List<string> ranks = new List<string>
            {
                "Рядовой", "Ефрейтор", "Генерал",
                "Сержант", "Лейтенант", "Самый вообще главный",
                "Чуть ещё главнее самого главного"
            };
            int[] militaryServicesRange = new int[]
            {
                5, 20
            };
            SoldiersFactory soldiersFactory = new SoldiersFactory(names, weapons, ranks, militaryServicesRange);
            WeaponsReport weaponsReport = new WeaponsReport(soldiersFactory.Create(soldiersCount));

            weaponsReport.Execute();
        }

        class WeaponsReport
        {
            private List<Soldier> _soldiers;

            public WeaponsReport(List<Soldier> soldiers)
            {
                _soldiers = soldiers;
            }

            public void Execute()
            {
                Console.WriteLine("Вот вообще все кто есть:\n");
                ShowInfo(_soldiers);
                var requiredSoldiers = _soldiers.Select(soldier => new { soldier.Name, soldier.Rank });
                Console.WriteLine("\nА вот после выборки:\n");
                int count = 1;

                foreach (var soldier in requiredSoldiers)
                {
                    Console.WriteLine(count++ + " " + soldier.Name + " " + soldier.Rank);
                }
            }

            private void ShowInfo(List<Soldier> soldiers)
            {
                int count = 1;
                foreach (Soldier soldier in soldiers)
                {
                    Console.Write(count++);
                    soldier.ShowInfo();
                }
            }
        }

        class SoldiersFactory
        {
            private List<string> _names;
            private List<string> _weapons;
            private List<string> _ranks;
            private int[] _militaryServicesRange;

            public SoldiersFactory(List<string> names, List<string> weapons, List<string> ranks, int[] militaryServicesRange)
            {
                _names = names;
                _weapons = weapons;
                _ranks = ranks;
                _militaryServicesRange = militaryServicesRange;
            }

            public List<Soldier> Create(int count)
            {
                List<Soldier> soldiers = new List<Soldier>();

                for (int i = 0; i < count; i++)
                {
                    soldiers.Add(new Soldier(_names[UserUtills.GenerateRandomPosiriveLimitedNumber(_names.Count)],
                        _weapons[UserUtills.GenerateRandomPosiriveLimitedNumber(_weapons.Count)],
                        _ranks[UserUtills.GenerateRandomPosiriveLimitedNumber(_ranks.Count)],
                        UserUtills.GenerateNumberByArrayLimits(_militaryServicesRange)));
                }

                return soldiers;
            }
        }

        class Soldier
        {
            public Soldier(string name, string weapon, string rank, int militaryService)
            {
                Name = name;
                Weapon = weapon;
                Rank = rank;
                MilitaryService = militaryService;
            }

            public string Name { get; }
            public string Weapon { get; }
            public string Rank { get; }
            public int MilitaryService { get; }

            public void ShowInfo()
            {
                Console.WriteLine($" Имя - {Name}. Вооружение - {Weapon}. Звание - {Rank}. Срок службы(мес.) - {MilitaryService}");
            }
        }

        static class UserUtills
        {
            private static Random s_random = new Random();

            public static int GenerateRandomPosiriveLimitedNumber(int maxValueExclusive)
            {
                return s_random.Next(maxValueExclusive);
            }

            public static int GenerateNumberByArrayLimits(int[] limits)
            {
                Array.Sort(limits);

                return s_random.Next(limits[0], limits[1]);
            }
        }
    }
}
