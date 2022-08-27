using System.Collections.Generic;

namespace People.AuthenticationModels;

public class UserConstants
{
	public static List<UserModel> Users = new()
	{
		new UserModel { Username = "admin", EmailAddress = "admin@email.com", Password = 
			"adminPassword", GivenName = "Roman", Surname = "Chirkov", Role = "Administrator" },
		new UserModel { Username = "user", EmailAddress = "user@email.com", Password = 
			"userPassword", GivenName = "John", Surname = "Doe", Role = "User" },
	};
}