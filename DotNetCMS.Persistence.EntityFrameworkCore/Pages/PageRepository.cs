using DotNetCMS.Domain.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DotNetCMS.Persistence.EntityFrameworkCore.Pages
{
	public sealed class PageRepository : IPageRepository
	{
		private readonly CmsContext _cmsContext;

		public PageRepository(CmsContext cmsContext)
		{
			_cmsContext = cmsContext;
		}

		public void Add(Page page)
		{
			_cmsContext.Pages.Add(page);
		}

		public Task<Page> GetByIdAsync(Guid id)
		{
			return _cmsContext.Pages.SingleAsync(page => page.Id == id);
		}
	}
}