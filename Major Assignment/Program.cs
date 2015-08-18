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
            String suitCode1 = fileContents.Split()[0];
            String suit1 = translateSuitCode(suitCode1);

            String suitCode2 = fileContents.Split()[2];
            String suit2 = translateSuitCode(suitCode2);

            String suitCode3 = fileContents.Split()[4];
            String suit3 = translateSuitCode(suitCode3);

            String suitCode4 = fileContents.Split()[6];
            String suit4 = translateSuitCode(suitCode4);

            String suitCode5 = fileContents.Split()[8];
            String suit5 = translateSuitCode(suitCode5);

            String cardNumber1 = fileContents.Split()[1];
            String number1 = translateCardNumber(cardNumber1);

            String cardNumber2 = fileContents.Split()[3];
            String number2 = translateCardNumber(cardNumber2);

            String cardNumber3 = fileContents.Split()[5];
            String number3 = translateCardNumber(cardNumber3);

            String cardNumber4 = fileContents.Split()[7];
            String number4 = translateCardNumber(cardNumber4);

            String cardNumber5 = fileContents.Split()[9];
            String number5 = translateCardNumber(cardNumber5);

            String hand = number1 + " of " + suit1 + ", " + number2 + 
                " of " + suit2 + ", " + number3 + " of " + suit3 + ", " + 
                number4 + " of " + suit4 + ", " + number5 + " of " + suit5;

            // generate rank
            String rank = "pair";

            // output result
            Console.WriteLine("Hand: " + hand);
            Console.WriteLine("Rank: " + rank);

            // TDD!
            String expectedHand = "queen of clubs, queen of diamonds, three of hearts, five of diamonds, six of spades";
            if(hand == expectedHand)
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
        static String translateCardNumber(String cardNumber)
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

        static String translateSuitCode(String suitCode)
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
    }
}
