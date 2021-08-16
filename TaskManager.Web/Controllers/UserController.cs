using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManager.Domain.Entites;
using TaskManager.Services.Exceptions;
using TaskManager.Services.UiServices;
using TaskManager.Services.Users;
using TaskManager.Services.Users.Data;

namespace TaskManager.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UiUserService _uiSerice;
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;

        public UserController(UiUserService uiSerice, IUserService userService, SignInManager<User> signInManager)
        {
            _uiSerice = uiSerice;
            _userService = userService;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            var viewModel =  _uiSerice.GetOrgProfileCreateModel();
            return View(viewModel);
        }

        [HttpPost] 
        public async Task<IActionResult> SignUp(OrgProfileCreateData request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userService.CreateOrgProfile(request);
                    await _signInManager.PasswordSignInAsync(user.UserName, request.Password, true, false);
                    return RedirectToAction("OrgProfile","Organization", new {id=user.OrganizationId });
                }
                catch (BadRequestException ex)
                {
                    ModelState.AddModelError(ex.FieldName, ex.Message);
                }

            }
            return View(request);
        }




        [HttpGet]
        [Authorize(Policy = "Can create ordinary user")]
        public IActionResult Create()
        {
            var model = _uiSerice.GetOrdinaryUserCreateModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "Can create ordinary user")]
        public IActionResult Create(OrdinaryUserCreateData request) {

            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userService.CreateOrdinaryUser(request);
                    var successViewModel = _uiSerice.GetUserDetailsModel(user);
                    return View("Details", successViewModel);
                }
                catch (BadRequestException ex)
                {
                    ModelState.AddModelError(ex.FieldName, ex.Message);
                }
            }

            return View(request);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }
            return View(new LoginData());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginData model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userService.GetUserForLogin(model);
                    var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, false);

                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Details", new { id = user.Id });
                        }
                    }
                }
                catch (BadRequestException ex)
                {
                    ModelState.AddModelError(ex.FieldName, ex.Message);
                }
            }
            return View(model);
        }

        public IActionResult Details(string id)
        {
            var user = _userService.GetUserForDetails(id);
            var model = _uiSerice.GetUserDetailsModel(user);
            return View(model);
        }

        [HttpPost] 
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied() {
            ViewData["ErrorMessage"] = "Access denied";
            return View();
        }
    }
}
