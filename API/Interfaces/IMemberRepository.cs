using API.Entities;

namespace API.Interfaces
{
    public interface IMemberRepository
    {
        Task<Member?> GetMemberByIdAsync(string id);
        Task<IReadOnlyList<Member>> GetMembersAsync();
        Task<IReadOnlyList<Photo>> GetPhotosForMembersAsync(string memberId);
        Task<bool> SaveAllAsync();
        void Update(Member member);
        Task<Member?> GetMemberForUpdate(string id);
    }
}
