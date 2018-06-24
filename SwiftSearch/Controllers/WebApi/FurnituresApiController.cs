using SwiftSearch.Data;
using SwiftSearch.Interfaces;
using SwiftSearch.Models;
using SwiftSearch.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SwiftSearch.Controllers.WebApi
{
    public class FurnituresApiController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FurnituresApiController()
        {
            _unitOfWork = new UnitOfWork(new SwiftSearchDbContext());
        }
        // GET: api/Furnitures
        [Route("api/furnitures")]
        public async Task<IHttpActionResult> Get()
        {
            var items = await _unitOfWork.Furniture.GetAllDataAsync();
            return Ok(items);
        }

        // GET: api/Furnitures/5
        [Route("api/furnitures/{id}")]
        public async Task<IHttpActionResult> Get(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var item = await _unitOfWork.Furniture.FindAsync(id);
            return Ok(item);
        }

        // POST: api/Furnitures
        [HttpPost, Route("api/furnitures")]
        public IHttpActionResult Post([FromBody]Furniture furniture )
        {
            try
            {
               
                _unitOfWork.Furniture.Insert(furniture);
                _unitOfWork.Complete();
                return Created(Request.RequestUri + "/" + furniture.ID, furniture);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        // PUT: api/Furnitures/5
        [HttpPut, Route("api/furnitures")]
        public IHttpActionResult Put(int? id, [FromBody]Furniture furniture)
        {
            try
            {
                if(id == null)
                {
                    return NotFound();
                }
                if(furniture == null)
                {
                    return BadRequest();
                }

                _unitOfWork.Furniture.Update(furniture);
                _unitOfWork.Complete();
                return Ok(furniture);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Furnitures/5
        [HttpDelete, Route("api/furnitures/{id}")]
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if(id == null)
                {
                    return NotFound();
                }
                _unitOfWork.Furniture.Delete(id);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
