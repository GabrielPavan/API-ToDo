namespace MyToDo.Models
{
    public class ToDo
    {
        public int id { get; set; }
        public required string Title { get; set; }
        public bool Done { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}