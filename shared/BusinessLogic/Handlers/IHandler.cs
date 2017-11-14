using BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Handlers
{
    public interface IHandler
    {
        Task<QueueResult> Handle(string data);
    }
}
