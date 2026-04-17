using System;

//återanvändbar kod (Loading.Screen())
// Jag välde en arrys här pga..
//     Fast 4 symboler, ändras aldrig
//     Bättre prestanda
//     Jag lägger inte till eller tar inte bort något
class Loading
{
    // Denna method går igenom alla strings i arrayen så att den visar en Loading
    // Vilket gör att den snurar runt
    public static void Screen()
    {
        string[] spinner = { "/", "-", "\\", "|" };
        for (int i = 0; i < 40; i++)
        {
            Console.Write($"\rLoading {spinner[i % spinner.Length]}");
            Thread.Sleep(100);
        }
        Console.WriteLine("\rLoading complete!");
    }
}

public class Character
{
    public string Name;
    public int Health;
    public int MaxHealth;
    public int Attack;
    public int Defense;
    public int Gold;
    public int CritChance = 21;
    public static bool HasRevive = false;
    public static bool ReviveUsed = false;
    public static bool HasBloodPact = false;
    public static int AttackCounter = 0;
    public static bool HasEchoGloves = false;
    public bool HasSpikedArmor;

    // Hämtar up allt namn, info om Character vi har vält
    public Character(string name, int health, int attack, int defense)
    {
        Name = name;
        Health = health;
        MaxHealth = health;
        Attack = attack;
        Defense = defense;
    }

    // När något healar kommer du inte få over-heal
    public void Heal(int amount)
    {
        Health += amount;
        if (Health > MaxHealth)
            Health = MaxHealth;
    }

    // Du får damage från denna method, men om din hp är lika/lägre än 0
    // och du har en specific item så kan du revive med den, annars dör du och förlorar
    public bool TakeDamage(int EnemyDamage)
    {
        Health -= EnemyDamage;
        if (Health < 0)
            Health = 0;

        if (Health <= 0)
        {
            if (HasRevive && !ReviveUsed)
            {
                ReviveUsed = true;
                Health = MaxHealth / 2;
                Console.WriteLine("Phoenix Heart activated");
                return false;
            }

            Console.WriteLine("You died");
            return true;
        }

        return false;
    }
}

namespace RPGGame
{
    public class ShopItem
    {
        public required string Name;
        public required string Description;
        public int Price;
        public required string Rarity;
        public required Action<Character> Effect;
    }

