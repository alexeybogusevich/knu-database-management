using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.IT.DbManagementSystem.Services.RowService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KNU.IT.DbManagementSystem.Pages
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
