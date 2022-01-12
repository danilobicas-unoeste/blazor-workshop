using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazingPizza.Server
{
    [ApiController]
    [Authorize]
    [Route("cashback")]
    public class CashBackController : Controller
    {
        private readonly PizzaStoreContext _db;

        public CashBackController(PizzaStoreContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IEnumerable<CashBack>> Get()
        {
            var cashsbyuser = _db.CashBacks.Where(c => c.UserId == GetUserId());
            var todoscashbacks =  _db.CashBacks;
            return todoscashbacks;
        }

        private string GetUserId()
        {
            return HttpContext.User.Identity.Name;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(CashBack cashback)
        {
            var usuario = _db.Users.Where(u => u.UserName == GetUserId()).FirstOrDefault();
            var usuarios = _db.Users.ToList();
            if (usuario == null)
            {
                usuario = new PizzaStoreUser();
                usuario.Id = "49311379-8bad-4087-9e85-549e11f90451";
            }
            cashback.UserId = usuario.Id;
            _db.CashBacks.Add(cashback);
            _db.SaveChanges();
            return cashback.Id;
        }
    }
}