    public static class ItemLibrary
    {
        // välde List pga..
        // Item läggs till och tast bort, kan använda .add() osv
        public static List<ShopItem> GetFullInventory()
        {
            return new List<ShopItem>
            {
                new ShopItem
                {
                    Name = "Old Shield",
                    Rarity = "Common",
                    Price = 25,
                    Description = "+1 armor",
                    Effect = static (p) => p.Defense += 1,
                },
                new ShopItem
                {
                    Name = "Minor Potion",
                    Rarity = "Common",
                    Price = 20,
                    Description = "Restores 15 HP",
                    Effect = (p) => p.Heal(15),
                },
                new ShopItem
                {
                    Name = "Coin Purse",
                    Rarity = "Common",
                    Price = 30,
                    Description = "+10% gold per kill",
                    Effect = (p) => Action.GoldDropMultiplier += 0.1f,
                },
                new ShopItem
                {
                    Name = "Wooden Amulet",
                    Rarity = "Common",
                    Price = 25,
                    Description = "+10 Max HP",
                    Effect = (p) => p.MaxHealth += 10,
                },
                new ShopItem
                {
                    Name = "Lucky Pebble",
                    Rarity = "Common",
                    Price = 35,
                    Description = "+10% crit chance",
                    Effect = (p) => p.CritChance -= 1,
                },
                new ShopItem
                {
                    Name = "Simple Bandage",
                    Rarity = "Common",
                    Price = 15,
                    Description = "Heal 10 HP",
                    Effect = (p) => p.Heal(10),
                },
                new ShopItem
                {
                    Name = "Sharpened Knife",
                    Rarity = "Common",
                    Price = 40,
                    Description = "+10 Damage",
                    Effect = (p) => p.Attack += 10,
                },
                new ShopItem
                {
                    Name = "Vampiric Blade",
                    Rarity = "Rare",
                    Price = 75,
                    Description = "Heal 10% of damage dealt",
                    Effect = (p) => p.Heal((int)(p.Attack * 0.1)),
                },
                new ShopItem
                {
                    Name = "Spiked Armor",
                    Rarity = "Rare",
                    Price = 75,
                    Description = "Reflect 10% damage taken",
                    Effect = (p) => p.HasSpikedArmor = true,
                },
                new ShopItem
                {
                    Name = "Rogue's Cloak",
                    Rarity = "Rare",
                    Price = 80,
                    Description = "10% chance to avoid attacks",
                    Effect = (p) =>
                    {
                        int roll = new Random().Next(1, 11);
                        if (roll == 1)
                        {
                            Action.DodgeNextHit = true;
                            Console.WriteLine("You feel light on your feet");
                            Console.WriteLine("You dodge thier attack");
                            Console.ReadLine();
                        }
                    },
                },
                new ShopItem
                {
                    Name = "Bag of Fortune",
                    Rarity = "Rare",
                    Price = 70,
                    Description = "+15% gold finding",
                    Effect = (p) => Action.GoldDropMultiplier += 0.15f,
                },
                new ShopItem
                {
                    Name = "Phoenix Heart",
                    Rarity = "Epic",
                    Price = 150,
                    Description = "Revive once with 50% HP",
                    Effect = (p) => Character.HasRevive = true,
                },
                new ShopItem
                {
                    Name = "Blood Pact",
                    Rarity = "Epic",
                    Price = 120,
                    Description = "+25% Damage, -15% Max HP",
                    Effect = (p) =>
                    {
                        p.Attack = (int)(p.Attack * 1.25f);
                        p.MaxHealth = (int)(p.MaxHealth * 0.85f);
                    },
                },
                new ShopItem
                {
                    Name = "Dragon Fang Sword",
                    Rarity = "Legendary",
                    Price = 300,
                    Description = "+40% Dmg, +20% Gold",
                    Effect = (p) =>
                    {
                        p.Attack = (int)(p.Attack * 1.4f);
                        Action.GoldDropMultiplier += 0.2f;
                    },
                },
                new ShopItem
                {
                    Name = "Crown of Damned",
                    Rarity = "Legendary",
                    Price = 250,
                    Description = "+2% Gold kill, -20% Max HP",
                    Effect = (p) =>
                    {
                        Action.GoldDropMultiplier += 0.02f;
                        p.MaxHealth = (int)(p.MaxHealth * 0.8f);
                    },
                },
                new ShopItem
                {
                    Name = "Echo Gloves",
                    Rarity = "Legendary",
                    Price = 350,
                    Description = "Every 3rd attack repeats",
                    Effect = (p) => Character.HasEchoGloves = true,
                },
                new ShopItem
                {
                    Name = "Golden Furnace",
                    Rarity = "Legendary",
                    Price = 100,
                    Description = "Sacrifice 50 HP for 100 Gold",
                    Effect = (p) =>
                    {
                        if (p.Health > 50)
                        {
                            p.TakeDamage(50);
                            p.Gold += 100;
                        }
                    },
                },
            };
        }
    }
}

namespace RPGGame
{
    class BlackMarket
    {
        // Här får du köpa items som kan hjälpa dig med ditt run, men om du inte vill så kan du bara lämna shopen
        public static void Market(Character player)
        {
            {
                // Jag bygger up Inventory så det kommer lägga tills och tas bort, därför använder jag list
                // Jag använder metoder i items

                Random rnd = new Random();
                // get all items from librery
                List<ShopItem> allItems = ItemLibrary.GetFullInventory();

                // filter out items already owned (except consumables)
                List<ShopItem> availableItems = allItems.ToList();

                //Pick 4 random items to showcase
                List<ShopItem> showcase = availableItems.OrderBy(x => rnd.Next()).Take(4).ToList();

                bool shopping = true;
                while (shopping)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("--- THE BlackMarket MARKET ---");
                    Console.ResetColor();
                    Console.WriteLine(
                        $"Your Gold: {player.Gold} | Your HP: {player.Health}/{player.MaxHealth}\n"
                    );
                    Console.WriteLine(new string('-', 30));

                    ShowItems(showcase);
                    Console.WriteLine("0. Leave Market");

                    shopping = DoYouWantToBuy(player, showcase, shopping);
                    Action.Text(player);
                }
            }
        }

