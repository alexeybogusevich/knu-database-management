using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace KNU.IT.DBMSWebApplication.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {

        }

        public void OnGet()
        {

        }

        public ActionResult OnPostExploreDatabases()
        {
            return RedirectToPage("./ExploreDatabases", "OnGetAsync");
        }
    }
}
