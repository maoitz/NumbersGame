namespace NumbersGame;
 // Mowitz Almstedt .NET24 //

/*=============================
 * Welcome to the numbers game!
 * Easy console application that
 * lets the user guess a number
 * based on which difficulty is
 * chosen. Max numbers of tries
 * is implemented and returns the
 * user to the difficulty option. 
 ============================*/

 public class Program
 {
     //Enum to declare difficulties to improve readability 
     public enum Difficulty
     {
         Easy = 1,
         Intermediate,
         Hard
     }
     
     public static void Main(string[] args)
     {
         Console.Clear();
         //Declare variables
         Random rnd = new Random();
         bool shouldContinue = true;
         int menuChoice;
         
         //Main loop to control the game
         while (shouldContinue)
         {
             //Show the main menu and get user input
             menuChoice = ShowMainMenu(); 

             switch (menuChoice)
             {
                 case 1: //Play
                     
                     bool isPlaying = true; //Loop to control the playing session
                     while (isPlaying)
                     {
                         //Show the difficulty menu and get user input
                         int difficulty = ShowDifficultyMenu();

                         switch (difficulty)
                         {
                             case (int)Difficulty.Easy: //Easy difficulty
                                 PlayGame(rnd, 1, 10, 6); //Start easy game
                                 break;
                             case (int)Difficulty.Intermediate: //Intermediate difficulty
                                 PlayGame(rnd, 1, 25, 5); //Start intermediate game
                                 break;
                             case (int)Difficulty.Hard: //Hard difficulty
                                 PlayGame(rnd, 1, 50, 3); //Start hard game
                                 break;
                             case 4: //Return user to main menu
                                 isPlaying = false; //Exit the game loop
                                 Console.Clear();
                                 break;
                         }
                     }
                     break;
                 
                 case 2: //Quit
                     Console.WriteLine("Avslutar!");
                     shouldContinue = false; //Exit the main loop
                     break;
             }
         }
     }

     //Method for the main menu
     public static int ShowMainMenu()
     {
         Console.WriteLine("Välkommen!");
         Console.WriteLine("[1] Spela");
         Console.WriteLine("[2] Avsluta");
         Console.Write("[ ]");
         Console.SetCursorPosition(1, Console.CursorTop);

         return GetValidInput(1, 2);
     }

     //Method for the difficulty menu
     public static int ShowDifficultyMenu()
     {
         Console.WriteLine("\nVälj svårighetsgrad");
         Console.WriteLine("[1] Lätt ( 1-10, 6 försök )");
         Console.WriteLine("[2] Medel ( 1-25, 5 försök )");
         Console.WriteLine("[3] Svår ( 1-50, 3 försök");
         Console.WriteLine("[4] Gå Tillbaka");
         Console.Write("[ ]");
         Console.SetCursorPosition(1, Console.CursorTop);

         return GetValidInput(1, 4);
     }

     //Method to validate user input as an integer
     public static int GetValidInput(int min, int max)
     {
         int input;
         do
         {
             if (int.TryParse(Console.ReadLine(), out input) && input >= min && input <= max)
             {
                 break;
             }

             Console.WriteLine($"\n**Ogiltigt!**\n");
             Console.Write($"{min} - {max}: ");
         } while (true);

         return input;
     }

     //Method to play the game
     public static void PlayGame(Random rnd, int min, int max, int maxTries)
     {
         Console.Clear();
         int userGuess = 0;
         int userTries = 0;
         int rndNumber = rnd.Next(min, max + 1);
         
         Console.WriteLine($"Jag tänker på ett tal mellan {min} och {max}");
         Console.Write($"Kan du gissa vilket? (Försök {userTries + 1}/{maxTries}): ");

         while (userGuess != rndNumber)
         {
             userGuess = GetValidInput(min, max);

             int difference = Math.Abs(userGuess - rndNumber); //Calculate difference between guess and correct number

             if (userGuess > rndNumber)
             {
                 Console.Clear();
                 Console.WriteLine($"{userGuess} är för högt!");
             }
             else if (userGuess < rndNumber)
             {
                 Console.Clear();
                 Console.WriteLine($"{userGuess} är för lågt!");
             }
             else
             {
                 Console.Clear();
                 Console.WriteLine("Korrekt! Bra jobbat :)");
                 Console.WriteLine($"Numret var: {rndNumber}");
                 break; //Correct answer
             }

             //Gives feedback based on how close the guess was
             if (difference <= 2)
             {
                 Console.WriteLine("Men du är mycket nära :)");
                 Console.Write($"Gissa igen (Försök {userTries + 2}/{maxTries}): ");
             }
             else if (difference <= 5)
             {
                 Console.WriteLine("Men du är nära");
                 Console.Write($"Gissa igen (Försök {userTries + 2}/{maxTries}): "); 
             }
             else
             {
                 Console.WriteLine("Du är väldigt långt ifrån.");
                 Console.Write($"Gissa igen (Försök {userTries + 2}/{maxTries}): "); 
             }
             
             userTries++;

             if (userTries == maxTries)
             {
                 Console.Clear();
                 Console.WriteLine($"Du har gissat {userTries} gånger");
                 Console.WriteLine($"Det rätta svaret var: {rndNumber}");
                 Console.WriteLine("Försök igen!\n");
                 break; //End the game after max tries
             }
         }
     }

 }