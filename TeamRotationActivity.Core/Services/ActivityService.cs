using TeamRotationActivity.Domain.Interfaces.Services;
using TeamRotationActivity.Domain.Models;

namespace TeamRotationActivity.Core.Services;

public class ActivityService : IActivityService
{
    private List<ActivityWork> _activities = new List<ActivityWork>();

    public ActivityWork Create(ActivityWork entity)
    {
        _activities.Add(entity);
        return entity;
    }

    public List<ActivityWork> GetAll()
    {
        return _activities;
    }

    public ActivityWork? GetById(Guid id)
    {
        return _activities.FirstOrDefault(m => m.Id == id);
    }
}

