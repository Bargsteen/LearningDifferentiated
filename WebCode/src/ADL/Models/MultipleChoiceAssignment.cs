using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ADL.Models
{
    public class MultipleChoiceAssignment : Assignment
    {
        [Required(ErrorMessage = "Venligst skriv svarmulighederne")]
        public List<AnswerOption> AnswerOptions { get; set; }

        [Required(ErrorMessage = "Venligst vælg det/de korrekte svar")]
        public List<int> CorrectAnswers { get; set; }
    }
}
