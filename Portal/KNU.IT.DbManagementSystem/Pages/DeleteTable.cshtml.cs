using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.IT.DbManagementSystem.Services.TableService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KNU.IT.DbManagementSystem.Pages
{
    public class DeleteTableModel : PageModel
    {
        private readonly ITableService tableService;

        public DeleteTableModel(ITableService tableService)
        {
            this.tableService = tableService;
        }

        public async Task<ActionResult> OnGet(Guid tableId, Guid databaseId)
        {
            await tableService.DeleteAsync(tableId);
            return RedirectToPage("./ExploreRows", new { databaseId });
        }
    }
}
