using System.Timers;
using static Labb2Tradar.Program;

namespace Labb2Tradar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Cars car1 = new Cars("Lightning McQueen");
            Cars car2 = new Cars("Chick 'Thunder' Hicks");

            Thread car1Thread = new Thread(car1.Drive);
            Thread car2Thread = new Thread(car2.Drive);

            Thread eventManagerThread = new Thread(() => EventManager(car1, car2));
            eventManagerThread.Start();


            Console.WriteLine($"The race has started between {car1.Name} and {car2.Name}!");
            Console.WriteLine($"Type 'S' during the race to check the status of the cars and the winner at the end!");
            car1Thread.Start();
            car2Thread.Start();

            while (!car1.RaceFinished && !car2.RaceFinished)
            {
                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.S)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Status update:");
                    Console.WriteLine($"{car1.Name}, Distance: {car1.TrackLength}m, Speed: {car1.Speed}m/s");
                    Console.WriteLine($"{car2.Name}, Distance: {car2.TrackLength}m, Speed: {car2.Speed}m/s");
                }

            }

            Console.WriteLine($"{(car1.TrackLength >= car2.TrackLength ? car1.Name : car2.Name)} won the race!");
        }
        private static void EventManager(Cars car1, Cars car2)
        {
            while (true) 
            {
                Thread.Sleep(30000); 

                if (car1.RaceFinished && car2.RaceFinished)
                {
                    break;
                }

                if (!car1.RaceFinished)
                {
                    car1.OnRandomEvent();
                }

                if (!car2.RaceFinished)
                {
                    car2.OnRandomEvent();
                }
            }
        }

        public class Cars
        {

            public static event Action<Cars> OnRandomEventStatic;
            public string Name { get; set; }

            public double Speed { get; set; } = 33; // 33m/s ~= 120km/h

            public double TrackLength { get; set; } = 0;

            public bool RaceFinished { get; set; } = false;
            public void OnRandomEvent()
            {
                RandomEventHandlerCar1();
                RandomEventHandlerCar2();
            }
            

            private static Random rand = new Random();

            public Cars(string name)
            {
                Name = name;
            }


            public void Drive()
            {
                while (TrackLength < 10000) // 10000m = 10km
                {
                    TrackLength += Speed;

                    Thread.Sleep(500);
                }

                RaceFinished = true;
                Console.WriteLine($"{Name} finished the race!");
            }

            public void RandomEventHandlerCar1()
            {
                double probability = rand.NextDouble();

                if (probability <= 0.02)
                {
                    Console.WriteLine($"{Name} is out of gas! Stopping for 30 seconds.");
                    Thread.Sleep(30000);
                }
                else if (probability <= 0.06)
                {
                    Console.WriteLine($"{Name} got a puncture! Stopping for 20 seconds.");
                    Thread.Sleep(20000);
                }
                else if (probability <= 0.16)
                {
                    Console.WriteLine($"{Name} got a dirty windshield! Stopping for 10 seconds.");
                    Thread.Sleep(10000);
                }
                else if (probability <= 0.36)
                {
                    Console.WriteLine($"{Name} got an engine problem! Reducing speed by 1km/h.");
                    Speed -= 0.3; // 0.3m/s ~= 1km/h
                }
            }

            public void RandomEventHandlerCar2()
            {
                double probability = rand.NextDouble();

                if (probability <= 0.02)
                {
                    Console.WriteLine($"{Name} is out of gas! Stopping for 30 seconds.");
                    Thread.Sleep(30000);
                }
                else if (probability <= 0.06)
                {
                    Console.WriteLine($"{Name} got a puncture! Stopping for 20 seconds.");
                    Thread.Sleep(20000);
                }
                else if (probability <= 0.16)
                {
                    Console.WriteLine($"{Name} got a dirty windshield! Stopping for 10 seconds.");
                    Thread.Sleep(10000);
                }
                else if (probability <= 0.36)
                {
                    Console.WriteLine($"{Name} got an engine problem! Reducing speed by 1km/h.");
                    Speed -= 0.3; // 0.3m/s ~= 1km/h
                }
            }
        }
    }
}
