﻿using AutoMapper;
using People.BussinesLogic.Blo.Models;
using People.DataAccess.Rto.Models;

namespace People.Mappings;

public class PeopleProfile : Profile
{
	public PeopleProfile()
	{
		CreateMap<PersonRto, PersonBlo>();
		CreateMap<PersonBlo, PersonRto>();
		CreateMap<ChildrenRto, ChildrenBlo>();
	}
}