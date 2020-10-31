using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Services.DatabaseService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KNU.IT.DBMSWebApplication.Pages
{
    public class ExploreDatabasesModel : PageModel
    {
        private readonly IDatabaseService databaseService;

        public ExploreDatabasesModel(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        [BindProperty]
        public IEnumerable<Database> Databases { get; set; }

        public async Task<ActionResult> OnGetAsync()
        {
            Databases = await databaseService.GetAllAsync();
            return Page();
        }

        public ActionResult OnPostCreateNewDatabase()
        {
            return RedirectToPage("./CreateDatabase");
        }
    }
}
