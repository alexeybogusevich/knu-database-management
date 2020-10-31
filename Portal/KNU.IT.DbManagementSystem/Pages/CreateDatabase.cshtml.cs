using System.Threading.Tasks;
using KNU.IT.DbServices.Services.DatabaseService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KNU.IT.DBMSWebApplication.Pages
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
