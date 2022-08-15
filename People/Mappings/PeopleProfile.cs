using AutoMapper;
using People.BussinesLogic.Blo.Models;
using People.DataAccess.Rto.Models;
using People.Models;

namespace People.Mappings;

public class PeopleProfile : Profile
{
	public PeopleProfile()
	{
		CreateMap<PersonRto, PersonBlo>().ReverseMap();
		CreateMap<ChildrenRto, ChildrenBlo>().ReverseMap();
		CreateMap<ToyRto, ToyBlo>().ReverseMap();

		CreateMap<PersonBlo, PersonDto>().ReverseMap();
		CreateMap<ChildrenBlo, ChildrenDto>().ReverseMap();
		CreateMap<ToyBlo, ToyDto>().ReverseMap();
	}
}