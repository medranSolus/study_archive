using System.Collections.Generic;
using System.Web.Http;

namespace MarekMachlinskiLab6.Controllers
{
    /// <summary>
    /// Przykładowy kontroler
    /// </summary>
    public class ValuesController : ApiController
    {
        /// <summary>
        /// Zwraca dwie wartości przykładowe
        /// </summary>
        /// <returns></returns>
        // GET api/values
        public IEnumerable<string> Get()
        {
            var headerValue = Request.Headers.GetValues("From");
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Nic nie robi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Nic nie robi
        /// </summary>
        /// <param name="value"></param>
        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// Nic nie robi
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT api/values
        public void Put([FromBody]string value)
        {
        }

        /// <summary>
        /// Nic nie robi
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// Nic nie robi
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
