using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPet
{
	enum DogNeeds
	{
		hunger,					// 0-100, lower is better
		hungerDecayRate,		// 0-10, higher means hunger decays faster
		thirst,					// 0-100, lower is better
		boredom,				// 0-100, lower is better
		bond,					// 0-200, higher is better
		coatShag,				// 0-10, lower is better
		coatShagDecayRate,		// 0-10, higher means coat shag increases faster
		energy,					// 0-100, higher is more energy
		energyDecayRate,        // 0-10, higher means they get tired faster
		intelligence,           // 1-10, determines learning rate for tricks and boredom decay rate (smarter dogs get bored faster)
		waste					// 0-100, lower is better
	};

	class Dog
	{
		//fields
		private string name;
		private string breed;
		private short[] dogValues;

		//properties
		public string Name
		{
			get { return name; }
		}

		public short Hunger
		{
			get { return dogValues[(int)DogNeeds.hunger]; }
			set { dogValues[(int)DogNeeds.hunger] = value; }
		}

		public short HungerDecayRate
		{
			get { return dogValues[(int)DogNeeds.hungerDecayRate]; }
			set { dogValues[(int)DogNeeds.hungerDecayRate] = value; }
		}

		public short Thirst
		{
			get { return dogValues[(int)DogNeeds.thirst]; }
			set { dogValues[(int)DogNeeds.thirst] = value; }
		}

		public short Boredom
		{
			get { return dogValues[(int)DogNeeds.boredom]; }
			set { dogValues[(int)DogNeeds.boredom] = value; }
		}

		public short Bond
		{
			get { return dogValues[(int)DogNeeds.bond]; }
			set { dogValues[(int)DogNeeds.bond] = value; }
		}

		public short CoatShag
		{
			get { return dogValues[(int)DogNeeds.coatShag]; }
			set { dogValues[(int)DogNeeds.coatShag] = value; }
		}

		public short CoatShagDecayRate
		{
			get { return dogValues[(int)DogNeeds.coatShagDecayRate]; }
			set { dogValues[(int)DogNeeds.coatShagDecayRate] = value; }
		}

		public short Energy
		{
			get { return dogValues[(int)DogNeeds.energy]; }
			set { dogValues[(int)DogNeeds.energy] = value; }
		}

		public short EnergyDecayRate
		{
			get { return dogValues[(int)DogNeeds.energyDecayRate]; }
			set { dogValues[(int)DogNeeds.energyDecayRate] = value; }
		}

		public short Intelligence
		{
			get { return dogValues[(int)DogNeeds.intelligence]; }
			set { dogValues[(int)DogNeeds.intelligence] = value; }
		}

		public short Waste
		{
			get { return dogValues[(int)DogNeeds.waste]; }
			set { dogValues[(int)DogNeeds.waste] = value; }
		}

		//constructors
		//conditions vary based on breed
		public Dog(string name, short intelligence, short energyDecayRate, short coatShagDecayRate, short hungerDecayRate)
		{
			this.name = name;
			this.Hunger = 35;
			this.HungerDecayRate = hungerDecayRate;
			this.Thirst = 35;
			this.Boredom = 35;
			this.Waste = 10;
			this.CoatShag = 1;
			this.CoatShagDecayRate = coatShagDecayRate;
			this.Energy = 50;
			this.Bond = 0;
			this.Intelligence = intelligence;
		}
		
		//methods
		public void DisplayStats()
		{
			if (Bond > 180)
			{
				Console.WriteLine("You are {0}'s sunshine.", Name);
			}
			else if (Bond > 140)
			{
				Console.WriteLine("You are a very special person to {0}.", Name);
			}
			else if (Bond > 100)
			{
				Console.WriteLine("{0} loves to cuddle on your lap.", Name);
			}
			else if (Bond > 60)
			{
				Console.WriteLine("{0} is comfortable around you.", Name);
			}
			else if (Bond > 30)
			{
				Console.WriteLine("{0} has started to trust you.", Name);
			}
			else
			{
				Console.WriteLine("{0} doesn't know you very well yet.", Name);
			}
			Console.WriteLine();

			Console.WriteLine("Hunger: {0}", Hunger);
			Console.WriteLine("Thirst: {0}", Thirst);
			Console.WriteLine("Boredom: {0}", Boredom);
			Console.WriteLine("Waste: {0}", Waste);
			Console.WriteLine("Energy: {0}", Energy);

			Console.WriteLine();
			
		}
		//run after every other method to prevent overflow
		public void CheckConditions()
		{
			if (Hunger > 100)
			{
				Hunger = 100;
			}
			if (Thirst > 100)
			{
				Thirst = 100;
			}
			if (Boredom > 100)
			{
				Boredom = 100;
			}
			if (CoatShag > 10)
			{
				CoatShag = 10;
			}
			if (Energy < 0)
			{
				Energy = 0;
			}
			if (Bond > 200)
			{
				Bond = 200;
			}
			if (Waste > 100)
			{
				Waste = 100;
			}
		}

		//treat hunger
		public void Feed()
		{
			Waste += (short)(Hunger / 2);
			Thirst += (short)(Hunger / 3);
			Energy += 5;
			Hunger = 0;
		}

		//treat thirst
		public void GiveWater()
		{
			Waste += (short)(Thirst / 2);
			Thirst = 0;
		}

		//treat boredom
		public void Play()
		{
			Boredom -= 30;
			Energy -= 30;
			Random rand = new Random();
			int temp = rand.Next(10);

			//dogs bond through playing
			if (temp > 6)
			{
				Bond += 20;
			}
			//...but it gets them dirty
			if (temp > 4)
			{
				CoatShag++;
			}
		}

		//treat waste
		public void Poop()
		{
			Waste = 0;
		}

		//treat coatShag
		public void Brush()
		{
			Random rand = new Random();
			int temp = rand.Next(10);

			//most dogs love being brushed
			if (CoatShag < 5 && temp > 8)
			{
				Bond += 10;
			}
			//...but brushing a very shaggy dog will try their patience
			else if (CoatShag > 7)
			{
				Boredom += 5;

				if (temp > 8)
				{
					Bond -= 5;
				}
			}

			CoatShag = 0;
			Boredom += 5;
			Energy -= 5;
		}

		//obligatory
		public void Pet()
		{
			Bond += 5;
		}
	}
}
