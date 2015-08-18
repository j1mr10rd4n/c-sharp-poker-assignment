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
            String fileName = "Pair.txt";
            String path = Path.Combine(System.Environment.CurrentDirectory, "..", "..", DATA_DIRECTORY, fileName);
            StreamReader streamReader = new StreamReader(path);
            String fileContents = streamReader.ReadToEnd();
            streamReader.Close();

            // generate hand
            PlayingCard card1;
            card1.suitCode = fileContents.Split()[0];
            card1.cardNumber = fileContents.Split()[1];
            String card1ValueString = card1.translateSuitCode() + " of " + card1.translateCardNumber();

            PlayingCard card2;
            card2.suitCode = fileContents.Split()[2];
            card2.cardNumber = fileContents.Split()[3];
            String card2ValueString = card2.translateSuitCode() + " of " + card2.translateCardNumber();

            PlayingCard card3;
            card3.suitCode = fileContents.Split()[4];
            card3.cardNumber = fileContents.Split()[5];
            String card3ValueString = card3.translateSuitCode() + " of " + card3.translateCardNumber();

            PlayingCard card4;
            card4.suitCode = fileContents.Split()[6];
            card4.cardNumber = fileContents.Split()[7];
            String card4ValueString = card4.translateSuitCode() + " of " + card4.translateCardNumber();

            PlayingCard card5;
            card5.suitCode = fileContents.Split()[8];
            card5.cardNumber = fileContents.Split()[9];
            String card5ValueString = card5.translateSuitCode() + " of " + card5.translateCardNumber();

            String hand = card1ValueString + ", " + card2ValueString + ", " + card3ValueString + ", " +
                card4ValueString + ", " + card5ValueString;

            // generate rank
            String rank = "pair";

            // output result
            Console.WriteLine("Hand: " + hand);
            Console.WriteLine("Rank: " + rank);

            // TDD!
            String expectedHand = "queen of clubs, queen of diamonds, three of hearts, five of diamonds, six of spades";
            if (hand == expectedHand)
            {
                Console.WriteLine("Yay!");
            }
            else
            {
                Console.WriteLine("Boo :(");
            }
            
            // wait before exit
            Console.ReadKey();
        }




        public struct PlayingCard
        {
            public String suitCode, cardNumber;

            public String translateSuitCode()
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

            public String translateCardNumber()
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
        }
    }
}
