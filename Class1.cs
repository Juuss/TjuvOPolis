using System;
using System.Collections.Generic;
using System.Text;

namespace TjuvOchPolis3
{
        class Person
        {
            public char Title { get; set; }
            public int PositionX { get; set; }
            public int PositionY { get; set; }
            public int DirectionX { get; set; }
            public int DirectionY { get; set; }
            public List<Items> Inventory { get; set; }
            public int NumberOfStolenGoods { get; set; }
            public int NumberOfMissingGoods { get; set; }
            public int NumberOfItemsInTheBeginning { get; set; }
            public int NumberOfSeizedItems { get; set; }
            public int Number { get; set; }
            public int GotInPrison { get; set; }
        }
        class Citizens : Person
        {
            public Citizens(char title, int positionX, int positionY, int directionX, int directionY, List<Items> inventory, int numberOfMissingGoods, int numberOfItemsInTheBeginning, int number)
            {
                Title = title;
                PositionX = positionX;
                PositionY = positionY;
                DirectionX = directionX;
                DirectionY = directionY;
                Inventory = inventory;
                NumberOfMissingGoods = numberOfMissingGoods;
                NumberOfItemsInTheBeginning = numberOfItemsInTheBeginning;
                Number = number;
            }
        }
        class Thief : Person

        {
            public Thief(char title, int positionX, int positionY, int directionX, int directionY, List<Items> inventory, int numberOfStolenGoods, int number)
            {
                Title = title;
                PositionX = positionX;
                PositionY = positionY;
                DirectionX = directionX;
                DirectionY = directionY;
                Inventory = inventory;
                NumberOfStolenGoods = numberOfStolenGoods;
                Number = number;
            }
        }
        class Police : Person
        {
            public Police(char title, int positionX, int positionY, int directionX, int directionY, List<Items> inventory, int numberOfSeizedItems, int number)
            {
                Title = title;
                PositionX = positionX;
                PositionY = positionY;
                DirectionX = directionX;
                DirectionY = directionY;
                Inventory = inventory;
                NumberOfSeizedItems = numberOfSeizedItems;
                Number = number;
            }
        }

    class Prisoner : Person
    {
        public Prisoner(char title, int positionX, int positionY, int directionX, int directionY, List<Items> inventory, int numberOfStolenGoods, int number, int gotInPrison)
        {
            Title = title;
            PositionX = positionX;
            PositionY = positionY;
            DirectionX = directionX;
            DirectionY = directionY;
            Inventory = inventory;
            NumberOfStolenGoods = numberOfStolenGoods;
            Number = number;
            GotInPrison = gotInPrison;

        }
    }
    class Items
        {
            public string Item1 { get; set; }
        public int ItemNumber { get; set; }

            public Items(string item1, int itemNumber)
            {
                Item1 = item1;
                ItemNumber = itemNumber;
            }
        }
}
