using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Models;
using KNU.IT.DbServices.Services.RowService;
using KNU.IT.DbServices.Services.TableService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace KNU.IT.DBMSWebApplication.Pages
{
    public class ExploreRowsModel : PageModel
    {
        private readonly IRowService rowService;
        private readonly ITableService tableService;

        public ExploreRowsModel(ITableService tableService, IRowService rowService)
        {
            this.tableService = tableService;
            this.rowService = rowService;
        }

        [BindProperty]
        public Table Table { get; set; }
        [BindProperty]
        public List<RowViewModel> Rows { get; set; }

        public async Task<ActionResult> OnGetAsync(Guid tableId)
        {
            Table = await tableService.GetAsync(tableId);
            Rows = await rowService.GetAllViewModelsByTableAsync(tableId);
            return Page();
        }

        public ActionResult OnPostCreateNewRow(Guid tableId)
        {
            return RedirectToPage("./CreateRow", new {tableId});
        }

        public ActionResult OnPostReturnToTables(Guid databaseId)
        {
            return RedirectToPage("./ExploreTables", new { databaseId });
        }
    }
}
