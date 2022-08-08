namespace People.DataAccess.Rto.Models;

public class ToyRto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string CountryOfManufacture { get; set; }
    
    public Guid ChildrenId { get; set; }
    public ChildrenRto ChildrenRto { get; set; }
}