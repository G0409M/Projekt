using Projekt.Contracts;
using Projekt.Dto.Review;
using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IProjektUnitOfWork _uow;

        public ReviewService(IProjektUnitOfWork context)
        {
            this._uow = context;
        }

        public List<ReviewDto> GetAll()
        {
            var reviews = _uow.ReviewRepository.GetAll();
            return reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                MovieId = r.MovieId,
                UserId = r.UserId,
                Comment = r.Comment,
                Rating = r.Rating,
                DateCreated = r.DateCreated
            }).ToList();
        }

        public ReviewDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Id is less than zero");
            }

            var review = _uow.ReviewRepository.Get(id);
            if (review == null)
            {
                throw new Exception($"Could not find review with id = {id}");
            }

            return new ReviewDto
            {
                Id = review.Id,
                MovieId = review.MovieId,
                UserId = review.UserId,
                Comment = review.Comment,
                Rating = review.Rating,
                DateCreated = review.DateCreated
            };
        }

        public int Create(CreateReviewDto dto)
        {
            if (dto == null)
            {
                throw new Exception("No review data");
            }

            var id = _uow.ReviewRepository.GetMaxId() + 1;

            var review = new Review
            {
                Id = id,
                MovieId = dto.MovieId,
                UserId = dto.UserId,
                Comment = dto.Comment,
                Rating = dto.Rating,
                DateCreated = DateTime.Now
            };

            _uow.ReviewRepository.Insert(review);
            _uow.Commit();

            return id;
        }

        public void Update(UpdateReviewDto dto)
        {
            if (dto == null)
            {
                throw new Exception("No review data");
            }

            var review = _uow.ReviewRepository.Get(dto.Id);
            if (review == null)
            {
                throw new Exception($"Could not find review with id = {dto.Id}");
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
                throw new Exception($"Could not find review with id = {id}");
            }

            _uow.ReviewRepository.Delete(review);
            _uow.Commit();
        }
    }
}
