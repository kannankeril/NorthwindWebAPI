using System.Collections.Generic;
using System.Linq;

using NorthwindWebAPI.Data;
using NorthwindWebAPI.Models;

namespace NorthwindWebAPI.Services
{
    public class OrderService
    {
        private OrderRepository _orderRepository;
        private OrderDetailRepository _orderDetailRepository;
        private CustomerRepository _customerRepository;
        private EmployeeRepository _employeeRepository;

        private static List<Order> _orderList;  //complete list of orders
        //private static List<Order> _orderListWithDetails; //partial list of orders with full details
        private static bool _orderListStale = true;

        public OrderService()
        {
            _orderRepository = new OrderRepository();
            _orderDetailRepository = new OrderDetailRepository();
            _customerRepository = new CustomerRepository();
            _employeeRepository = new EmployeeRepository();
        }

        /// <summary>
        /// Gets all orders with names of customer, customer contact and employee.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> Get()
        {
            if(_orderList == null || _orderListStale)
            {
                _orderList = _orderRepository.Get().ToList();
                _orderListStale = false;
            }

            return _orderList;
        }

        /// <summary>
        /// Get an order with related customer, employee & order details
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IEnumerable<Order> Get(int orderId)
        {
            List<Order> orderList = null;
            if(_orderList != null && !_orderListStale)
            {
                ///look for order within cached order list
                orderList = _orderList.Where(ord => ord.OrderID == orderId).ToList();
            }

            if(orderList == null)
            {
                orderList = _orderRepository.Get(orderId).ToList();
                if (orderList == null)
                    return orderList;
            }

            ///get details for this order
            foreach (var order in orderList)
                order.Order_Details = _orderDetailRepository.Get(order.OrderID).ToList();

            return orderList;
        }

        /// <summary>
        /// Adds a new order including order details.
        /// New products, customers & employees are NOT added. It is assumed all references to 
        ///     producte, customers & employees refer to existing instances.
        /// </summary>
        /// <param name="order"></param>
        public void Add(Order order)
        {
            _orderRepository.Add(order);
        }

        public void Update(Order order)
        {
            _orderRepository.Update(order);
        }

        public void Delete(int orderId)
        {
            _orderRepository.Delete(orderId);
        }
    }
}
