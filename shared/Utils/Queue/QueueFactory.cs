using Data.Db;
using Helper.Configuration;

namespace Utils.Queue
{
    public static class QueueFactory
    {
        public static IQueueHandler GetQueueHandler(AppSettings settings, GymOrganizerContext db)
        {
            return new RabbitMQHandler(settings, db);
        }
    }
}
