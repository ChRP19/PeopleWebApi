namespace People.DataAccess.Rto.Models;

public class PersonRto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? Passport { get; set; }
    public DateTime WeddingDate { get; set; }
    public int? ConvictionsNumber { get; set; }
    
    public ICollection<ChildrenRto> Childrens { get; set; }
}