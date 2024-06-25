using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt.SharedKernel.Dto.User;
using Projekt.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Projekt.SharedKernel.Dto.Review;

namespace Projekt.Web.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<CreateUserDto> _logger;
        private readonly IValidator<CreateUserDto> _validator;

        public UserController(IUserService userService, ILogger<CreateUserDto> logger, IValidator<CreateUserDto> validator)
        {
            _userService = userService;
            _logger = logger;
            _validator = validator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> Get()
        {
            _logger.LogDebug("Rozpoczęto pobieranie listy wszystkich użytkownikó");
            var result = _userService.GetAll();
            _logger.LogDebug("Zakończono pobieranie listy wszystkich użytkowników");
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDto> Get(int id)
        {
            var result = _userService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CreateUserDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data received for user creation.");
            }

            int id;
            try
            {
                id = _userService.Create(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var routeValues = new { id };
            return CreatedAtAction(nameof(Get), routeValues, null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] UpdateUserDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("Mismatch between ID in URL and ID in request body.");
            }

            try
            {
                _userService.Update(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            try
            {
                _userService.Delete(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}
