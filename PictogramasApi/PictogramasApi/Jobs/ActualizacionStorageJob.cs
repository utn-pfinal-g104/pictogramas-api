using Quartz;
using System.Threading.Tasks;

namespace PictogramasApi.Jobs
{
    public class ActualizacionStorageJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
            });
        }
    }
}
