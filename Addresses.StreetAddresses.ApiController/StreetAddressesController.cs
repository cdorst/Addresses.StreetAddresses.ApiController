// Copyright © Christopher Dorst. All rights reserved.
// Licensed under the GNU General Public License, Version 3.0. See the LICENSE document in the repository root for license information.

using Addresses.StreetAddresses.DatabaseContext;
using DevOps.Code.DataAccess.Interfaces.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Addresses.StreetAddresses.ApiController
{
    /// <summary>ASP.NET Core web API controller for StreetAddress entities</summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StreetAddressesController : ControllerBase
    {
        /// <summary>Represents the application events logger</summary>
        private readonly ILogger<StreetAddressesController> _logger;

        /// <summary>Represents repository of StreetAddress entity data</summary>
        private readonly IRepository<StreetAddressDbContext, StreetAddress, int> _repository;

        /// <summary>Constructs an API controller for StreetAddress entities using the given repository service</summary>
        public StreetAddressesController(ILogger<StreetAddressesController> logger, IRepository<StreetAddressDbContext, StreetAddress, int> repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>Handles HTTP GET requests to access StreetAddress resources at the given ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<StreetAddress>> Get(int id)
        {
            if (id < 1) return NotFound();
            var resource = await _repository.FindAsync(id);
            if (resource == null) return NotFound();
            return resource;
        }

        /// <summary>Handles HTTP HEAD requests to access StreetAddress resources at the given ID</summary>
        [HttpHead("{id}")]
        public ActionResult<StreetAddress> Head(int id) => null;

        /// <summary>Handles HTTP POST requests to save StreetAddress resources</summary>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<StreetAddress>> Post(StreetAddress resource)
        {
            var saved = await _repository.AddAsync(resource);
            return CreatedAtAction(nameof(Get), new { id = saved.GetKey() }, saved);
        }
    }
}
