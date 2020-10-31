using System;
using System.Threading.Tasks;
using KNU.IT.DbServices.Services.DatabaseService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KNU.IT.DBMSWebApplication.Pages
{
    public class DeleteDatabaseModel : PageModel
    {
        private readonly IDatabaseService databaseService;

        public DeleteDatabaseModel(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }
        public async Task<ActionResult> OnGetAsync(Guid databaseId)
        {
            await databaseService.DeleteAsync(databaseId);
            return RedirectToPage("./ExploreDatabases");
        }
    }
}
