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

namespace KNU.IT.DbManagementSystem.Pages
{
    public class SearchResultsModel : PageModel
    {
        private readonly ITableService tableService;
        private readonly IRowService rowService;

        public SearchResultsModel(ITableService tableService, IRowService rowService)
        {
            this.tableService = tableService;
            this.rowService = rowService;
        }

        [BindProperty]
        public TableViewModel Table { get; set; }
        [BindProperty]
        public List<RowViewModel> Rows { get; set; }
        [BindProperty]
        public string Column { get; set; }
        [BindProperty]
        public string Keyword { get; set; }

        public async Task<ActionResult> OnGetAsync(Guid tableId, string column, string keyword)
        {
            Table = await tableService.GetViewModelAsync(tableId);
            Rows = await rowService.SearchByKeywordAsync(tableId, keyword, column);
            Column = column;
            Keyword = keyword;
            return Page();
        }

        public ActionResult OnPostReturnToTables(Guid databaseId)
        {
            return RedirectToPage("./ExploreTables", new { databaseId });
        }
    }
}
