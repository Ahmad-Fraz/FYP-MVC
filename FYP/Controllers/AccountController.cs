using Encapsulation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models;

namespace FYP.Controllers
{
    public class AccountController : Controller
    {        

        public AccountController(Interface @interface, IWebHostEnvironment webHostEnvironment)
        {
            Interface = @interface;
            WebHostEnvironment = webHostEnvironment;
        }

        public Interface Interface { get; }
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
                var result =await Interface.SignInAsync(signIns);
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

                var result = Interface.signup(signUp);

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

        [Route("/Profile")]
        public IActionResult Profile()
        {
            return View();
        }

        [Route("/Setting")]
        public IActionResult Setting()
        {
            return View();
        }
        public bool signout_notification { get; set; }
        public async Task<IActionResult> SignOut()
        {
            await Interface.signout();
            signout_notification = true;
            return RedirectToAction("Dashboard", "Dashboard", new {  signout_notification });
        }
    }
}