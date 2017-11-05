using Data.Model;
using System.Threading.Tasks;

namespace Utils.Queue
{
    public interface IQueueHandler
    {
        Task AddToQueue(ProcessQueueHistory processQueue);
    }
}
