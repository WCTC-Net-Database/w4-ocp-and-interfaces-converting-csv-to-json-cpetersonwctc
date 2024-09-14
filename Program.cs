using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;
using W4_assignment_template.Services;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace W4_assignment_template;

class Program
{
    static IFileHandler fileHandler;
    static List<Character> characters;
    static string filePath = "Files/input.csv"; // Default to CSV file


    static void Main()
    {
        fileHandler = new CsvFileHandler(); // Default to CSV handler
        characters = fileHandler.ReadCharacters(filePath);

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Display Characters");
            Console.WriteLine("2. Find Character");
            Console.WriteLine("3. Add Character");
            Console.WriteLine("4. Level Up Character");
            Console.WriteLine("5. Change File Format");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayAllCharacters();
                    break;
                case "2":
                    FindCharacter();
                    break;
                case "3":
                    AddCharacter();
                    break;
                case "4":
                    LevelUpCharacter();
                    break;
                case "5":
                    ChangeFileFormat();
                    break;
                case "6":
                    fileHandler.WriteCharacters(filePath, characters);
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void DisplayAllCharacters()
    {
        foreach (var character in characters)
        {
            string itemString = "";
            foreach (string item in character.Equipment)
            {
                if (itemString == "")
                {
                    itemString = item;
                }
                else
                {
                    itemString = itemString + ", " + item;
                } 
            }
            Console.WriteLine($"Name: {character.Name}, Class: {character.Class}, Level: {character.Level}, HP: {character.HP}, Equipment: {itemString}");
        }
    }

    static void FindCharacter()
    {
        Console.Write("Enter the name of the character to find: ");
        string nameToFind = Console.ReadLine();

        var character = characters.Find(c => c.Name.Equals(nameToFind, StringComparison.OrdinalIgnoreCase));
        if (character != null)
        {
            Console.WriteLine($"The stats for the character are:\n------------------\nName- {character.Name}\nClass- {character.Class}\nLevel- {character.Level}\nHealth- {character.HP}\nEquipment- {string.Join(", ", character.Equipment)}\n------------------");
        }
        else
        {
            Console.WriteLine($"No character by the name of \"{nameToFind}\" Found");
        }

    }

    static void AddCharacter()
    {
        Console.Write("Give the player a name (No Quotes): ");
        var name = Console.ReadLine();
        if (name != null && name.Contains(","))
        {
            name = '"' + name + '"';
        }
        Console.Write("Give the player a class: ");
        var charClass = Console.ReadLine();
        Console.Write("Give the player a level: ");
        int level = Convert.ToInt32(Console.ReadLine());
        Console.Write("Give the player a max HP: ");
        int hp = Convert.ToInt32(Console.ReadLine());

        var items = new List<string>();
        while (true)
        {
            Console.Write("Add item? (y/n): ");
            var addItem = Console.ReadLine()?.ToLower();
            if (addItem == "n" || addItem == "no")
            {
                break;
            }
            else if (addItem == "y" || addItem == "yes")
            {
                Console.WriteLine("Give the item a name: ");
                string? newItem = Console.ReadLine();
                if (newItem != null)
                {
                    items.Add(newItem);
                }
            }
        }
        Character newCharacter = new Character() { Name = name, Class = charClass, Level = level, HP = hp, Equipment = items };

        characters.Add(newCharacter);

    }

    static void LevelUpCharacter()
    {
        Console.Write("Enter the name of the character to level up: ");
        string nameToLevelUp = Console.ReadLine();

        var character = characters.Find(c => c.Name.Equals(nameToLevelUp, StringComparison.OrdinalIgnoreCase));
        if (character != null)
        {
            character.Level++;
            Console.WriteLine($"Character {character.Name} leveled up to level {character.Level}!");
        }
        else
        {
            Console.WriteLine("Character not found.");
        }
    }

    static void ChangeFileFormat()
    {
        fileHandler.WriteCharacters(filePath, characters);
        if (filePath == "Files/input.csv")
        {
            filePath = "Files/input.json";
            fileHandler = new JsonFileHandler();
            Console.WriteLine("Set file format to JSON");
        }
        else
        {
            filePath = "Files/input.csv";
            fileHandler = new CsvFileHandler();
            Console.WriteLine("Set file format to CSV");
        }
        characters = fileHandler.ReadCharacters(filePath);
    }

}