using Microsoft.AspNetCore.Mvc;
using ApiCrud.Models;

namespace ApiCrud.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly LearnDbContext _DBContext;

        public ProductController(LearnDbContext dBContext)
        {
            this._DBContext = dBContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var product = this._DBContext.Products.ToList();
            return Ok(product);
        }

        [HttpGet("GetByCode/{code}")]
        public IActionResult GetByCode(int code)
        {
            var product = this._DBContext.Products.FirstOrDefault(o => o.Id == code);
            return Ok(product);
        }

        [HttpDelete("Remove/{code}")]
        public IActionResult Remove(int code)
        {
            var product = this._DBContext.Products.FirstOrDefault(o => o.Id == code);
            if (product != null)
            {
                this._DBContext.Remove(product);
                this._DBContext.SaveChanges();
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Product _product)
        {
            var product = this._DBContext.Products.FirstOrDefault(o => o.Id == _product.Id);
            if (product != null)
            {
                product.Name = _product.Name;
                product.Price = _product.Price;
                this._DBContext.SaveChanges();
            }
            else
            {
                this._DBContext.Products.Add(_product);
                this._DBContext.SaveChanges();
            }
            return Ok(true);
        }
    }
}