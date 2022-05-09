using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BerrasBio.Models;
using BerrasBio.ViewModels;
using Microsoft.AspNetCore.Authorization;
using BerrasBio.Data;
using Microsoft.EntityFrameworkCore;

namespace BerrasBio.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly BerrasBioContext _context;

        public UserController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, BerrasBioContext context)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }


            var user = _mapper.Map<User>(userModel);
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(userModel);
            }

            //First user created is admin
            if (_context.Users.Count() == 1)
            {
                await _userManager.AddToRoleAsync(user, "Administrator");
            }
            else 
            {
                await _userManager.AddToRoleAsync(user, "Visitor");
            }
            return RedirectToAction(nameof(StartController.Index), "Start");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel userModel)
        {
            if (ModelState.IsValid)
            {
                var identityResult = await _signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, userModel.RememberMe, false);
                if (identityResult.Succeeded)
                {
                    //return to returnurl
                    //Return to start page
                    return RedirectToAction(nameof(StartController.Index), "Start");


                }
                ModelState.AddModelError("", "Fel uppgifter");
            }

            return PartialView();
        }

        [HttpPost]
        public  async Task<IActionResult> Logout()
        {
            //if user is logged in
            if (_signInManager.IsSignedIn(User))
            {
                await _signInManager.SignOutAsync();
            }

            return PartialView("_LoginPartial", "Shared");
        }
        public async Task<IActionResult> Privacy()
        {
            if (!_signInManager.IsSignedIn(User)) return RedirectToAction(nameof(StartController.Index), "Start");


            // Get user
            var user = await _userManager.
                GetUserAsync(User);


            user.Bookings = _context.Booking.Where(b => b.UserId == user.Id)
                .Include(b => b.Show)
                    .ThenInclude(s => s.Movie)
                .Include(s => s.Show)
                    .ThenInclude(s => s.Saloon)
                .Include(b => b.Booked_Seats)
                .ToList();


            return View(user);
            
        }

    }
}
