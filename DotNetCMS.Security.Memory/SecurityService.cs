namespace DotNetCMS.Security.Memory;

using DotNetCMS.Application.Security;

/// <remarks>
///		This class uses an in-memory dictionary and therefore has to be registered in ASP.NET using the
///		AddSingleton method. Otherwise this class will be regenerated on every request causing it to lose its data.
/// </remarks>
public class SecurityService : ISecurityService
{
	public void DenyAccessUnlessAuthorized()
	{
		/* throw new AccessDeniedException(); */
	}
}
