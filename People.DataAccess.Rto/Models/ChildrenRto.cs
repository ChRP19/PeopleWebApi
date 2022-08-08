namespace People.DataAccess.Rto.Models;

public class ChildrenRto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? BirthСertificate { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int? SchoolNumber { get; set; }
    
    public ICollection<ToyRto>? Toys { get; set; }
    
    public Guid PersonId { get; set; }
    public PersonRto PersonRto { get; set; }
}