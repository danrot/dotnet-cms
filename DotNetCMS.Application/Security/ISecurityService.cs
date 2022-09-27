namespace DotNetCMS.Application.Security;

public interface ISecurityService
{
	public void DenyAccessUnlessAuthorized();
}
