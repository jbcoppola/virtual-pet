using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPet
{
	class Program
	{
		static void Main(string[] args)
		{
			string input = "";
			Dog dog = Adopt();
			while (input != "quit")
			{
			}
		}

		//quits if input is "quit"
		static string GetInput()
		{
			string input = Console.ReadLine();
			if (input.ToLower() == "quit")
			{
				Environment.Exit(0);
			}
			return input;
		}

		//runs at the start of the program and chooses which kind of dog the user has
		static Dog Adopt()
		{
			Console.WriteLine("Welcome to the Dog Nursery. Which dog would you like to adopt? (Enter a number, \"quit\" to quit)");
			Console.WriteLine("1 - Black lab");
			Console.WriteLine("2 - Golden Retriever");
			Console.WriteLine("3 - Welsh Corgi");
			Console.WriteLine("4 - Australian Shepard");

			//find which breed the dog is
			string breed = GetInput();

			//verify
			while (breed != "1" && breed != "2" && breed != "3" && breed != "4")
			{
				Console.WriteLine("Invalid input. Input a number 1-4.");
				breed = GetInput();
			}

			Console.WriteLine("What is your new best friend's name?");
			string name = GetInput();

			Dog dog;
			switch (breed)
			{
				case "1":
					//labs have medium intelligence, lots of energy, a low maintenence coat and a huge appetite.
					dog = new Dog(name, 5, 4, 1, 9);
					break;
				case "2":
					//goldens have medium intelligence, moderate energy, a high maintence long coat and a large appetite.
					dog = new Dog(name, 5, 6, 7, 7);
					break;
				case "3":
					//corgis are very intelligent, have lots of energy, a short coat and lower appetite.
					dog = new Dog(name, 8, 3, 3, 4);
					break;
				case "4":
					//aussies are extremely intelligent, have boatloads of energy, a high maintenence coat and very large appetite
					dog = new Dog(name, 10, 1, 10, 8);
					break;
				default:
					//should never happen
					dog = new Dog(name, 5, 5, 5, 5);
					break;
			}

			return dog;
		}
	}
}
