using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.IT.DbManagementSystem.Services.DatabaseService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KNU.IT.DbManagementSystem.Pages
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
