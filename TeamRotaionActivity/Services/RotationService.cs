using TeamRotaionActivity.Model;

namespace TeamRotaionActivity.Services
{
    public static class RotationService
    {
        public static bool NeedRotation(ActivityWork activityWork)
        {
            var differentDays = (DateTime.Now - activityWork.LastChangeActivity).TotalDays;
            switch (activityWork.RotationPeriod)
            {
                case RotationPeriod.Daily:
                    return differentDays >= 1;
                case RotationPeriod.Weekly:
                    return differentDays >= 7;
                case RotationPeriod.Monthly:
                    return differentDays >= 30;
            }
            return false;
        }

        public static void Rotate(ActivityWork activityWork)
        {
            var currentMembers = activityWork.Members;
            if (currentMembers.Count > 1) 
            {
                var first = currentMembers.First();
                currentMembers.Remove(first);
                currentMembers.Add(first);
                activityWork.LastChangeActivity = DateTime.Now;
            }
        }

    }
}
