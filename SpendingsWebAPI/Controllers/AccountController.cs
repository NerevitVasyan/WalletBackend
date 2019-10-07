using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTOLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SpendingsWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly WalletContext db;
        public AccountController(WalletContext _db)
        {
            db = _db;
        }

        [HttpPost]
        public async Task<ApiResult<bool>> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResult<bool>
                {
                    Message = "Something wrong",
                    Data = false,
                    Successful = false
                };
            }

            db.Users.Add(new Entities.User
            {
                Email = model.Email,
                Password = model.Password,
                UserName = model.Username
            });
            await db.SaveChangesAsync();
            return new ApiResult<bool>
            {
                Message="Successfully registered",
                Data = true,
                Successful = true
            };
        }

        [HttpPost]
        public ApiResult<int> Login(LoginDto model)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == model.Email);

            if(user == null || user.Password != model.Password)
            {
                return new ApiResult<int>
                {
                    Message = "Incorrect password or email",
                    Data = -1,
                    Successful = false
                };
            }

            return new ApiResult<int>
            {
                Message = "Login Successful",
                Data = user.Id,
                Successful = true
            };
        }
    }

    public class ApiResult<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
        public bool Successful { get; set; }
    }
}