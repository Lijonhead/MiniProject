namespace MiniProject.Models
{
    public class PersonIntrest
    {
        public int PersonIntrestId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int IntrestId { get; set; }
        public Intrest Intrest { get; set; }
        public ICollection<Link> links { get; set; }
    }
}
