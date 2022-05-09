using AutoMapper;
using BerrasBio.Data;
using BerrasBio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BerrasBio.Controllers
{
    public class NavbarController : Controller
    {

        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly BerrasBioContext _context;

        public NavbarController(IMapper mapper, UserManager<User> userManager, BerrasBioContext context)
        {
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Menu()
        {
            //If user is in role admin
            if (User.IsInRole("Administrator"))
            {
                return PartialView("_Admin");
            }
            //If user is in role admin
            if (User.IsInRole("Visitor"))
            {
                return PartialView("_User");
            }
            return PartialView("_Default");
        }
    }
}