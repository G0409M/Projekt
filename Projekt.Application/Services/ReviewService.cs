using AutoMapper;
using Projekt.Domain.Contracts;
using Projekt.SharedKernel.Dto.Review;
using Projekt.Domain.Exceptions;
using Projekt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IProjektUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ReviewService(IProjektUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public List<ReviewDto> GetAll()
        {
            var reviews = _uow.ReviewRepository.GetAll();
            List<ReviewDto> result = _mapper.Map<List<ReviewDto>>(reviews);
            return result;
        }

        public ReviewDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }

            var review = _uow.ReviewRepository.Get(id);
            if (review == null)
            {
                throw new NotFoundException($"Could not find review with id = {id}");
            }

            var result = _mapper.Map<ReviewDto>(review);
            return result;
        }

        public int Create(CreateReviewDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No review data");
            }

            var id = _uow.ReviewRepository.GetMaxId() + 1;
            var review = _mapper.Map<Review>(dto);
            review.Id = id;
            review.DateCreated = DateTime.Now;

            _uow.ReviewRepository.Insert(review);
            _uow.Commit();

            return id;
        }

        public void Update(UpdateReviewDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No review data");
            }

            var review = _uow.ReviewRepository.Get(dto.Id);
            if (review == null)
            {
                throw new NotFoundException($"Could not find review with id = {dto.Id}");
            }

            review.MovieId = dto.MovieId;
            review.UserId = dto.UserId;
            review.Comment = dto.Comment;
            review.Rating = dto.Rating;

            _uow.Commit();
        }

        public void Delete(int id)
        {
            var review = _uow.ReviewRepository.Get(id);
            if (review == null)
            {
                throw new NotFoundException($"Could not find review with id = {id}");
            }

            _uow.ReviewRepository.Delete(review);
            _uow.Commit();
        }
        public List<ReviewDto> GetByMovieId(int movieId)
        {
            var reviews = _uow.ReviewRepository.GetAll().Where(r => r.MovieId == movieId).ToList();
            List<ReviewDto> result = _mapper.Map<List<ReviewDto>>(reviews);
            return result;
        }

    }
}
