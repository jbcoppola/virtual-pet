using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
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
		coatShag,				// 0-100, lower is better
		coatShagDecayRate,		// 0-10, higher means coat shag increases faster
		energy,					// 0-100, higher is more energy
		energyDecayRate,        // 0-10, higher means they get tired faster
		intelligence,           // 1-10, determines learning rate for tricks and boredom decay rate (smarter dogs get bored faster)
		waste,					// 0-100, lower is better
		tricksLearned,			// 0-8, higher means bond increases faster
		trickProgress,			// 0-10, keeps track of progress to learning next trick
		last
	};


	class Dog
	{
		//fields
		private string name;
		private short[] dogValues = new short[(int)DogNeeds.last];
		private Dictionary<string, bool> dogTricks = new Dictionary<string, bool>();
		private static Timer needTimer;

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

		public short TricksLearned
		{
			get { return dogValues[(int)DogNeeds.tricksLearned]; }
			set { dogValues[(int)DogNeeds.tricksLearned] = value; }
		}

		public short TrickProgress
		{
			get { return dogValues[(int)DogNeeds.trickProgress]; }
			set { dogValues[(int)DogNeeds.trickProgress] = value; }
		}

		//constructors
		//conditions vary based on breed
		public Dog(string name, short intelligence, short energyDecayRate, short coatShagDecayRate, short hungerDecayRate)
		{
			this.name = name;
			Hunger = 35;
			this.HungerDecayRate = hungerDecayRate;
			Thirst = 35;
			Boredom = 35;
			Waste = 10;
			CoatShag = 1;
			this.CoatShagDecayRate = coatShagDecayRate;
			Energy = 50;
			this.EnergyDecayRate = energyDecayRate;
			Bond = 0;
			this.Intelligence = intelligence;
			TricksLearned = 0;
			TrickProgress = 10;

			dogTricks = new Dictionary<string, bool>();
			dogTricks.Add("Sit", false);
			dogTricks.Add("Stay", false);
			dogTricks.Add("Down", false);
			dogTricks.Add("Come", false);
			dogTricks.Add("Heel", false);
			dogTricks.Add("Fetch", false);
			dogTricks.Add("Shake", false);
			dogTricks.Add("Play dead", false);
		}
		
		//methods
		
		//display the current stats for the dog
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
				Console.WriteLine("{0} loves to cuddle with you.", Name);
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
			Console.WriteLine("Coat shagginess: {0}", CoatShag);
			Console.WriteLine("Tricks learned: {0}", TricksLearned);

			Console.WriteLine();
		}

		//run after every other method to prevent variable overflow
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
			else if (Boredom < 0)
			{
				Boredom = 0;
			}
			if (CoatShag > 100)
			{
				CoatShag = 100;
			}
			if (Energy < 0)
			{
				Energy = 0;
			}
			else if (Energy > 100)
			{
				Energy = 100;
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
			Console.WriteLine("{0} scarfs down kibble.", Name);
		}

		//treat thirst
		public void GiveWater()
		{
			Waste += (short)(Thirst / 2);
			Thirst = 0;
			Console.WriteLine("{0} laps the water dish.", Name);
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
				Bond += (short)(TricksLearned * 3);
			}
			//...but it gets them dirty
			if (temp > 4)
			{
				CoatShag += CoatShagDecayRate;
			}
			Console.WriteLine("{0} romps around the room.", Name);
		}

		//treat waste
		public void Poop()
		{
			Waste = 0;
			Console.WriteLine("{0} sniffs an overgrown patch of yard to go in.", Name);
		}

		//treat coatShag
		public void Brush()
		{
			Random rand = new Random();
			int temp = rand.Next(10);

			//most dogs love being brushed
			if (CoatShag < 50 && temp > 8)
			{
				Bond += TricksLearned;
			}
			//...but brushing a very shaggy dog will try their patience
			else if (CoatShag > 70)
			{
				Boredom += Intelligence;

				if (temp > 8)
				{
					Bond -= 10;
				}
			}

			CoatShag = 0;
			Boredom += Intelligence;
			Energy -= EnergyDecayRate;
			Console.WriteLine("{0} wiggles under the brush.", Name);
		}

		//obligatory
		public void Pet()
		{
			Bond += TricksLearned;
			Console.WriteLine("{0} leans into your hand.", Name);
		}

		//treat energy
		public void Sleep()
		{
			Thirst += (short)((100 - Energy) / 4);
			Hunger += (short)((100 - Energy) / 2);
			Energy = 100;
			Console.WriteLine("{0} lets out a quiet sleep-bark.", Name);
		}

		//display tricks known and tricks not known
		public void DisplayTricks()
		{
			foreach(KeyValuePair<string,bool> kvp in dogTricks)
			{
				if (kvp.Value == true)
				{
					Console.WriteLine("{0}: known", kvp.Key);
				}
				else
				{
					Console.WriteLine("{0}: not known", kvp.Key);
				}
			}
			Console.WriteLine();
		}

		//learn new trick
		public void LearnTrick()
		{
			//first list the tricks
			DisplayTricks();

			//tired or bored dogs won't learn tricks
			if (Energy < 70 || Boredom > 60)
			{
				Console.WriteLine("{0} is too unfocused to learn right now.", Name);
			}
			else if (dogTricks["Play dead"] == true)
			{
				Console.WriteLine("Your dog has learned every trick!");
			}
			else
			{
				//loop through dogTricks until it hits a trick that is unlearned, then decreases trickProgress until the 
				//trick is learned (smart dogs learn faster). Then it sets that trick to true (learned) and increases trick count
				foreach (KeyValuePair<string,bool> kvp in dogTricks)
				{
					if (kvp.Value == false)
					{
						TrickProgress -= Intelligence;
						if (TrickProgress <= 0)
						{
							dogTricks[kvp.Key] = true;
							Console.WriteLine("Your dog has learned the trick \"{0}\"!", kvp.Key);
							TricksLearned++;
							TrickProgress = 10;
						}
						Energy -= (short)(EnergyDecayRate * 5);
						break;
					}
				}
			}
		}

		//increment stats when run
		public void Tick(Object source, ElapsedEventArgs e)
		{
			Random rand = new Random();
			Hunger += HungerDecayRate;
			Thirst += (short)rand.Next(5);
			Boredom += Intelligence;
			Energy -= EnergyDecayRate;
			CoatShag += CoatShagDecayRate;
		}

		//every x seconds the Tick method will trigger
		public void NeedIncrement(int seconds)
		{
			needTimer = new Timer(seconds * 1000);
			needTimer.Elapsed += Tick;
			needTimer.AutoReset = true;
			needTimer.Enabled = true;
		}

	}
}
