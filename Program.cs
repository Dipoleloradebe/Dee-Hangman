using System;

class Hangman
{
    private string[] words;
    private string[] categories;
    private string secretWord;
    private char[] guessedLetters;
    private int lives;
    private int score;
    private bool continuePlaying;

    public Hangman()
    {
        words = new string[] { "cat", "dog", "elephant", "Orange", "Banana", "Guava", "Gauteng", "Mpumalanga", "Northwest" };
        categories = new string[] { "Animals", "Fruits", "Places", };
        secretWord = "";
        guessedLetters = new char[0];
        lives = 6;
        score = 0;
        continuePlaying = true;
    }

    public void PlayGame()
    {
        Console.WriteLine("Welcome to Dee Hangman game!");

        while (continuePlaying)
        {
            Console.WriteLine("Category: " + GetCategory());
            Console.WriteLine(" In this hangman game you are given the category and 6 lives, choose wisely");
            Console.WriteLine("Guess the secret word by entering one letter at a time.");
            Console.WriteLine("You have " + lives + " lives. Good luck!");
            Console.WriteLine();

            InitializeGame();

            bool wordGuessed = false; // Added variable to track if the word is guessed

            while (lives > 0 && !wordGuessed) // Update loop condition
            {
                Console.WriteLine("Secret word: " + GetClue());
                Console.WriteLine("Lives remaining: " + lives);
                Console.Write("Enter a letter: ");
                char letter = char.ToLower(Console.ReadKey().KeyChar);
                Console.WriteLine();

                if (!IsLetterValid(letter))
                {
                    Console.WriteLine(" You have entered a wrong letter. Please enter another letter.");
                    continue;
                }

                if (IsLetterAlreadyGuessed(letter))
                {
                    Console.WriteLine("You already guessed that letter. Try again.");
                    continue;
                }

                guessedLetters = UpdateGuessedLetters(letter);

                if (IsWordGuessed())
                {
                    Console.WriteLine("Congratulations! You won!");
                    Console.WriteLine("The secret word was: " + secretWord);
                    score++;
                    wordGuessed = true; // Set wordGuessed to true when word is guessed
                }

                if (!secretWord.Contains(letter.ToString()))
                {
                    lives--;
                    Console.WriteLine("Wrong guess!");
                }
            }

            if (!wordGuessed) // Check if the word was not guessed within the loop
            {
                Console.WriteLine("Ahh sorry, you lost!");
                Console.WriteLine("The secret word was: " + secretWord);
            }

            Console.WriteLine("Score: " + score);
            Console.WriteLine();

            Console.Write("Do you want to play again? (y/n): ");
            char playAgain = char.ToLower(Console.ReadKey().KeyChar);
            Console.WriteLine();

            if (playAgain != 'y')
            {
                continuePlaying = false;
            }
        }

        Console.WriteLine("Thank you for playing Hangman!");
    }

    private void InitializeGame()
    {
        int randomIndex = new Random().Next(words.Length);
        secretWord = words[randomIndex];
        guessedLetters = new char[secretWord.Length];
        lives = 6;
    }

    private string GetCategory()
    {
        int randomIndex = new Random().Next(categories.Length);
        return categories[randomIndex];
    }

    private string GetClue()
    {
        string clue = "";
        for (int i = 0; i < secretWord.Length; i++)
        {
            if (guessedLetters[i] != '\0')
            {
                clue += guessedLetters[i];
            }
            else
            {
                clue += "_";
            }
        }
        return clue;
    }

    private bool IsLetterValid(char letter)
    {
        return Char.IsLetter(letter);
    }

    private bool IsLetterAlreadyGuessed(char letter)
    {
        return Array.IndexOf(guessedLetters, letter) >= 0;
    }

    private char[] UpdateGuessedLetters(char letter)
    {
        for (int i = 0; i < secretWord.Length; i++)
        {
            if (secretWord[i] == letter)
            {
                guessedLetters[i] = letter;
            }
        }
        return guessedLetters;
    }

    private bool IsWordGuessed()
    {
        return secretWord == new string(guessedLetters);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Hangman hangman = new Hangman();
        hangman.PlayGame();
    }
}

