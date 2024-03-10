
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TicketHive.Server.Models;

namespace TicketHive.Server.Areas.Identity.Pages.Account
{

    [BindProperties]
    public class LogInModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        [Required(ErrorMessage = "Please enter a username")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Please enter a password")]
        public string? Password { get; set; }

        public LogInModel(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        /// <summary>
        /// Handles the post request for user authentication
        /// </summary>
        public async Task<IActionResult> OnPost()
        {
           

            if (ModelState.IsValid)
            {
                

                var signedInResult = await signInManager.PasswordSignInAsync(Username!, Password!, false, false);

                if (signedInResult.Succeeded)
                {
                    if (Username == "admin")
                    {
                        return Redirect("/admin");
                    }
                        return Redirect("/home");
                   
                }
                


                else if (signedInResult.Succeeded == false)
                {
                    // Printa ut att l�senordet inte matchar
                    ModelState.AddModelError("Password", "Your Username or Password are incorrect");
                }
            }
            
            return Page();

        }

    }

}
