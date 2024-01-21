namespace MiniProject.Models
{
    public class Intrest
    {
        public int IntrestId { get; set; }
        public string Titel { get; set; }
        public string Description { get; set; }

        public ICollection<PersonIntrest> personIntrests { get; set; }
    }
}
