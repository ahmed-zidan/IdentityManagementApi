using Api.Dtos;
using Api.Errors;
using Api.Helpers;
using AutoMapper;
using Core.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace Api.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJWTService _jWTService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager,
            IJWTService jWTService, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager; 
            _jWTService = jWTService;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserInfoDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) { 
                return Unauthorized(new ApiResponse(404));
            }
            if (user.EmailConfirmed == false) { 
                return Unauthorized(new ApiResponse(400,"Please confirm your email"));
            }
            var res = await _signInManager.CheckPasswordSignInAsync(user, model.Password,false);
            if (!res.Succeeded) {
                return Unauthorized(new ApiResponse(404));
            }
            return Ok(getUserInfo(user));
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDto model)
        {
            if (await isEmailExists(model.Email))
            {
                return BadRequest(new ApiResponse(400, "Email already exist"));
            }
            var user = _mapper.Map<AppUser>(model);
            var res = await _userManager.CreateAsync(user,model.Password);
            if (!res.Succeeded)
            {
                return BadRequest(new ApiResponse(400 , string.Join(",",res.Errors)));
            }
            return Ok();
        }

        private UserInfoDto getUserInfo(AppUser user)
        {
            return new UserInfoDto()
            {
                Token =  _jWTService.CreateJWT(user),
                UserId = user.Id,
                Username = user.UserName
            };
        }

        private async Task<bool> isEmailExists(string email)
        {
            return await _userManager.Users.AnyAsync(x=>x.Email.ToLower() == email.ToLower());
        }

    }
}
