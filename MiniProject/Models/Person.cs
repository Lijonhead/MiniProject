namespace MiniProject.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<PersonIntrest> personIntrests { get; set; }

        
    }
}
