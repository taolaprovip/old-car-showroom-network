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
    public class IndexModel : PageModel
    {
        private readonly BOs.Models.OldCarShowroomNetworkContext _context;

        public IndexModel()
        {
            _context = new OldCarShowroomNetworkContext();
        }

        public IList<BOs.Models.Showroom> Showroom { get;set; } = default!;

        
    }
}