        private static bool DoYouWantToBuy(Character player, List<ShopItem> showcase, bool shopping)
        {
            Console.Write("\nWhat would you like to buy? ");
            string input = Console.ReadLine();

            if (input == "0")
                shopping = false;
            else if (int.TryParse(input, out int choice) && choice >= 1 && choice <= showcase.Count)
            {
                ShopItem selected = showcase[choice - 1];

                if (player.Gold >= selected.Price)
                {
                    player.Gold -= selected.Price;
                    selected.Effect(player); // Trigger the code from the Library
                    Console.WriteLine($"\nYou bought {selected.Name}!");
                    showcase.RemoveAt(choice - 1); // Remove from shelf after buying
                }
                else
                {
                    Console.WriteLine("\nYou don't have enough gold for that!");
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

            return shopping;
        }

        private static void ShowItems(List<ShopItem> showcase)
        {
            for (int i = 0; i < showcase.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {showcase[i].Name} [{showcase[i].Rarity}]");
                Console.WriteLine($"   Cost: {showcase[i].Price} Gold");
                Console.WriteLine($"   Effect: {showcase[i].Description}");
                Console.WriteLine(new string('-', 30));
            }
        }
    }
}

class Action
{
    public static int Round = 1;
    public static int RoundEnemyMultiplier = 1;
    public static int EnemyOneHealth = 100;
    public static int EnemyTwoHealth = 100;
    public static bool ExtraDefenseActive = false;
    public static int ExtraDefenseRounds = 0;
    public static float GoldDropMultiplier = 1.0f; //items that increase % gold
    public static int ExtraGoldPerKill = 0; //extra gold per kill
    private const int BaseGoldPerKill = 20; //base gold per kill
    private const float GoldScalingPerRound = 0.75f; //additional gold per Round
    private const float GoldScalingCap = 35f; //cap for scaling
    public static int EnemyDamage = 10;
    public static bool DodgeNextHit = false;
    public static bool CritActive = false;
    public static bool Defending;
    private static int defendRounds;

    // En UI for din Combat som visar allt du behöver veta
    public static void Text(Character player)
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== Combat ===");
            Console.ResetColor();
            Console.WriteLine($"HP: {player.Health}/{player.MaxHealth} | Round: {Round}\n");
            Console.WriteLine("Attack | Defend | Run");
            string input = Console.ReadLine().Trim().ToLower();

