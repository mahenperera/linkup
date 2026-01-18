using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IMemberRepository
    {
        Task<Member?> GetMemberByIdAsync(string id);
        Task<PaginatedResult<Member>> GetMembersAsync(MemberParams memberParams);
        Task<IReadOnlyList<Photo>> GetPhotosForMemberAsync(string memberId, bool isCurrentUser);
        void Update(Member member);
        Task<Member?> GetMemberForUpdateAsync(string id);
    }
}
