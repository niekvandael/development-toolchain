using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GetStartedDotnet.Models;
using System.Linq;
using System.Text.Encodings.Web;

namespace GreenLiving.Controllers
{
 [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly HtmlEncoder _htmlEncoder;
        private readonly GreenLivingDbContext _dbContext;

        public ProductsController(HtmlEncoder htmlEncoder, GreenLivingDbContext dbContext = null)
        {
            _dbContext = dbContext;
            _htmlEncoder = htmlEncoder;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            if (_dbContext == null)
            {
                return null;
            }
            else
            {
                return _dbContext.Set<Product>();
            }
        }
 
        // GET api/values/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            if (_dbContext == null)
            {
                return null;
            }
            else
            {
                return _dbContext.Products.Where(i=>i.productId==id).FirstOrDefault();
            }
        }
 
        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
 
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
 
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}