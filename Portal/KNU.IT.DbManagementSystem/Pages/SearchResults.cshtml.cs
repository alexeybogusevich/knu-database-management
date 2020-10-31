using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.IT.DbServices.Models;
using KNU.IT.DbServices.Services.RowService;
using KNU.IT.DbServices.Services.TableService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KNU.IT.DBMSWebApplication.Pages
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
