﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOs.Models;

namespace OldCarShowroomNetworkRazorPage.Pages.Showroom
{
    public class EditModel : PageModel
    {
        private readonly BOs.Models.OldCarShowroomNetworkContext _context;

        public EditModel(BOs.Models.OldCarShowroomNetworkContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BOs.Models.Showroom Showroom { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Showrooms == null)
            {
                return NotFound();
            }

            var showroom =  await _context.Showrooms.FirstOrDefaultAsync(m => m.ShowroomId == id);
            if (showroom == null)
            {
                return NotFound();
            }
            Showroom = showroom;
           ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityId");
           ViewData["DistrictId"] = new SelectList(_context.Districts, "DistrictId", "DistrictId");
           ViewData["ImageId"] = new SelectList(_context.ImageShowrooms, "ImageId", "ImageId");
           ViewData["Wards"] = new SelectList(_context.Wards, "WardId", "WardId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Showroom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShowroomExists(Showroom.ShowroomId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ShowroomExists(int id)
        {
          return (_context.Showrooms?.Any(e => e.ShowroomId == id)).GetValueOrDefault();
        }
    }
}
