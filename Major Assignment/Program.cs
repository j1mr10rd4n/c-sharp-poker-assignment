using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Major_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            String pokerHand;

            // print menu and ask for input file
            Console.WriteLine("Please enter the number of the poker hand you wish to see");
            pokerHand = Console.ReadLine();

            // read file
            const String DATA_DIRECTORY = "inputHands";
            String directoryPath = Path.Combine(System.Environment.CurrentDirectory, "..", "..", DATA_DIRECTORY);
            String[] filesInDirectory = Directory.GetFiles(directoryPath);
            foreach (String path in filesInDirectory)
            {
                FileInfo fileInfo = new FileInfo(path);
                String fileName = fileInfo.Name;
                String expectedRank = fileName.Split('.')[0];

                StreamReader streamReader = new StreamReader(path);
                String fileContents = streamReader.ReadToEnd();
                streamReader.Close();

                Console.WriteLine("Testing: " + expectedRank + " " + fileContents);

                // generate hand
                PlayingCard card1 = new PlayingCard(fileContents.Split()[0], fileContents.Split()[1]);
                PlayingCard card2 = new PlayingCard(fileContents.Split()[2], fileContents.Split()[3]);
                PlayingCard card3 = new PlayingCard(fileContents.Split()[4], fileContents.Split()[5]);
                PlayingCard card4 = new PlayingCard(fileContents.Split()[6], fileContents.Split()[7]);
                PlayingCard card5 = new PlayingCard(fileContents.Split()[8], fileContents.Split()[9]);

                String hand = card1.valueString() + ", " + card2.valueString() + ", " + card3.valueString() + ", " +
                    card4.valueString() + ", " + card5.valueString();

                // generate rank
                String rank = "";

                // test for royal flush
                if (handContainsRoyalFlush(card1, card2, card3, card4, card5))
                {
                    rank = "RoyalFlush";
                }

                // output result
//                Console.WriteLine("Hand: " + hand);
                Console.WriteLine("Rank: " + rank);

                // TDD!
                if (rank == expectedRank)
                {
                    Console.WriteLine("Yay!");
                }
                else
                {
                    Console.WriteLine("Boo :(");
                }
            }
           
            // wait before exit
            Console.ReadKey();
        }


        static Boolean handContainsRoyalFlush(PlayingCard card1, PlayingCard card2, PlayingCard card3, PlayingCard card4, PlayingCard card5)
        {
            // - are cards all same suit?
            if (card1.suitCode == card2.suitCode &&
                card1.suitCode == card3.suitCode &&
                card1.suitCode == card4.suitCode &&
                card1.suitCode == card5.suitCode)
            {
                // card values must be 14,13,12,11,10 or 13,12,11,10,1
                int[] cardValues = { Convert.ToInt32(card1.cardNumber),
                                         Convert.ToInt32(card2.cardNumber),
                                         Convert.ToInt32(card3.cardNumber),
                                         Convert.ToInt32(card4.cardNumber),
                                         Convert.ToInt32(card5.cardNumber) };
                Array.Sort(cardValues);
                int[] royalFlushValuesLowAce = { 1, 10, 11, 12, 13 };
                int[] royalFlushValuesHighAce = { 10, 11, 12, 13, 14 };
                if (Enumerable.SequenceEqual(cardValues, royalFlushValuesLowAce) || Enumerable.SequenceEqual(cardValues, royalFlushValuesHighAce))
                {
                    return true;
                }
            }
            return false;
        }

        public struct PlayingCard
        {
            public String suitCode, cardNumber;

            // Constructor
            public PlayingCard(String suitCode, String cardNumber)
            {
                this.suitCode = suitCode;
                this.cardNumber = cardNumber;
            }

            private String translateSuitCode()
            {
                String suit = "";
                if (suitCode == "C" || suitCode == "c")
                {
                    suit = "clubs";
                }
                else if (suitCode == "D" || suitCode == "d")
                {
                    suit = "diamonds";
                }
                else if (suitCode == "H" || suitCode == "h")
                {
                    suit = "hearts";
                }
                else if (suitCode == "S" || suitCode == "s")
                {
                    suit = "spades";
                }
                return suit;
            }

            private String translateCardNumber()
            {
                String card = "";
                if (cardNumber == "2")
                {
                    card = "two";
                }
                else if (cardNumber == "3")
                {
                    card = "three";
                }
                else if (cardNumber == "4")
                {
                    card = "four";
                }
                else if (cardNumber == "5")
                {
                    card = "five";
                }
                else if (cardNumber == "6")
                {
                    card = "six";
                }
                else if (cardNumber == "7")
                {
                    card = "seven";
                }
                else if (cardNumber == "8")
                {
                    card = "eight";
                }
                else if (cardNumber == "9")
                {
                    card = "nine";
                }
                if (cardNumber == "10")
                {
                    card = "ten";
                }
                else if (cardNumber == "11")
                {
                    card = "jack";
                }
                else if (cardNumber == "12")
                {
                    card = "queen";
                }
                else if (cardNumber == "13")
                {
                    card = "king";
                }
                else if (cardNumber == "14")
                {
                    card = "ace";
                }
                return card;
            }

            public String valueString()
            {
                return translateCardNumber() + " of " + translateSuitCode();
            }
        }
    }
}
