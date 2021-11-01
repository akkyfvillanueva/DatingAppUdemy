using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Extensions;
using API.Helpers;

namespace API.Data
{
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext _context;
        public LikesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
        {
            return await _context.Likes.FindAsync(sourceUserId, likedUserId);
        }

        public async Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams)
        {
            var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            if (likesParams.Predicate == "liked")
            {
                likes = likes.Where(like => like.SourceUserId == likesParams.UserId);
                users = likes.Select(like => like.LikedUser);
            }

            if (likesParams.Predicate == "likedBy")
            {
                likes = likes.Where(like => like.LikedUserId == likesParams.UserId);
                users = likes.Select(like => like.SourceUser);
            }

            var likedUsers = users.Select(x => 
                new LikeDto 
                {
                    UserName = x.UserName,
                    KnownAs = x.KnownAs,
                    Age = x.DateOfBirth.CalculateAge(),
                    PhotoUrl = x.Photos.FirstOrDefault(p => p.IsMain).Url,
                    City = x.City,
                    Id = x.Id
                }
            );

            return await PagedList<LikeDto>.CreateAsync(likedUsers, 
                likesParams.PageNumber, likesParams.PageSize);
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            return await _context.Users
                .Include(x => x.LikedByUsers)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}