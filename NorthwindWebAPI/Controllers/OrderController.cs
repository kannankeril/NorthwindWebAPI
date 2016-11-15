using System.Net;
using System.Net.Http;
using System.Web.Http;

using NorthwindWebAPI.Models;
using NorthwindWebAPI.Services;

namespace NorthwindWebAPI.Controllers
{
    public class OrderController : ApiController
    {
        private OrderService _orderService;
        public OrderController()
        {
            _orderService = new OrderService();
        }
        // GET api/order
        public HttpResponseMessage Get()
        {
            var orders = _orderService.Get();
            if (orders != null)
                return Request.CreateResponse(HttpStatusCode.OK, orders);
            else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No orders found");
        }

        // GET api/order/10248
        public HttpResponseMessage Get(int id)
        {
            var order = _orderService.Get(id);
            if (order != null)
                return Request.CreateResponse(HttpStatusCode.OK, order);
            else return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                                            "Order #" + id.ToString() + " does not exist");
        }

        // POST api/order
        public HttpResponseMessage Post([FromBody]Order order)
        {
            _orderService.Add(order);
            return Request.CreateResponse(HttpStatusCode.Created, order);
        }

        // PUT api/order/10248
        public HttpResponseMessage Put([FromBody]Order order)
        {
            _orderService.Update(order);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/order/10248
        public HttpResponseMessage Delete(int id)
        {
            _orderService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
