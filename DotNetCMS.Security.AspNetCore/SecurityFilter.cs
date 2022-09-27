namespace DotNetCMS.Security.AspNetCore;

using DotNetCMS.Application.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public sealed class SecurityFilter : IExceptionFilter
{
	public void OnException(ExceptionContext exceptionContext)
	{
		if (exceptionContext.Exception is AccessDeniedException)
		{
			exceptionContext.Result = new ForbidResult();
		}
	}
}
