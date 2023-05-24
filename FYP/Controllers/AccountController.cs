using Encapsulation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models;
using System.Security.Claims;

namespace FYP.Controllers
{
    public class AccountController : Controller
    {
        string folder = "Profile_Pics/";
        private readonly Interface @interface;
        private readonly IHttpContextAccessor httpContext;


        public AccountController(Interface @interface, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContext)
        {

            this.@interface = @interface;
            WebHostEnvironment = webHostEnvironment;
            this.httpContext = httpContext;
   
        }


        public IWebHostEnvironment WebHostEnvironment { get; }

        [Route("/SignIn")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [Route("/SignIn")]
        public async Task<IActionResult> SignIn(SignInModel signIns)
        {
            var modelValid = false;
            if (signIns.Email != null && signIns.password != null) { modelValid = true; }

            if (modelValid)
            {
                var result = await @interface.SignInAsync(signIns);
                if (result.Succeeded)
                {
                    TempData["signin_notification"] = "true";
                    return RedirectToAction("Dashboard", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "SignIn not succeeded .\n Wrong Credentials");
                }
            }

            return View();
        }

        [Route("/SignUp")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [Route("/SignUp")]
        public IActionResult SignUp(SignUpModel signUp)
        {

            bool Modelvalid = false;

            if (signUp.Member_Type == null || signUp.Email == null || signUp.Degree == null || signUp.confirm_password == null || signUp.password == null || signUp.PhoneNo == null || signUp.Name == null || signUp.DOB == null)
            {
                Modelvalid = false;
            }

            else
            {
                Modelvalid = true;
            }
            if (Modelvalid)
            {
                var photoname = AddFile(signUp);

                signUp.Profile_Photo_Path = photoname;

                var result = @interface.signup(signUp);

                if (result.Result.Succeeded)
                {
                    ModelState.Clear();
                    TempData["UserCreated"] = "successfully";
                    return RedirectToAction("SignIn");
                }
                else
                {
                    foreach (var errors in result.Result.Errors)
                    {
                        ModelState.AddModelError("", "Error : " + errors.Description);
                    }
                }
            }

            else
            {
                ModelState.AddModelError("", "SignUp request was not successfull");
            }

            return View();
        }




        [Route("/Profile")]
        public IActionResult Profile()
        {
            return View();
        }

        [Route("/Profile")]
        [HttpPost]
        public async Task<IActionResult> Profile(SignUpModel signUp)
        {
            var userId = GetId();

            if (signUp.Profile_pic != null || signUp.Profile_Photo_Path != null)
            {
           
                if (signUp.Profile_Photo_Path != null)
                {
                    deleteFile(signUp.Profile_Photo_Path);
                }
                var photoName = AddFile(signUp);
                await @interface.AddPhotoAsync(photoName, userId);
            }
            else if (signUp.Name != null && signUp.Gender != null)
            {
                await @interface.BasicInfoUpdateAsync(userId, signUp);
            }
            else if (signUp.Email != null && signUp.PhoneNo != null)
            {
                await @interface.ContactInfoUpdateAsync(userId, signUp);
            }
            else if (signUp.Home_Address != null && signUp.City_Name != null)
            {
                await @interface.AddressInfoUpdateAsync(userId, signUp);
            }
            else if (signUp.Two_step_Verification_Phone != null && signUp.Recovery_Email != null)
            {
                await @interface.SignInInfoUpdateAsync(userId, signUp);
            }
            else
            {
                ModelState.AddModelError("", "No Update function can be runned from Profile");
            return View();
            }
            TempData["NeedsSignInagain"] = "true";
            return RedirectToAction("SignOut");
        }

        [Route("/Setting")]
        public IActionResult Setting()
        {
            return View();
        }
        public bool signout_notification { get; set; }
        public async Task<IActionResult> SignOut()
        {
            await @interface.signout();
            signout_notification = true;
            return RedirectToAction("Dashboard", "Dashboard", new { signout_notification });
        }

       


        //Woring with files

        private string AddFile(SignUpModel signUpModel)
        {
            string filename = null;
            if (signUpModel.Profile_pic != null)
            {

                string folder = "Profile_Pics/";
                filename = (Guid.NewGuid().ToString()) + " " + signUpModel.Profile_pic.FileName;
                string path = folder + filename;

                string serverPath = Path.Combine(WebHostEnvironment.WebRootPath, path);

                signUpModel.Profile_pic.CopyTo(new FileStream(serverPath, FileMode.Create));
            }
            return filename;
        }
        public string GetId()
        {
            return httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        private void deleteFile(string name)
        {
            var oldImage = name;
            string oldPath = folder + oldImage;
            var Old_serverPath = Path.Combine(WebHostEnvironment.WebRootPath, oldPath);
            System.IO.File.Delete(Old_serverPath);
        }
    }
}