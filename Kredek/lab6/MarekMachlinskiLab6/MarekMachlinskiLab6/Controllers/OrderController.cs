using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MarekMachlinskiLab6.Controllers
{
    /// <summary>
    /// Kontroler do obsługi bazy danych
    /// </summary>
    public class OrderController : ApiController
    {
        /// <summary>
        /// Zwraca listę zleceń
        /// </summary>
        /// <returns></returns>
        // GET api/<controller>
        public IEnumerable<Dto.OrderDto> Get()
        {
            //Połączenie z bazą
            using (var context = new Database.DataContext())
            {
                var orders = context.Order.ToList();
                return orders.Select(order => new Dto.OrderDto() { OrderId = order.OrderId, FirstName = order.FirstName, LastName = order.LastName, Email = order.Email }).ToList();
            }
        }

        /// <summary>
        /// Nic nie robi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Nic nie robi
        /// </summary>
        /// <param name="value"></param>
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// Nic nie robi
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// Nic nie robi
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}