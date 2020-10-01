using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TjuvOchPolis3
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfRobbedCitizens = 0;
            int numberOfPrisoners = 0;
            int sec = 0;
            int quasec = 0;

            Random rnd = new Random();
            List<Person> city = CreatePeople(rnd, Belongings());
            List<Person> prison = new List<Person>();

                while (true)
                {
                    //ritar upp planen
                    Console.Clear();
                    for (int i = 0; i < 24; i++)
                    {
                        for (int j = 0; j < 99; j++)
                        {
                            Console.Write(" ");
                            foreach (Person person in city)
                            {
                                if (i == person.PositionX && j == person.PositionY)
                                {
                                    Console.Write($"{person.Title}");
                                }
                            }
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine("Antal rånade medborgare: " + numberOfRobbedCitizens);
                    Console.WriteLine("Antal tjuvar i fängelse: " + numberOfPrisoners);
                    foreach (var prisoner in prison)
                    {
                    Console.WriteLine($"Tjuv #{prisoner.Number} är i fängelset och har nu varit här i " + (sec - prisoner.GotInPrison) + " sekunder");
                    }

                    // Ny placering (och ifall de kommer utanför skärmen)
                    foreach (Person person in city.ToList())
                    {
                        if (person.DirectionX > 0)
                        {
                            person.PositionX = (person.PositionX + person.DirectionX);
                        }
                        if (person.DirectionX < 0)
                        {
                            person.PositionX = (person.PositionX + person.DirectionX);
                        }
                        if (person.DirectionY > 0)
                        {
                            person.PositionY = (person.PositionY + person.DirectionY);
                        }
                        if (person.DirectionY < 0)
                        {
                            person.PositionY = (person.PositionY + person.DirectionY);
                        }
                        // om dom hamnar utanför skärmen.
                        if (person.PositionX < 0)
                        {
                            person.PositionX = 24;
                        }
                        if (person.PositionX > 24)
                        {
                            person.PositionX = 0;
                        }
                        if (person.PositionY < 0)
                        {
                            person.PositionY = 99;
                        }
                        if (person.PositionY > 99)
                        {
                            person.PositionY = 0;
                        }

                        //Om de stöter på varandra
                        //Om citizen stöter på tjuv
                        if (person.Title == 'C')
                        {
                            foreach (Person p in city.ToList())
                            {
                                if (p.Title == 'T')
                                {
                                    if (person.PositionX == p.PositionX && person.PositionY == p.PositionY)
                                    {
                                        Console.Write($"Tjuv #{p.Number} har stulit från medborgare #{person.Number}. ");
                                        int t = taken(rnd, person.Inventory.Count());

                                        foreach (var i in person.Inventory.ToList())
                                        {
                                            if (i.ItemNumber == t)
                                            {
                                                Console.Write($"Hen tog: {i.Item1}");
                                                p.Inventory[p.NumberOfStolenGoods] = person.Inventory[t];
                                                person.Inventory.RemoveAt(t);
                                            }
                                        }
                                        if (person.Inventory.Count == 0)
                                    {
                                        Console.Write(" Hen har inget kvar");
                                    }
                                        Console.WriteLine();
                                        p.NumberOfStolenGoods++;
                                        person.NumberOfMissingGoods++;
                                        numberOfRobbedCitizens++;
                                        sec++;
                                        Thread.Sleep(1000);
                                    }

                                }
                            }
                        }
                        //Om tjuv stöter på polis
                        if (person.Title == 'T')
                        {
                            foreach (Person p in city.ToList())
                            {
                                if (p.Title == 'P')
                                {
                                    if (person.PositionX == p.PositionX && person.PositionY == p.PositionY)
                                    {
                                        Console.Write($"Polis #{p.Number} har tagit tjuv #{person.Number}. Hen hade på sig: ");

                                        foreach (var taken in person.Inventory)
                                        {
                                            Console.Write($"{taken.Item1} ");
                                        }
                                        int i;
                                        for (i = 0; i < person.NumberOfStolenGoods; i++)
                                        {
                                            p.Inventory[i] = person.Inventory[i];
                                            person.Inventory.RemoveAt(i);
                                            p.NumberOfSeizedItems++;
                                        }
                                        prison.Add(person);
                                        person.GotInPrison = sec;
                                        city.Remove(person);
                                        numberOfPrisoners++;
                                        Console.WriteLine();
                                        person.NumberOfStolenGoods = 0;
                                        Thread.Sleep(1000);
                                        sec++;
                                    }
                                }
                            }
                        }
                    }

                    //Fånge lämnar fängelse
                    foreach (var prisoner in prison.ToList())
                    {
                        if (sec - prisoner.GotInPrison >= 20)
                        {
                            Console.WriteLine($"Fånge #{prisoner.Number} har kommit ut ur fängelset.");
                            city.Add(prisoner);
                            prison.Remove(prisoner);
                            numberOfPrisoners--;
                            Thread.Sleep(1000);
                            sec++;
                        }
                    }

                //Infon under simuleringen, för att kunna följa hur alla personer ändras.
                
                /*foreach (Person person in city)
                {
                    Console.Write($"{person.Title}{person.Number}, Property:");
                    if (person.Title == 'C')
                    {
                        foreach (var i in person.Inventory)
                        {
                            Console.Write(i.Item1 + " ");
                        }
                    }
                    if (person.Title == 'T')
                    {
                        foreach (var i in person.Inventory)
                        {
                            Console.Write(i.Item1 + " ");
                        }
                        Console.Write($"StolenGoodsNr {person.NumberOfStolenGoods}");
                    }
                    if (person.Title == 'P')
                    {
                        foreach (var i in person.Inventory)
                        {
                            Console.Write(i.Item1 + " ");
                        }
                        Console.Write($"Taken back: {person.NumberOfSeizedItems}");
                    }
                    Console.WriteLine();
                }*/

                Thread.Sleep(250);
                quasec++;
                if (quasec == 4)
                {
                    sec++;
                    quasec = 0;
                }
            }
        }
        //METODER
        //Skapa människorna
        public static List<Person> CreatePeople(Random rnd, List<Items> bag)
        {
            List<Person> city = new List<Person>();
            int NumberCitizen = 1;
            int NumberPolice = 1;
            int NumberThief = 1;
            for (int i = 0; i < 20; i++)
            {
                city.Add(new Citizens('C', random1(rnd), random2(rnd), random3(rnd), random3(rnd), Belongings(), 0, 4, NumberCitizen));
                NumberCitizen++;

            }
            for (int i = 0; i < 15; i++)
            {
                city.Add(new Police('P', random1(rnd), random2(rnd), random3(rnd), random3(rnd), Seized(), 0, NumberPolice));
                NumberPolice++;
            }
            for (int i = 0; i < 15; i++)
            {
                city.Add(new Thief('T', random1(rnd), random2(rnd), random3(rnd), random3(rnd), StolenGoods(), 0, NumberThief));
                NumberThief++;
            }
            return city;
        }
        //första X koordinater 
        public static int random1(Random rnd)
        {
            int t = rnd.Next(24);
            return t;
        }
        //första Y koordinater
        public static int random2(Random rnd)
        {
            int t = rnd.Next(99);
            return t;
        }
        // Directions
        public static int random3(Random rnd)
        {
            int t = rnd.Next(-1, 2);
            return t;
        }
        //När tjuv tar random sak ur inventory?
        public static int taken(Random rnd, int Number)
        {
            int t = rnd.Next(Number);
            return t;
        }
        //Citizens inventory
        public static List<Items> Belongings()
        {
            List<Items> belongings = new List<Items>();
            belongings.Add(new Items("Nycklar", 0));
            belongings.Add(new Items("Mobil", 1));
            belongings.Add(new Items("Pengar", 2));
            belongings.Add(new Items("Klocka", 3));
            return belongings;
        }
        //Polisens inventory
        public static List<Items> Seized()
        {
            List<Items> seized = new List<Items>();
            seized.Add(new Items(" ", 0));
            seized.Add(new Items(" ", 1));
            seized.Add(new Items(" ", 2));
            seized.Add(new Items(" ", 3));
            seized.Add(new Items(" ", 4));
            seized.Add(new Items(" ", 5));
            seized.Add(new Items(" ", 6));
            seized.Add(new Items(" ", 7));
            return seized;
        }
        //Tjuvens inventory
        public static List<Items> StolenGoods()
        {
            List<Items> stolenGoods = new List<Items>();
            stolenGoods.Add(new Items(" ", 0));
            stolenGoods.Add(new Items(" ", 1));
            stolenGoods.Add(new Items(" ", 2));
            stolenGoods.Add(new Items(" ", 3));
            stolenGoods.Add(new Items(" ", 4));
            stolenGoods.Add(new Items(" ", 5));
            stolenGoods.Add(new Items(" ", 6));
            stolenGoods.Add(new Items(" ", 7));
            return stolenGoods;
        }
    }
    

}