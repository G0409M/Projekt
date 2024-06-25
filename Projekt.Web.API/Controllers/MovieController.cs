using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt.SharedKernel.Dto.Movie;
using Projekt.SharedKernel.Dto.MovieRole;
using Projekt.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace Projekt.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IValidator<CreateMovieDto> _validator;

        public MovieController(IMovieService movieService, IValidator<CreateMovieDto> validator)
        {
            _movieService = movieService;
            _validator = validator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovieDto>> Get()
        {
            var result = _movieService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetMovie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MovieDto> Get(int id)
        {
            var result = _movieService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CreateMovieDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult);
            }
            var id = _movieService.Create(dto);
            var actionName = nameof(Get);
            var routeValues = new { id };
            return CreatedAtAction(actionName, routeValues, null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            try
            {
                _movieService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // log error
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] UpdateMovieDto dto)
        {
            if (id != dto.Id)
            {
                throw new Exception("Id param is not valid");
            }

            _movieService.Update(dto);
            return NoContent();
        }
    }
}