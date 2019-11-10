using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DTOLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SpendingsWebAPI.Entities;

namespace SpendingsWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly WalletContext db;
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(WalletContext _db,
                                 IConfiguration _configuration,
                                 UserManager<User> _userManager,
                                 SignInManager<User> _signInManager)
        {
            db = _db;
            configuration = _configuration;
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [HttpPost]
        public async Task<ResultDto> Login([FromBody] LoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, model.IsRemember,false);
                if (result.Succeeded)
                {
                    //await signInManager.SignInAsync(user, model.IsRemember);
                    var token = GenerateJwtToken(user.Id, user.Email);
                    return new SingleResultDto<string>
                    {
                        Successful = true,
                        Data = token
                    };
                }
                throw new Exception("Wrong password");
                
            }
            throw new Exception("User not found");
        }

        [HttpPost]
        public async Task<ResultDto> Register([FromBody] RegisterDto model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.Username
            };

            var res = await userManager.CreateAsync(user, model.Password);

            if (res.Succeeded)
            {
                return new ResultDto
                {
                    Message = "Succeeded",
                    Successful = true
                };
            }

            string errors = "";
            foreach(var err in res.Errors)
            {
                errors += err.Description + "\n";
            }

            return new ResultDto
            {
                Successful = false,
                Message = errors
            };
        }

        private string GenerateJwtToken(string id,string email)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, id)
                //add role
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                configuration["JwtIssuer"],
                configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //[HttpPost]
        //public async Task<ApiResult<bool>> Register(RegisterDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return new ApiResult<bool>
        //        {
        //            Message = "Something wrong",
        //            Data = false,
        //            Successful = false
        //        };
        //    }

        //    db.Users.Add(new Entities.User
        //    {
        //        Email = model.Email,
        //        Password = model.Password,
        //        UserName = model.Username
        //    });
        //    await db.SaveChangesAsync();
        //    return new ApiResult<bool>
        //    {
        //        Message="Successfully registered",
        //        Data = true,
        //        Successful = true
        //    };
        //}

        //[HttpPost]
        //public ApiResult<int> Login(LoginDto model)
        //{
        //    var user = db.Users.FirstOrDefault(x => x.Email == model.Email);

        //    if(user == null || user.Password != model.Password)
        //    {
        //        return new ApiResult<int>
        //        {
        //            Message = "Incorrect password or email",
        //            Data = -1,
        //            Successful = false
        //        };
        //    }

        //    return new ApiResult<int>
        //    {
        //        Message = "Login Successful",
        //        Data = user.Id,
        //        Successful = true
        //    };
        //}
    }

    public class ResultDto
    {
        public string Message { get; set; }
        public bool Successful { get; set; }
    }

    public class SingleResultDto<T> : ResultDto
    {
        public T Data { get; set; }
    }

    public class CollectionResultDto<T> : ResultDto
    {
        public ICollection<T> Data { get; set; }
        public int Count { get; set; }
    }
}