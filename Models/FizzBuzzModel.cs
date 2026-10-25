namespace CodeChallenge.FizzBuzz.Models
{
    using System.ComponentModel.DataAnnotations;
    public class FizzBuzzModel
    {
        [Required(ErrorMessage = "Please enter a fizz value")]
        [Range(minimum: 1, maximum: 200, ErrorMessage = "Fizz Value must be between 1 and 200")]
        public int FizzValue { get; set; } = 3;
        
        [Required(ErrorMessage = "Please enter a buzz value")]
        [Range(minimum: 1, maximum: 200, ErrorMessage = "Buzz Value must be between 1 and 200")]
        public int BuzzValue { get; set; } = 5;

        [Required(ErrorMessage = "Please enter a stop value")]
        [Range(minimum: 1, maximum: 1000, ErrorMessage = "Stop Value must be between 1 and 1000")]
        public int StopValue { get; set; } = 100;
    }
}
