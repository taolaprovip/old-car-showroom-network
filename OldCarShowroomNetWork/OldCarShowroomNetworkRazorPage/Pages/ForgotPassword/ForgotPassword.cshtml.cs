using BOs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using REPOs;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OldCarShowroomNetworkRazorPage.Pages.ForgotPassword
{
    public class ForgotPasswordModel : PageModel
    {
        //        public readonly UserRepository _userRepo;

        //        public ForgotPasswordModel(UserRepository userRepo)
        //        {
        //            _userRepo = userRepo;
        //        }
        //        public class InputModel
        //        {
        //            [Required]
        //            [EmailAddress]
        //            [Display(Name = "Nhập chính xác địa chỉ email")]
        //            public string Email { get; set; }
        //        }

        //        [BindProperty]
        //        public User User { get; set; }
        //        public async Task<IActionResult> OnPostAsync()
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                // Tìm user theo email gửi đến
        //                var user =  _userRepo.GetAll().FirstOrDefault(x => x.Email.Equals(User.Email));
        //                if (user == null)
        //                {
        //                    return RedirectToPage("./ForgotPasswordConfirmation");
        //                }

        //                // Phát sinh Token để reset password
        //                // Token sẽ được kèm vào link trong email,
        //                // link dẫn đến trang /Account/ResetPassword để kiểm tra và đặt lại mật khẩu
        //                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        //                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        //                var callbackUrl = Url.Page(
        //                    "/Account/ResetPassword",
        //                    pageHandler: null,
        //                    values: new { area = "Identity", code },
        //                    protocol: Request.Scheme);

        //                // Gửi email
        //                await _emailSender.SendEmailAsync(
        //                    Input.Email,
        //                    "Đặt lại mật khẩu",
        //                    $"Để đặt lại mật khẩu hãy <a href='{callbackUrl}'>bấm vào đây</a>.");

        //                // Chuyển đến trang thông báo đã gửi mail để reset password
        //                return RedirectToPage("./ForgotPasswordConfirmation");
        //            }

        //            return Page();
        //        }
    }
}
