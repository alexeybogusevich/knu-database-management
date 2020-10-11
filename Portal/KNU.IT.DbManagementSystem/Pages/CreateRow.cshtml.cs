using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using KNU.IT.DbManagementSystem.Services.RowService;
using KNU.IT.DbManagementSystem.Services.TableService;
using KNU.IT.DbManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace KNU.IT.DbManagementSystem.Pages
{
    public class CreateRowModel : PageModel
    {
        private readonly IRowService rowService;
        private readonly ITableService tableService;

        public CreateRowModel(IRowService rowService, ITableService tableService)
        {
            this.rowService = rowService;
            this.tableService = tableService;
        }

        [BindProperty]
        public Guid TableId { get; set; }
        [BindProperty]
        public Dictionary<string, string> RowFields { get; set; }
        [BindProperty]
        public Dictionary<string, string> Schema { get; set; }

        public async Task<ActionResult> OnGet(Guid tableId)
        {
            TableId = tableId;
            var table = await tableService.GetAsync(tableId);
            Schema = JsonConvert.DeserializeObject<Dictionary<string, string>>(table.Schema);
            RowFields = new Dictionary<string, string>();
            foreach(var column in Schema)
            {
                RowFields.Add(column.Key, null);
            }
            return Page();
        }

        public async Task<ActionResult> OnPostCreateRowAsync(Guid tableId)
        {
            var table = await tableService.GetAsync(tableId);
            Schema = JsonConvert.DeserializeObject<Dictionary<string, string>>(table.Schema);

            foreach (var field in RowFields)
            {
                if(!TypeDescriptor.GetConverter(Schema[field.Key]).IsValid(field.Value))
                {
                    return RedirectToPage("./ExploreRows", new { tableId });
                }
            }

            var row = new Row
            {
                TableId = TableId,
                Content = JsonConvert.SerializeObject(RowFields)
            };

            await rowService.CreateAsync(row);

            return RedirectToPage("./ExploreRows", new { tableId });
        }
    }
}
