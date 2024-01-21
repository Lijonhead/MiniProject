namespace MiniProject.Models
{
    public class Link
    {
        public int LinkId { get; set; }
        public string Url { get; set; }
        
        public int PersonIntrestId { get; set; }
        public PersonIntrest PersonIntrest { get; set; }
    }
}
