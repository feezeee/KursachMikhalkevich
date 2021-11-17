using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KursachMikhalkevich.Models
{
    public static class WorkerExtension
    {
        public static string FIO(this Worker worker)
        {
            return $"{worker?.LastName} {worker?.FirstName} {worker?.MiddleName}";
        }
    }
}
