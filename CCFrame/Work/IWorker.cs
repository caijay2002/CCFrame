using CCFrame.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCFrame.Work
{
    public interface IWorker
    {
        void Initialize(List<DriverConfigItem> configItems);

        void Start();

        void Stop();
    }
}
