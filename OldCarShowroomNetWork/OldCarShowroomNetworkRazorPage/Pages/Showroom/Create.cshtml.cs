﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BOs.Models;

namespace OldCarShowroomNetworkRazorPage.Pages.Showroom
{
    public class CreateModel : PageModel
    {
        private readonly BOs.Models.OldCarShowroomNetworkContext _context;

        public CreateModel()
        {
            _context = new OldCarShowroomNetworkContext();
        }

        public IActionResult OnGet()
        {
          
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "Name");
            ViewData["DistrictId"] = new SelectList(_context.Districts, "DistrictId", "Name");
            ViewData["ImageId"] = new SelectList(_context.ImageShowrooms, "ImageId", "ImageId");
            ViewData["Wards"] = new SelectList(_context.Wards, "WardId", "Name");

            return Page();
        }


        [BindProperty]
        public BOs.Models.Showroom Showroom { get; set; } = default!;
        



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Showrooms == null || Showroom == null)
            {
                return Page();
            }

            _context.Showrooms.Add(Showroom);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
