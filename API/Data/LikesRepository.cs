using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LikesRepository(AppDbContext context) : ILikesRepository
    {
        public async Task<MemberLike?> GetMemberLikeAsync(string sourceMemberId, string targetMemberId)
        {
            return await context.Likes.FindAsync(sourceMemberId, targetMemberId);
        }

        public async Task<PaginatedResult<Member>> GetMemberLikesAsync(LikesParams likesParams)
        {
            var query = context.Likes.AsQueryable();
            IQueryable<Member> result;

            switch (likesParams.Predicate)
            {
                case "liked":
                    result = query
                        .Where(x => x.SourceMemberId == likesParams.MemberId)
                        .Select(x => x.TargetMember);
                    break;
                case "likedBy":
                    result = query
                        .Where(x => x.TargetMemberId == likesParams.MemberId)
                        .Select(x => x.SourceMember);
                    break;
                default:
                    var likeIds = await GetCurrentMemberLikeIdsAsync(likesParams.MemberId);

                    result = query
                        .Where(x => x.TargetMemberId == likesParams.MemberId && likeIds.Contains(x.SourceMemberId))
                        .Select(x => x.SourceMember);
                    break;
            }

            return await PaginationHelper.CreateAsync(result, likesParams.PageNumber, likesParams.PageSize);
        }

        public async Task<IReadOnlyList<string>> GetCurrentMemberLikeIdsAsync(string memberId)
        {
            return await context.Likes
                .Where(x => x.SourceMemberId == memberId)
                .Select(x => x.TargetMemberId)
                .ToListAsync();
        }

        public void AddLike(MemberLike like)
        {
            context.Likes.Add(like);
        }

        public void DeleteLike(MemberLike like)
        {
            context.Likes.Remove(like);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
