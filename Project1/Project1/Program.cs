using System.Reflection.Emit;

namespace Project1
{
    internal class Program
    {
        private static Character player;
        private static Weapon armor;
        private static Weapon sword;
        private static List <Weapon> weapons;

        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보 세팅
            armor = new Weapon("낡은 갑옷", "무쇠로 만들어져 있는 낡은 갑옷입니다.", 0, 5, 0, true);
            sword = new Weapon("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 2, 0, 0, false);

            weapons = new List <Weapon>();
            weapons.Add(armor);
            weapons.Add(sword);

        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    DisplayInventory();
                    break;
            }
        }

        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보르 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.Write($"공격력 :{player.Atk}");
            for ( int i = 0; i < weapons.Count; i++ )
            {
                if (weapons[i].Atk != 0 && weapons[i].Equip)
                {
                    Console.Write($" (+{weapons[i].Atk})");
                }
            }
            Console.WriteLine();
            Console.Write($"방어력 :{player.Def}");
            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].Def != 0 && weapons[i].Equip)
                {
                    Console.Write($" (+{weapons[i].Def})");
                }
            }
            Console.WriteLine();
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        static void DisplayInventory()
        {
            Console.Clear();

            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < weapons.Count; i++)
            {
                Console.Write($"- {i+1}");
                if (weapons[i].Equip)
                {
                    Console.Write("[E]");
                }
                Console.Write($"{weapons[i].Name}\t");
                if (weapons[i].Def != 0)
                {
                    Console.Write($"| 방어력 +{weapons[i].Def} |");
                }
                if (weapons[i].Atk != 0)
                {
                    Console.Write($"| 공격력 +{weapons[i].Atk} |");
                }
                Console.WriteLine($"{weapons[i].Note}");
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착관리");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    InventoryManagement();
                    break;
            }
        }

        static void InventoryManagement()
        {
            Console.Clear();

            Console.WriteLine("인벤토리 - 장착관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < weapons.Count; i++)
            {
                Console.Write($"- {i + 1}");
                if (weapons[i].Equip)
                {
                    Console.Write("[E]");
                }
                Console.Write($"{weapons[i].Name}\t");
                if (weapons[i].Def != 0)
                {
                    Console.Write($"| 방어력 +{weapons[i].Def} |");
                }
                if (weapons[i].Atk != 0)
                {
                    Console.Write($"| 공격력 +{weapons[i].Atk} |");
                }
                Console.WriteLine($"{weapons[i].Note}");
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, weapons.Count);
            switch (input)
            {
                case 0:
                    DisplayInventory();
                    break;
                case int n when 1 <= n && n <= weapons.Count:
                    if (weapons[n-1].Equip) { weapons[n-1].Equip = false; }
                    else { weapons[n-1].Equip = true;}
                    InventoryManagement();
                    break;
            }
        }

        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }


    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }

    public class Weapon
    {
        public string Name { get; }
        public string Note { get; }
        public int Gold { get; }
        public int Atk { get; }
        public int Def { get; }
        public bool Equip { get; set;  }

        public Weapon(string name, string note, int atk, int def, int gold, bool equip)
        {
            Name = name;
            Note = note;
            Atk = atk;
            Def = def;
            Gold = gold;
            Equip = equip;
        }

    }
}

