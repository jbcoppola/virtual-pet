using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace VirtualPet
{
	class Program
	{
		static void Main(string[] args)
		{
			//initial setup
			Dog dog = Adopt();
			//set timer to 30 seconds
			dog.NeedIncrement(30);

			//main loop
			while (true)
			{
				dog.DisplayStats();
				ActionLoop(dog);
			}
		}

		//quits if input is "quit"
		static string GetInput()
		{
			string input = Console.ReadLine();
			if (input.ToLower() == "quit")
			{
				Console.WriteLine("\"Woof\" (Come back soon)");
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
					Console.WriteLine("Labs have medium intelligence, lots of energy, a low maintenence coat and a huge appetite.");
					dog = new Dog(name, 5, 4, 1, 9);
					break;
				case "2":
					Console.WriteLine("Goldens have medium intelligence, moderate energy, a high maintence long coat and a large appetite.");
					dog = new Dog(name, 5, 6, 7, 7);
					break;
				case "3":
					Console.WriteLine("Corgis are very intelligent, have lots of energy, a short coat and lower appetite.");
					dog = new Dog(name, 8, 3, 3, 4);
					break;
				case "4":
					Console.WriteLine("Aussies are extremely intelligent, have boatloads of energy, a high maintenence coat and very large appetite");
					dog = new Dog(name, 10, 1, 10, 8);
					break;
				default:
					//should never happen
					dog = new Dog(name, 5, 5, 5, 5);
					break;
			}
			return dog;
		}

		//gives the user their choices of what they can do
		static void DisplayActionMenu(Dog dog)
		{
			Console.WriteLine("What will you do?");
			Console.WriteLine();
			Console.WriteLine("1 - Feed {0}", dog.Name);
			Console.WriteLine("2 - Fill {0}'s water bowl", dog.Name);
			Console.WriteLine("3 - Play with {0}", dog.Name);
			Console.WriteLine("4 - Brush {0}", dog.Name);
			Console.WriteLine("5 - Take {0} outside", dog.Name);
			Console.WriteLine("6 - Pet {0}", dog.Name);
			Console.WriteLine("7 - Put {0} to bed", dog.Name);
			Console.WriteLine("8 - Teach {0} a trick", dog.Name);
			Console.WriteLine();
		}

		//makes sure they pick the correct range of actions
		static string VerifyAction(string input)
		{
			while (	input != "1" && input != "2" && input != "3" && input != "4" && 
					input != "5" && input != "6" && input != "7" && input != "8"	)
			{
				Console.WriteLine("Invalid input. Enter 1-8");
				input = Console.ReadLine();
			}
			return input;
		}

		//gets input, verifies it and picks an action to take, then checks the values before returning dog
		static Dog ActionLoop(Dog dog)
		{
			DisplayActionMenu(dog);
			string input = VerifyAction(GetInput());
			switch (input)
			{
				case "1":
					dog.Feed();
					break;
				case "2":
					dog.GiveWater();
					break;
				case "3":
					dog.Play();
					break;
				case "4":
					dog.Brush();
					break;
				case "5":
					dog.Poop();
					break;
				case "6":
					dog.Pet();
					break;
				case "7":
					dog.Sleep();
					break;
				case "8":
					dog.LearnTrick();
					break;
				default:
					break;
			}
			dog.CheckConditions();
			return dog;
		}
	}
}
