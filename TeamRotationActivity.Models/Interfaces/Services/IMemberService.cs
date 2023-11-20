using TeamRotationActivity.Domain.Models;

namespace TeamRotationActivity.Domain.Interfaces.Services;

public interface IMemberService
{
    List<Member> GetAll();
    Member? GetById(Guid id);
    Member Create(Member entity);
}

