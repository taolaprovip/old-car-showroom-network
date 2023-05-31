﻿using BOs.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using REPOs;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Protocol.Plugins;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace OldCarShowroomNetworkRazorPage.Pages
{
    public class LoginModel : PageModel
    {
        public readonly UserRepository _userRepo;
        public LoginModel(UserRepository userRepository)
        {
            _userRepo = userRepository;
        }
        public string Msg1 { get; set; }
        public string Msg2 { get; set; }

        [BindProperty]
        public string Key { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

            if (Key == null)
            {
                Msg1 = "Nhập tài khoản";
                return Page();
            }

            if (Password == null)
            {
                Msg2 = "Nhập Mật khẩu";
                return Page();
            }
            var checkLoginByEmail = _userRepo.GetAll().FirstOrDefault(p => p.Email.Equals(Key) && p.Password.Equals(Password));
            //var checkLoginByPhone = _userRepo.GetAll().FirstOrDefault(p => p.Phone.Equals(Key) && p.Password.Equals(Password));
            //var checkLoginByUserName = _userRepo.GetAll().FirstOrDefault(p => p.Username.Equals(Key) && p.Password.Equals(Password));

            if (checkLoginByEmail == null)
            {
                Msg1 = "Tài khoản không tồn tại";
                return Page();
            }
            HttpContext.Session.SetString("Key", Key);
            var scheme = CookieAuthenticationDefaults.AuthenticationScheme;

            var User = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[] { new Claim(ClaimTypes.Name, Key) },
                    scheme
                    ));

            return RedirectToPage("./Home");
        }

        public async Task Login()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
            return (IActionResult)claims;
        }
    }
}
