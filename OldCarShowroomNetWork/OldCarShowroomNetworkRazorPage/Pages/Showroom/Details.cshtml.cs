using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Models;

namespace OldCarShowroomNetworkRazorPage.Pages.Showroom
{
    public class DetailsModel : PageModel
    {
        private readonly BOs.Models.OldCarShowroomNetworkContext _context;

        public DetailsModel(BOs.Models.OldCarShowroomNetworkContext context)
        {
            _context = context;
        }

      public BOs.Models.Showroom Showroom { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Showrooms == null)
            {
                return NotFound();
            }

            var showroom = await _context.Showrooms.FirstOrDefaultAsync(m => m.ShowroomId == id);
            if (showroom == null)
            {
                return NotFound();
            }
            else 
            {
                Showroom = showroom;
            }
            return Page();
        }
    }
}
