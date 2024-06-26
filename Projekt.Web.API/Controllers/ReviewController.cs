﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt.SharedKernel.Dto.Review;
using Projekt.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt.SharedKernel.Dto.Movie;

namespace Projekt.Web.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IValidator<CreateReviewDto> _validator;
        private readonly ILogger<CreateReviewDto> _logger;

        public ReviewController(IReviewService reviewService, IValidator<CreateReviewDto> validator, ILogger<CreateReviewDto> logger)
        {
            _reviewService = reviewService;
            _validator = validator;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReviewDto>> Get()
        {
            _logger.LogDebug("Rozpoczęto pobieranie listy wszystkich recenzji");
            var result = _reviewService.GetAll();
            _logger.LogDebug("Zakończono pobieranie listy wszystkich recenzji");
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetReview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ReviewDto> Get(int id)
        {
            var result = _reviewService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CreateReviewDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data received for review creation.");
            }

            int id;
            try
            {
                id = _reviewService.Create(dto);
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
        public ActionResult Update(int id, [FromBody] UpdateReviewDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("Mismatch between ID in URL and ID in request body.");
            }

            try
            {
                _reviewService.Update(dto);
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
                _reviewService.Delete(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}
