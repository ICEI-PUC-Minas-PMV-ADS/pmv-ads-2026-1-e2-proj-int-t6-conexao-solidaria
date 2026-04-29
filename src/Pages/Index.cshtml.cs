using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConexaoSolidaria.Pages;

public class IndexModel : PageModel
{
    public IActionResult OnGet()
    {
        // Se já estiver autenticado, vai direto para o Dashboard
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToPage("/Dashboard");
        return Page();
    }
}
