using Microsoft.AspNetCore.Mvc;
using System;

namespace GuessingGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuessingGameController : ControllerBase
    {
        private static Random random = new Random();
        private static int secretNumber;
        private static int guesses = 0;
        private static int wins = 0;
        private static int totalGames = 0;
        private const int maxAttempts = 3;

        [HttpGet("start")]
        public ActionResult<string> StartNewGame()
        {
            secretNumber = random.Next(0, 10);
            guesses = 0;
            totalGames++;
            return Ok("New game started. Guess a number between 0 and 9.");
        }

        [HttpGet("{id}")]
        public ActionResult<string> CheckGuess(int id)
        {
            guesses++;

            if (id == secretNumber)
            {
                wins++;
                totalGames++;
                ResetGame();
                return Ok("Congratulations! You guessed the number.");
            }
            else if (guesses >= maxAttempts)
            {
                totalGames++;
                ResetGame();
                return Ok($"Sorry, you didn't guess the number. The correct number was {secretNumber}.");
            }
            else if (id < secretNumber)
            {
                return Ok("Choose a larger number!");
            }
            else
            {
                return Ok("Choose a smaller number!");
            }
        }

        private void ResetGame()
        {
            secretNumber = random.Next(0, 10);
            guesses = 0;
        }

        [HttpGet("stats")]
        public ActionResult<string> GetStats()
        {
            double winRate = totalGames > 0 ? (double)wins / totalGames * 100 : 0;
            return Ok($"Total Games: {totalGames}, Wins: {wins}, Win Rate: {winRate.ToString("0.00")}%");
        }
    }
}
