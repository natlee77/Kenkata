using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KenkataWebApi.Data;
using KenkataWebApi.Entities;
using SharedLibrary.Models;

namespace KenkataWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly KenkataDbContext _context;

        public OrdersController(KenkataDbContext context)
        {
            _context = context;           
        }



        [HttpPost("create")]   // POST: api/Orders/create -----------> To place an order with ShoppingCarModel
        public async Task<ActionResult<Order>> CreateOrder([FromBody] List<ShoppingCartModel> cartList)
        {
            Order currentOrder = new Order()
            {
                CustomerId = cartList[0].CustomerId,
                OrderDate = DateTime.Now,

                OrderDetails = (from item in cartList
                                select new OrderDetail()
                                {
                                    Price = item.Price,
                                    ProductId = item.ProductId,
                                    Quantity = item.QuantityByUser

                                }).ToList()
            };
            _context.Orders.Add(currentOrder);

            await _context.SaveChangesAsync();

            return new OkResult();
        }




        [HttpGet]   // GET: api/Orders   To list all orders in the database(this function is just for Administrator)
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetOrders()
        {
            var orderList = await _context.Orders.Include(o => o.OrderDetails).ToListAsync();

            IList<OrderModel> orderModel = new List<OrderModel>();
            foreach (var order in orderList)
            {
                orderModel.Add(new OrderModel
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    OrderDate = order.OrderDate,
                    OrderDetails = (from item in order.OrderDetails select new OrderDetailModel()
                    {
                        Id = item.Id,
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Price = item.Price,
                        Quantity = item.Quantity
                    }).ToList()
                });
            }
            return Ok(orderModel.AsEnumerable());
        }



        [HttpGet("{id}")]   // GET: api/Orders/5  To list all orders of a customer by customerId
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetOrder(int id)
        {
            var orderList = await _context.Orders.Include(o => o.OrderDetails).Where(o => o.CustomerId == id).ToListAsync();

            IList<OrderModel> orderModel = new List<OrderModel>();
            foreach (var order in orderList)
            {
                orderModel.Add(new OrderModel
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    OrderDate = order.OrderDate,
                    OrderDetails = (from item in order.OrderDetails
                                    select new OrderDetailModel()
                                    {
                                        Id = item.Id,
                                        OrderId = order.Id,
                                        ProductId = item.ProductId,
                                        Price = item.Price,
                                        Quantity = item.Quantity
                                    }).ToList()
                });
            }
            if (orderModel == null)
            {
                return NotFound();
            }

            return Ok(orderModel.AsEnumerable());
        }



        
        [HttpPut("{id}")]  // PUT: api/Orders/5
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        
        [HttpDelete("{id}")]  // DELETE: api/Orders/5
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }


        // ***************** an alternative function to post/place an order **********************
        //[HttpPost("create")]    // POST: api/Orders/create -----------> To place an order with OrderPlacementModel
        //public async Task<ActionResult> CreateOrder([FromBody] OrderPlacementModel model)
        //{
        //    var orderDate = DateTime.Now;
        //    var newOrder = new Order()
        //    {
        //        CustomerId = model.CustomerId,
        //        OrderDate = orderDate,
        //        OrderDetails = (from item in model.Products
        //                        select new OrderDetail()
        //                        {
        //                            ProductId = item.ProductId,
        //                            Price = item.Price,
        //                            Quantity = item.Quantity
        //                        }).ToList()
        //    };
        //    _context.Orders.Add(newOrder);
        //    await _context.SaveChangesAsync();

        //var currentOrder = await _context.Orders.FirstOrDefaultAsync(x => x.CustomerId == model.CustomerId && x.OrderDate == orderDate);
        //foreach (var item in model.Products)
        //{
        //    _context.OrderDetails.Add(new OrderDetail { OrderId = currentOrder.Id, ProductId = item.ProductId, Quantity = item.Quantity });               
        //}
        //await _context.SaveChangesAsync();

        //    return new OkResult();
        //}

    }
}
