using TeamRotationActivity.Domain.Interfaces.Services;
using TeamRotationActivity.Domain.Models;

namespace TeamRotationActivity.Core.Services;

public class MemberService : IMemberService
{
    private List<Member> _members = new List<Member>();

    public Member Create(Member entity)
    {
        _members.Add(entity);
        return entity;
    }

    public List<Member> GetAll()
    {
        return _members;
    }

    public Member? GetById(Guid id)
    {
        return _members.FirstOrDefault(m => m.Id == id);
    }
}

