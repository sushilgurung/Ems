using Library.DataAcessLayer.Entities;
using Library.DataAcessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Implementation
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityLogRepository _activityLogRepository;
        public ActivityService(IActivityLogRepository activityLogRepository)
        {
            _activityLogRepository = activityLogRepository;
        }
        public void AddActivityLog(ActivityLog model)
        {
            _activityLogRepository.Add(model);
            _activityLogRepository.SaveChanges();
        }
    }
}
