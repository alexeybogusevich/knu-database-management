using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.IT.DbManagementSystem.Models;
using KNU.IT.DbManagementSystem.Services.RowService;
using KNU.IT.DbManagementSystem.Services.TableService;
using KNU.IT.DbManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace KNU.IT.DbManagementSystem.Pages
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
    }
}
