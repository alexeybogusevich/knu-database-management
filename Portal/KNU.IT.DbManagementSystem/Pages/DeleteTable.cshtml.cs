using System;
using System.Threading.Tasks;
using KNU.IT.DbServices.Services.TableService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KNU.IT.DBMSWebApplication.Pages
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
            return RedirectToPage("./ExploreTables", new { databaseId });
        }
    }
}
