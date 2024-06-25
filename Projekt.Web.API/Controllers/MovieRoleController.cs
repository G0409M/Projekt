using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt.SharedKernel.Dto.MovieRole;
using Projekt.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt.SharedKernel.Dto.Movie;
using FluentValidation;

namespace Projekt.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieRoleController : ControllerBase
    {
        private readonly IMovieRoleService _movieRoleService;
        private readonly IValidator<CreateMovieRoleDto> _validator;
        private readonly ILogger<CreateMovieRoleDto> _logger;

        public MovieRoleController(IMovieRoleService movieRoleService, ILogger<CreateMovieRoleDto> logger, IValidator<CreateMovieRoleDto> validator)
        {
            _movieRoleService = movieRoleService;
            _logger = logger;
            _validator = validator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovieRoleDto>> Get()
        {
            _logger.LogDebug("Rozpoczęto pobieranie listy wszystkich ról filmowych");
            var result = _movieRoleService.GetAll();
            _logger.LogDebug("Zakończono pobieranie listy wszystkich ról filmowych");
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetMovieRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MovieRoleDto> Get(int id)
        {
            var result = _movieRoleService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CreateMovieRoleDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data received for movie role creation.");
            }

            int id;
            try
            {
                id = _movieRoleService.Create(dto);
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
        public ActionResult Update(int id, [FromBody] UpdateMovieRoleDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("Mismatch between ID in URL and ID in request body.");
            }

            try
            {
                _movieRoleService.Update(dto);
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
                _movieRoleService.Delete(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}
