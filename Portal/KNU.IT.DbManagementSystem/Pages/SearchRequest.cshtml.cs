using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.IT.DbManagementSystem.Constants;
using KNU.IT.DbManagementSystem.Models;
using KNU.IT.DbManagementSystem.Services.RowService;
using KNU.IT.DbManagementSystem.Services.TableService;
using KNU.IT.DbManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KNU.IT.DbManagementSystem.Pages
{
    public class SearchRequestModel : PageModel
    {
        private readonly ITableService tableService;

        public SearchRequestModel(ITableService tableService)
        {
            this.tableService = tableService;
        }

        [BindProperty]
        public TableViewModel Table { get; set; }
        public SelectList ColumnNames { get; set; }
        [BindProperty]
        public string SearchColumn { get; set; }
        [BindProperty]
        public string SearchKeyword { get; set; }

        public async Task<ActionResult> OnGetAsync(Guid tableId)
        {
            Table = await tableService.GetViewModelAsync(tableId);
            var selectListItems = Table
                .Schema
                .Where(s => !s.Value.Equals(TypeConstants.Picture))
                .Select(s => new { Column = s.Key })
                .ToList()
                .AsQueryable();
            ColumnNames = new SelectList(selectListItems, "Column", "Column", null);
            return Page();
        }

        public ActionResult OnPostSearchAsync(Guid tableId)
        {
            return RedirectToPage("./SearchResults", new { tableId, column = SearchColumn, keyword = SearchKeyword });
        }
    }
}
