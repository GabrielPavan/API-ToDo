using System.ComponentModel.DataAnnotations;

namespace MyToDo.ViewModels
{
    public class CreateToDoViewModel
    {
        [Required]
        public required string Title { get; set; }
    }
}