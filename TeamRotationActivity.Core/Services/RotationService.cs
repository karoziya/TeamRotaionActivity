using TeamRotationActivity.Domain.Enums;
using TeamRotationActivity.Domain.Interfaces.Services;
using TeamRotationActivity.Domain.Models;

namespace TeamRotationActivity.Core.Services;

/// <summary>
/// Сервис ротации.
/// </summary>
public class RotationService : IRotationService
{
    /// <summary>
    /// Определяет, нужна ли смена ведущего в активности.
    /// </summary>
    /// <param name="activityWork">Активность.</param>
    /// <returns>Нужна ли ротация.</returns>
    public bool NeedRotation(ActivityWork activityWork)
    {
        var differentDays = (DateTime.Now - activityWork.LastRotation).TotalDays;
        var rotationPeriodInDays = GetDaysFromRotationPeriod(activityWork);
        return differentDays >= rotationPeriodInDays;
    }

    /// <summary>
    /// Сменить ведущего активности.
    /// </summary>
    /// <param name="activityWork">Активность.</param>
    public void Rotate(ActivityWork activityWork)
    {
        var currentMembers = activityWork.Members;
        if (currentMembers is null)
            return;
        if (currentMembers is { Count: <= 1 }) 
            return;
        var first = currentMembers.First();
        currentMembers.Remove(first);
        currentMembers.Add(first);
        activityWork.LastRotation = DateTime.Now;
        var rotationPeriodInDays = GetDaysFromRotationPeriod(activityWork);
        activityWork.NextRotation = DateTime.Now + TimeSpan.FromDays(rotationPeriodInDays);
    }

    private int GetDaysFromRotationPeriod(ActivityWork activityWork)
    {
        return activityWork.RotationPeriod switch
        {
            RotationPeriod.Daily => 1,
            RotationPeriod.Weekly => 7,
            RotationPeriod.Monthly => 30,
            _ => 0
        };
    }
}