namespace DotNetCMS.Security.AspNetCore;

using DotNetCMS.Application.Security;

public class SecurityService : ISecurityService
{
	public void DenyAccessUnlessAuthorized()
	{
		// TODO Pass inner exception
		throw new AccessDeniedException();
	}
}
