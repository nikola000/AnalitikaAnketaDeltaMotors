namespace UnitOfWorkExample.UnitOfWork.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsAdministrator { get; set; }
        public string Mail { get; set; }
    }
}