using System;
using System.Threading.Tasks;
using KNU.IT.DbServices.Services.RowService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KNU.IT.DBMSWebApplication.Pages
{
    public class DeleteRowModel : PageModel
    {
        private readonly IRowService rowService;

        public DeleteRowModel(IRowService rowService)
        {
            this.rowService = rowService;
        }
        public async Task<ActionResult> OnGet(Guid rowId, Guid tableId)
        {
            await rowService.DeleteAsync(rowId);
            return RedirectToPage("./ExploreRows", new { tableId });
        }
    }
}
