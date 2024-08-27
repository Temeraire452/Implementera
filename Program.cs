namespace Implementera
{
    // Designmönster implementerade i denna applikation: Factory method, Singleton och Strategy


    // Singleton: Endast en instans av spelet kan existera
    public sealed class GameSession
        {
            private static readonly GameSession instance = new GameSession();

            public static GameSession Instance
            {
                get
                {
                    return instance;
                }
            }

            private GameSession() { }

            public void PlayGame(IGameDifficulty difficulty)
            {
                int numberToGuess = difficulty.GenerateNumber();
                Console.WriteLine("Gissa ett nummer mellan 1 och " + difficulty.MaxNumber);
                int guess;
                do
                {
                    guess = int.Parse(Console.ReadLine());
                    if (guess < numberToGuess)
                    {
                        Console.WriteLine("För lågt, försök igen!");
                    }
                    else if (guess > numberToGuess)
                    {
                        Console.WriteLine("För högt, försök igen!");
                    }
                } while (guess != numberToGuess);
                Console.WriteLine("Grattis! Du gissade rätt!");
            }
        }

        // Strategy: Definiera en gemensam strategi för svårighetsgrad
        public interface IGameDifficulty
        {
            int MaxNumber { get; }
            int GenerateNumber();
        }

        public class EasyDifficulty : IGameDifficulty
        {
            public int MaxNumber => 10;
            public int GenerateNumber()
            {
                return new Random().Next(1, MaxNumber + 1);
            }
        }

        public class MediumDifficulty : IGameDifficulty
        {
            public int MaxNumber => 50;
            public int GenerateNumber()
            {
                return new Random().Next(1, MaxNumber + 1);
            }
        }

        public class HardDifficulty : IGameDifficulty
        {
            public int MaxNumber => 100;
            public int GenerateNumber()
            {
                return new Random().Next(1, MaxNumber + 1);
            }
        }

        // Factory Method: Skapa rätt svårighetsgrad baserat på användarens val
        public static class GameFactory
        {
            public static IGameDifficulty CreateGame(string difficultyLevel)
            {
                switch (difficultyLevel.ToLower())
                {
                    case "easy":
                        return new EasyDifficulty();
                    case "medium":
                        return new MediumDifficulty();
                    case "hard":
                        return new HardDifficulty();
                    default:
                        throw new ArgumentException("Fel svårighetsgrad.");
                }
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("Välj svårighetsgrad: easy, medium, hard");
                string difficulty = Console.ReadLine();

                try
                {
                    IGameDifficulty game = GameFactory.CreateGame(difficulty);
                    GameSession.Instance.PlayGame(game);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
