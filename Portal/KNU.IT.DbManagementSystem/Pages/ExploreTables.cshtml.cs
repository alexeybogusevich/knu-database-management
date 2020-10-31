using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Services.DatabaseService;
using KNU.IT.DbServices.Services.TableService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KNU.IT.DBMSWebApplication.Pages
{
    public class ExploreTablesModel : PageModel
    {
        private readonly ITableService tableService;
        private readonly IDatabaseService databaseService;

        public ExploreTablesModel(ITableService tableService, IDatabaseService databaseService)
        {
            this.tableService = tableService;
            this.databaseService = databaseService;
        }

        [BindProperty]
        public Database Database { get; set; }
        [BindProperty]
        public IEnumerable<Table> Tables { get; set; }

        public async Task<ActionResult> OnGetAsync(Guid databaseId)
        {
            Database = await databaseService.GetAsync(databaseId);
            Tables = await tableService.GetAllByDatabaseAsync(databaseId);
            return Page();
        }

        public ActionResult OnPostCreateNewTable(Guid databaseId)
        {
            return RedirectToPage("./CreateTable", new {databaseId});
        }

        public ActionResult OnPostReturnToDatabases()
        {
            return RedirectToPage("./ExploreDatabases");
        }
    }
}
