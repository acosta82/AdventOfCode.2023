using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Advent._2023.Console
{
    internal class Day3 : IAdventCalendarDay
    {
        public void Run()
        {
            var input = GetRawInput(false);

            var list = new Day3LinkedList(input);

            var sum = list.GetPossibleNumbers().Sum();

            System.Console.WriteLine();
            System.Console.WriteLine("----------");
            System.Console.WriteLine(sum);

            System.Console.WriteLine();
            System.Console.WriteLine("----------");

            list.CheckGearRatio();
        }

        private IEnumerable<string> GetRawInput(bool isTest = false)
        {
            if (isTest)
            {
                yield return "467..114..";
                yield return "...*......";
                yield return "..35..633.";
                yield return "......#...";
                yield return "617*......";
                yield return ".....+.58.";
                yield return "..592.....";
                yield return "......755.";
                yield return "...$.*....";
                yield return ".664.598..";
            }
            else
            {
                var fileName = "Day3PuzzleInput.txt";
                StreamReader sr = new StreamReader(fileName);
                //Read the first line of text
                var line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    yield return line;
                    //Read the next line
                    line = sr.ReadLine();
                }
            }
        }
    }

    internal class Day3LinkedList : LinkedList<string>
    {
        public Day3LinkedList(IEnumerable<string> collection) : base(collection)
        {
        }

        public void CheckGearRatio() // part 2 of the day3
        {
            var ratio = 0;
            var line = 0;
            foreach (var item in this)
            {
                line++;
                if (!item.Contains("*")) continue;

                for(int i = 0; i < item.Length; i++)
                {
                    if (item[i] != '*') continue;

                    System.Console.WriteLine($"[Line {line}] Found a gear at position {i}");

                    var currentRatio = GetRatioForThisGear(line - 1, i);

                    if (currentRatio == 0) continue;

                    System.Console.WriteLine($"This gear is a valid one with ratio {currentRatio}");

                    ratio += currentRatio;
                }
            }

            System.Console.WriteLine("RATIO: " + ratio);
        }

        private int GetRatioForThisGear(int elementindex, int positionInString)
        {
            List<int> indexToCheck = new List<int>();
            if (positionInString != 0)
                indexToCheck.Add(positionInString - 1);

            indexToCheck.Add(positionInString);

            if (positionInString < this.ElementAt(0).Length - 2)
                indexToCheck.Add(positionInString + 1);

            var numberCount = 0;
            var currentlyCounting = false;

            var currentRatio = 0;

            var visitPreviousLine = false;
            var visitNextLine = false;
            var visitCurrentLine = false;

            //get previous element if applicable
            /*
                -------------------
                |  X  |  X  |  X  |
                -------------------
                |     |  *  |     |
                -------------------
                |     |     |     |
                -------------------
             */
            if (elementindex != 0)
            {
                var previousLine = this.ElementAt(elementindex - 1);
                foreach (var lineIndex in indexToCheck)
                {
                    if (char.IsDigit(previousLine.ElementAt(lineIndex)))
                    {
                        if (!currentlyCounting)
                        {
                            currentlyCounting = true;
                            numberCount++;
                            visitPreviousLine = true;
                        }
                    }
                    else
                    {
                        currentlyCounting = false;
                    }
                }
            }

            currentlyCounting = false;

            //get Next element if applicable
            /*
                -------------------
                |     |     |     |
                -------------------
                |     |  *  |     |
                -------------------
                |  X  |  X  |  X  |
                -------------------
             */
            if (elementindex < this.Count - 1)
            {
                var nextLine = this.ElementAt(elementindex + 1);

                foreach (var lineIndex in indexToCheck)
                {
                    if (char.IsDigit(nextLine.ElementAt(lineIndex)))
                    {
                        if (!currentlyCounting)
                        {
                            currentlyCounting = true;
                            numberCount++;
                            visitNextLine = true;
                        }
                    }
                    else
                    {
                        currentlyCounting = false;
                    }
                }
            }

            if (numberCount > 2) return 0;

            currentlyCounting = false;

            //If still valid, let's check on it's side
            /*
                -------------------
                |     |     |     |
                -------------------
                |  X  |  *  |  X  |
                -------------------
                |     |     |     |
                -------------------
             */

            var currentLine = this.ElementAt(elementindex);


            foreach (var lineIndex in indexToCheck.Where(i => i != positionInString))
            {
                if (char.IsDigit(currentLine.ElementAt(lineIndex)))
                {
                    numberCount++; //We don't need to start / stop counting as this will always be different numbers
                    visitCurrentLine = true;
                }
            }

            if (numberCount != 2) return 0;

            List<int> numbers = new List<int>();

            if (visitPreviousLine)
            {
                var visitedLine = this.ElementAt(elementindex - 1);

                var matches = Regex.Matches(visitedLine, @"\d+");
                var candidates = matches.Select(m => m.Value);

                var ignore = 0;

                foreach (var candidate in candidates)
                {
                    var init = WhereIsSubstring(visitedLine, candidate, ignore);
                    var end = init + candidate.Length - 1;
                    ignore = end;

                    for (int i = init; i <= end; i++)
                    {
                        if(indexToCheck.Contains(i))
                        {
                            numbers.Add(GetInt(candidate));
                            break;
                        }
                    }
                }
            }

            if(visitNextLine)
            {
                var visitedLine = this.ElementAt(elementindex + 1);

                var matches = Regex.Matches(visitedLine, @"\d+");
                var candidates = matches.Select(m => m.Value);

                var ignore = 0;

                foreach (var candidate in candidates)
                {
                    var init = WhereIsSubstring(visitedLine, candidate, ignore);
                    var end = init + candidate.Length - 1;
                    ignore = end;

                    for (int i = init; i <= end; i++)
                    {
                        if (indexToCheck.Contains(i))
                        {
                            numbers.Add(GetInt(candidate));
                            break;
                        }
                    }
                }
            }

            if(visitCurrentLine)
            {
                var visitedLine = this.ElementAt(elementindex);

                var matches = Regex.Matches(visitedLine, @"\d+");
                var candidates = matches.Select(m => m.Value);

                var ignore = 0;

                foreach (var candidate in candidates)
                {
                    var init = WhereIsSubstring(visitedLine, candidate, ignore);
                    var end = init + candidate.Length-1;
                    ignore = end;

                    for (int i = init; i <= end; i++)
                    {
                        if (indexToCheck.Where(i => i != positionInString).Contains(i))
                        {
                            numbers.Add(GetInt(candidate));
                            break;
                        }
                    }
                }
            }

            currentRatio = numbers.First() * numbers.Last(); // there should only be 2 anyway.

            return currentRatio;
        }

        public List<int> GetPossibleNumbers()
        {
            var list = new List<int>();

            for (int i = 0; i < this.Count; i++)
            {
                var thisLine = this.ElementAt(i) as string;

                var ignore = 0;

                System.Console.WriteLine();
                System.Console.Write("Line " + (i + 1) + " - ");

                var matches = Regex.Matches(thisLine, @"\d+");
                var candidates = matches.Select(m => m.Value);

                foreach (var candidate in candidates)
                {
                    //which indexes is the candidate in the string?
                    var whereIsCandidate = WhereIsSubstring(thisLine, candidate, ignore);

                    ignore = whereIsCandidate + candidate.Length;

                    if (candidate.Any(x => char.IsDigit(x)) && IsThereASymbolInTheEdges(candidate, thisLine, whereIsCandidate))
                    {
                        System.Console.Write(candidate + "   ");
                        list.Add(GetInt(candidate));
                        continue;
                    }

                    if (i != 0) // check previous line
                    {
                        var previousLine = this.ElementAt(i-1) as string;
                        var skip = whereIsCandidate == 0 ? 0 : whereIsCandidate - 1;
                        var substringCheckForSymbols =
                            new string(previousLine.Skip(skip).Take(candidate.Length + 2).ToArray());

                        if (substringCheckForSymbols.Any(x => !char.IsDigit(x) && x != '.'))
                        {
                            System.Console.Write(candidate + "   ");
                            list.Add(GetInt(candidate));
                            continue;
                        }
                    }

                    if (i != Count - 1) // check next line
                    {
                        var nextLine = this.ElementAt(i + 1) as string;
                        var skip = whereIsCandidate == 0 ? 0 : whereIsCandidate - 1;

                        var substringCheckForSymbols =
                            new string(nextLine.Skip(skip).Take(candidate.Length + 2).ToArray());

                        if (substringCheckForSymbols.Any(x => !char.IsDigit(x) && x != '.'))
                        {
                            System.Console.Write(candidate + "   ");
                            list.Add(GetInt(candidate));
                        }
                    }
                }
            }

            return list;
        }

        private static bool IsThereASymbolInTheEdges(string candidate, string thisLine, int whereIsCandidate)
        {
            //Console.WriteLine("Candidate is at " + whereIsCandidate);
            //Console.WriteLine("Candidate + lenght is at " + (whereIsCandidate + candidate.Length));
            if (whereIsCandidate != 0 && thisLine.ElementAt(whereIsCandidate - 1) != '.')
            {
                //Console.WriteLine("BEFORE: " + thisLine.ElementAt(whereIsCandidate - 1));
                return true;
            }
            if (whereIsCandidate + candidate.Length < thisLine.Length &&
                thisLine.ElementAt(whereIsCandidate + candidate.Length) != '.')
            {
                //Console.WriteLine("AFTER: " + thisLine.ElementAt(whereIsCandidate + candidate.Length));
                return true;
            }

            return false;
        }

        private int GetInt(string item)
        {
            string cleanedUpString = string.Empty;
            foreach (char c in item)
                if (char.IsDigit(c))
                    cleanedUpString += c;
            return int.Parse(cleanedUpString);
        }

        private int WhereIsSubstring(string original, string substr, int ignore = 0)
        {
            var index = new string(original.Skip(ignore).ToArray()).IndexOf(substr);

            return index + ignore;
        }
    }
}