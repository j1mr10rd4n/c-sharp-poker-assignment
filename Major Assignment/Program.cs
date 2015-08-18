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
            // print menu and ask for input file
            Console.WriteLine("Please enter the number of the poker hand you wish to see");

            // read files in directory
            const String DATA_DIRECTORY = "inputHands";
            String directoryPath = Path.Combine(System.Environment.CurrentDirectory, "..", "..", DATA_DIRECTORY);
            String[] filesInDirectory = Directory.GetFiles(directoryPath);

            for (int fileIndex = 0; fileIndex < filesInDirectory.Length; fileIndex++)
            {
                FileInfo fileInfo = new FileInfo(filesInDirectory[fileIndex]);
                String fileName = fileInfo.Name;
                String expectedRank = fileName.Split('.')[0];
                Console.WriteLine((fileIndex + 1) + ": " + expectedRank);
            }

            String selectedFileNumberString = Console.ReadLine();
            int selectedFileNumber = Convert.ToInt32(selectedFileNumberString);

            // read file
            String selectedFilePath = filesInDirectory[selectedFileNumber - 1];
            StreamReader streamReader = new StreamReader(selectedFilePath);
            String fileContents = streamReader.ReadToEnd();
            streamReader.Close();

            Hand hand = new Hand(fileContents);
            // output hand
            Console.WriteLine("Hand: " + hand.handString());
            // output rank
            Console.WriteLine("Rank: " + hand.rankString());

            // wait before exit
            Console.ReadKey();
        }


        public struct Hand
        {
            public PlayingCard[] playingCards;

            // Constructor
            public Hand(String fileContents)
            {
                PlayingCard[] hand = { new PlayingCard(fileContents.Split()[0], fileContents.Split()[1]),
                                       new PlayingCard(fileContents.Split()[2], fileContents.Split()[3]),
                                       new PlayingCard(fileContents.Split()[4], fileContents.Split()[5]),
                                       new PlayingCard(fileContents.Split()[6], fileContents.Split()[7]),
                                       new PlayingCard(fileContents.Split()[8], fileContents.Split()[9]) };
                this.playingCards = hand;
            }

            public String handString()
            {
                return playingCards[0].valueString() + ", " + playingCards[1].valueString() + ", " + playingCards[2].valueString() + ", " +
                    playingCards[3].valueString() + ", " + playingCards[4].valueString();
            }

            public String rankString()
            {
                String rank = "";

                // test for royal flush
                if (containsRoyalFlush())
                {
                    rank = "RoyalFlush";
                } else if (containsStraightFlush())
                {
                    rank = "StraightFlush";
                }
                else if (containsFlush())
                {
                    rank = "Flush";
                }
                else if (containsStraight())
                {
                    rank = "Straight";
                }
                else if (containsFourKind())
                {
                    rank = "FourKind";
                }
                else if (containsThreeKind())
                {
                    rank = "ThreeKind";
                }
                else if (containsFullHouse())
                {
                    rank = "FullHouse";
                }
                else if (containsTwoPair())
                {
                    rank = "TwoPair";
                }
                else if (containsPair())
                {
                    rank = "Pair";
                }
                return rank;
            }

            private Boolean containsRoyalFlush()
            {
                // - are cards all same suit?
                if (cardsAreAllSameSuit())
                {
                    // card values must be 14,13,12,11,10 or 13,12,11,10,1
                    int[] cardValues = sortedCardValues();
                    int[] royalFlushValuesLowAce = { 1, 10, 11, 12, 13 };
                    int[] royalFlushValuesHighAce = { 10, 11, 12, 13, 14 };
                    if (Enumerable.SequenceEqual(cardValues, royalFlushValuesLowAce) || Enumerable.SequenceEqual(cardValues, royalFlushValuesHighAce))
                    {
                        return true;
                    }
                }
                return false;
            }

            private Boolean containsStraightFlush()
            {
                // - are cards all same suit?
                if (cardsAreAllSameSuit())
                {
                    // card values must be consecutive
                    return cardValuesAreConsecutive();
                }
                return false;
            }

            private Boolean cardsAreAllSameSuit()
            {
                return playingCards[0].suitCode == playingCards[1].suitCode &&
                    playingCards[0].suitCode == playingCards[2].suitCode &&
                    playingCards[0].suitCode == playingCards[3].suitCode &&
                    playingCards[0].suitCode == playingCards[4].suitCode;
            }

            private Boolean cardValuesAreConsecutive()
            {
                int[] cardValues = sortedCardValues();
                int[] straightFlushValues = { cardValues[0], 
                                                    cardValues[0] + 1, 
                                                    cardValues[0] + 2, 
                                                    cardValues[0] + 3, 
                                                    cardValues[0] + 4 };
                if (Enumerable.SequenceEqual(cardValues, straightFlushValues))
                {
                    return true;
                }
                return false;
            }

            private Boolean containsFlush()
            {
                return cardsAreAllSameSuit();
            }

            private Boolean containsStraight()
            {
                // card values must be consecutive
                return cardValuesAreConsecutive();
            }

            private Boolean containsFourKind()
            {
                // should be 1,4
                int[] fourKind = { 1, 4 };
                return hasValueCount(fourKind);
            }

            private Boolean containsThreeKind()
            {
                int[] threeKind = { 1, 1, 3 };
                return hasValueCount(threeKind);
            }

            private Boolean containsFullHouse()
            {
                int[] fullHouse = { 2, 3 };
                return hasValueCount(fullHouse);
            }

            private Boolean containsTwoPair()
            {
                int[] twoPair = { 1, 2, 2 };
                return hasValueCount(twoPair);
            }

            private Boolean containsPair()
            {
                int[] pair = { 1, 1, 1, 2 };
                return hasValueCount(pair);
            }

            private Boolean hasValueCount(int[] valueCount)
            {
                int[] cardValues = sortedCardValues();
                int[] distinctValueCount = cardValues.GroupBy(item => item).Select(item => item.Count()).ToArray();
                Array.Sort(distinctValueCount);
                return Enumerable.SequenceEqual(distinctValueCount, valueCount);
            }

            private int[] sortedCardValues()
            {
                int[] cardValues = { Convert.ToInt32(playingCards[0].cardNumber),
                                         Convert.ToInt32(playingCards[1].cardNumber),
                                         Convert.ToInt32(playingCards[2].cardNumber),
                                         Convert.ToInt32(playingCards[3].cardNumber),
                                         Convert.ToInt32(playingCards[4].cardNumber) };
                Array.Sort(cardValues);
                return cardValues;
            }
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
