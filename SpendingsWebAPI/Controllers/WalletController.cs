using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using DTOLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpendingsWebAPI.DAL.Abstraction;
using SpendingsWebAPI.Entities;

namespace SpendingsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IRepos<Spending> _spendingRepos;
        private readonly WalletContext db;
        public WalletController(IRepos<Spending> spendingRepos,
                                WalletContext _db)
        {
            _spendingRepos = spendingRepos;
            db = _db;
        }

        
        // api/wallet/6
        [Authorize]
        [HttpGet]
        public List<SpendingDto> Get()
        {
            var spendings = _spendingRepos.GetAll()
                .Include(x => x.Category)
                .Include(x => x.Tags);

            var claim = HttpContext.User.Claims;
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = spendings
                .Where(x=>x.UserId == userId)
                .Select(x => new SpendingDto
                {
                Description = x.Description,
                Value = x.Value,
                Date = x.Date,
                Id = x.Id,
                Category = x.Category.Name,
                Tags = x.Tags.Select(t => t.Tag.Name).ToList()
            }).ToList();

            return result;
        }

        //[HttpGet]
        //public List<SpendingDto> Get()
        //{
        //    var spendings = _spendingRepos.GetAll()
        //        .Include(x => x.Category)
        //        .Include(x => x.Tags);

        //    var result = spendings.Select(x => new SpendingDto
        //    {
        //        Description = x.Description,
        //        Value = x.Value,
        //        Date = x.Date,
        //        Id = x.Id,
        //        Category = x.Category.Name,
        //        Tags = x.Tags.Select(t => t.Tag.Name).ToList()
        //    }).ToList();

        //    Thread.Sleep(5000);

        //    return result;
        //}

        [HttpGet("[action]/{tag}")]
        public List<SpendingDto> GetByTag(string tag)//tag = "Coffee"
        {


            //Service Method
            var spendings = _spendingRepos.GetAll()
                .Include(x => x.Category)
                .Include(x => x.Tags).
                Where(x => x.Tags.Any(t => t.Tag.Name == tag));

            var result = spendings.Select(x => new SpendingDto
            {
                Description = x.Description,
                Value = x.Value,
                Date = x.Date,
                Id = x.Id,
                Category = x.Category.Name,
                Tags = x.Tags.Select(t => t.Tag.Name).ToList()
            }).ToList();
            //
            return result;
        }

        //[HttpGet("{id}")]
        //public SpendingDto Get(int id)
        //{
        //    var spending = db.Spendings
        //        .Include(x => x.Category)
        //        .Include(x => x.Tags)
        //        .Select(x => new SpendingDto
        //        {
        //            Description = x.Description,
        //            Value = x.Value,
        //            Date = x.Date,
        //            Id = x.Id,
        //            Category = x.Category.Name,
        //            Tags = x.Tags.Select(t => t.Tag.Name).ToList()
        //        }).FirstOrDefault(x => x.Id == id);

        //    if (spending==null)
        //    {
        //        throw new Exception("Not exists");
        //    }
        //    return spending;
        //}

        [Authorize]
        [HttpPost("[action]")]
        public void AddSpending(SpendingDto model)
        {
            //var cat = db.Categories.FirstOrDefault(x => x.Name == model.Category) ?? new Category { Name = model.Category };

            var category = db.Categories.FirstOrDefault(x => x.Name == model.Category);
            if (category == null)
            {
                category = new Category() { Name = model.Category };
                db.Categories.Add(category);
            }

            var spending = new Spending
            {
                Category = category,
                Value = model.Value,
                Date = model.Date,
                Description = model.Description,
                Tags = new List<SpendingTag>()
            };

            foreach (var t in model.Tags)
            {
                var tag = db.Tags.FirstOrDefault(x => x.Name == t);
                if (tag == null)
                {
                    tag = new Tag() { Name = t };
                }
                spending.Tags.Add(new SpendingTag { Tag = tag });
            }

            // added user

            //var user = db.Users.FirstOrDefault(x => x.Id == model.UserId);
            //spending.User = user;

            var claims = HttpContext.User.Claims;

            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            spending.UserId = userId;

            //

            db.Spendings.Add(spending);

            db.SaveChanges();
        }


        //[HttpPost]
        //public void AddCategory(string name)
        //{
        //    var category = new Category()
        //    {
        //        Name = name
        //    };
        //    db.Categories.Add(category);
        //    db.SaveChanges();
        //}
    }
}
