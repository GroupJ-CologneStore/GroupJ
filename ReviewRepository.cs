using CologneStore.Data;
using CologneStore.Models;

namespace CologneStore.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ReviewRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void AddReview(Review review)
        {
            _applicationDbContext.Reviews.Add(review);
            _applicationDbContext.SaveChanges();
        }

        public void DeleteReview(Review review)
        {
            _applicationDbContext.Reviews.Remove(review);
            _applicationDbContext.SaveChanges();
        }

        public IEnumerable<Review> GetAllReviews(string search = "")
        {
            search = search.ToLower();
            var reviews = _applicationDbContext.Reviews.AsEnumerable();

            reviews = reviews.Where(a => a.CologneName.ToLower().StartsWith(search));

            return reviews;
        }

        public void UpdateReview(Review review)
        {
            _applicationDbContext.Reviews.Update(review);
            _applicationDbContext.SaveChanges();
        }
    }
}
