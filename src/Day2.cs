namespace Advent._2023.Console
{
    internal class Day2 : IAdventCalendarDay
    {
        public void Run()
        {
            var isTest = false;

            var games = GetGames(isTest);

            var possible = games.Where(x => x.Red <= 12 && x.Green <= 13 && x.Blue <= 14).ToList();

            var possibleGamesString = string.Join(", ", possible.Select(x => x.Id.ToString()));

            var powerAdded = 0;

            System.Console.WriteLine($"Games {possibleGamesString} are possible. Total of {possible.Sum(x => x.Id)}");

            foreach (var game in games)
            {
                var power = game.Green * game.Blue * game.Red;
                System.Console.WriteLine($" - The power of game {game.Id} is {power}");
                powerAdded += power;
            }


            System.Console.WriteLine("Added power is: " + powerAdded);
        }

        private IEnumerable<CubeBagGame> GetGames(bool isTest = false)
        {
            var rawGames = GetRawGames(isTest);

            var games = rawGames.Select(g =>
            {
                // get the id
                var gameId = int.Parse(g.Split(":")[0].Split(" ")[1]);
                // create the game
                var game = new CubeBagGame(gameId);

                var subsets = g.Split(":")[1].Split(";");

                foreach (var subset in subsets)
                {
                    var ballCounts = subset.Split(",");

                    var dic = ballCounts.Select(x => new KeyValuePair<string, int>(x.Trim().Split(" ")[1], int.Parse(x.Trim().Split(" ")[0]))).ToDictionary();

                    foreach (var kvp in dic)
                    {
                        switch (kvp.Key)
                        {
                            case "blue":
                                if(game.Blue < kvp.Value)
                                    game.Blue = kvp.Value;
                                break;
                            case "red":
                                if (game.Red < kvp.Value)
                                    game.Red = kvp.Value;
                                break;
                            case "green":
                                if (game.Green < kvp.Value)
                                    game.Green = kvp.Value;
                                break;
                        }
                    }
                }
                return game;
            });

            return games;
        }

        private IEnumerable<string> GetRawGames(bool isTest = false)
        {
            if (isTest)
            {
                yield return "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green";
                yield return "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 gree, 1 blue";
                yield return "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green 1 red";
                yield return "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red";
                yield return "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";
            }
            else
            {
                var fileName = "Day2PuzzleInput.txt";
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

    public class CubeBagGame
    {
        public int Id { get; set; }
        public int Red { get; set; }
        public int Blue { get; set; }
        public int Green { get; set; }

        public CubeBagGame(int id)
        {
            Id = id;
            Red = 0;
            Blue = 0;
            Green = 0;
        }
    }
}