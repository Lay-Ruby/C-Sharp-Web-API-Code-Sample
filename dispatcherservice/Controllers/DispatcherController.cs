using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DispatcherDataAccess;

namespace dispatcherservice.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class DispatcherController : ApiController
    {
        /// <summary>
        /// Returns all Fuel Delivery Event records. 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<fuel_delivery_event> Get()
        {
            using (efc_testEntities1 entities = new efc_testEntities1())
            {
                return entities.fuel_delivery_event.ToList();
            }
        }

        /// <summary>
        /// Returns the Fuel Delivery Event record for the specified pkey. 
        /// </summary>
        /// <param name="pkey"></param>
        /// <returns></returns>
        public fuel_delivery_event Get(int pkey)
        {
            using (efc_testEntities1 entities = new efc_testEntities1())
            {
                return entities.fuel_delivery_event.FirstOrDefault(e => e.pkey == pkey);
            }
        }

        /// <summary>
        /// Updates an existing Fuel Delivery Event record.
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="fuel_Delivery_Event"></param>
        public HttpResponseMessage Put(int pkey, [FromBody]fuel_delivery_event fuel_Delivery_Event)
        {
            try
            {
                using (efc_testEntities1 entities = new efc_testEntities1())
                {
                    var entity = entities.fuel_delivery_event.FirstOrDefault(e => e.pkey == pkey);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "A record with pkey " + pkey.ToString() + " was not found.");
                    }
                    else
                    {
                        entity.current_stop = fuel_Delivery_Event.current_stop;
                        entity.next_stop = fuel_Delivery_Event.next_stop;
                        entity.current_fuel_level = fuel_Delivery_Event.current_fuel_level;

                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// Posts a new Fuel Delivery Event Record
        /// </summary>
        /// <param name="fuel_Delivery_Event"></param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody] fuel_delivery_event fuel_Delivery_Event)
        {
            try
            {
                using (efc_testEntities1 entities = new efc_testEntities1())
                {
                    entities.fuel_delivery_event.Add(fuel_Delivery_Event);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, fuel_Delivery_Event);
                    message.Headers.Location = new Uri(Request.RequestUri + fuel_Delivery_Event.pkey.ToString());
                    return message;
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}