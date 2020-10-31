using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Services.TableService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace KNU.IT.DBMSWebApplication.Pages
{
    public class CreateTableModel : PageModel
    {
        private readonly ITableService tableService;

        public CreateTableModel(ITableService tableService)
        {
            this.tableService = tableService;
        }

        [BindProperty]
        public Dictionary<string, string> Dictionary { get; set; }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public Guid DatabaseId { get; set; }

        public ActionResult OnGet(Guid databaseId)
        {
            Dictionary = new Dictionary<string, string>();
            DatabaseId = databaseId;
            return Page();
        }

        public async Task<ActionResult> OnPostAsync(Dictionary<string, string> dict)
        {
            if (dict.Count == 0)
            {
                return RedirectToPage("./ExploreTables", new { DatabaseId });
            }
            var json = JsonConvert.SerializeObject(dict);
            var table = new Table
            {
                Name = Name,
                DatabaseId = DatabaseId,
                Schema = json
            };
            await tableService.CreateAsync(table);
            return RedirectToPage("./ExploreTables", new { databaseId = DatabaseId});
        }
    }
}
