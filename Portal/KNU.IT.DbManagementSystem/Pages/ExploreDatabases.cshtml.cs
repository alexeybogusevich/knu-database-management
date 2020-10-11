using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.IT.DbManagementSystem.Services.DatabaseService;
using KNU.IT.DbManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KNU.IT.DbManagementSystem.Pages
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
    }
}
