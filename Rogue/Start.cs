using System;

Console.WriteLine("There is a cave entrance upon you, you shall slay every enemy in there");
Console.WriteLine("Mission > Slay every enemy");
Console.WriteLine();
Console.WriteLine("Press enter to continue");
Console.ReadLine();

Loading.Screen();
Console.ReadLine();
Console.Clear();

Character selectedCharacter = null;
bool choose = true;

while (choose == true)
{
    Console.WriteLine("Well before you go into that hell hole");
    Console.WriteLine("Choose your class:");
    Console.WriteLine();

    Console.WriteLine("Do you want to be the:");
    Console.WriteLine();

    Console.WriteLine("-- Vanguard --");
    Console.WriteLine(" Weapon: Longsword (common)");
    Console.WriteLine(" Armor: Heavy (rare)"); // armor level 4
    Console.WriteLine();

    Console.WriteLine("-- Ranger --");
    Console.WriteLine(" Weapon: Bow (Common)");
    Console.WriteLine(" Armor: Light (Common)"); // armor level 2
    Console.WriteLine();

    Console.WriteLine("-- Assassin --");
    Console.WriteLine(" Weapon: Double Daggers (rare)");
    Console.WriteLine(" Armor: Medium (rare)"); // armor level 3
    Console.WriteLine();

    Console.WriteLine("-- Warlock --");
    Console.WriteLine(" Weapon: Dark Tome");
    Console.WriteLine(" Armor: Light (common)"); // armor level 1
    Console.WriteLine();

    Console.WriteLine("-- Samurai --");
    Console.WriteLine(" Weapon: Katana (epic)");
    Console.WriteLine(" Armor: Medium (rare)"); // armor level 3
    Console.WriteLine();

    Console.WriteLine("-- Gunslinger --");
    Console.WriteLine(" Weapon: Revolver (rare)");
    Console.WriteLine(" Armor: Light"); // armor level 2
    Console.WriteLine();

    Console.WriteLine("-- Blood Knight --");
    Console.WriteLine(" Weapon: Blood Katana (epic)");
    Console.WriteLine(" Armor: Heavy (rare)"); // armor level 4
    Console.WriteLine();

    Console.WriteLine("-- Juggernaut --");
    Console.WriteLine(" Weapon: Dual Axes (rare)");
    Console.WriteLine(" Armor: Heavy (epic)");
    Console.WriteLine();

    Character vanguard = new Character("Vanguard", 120, 40, 4);
    Character ranger = new Character("Ranger", 90, 55, 2);
    Character assassin = new Character("Assassin", 85, 65, 3);
    Character warlock = new Character("Warlock", 80, 70, 1);
    Character samurai = new Character("Samurai", 95, 60, 3);
    Character gunslinger = new Character("Gunslinger", 90, 50, 2);
    Character bloodKnight = new Character("Blood Knight", 110, 55, 4);
    Character juggernaut = new Character("Juggernaut", 130, 45, 5);

    Console.WriteLine("So what do you choose?");
    string chosenClass = Console.ReadLine().Trim().ToLower();

    if (chosenClass == "vanguard")
        selectedCharacter = vanguard;
    else if (chosenClass == "ranger")
        selectedCharacter = ranger;
    else if (chosenClass == "assassin")
        selectedCharacter = assassin;
    else if (chosenClass == "warlock")
        selectedCharacter = warlock;
    else if (chosenClass == "samurai")
        selectedCharacter = samurai;
    else if (chosenClass == "gunslinger")
        selectedCharacter = gunslinger;
    else if (chosenClass == "blood knight")
        selectedCharacter = bloodKnight;
    else if (chosenClass == "juggernaut")
        selectedCharacter = juggernaut;
    else
    {
        Console.Clear();
        Console.WriteLine("There is no such class.");
        Console.WriteLine("- Try again -");
        Console.ReadLine();
        continue;
    }

    break;
}

Console.Clear();
Console.WriteLine($"You chose {selectedCharacter.Name}");
Console.WriteLine($"Health: {selectedCharacter.Health}");
Console.WriteLine($"Attack: {selectedCharacter.Attack}");
Console.WriteLine($"Defense: {selectedCharacter.Defense}");

Console.WriteLine();
Console.WriteLine("You went into the cave");
Console.WriteLine("You stumble upon 2 enemy skeletons, you take your stance");
Console.WriteLine();

Action.Text(selectedCharacter);
