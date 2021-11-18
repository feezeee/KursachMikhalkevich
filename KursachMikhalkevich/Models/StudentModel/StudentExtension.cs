using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KursachMikhalkevich.Models
{
    public static class StudentExtension
    {
        public static string FIO(this Student student)
        {
            return $"{student?.LastName} {student?.FirstName} {student?.MiddleName}";
        }
    }
}
