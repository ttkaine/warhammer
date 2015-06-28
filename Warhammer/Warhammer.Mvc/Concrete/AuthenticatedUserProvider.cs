﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web;
using Sample.Core.Abstract;

namespace Warhammer.Mvc.Concrete
{
    public class AuthenticatedUserProvider : IAuthenticatedUserProvider
    {
        public bool UserIsAuthenticated
        {
            get
            {
                var context = HttpContext.Current;
                if (context != null && context.User != null && context.User.Identity != null)
                    return HttpContext.Current.User.Identity.IsAuthenticated;
                else
                    return false;
            }
        }

        public string UserName
        {
            get
            {
                var context = HttpContext.Current;
                if (context != null && context.User != null && context.User.Identity != null)
                    return HttpContext.Current.User.Identity.Name;
                else
                    return null;
            }
        }
        public ICollection<string> Roles
        {
            get
            {
                return new Collection<string>();
            }
        }
    }
}