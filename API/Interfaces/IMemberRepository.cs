using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IMemberRepository
    {
        Task<Member?> GetMemberByIdAsync(string id);
        Task<PaginatedResult<Member>> GetMembersAsync(MemberParams memberParams);
        Task<IReadOnlyList<Photo>> GetPhotosForMembersAsync(string memberId);
        void Update(Member member);
        Task<Member?> GetMemberForUpdate(string id);
    }
}
