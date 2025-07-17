namespace testwithnet8.Data
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }


    class CreatePersonDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    class PersonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}