using Marvin.JsonPatch;
using SwiftSearch.Data;
using SwiftSearch.Interfaces;
using SwiftSearch.Models;
using SwiftSearch.Repository;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SwiftSearch.Controllers.WebApi
{
    public class VehiclesApiController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public VehiclesApiController()
        {
            _unitOfWork = new UnitOfWork(new SwiftSearchDbContext());
        }
        // GET: api/Vehicles
        [HttpGet, Route("api/vehicles")]
       public async Task<IHttpActionResult> Get()
        {
            try
            {
                var list = await _unitOfWork.Vehicle.GetAllDataAsync();
                return Ok(list);
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }

        // GET: api/Vehicles/5
        [HttpGet, Route("api/vehicles/{id}")]
        public async Task<IHttpActionResult> Get(int? id)
        {
            try
            {
                if(id == null)
                {
                    return NotFound();
                }
                var item = await _unitOfWork.Vehicle.FindAsync(id);
                if(item == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(item);
                }
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }

        // POST: api/Vehicles
        [HttpPost, Route("api/vehicles")]
        public IHttpActionResult Post([FromBody] Vehicle vehicle)
        {
            try
            {
                if(vehicle == null)
                {
                    return BadRequest();
                }
                
                _unitOfWork.Vehicle.Insert(vehicle);
                _unitOfWork.Complete();

                return Created(Request.RequestUri+ "/" + vehicle.ID, vehicle);
            }
            catch (Exception)
            {

                return InternalServerError();
            }

        }

        // PUT: api/Vehicles/5
        [HttpPut, Route("api/vehicles/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int? id, Vehicle vehicle)
        {
           
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                _unitOfWork.Vehicle.Update(vehicle);
                _unitOfWork.Complete();

                return Ok(vehicle);
            }

            
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Vehicles/5
        [HttpDelete, Route("api/vehicles/{id}")]
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                _unitOfWork.Vehicle.Delete(id);
                _unitOfWork.Complete();
                return Ok("Vehicle Deleted Successfully");
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
