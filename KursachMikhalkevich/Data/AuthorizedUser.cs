using KursachMikhalkevich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KursachMikhalkevich.Data
{
    public class AuthorizedUser
    {
        private static AuthorizedUser _authorizedUser;
        private static Worker _worker;

        private AuthorizedUser()
        { }

        public static AuthorizedUser GetInstance()
        {
            if (_authorizedUser == null)
                _authorizedUser = new AuthorizedUser();
            return _authorizedUser;
        }

        public static void SetUser(Worker worker)
        {
            _worker = worker;
        }

        public static void ClearUser()
        {
            _worker = null;
        }

        public static Worker GetUser()
        {
            return _worker;
        }
    }
}
