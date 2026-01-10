using API.Entities;

namespace API.Interfaces
{
    public interface ILikesRepository
    {
        Task<MemberLike?> GetMemberLikeAsync(string sourceMemberId, string targetMemberId);
        Task<IReadOnlyList<Member>> GetMemberLikesAsync(string predicate, string memberId);
        Task<IReadOnlyList<string>> GetCurrentMemberLikeIdsAsync(string memberId);
        void AddLike(MemberLike like);
        void DeleteLike(MemberLike like);
        Task<bool> SaveAllAsync();
    }
}