            bool flowControl = CheckAction(player, input);
            if (!flowControl)
            {
                return;
            }
        }
    }

    // Här kan du välja mellan attack, defend eller run.
    // Run kommer bara sluta spelet och defend ger dig damage reduction
    private static bool CheckAction(Character player, string input)
    {
        if (input == "attack")
        {
            Character.AttackCounter++;
            Attack(player);
        }
        else if (input == "defend")
        {
            Defending = true;
            defendRounds = 2;
            Console.WriteLine("Defense stance activated");
            EnemyTurn(player);
        }
        else if (input == "run")
        {
            Console.WriteLine("You fled the cave");
            return false;
        }
        else
        {
            Console.WriteLine("Invalid input");
            Console.ReadLine();
        }
        if (EnemyOneHealth <= 0 && EnemyTwoHealth <= 0)
        {
            CheckRoundEnd(player);
            return false;
        }
        return true;
    }

    // Låter dig välja mellan 2 fienden att attackera, efter attack om du har en item så kan du attack 2x
    static void Attack(Character player)
    {
        Console.Clear();
        Console.WriteLine("Choose target: Skeleton 1 or 2");
        string target = Console.ReadLine();
        int damage = player.Attack;
        int critMax = Math.Max(2, player.CritChance);
        ChooseEnemyToAttack(player, target, damage, critMax);

        if (Character.HasEchoGloves && Character.AttackCounter % 3 == 0)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Echo attack triggered!");
            Console.ResetColor();
            if (target == "1")
                EnemyOneHealth -= player.Attack;
            else
                EnemyTwoHealth -= player.Attack;
        }

        Console.WriteLine($"Skeleton 1 HP: {EnemyOneHealth}");
        Console.WriteLine($"Skeleton 2 HP: {EnemyTwoHealth}");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();

        EnemyTurn(player);
    }

    // Attack träffar 2 personer men gör andra fienden få mindre damage och du har en chanse att få crit
    private static void ChooseEnemyToAttack(
        Character player,
        string target,
        int damage,
        int critMax
    )
    {
        if (new Random().Next(1, critMax) == 1)
            CritActive = true;

        if (target == "1")
        {
            if (CritActive == true)
            {
                EnemyOneHealth -= damage * 3;
                CritActive = false;
            }
            else
                EnemyOneHealth -= damage;
            EnemyTwoHealth -= damage / 2;
        }
        else if (target == "2")
        {
            if (CritActive == true)
            {
                EnemyTwoHealth -= damage * 3;
                CritActive = false;
            }
            else
                EnemyTwoHealth -= damage;
            EnemyOneHealth -= damage / 2;
        }
        else
        {
            Console.WriteLine("Not valid Target");
            Console.ReadLine();
            Attack(player);
        }
    }

    // Kållar om dina fienden är döda, om inte attackerar men kan dodga den, damage reduction och extra om du väljde defende
    static void EnemyTurn(Character player)
    {
        if (EnemyOneHealth <= 0 && EnemyTwoHealth <= 0)
            return;

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("=== Enemy Turn ===");
        Console.ResetColor();

        if (DodgeNextHit)
        {
            DodgeNextHit = false;
            return;
        }

        float reduction = 1f - player.Defense * 0.05f;
        if (reduction < 0.2f)
            reduction = 0.2f;

        int dmg = (int)(EnemyDamage * reduction);
        if (Defending)
            dmg /= 2;
        HealthDeduction(player, dmg);
    }

    // här tar du damagen och kållar om du är död eller inte
    private static void HealthDeduction(Character player, int dmg)
    {
        player.TakeDamage(dmg);
        Console.WriteLine($"You took {dmg} damage");
        Console.WriteLine($"Your health is {player.Health}");

        if (Defending)
        {
            defendRounds--;
            if (defendRounds == 0)
                Defending = false;
        }

        if (player.Health <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Game Over!");
            Console.ResetColor();
            Console.ReadKey();
            return;
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        CheckRoundEnd(player);
        return;
    }

    // När dinna enemys är död så kommer du få gold, resetar enemys och buffar de, och tar dig till market,
    // om fiende är inte död forsäter den från combat
    static void CheckRoundEnd(Character player)
    {
        if (EnemyOneHealth <= 0 && EnemyTwoHealth <= 0)
        {
            float scaling = GoldScalingPerRound * (Round - 1);
            if (scaling > GoldScalingCap)
                scaling = GoldScalingCap;

            int gold = (int)((BaseGoldPerKill + scaling) * GoldDropMultiplier);
            player.Gold += gold;

            Console.WriteLine($"Round cleared. Gold +{gold}");

            Round++;
            EnemyOneHealth = 100 + Round * 5;
            EnemyTwoHealth = 100 + Round * 5;

            float scale = 1f + (Round - 1) * 0.15f;
            if (scale > 4f)
                scale = 4f;
            EnemyDamage = (int)(10 * scale);
            RPGGame.BlackMarket.Market(player);
        }
        else
            Text(player);
    }
}
// sort / fix / UI changes

// if extra time
//      Make more items
//      Abilitys (Limited amount)
//      Add more actions
//      Remove useless code
//      Add permanent unlimited Heal and Ability points items in shop
