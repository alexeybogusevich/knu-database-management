using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.IT.DbManagementSystem.Services.DatabaseService;
using KNU.IT.DbManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage;

namespace KNU.IT.DbManagementSystem.Pages
{
    public class CreateDatabaseModel : PageModel
    {
        private readonly IDatabaseService databaseService;

        public CreateDatabaseModel(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        [BindProperty]
        public DbManager.Models.Database Database { get; set; }

        public ActionResult OnGet()
        {
            return Page();
        }

        public async Task<ActionResult> OnPostCreateDatabaseAsync()
        {
            await databaseService.CreateAsync(Database);
            return RedirectToPage("./ExploreDatabases");
        }
    }
}
