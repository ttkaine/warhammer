using System;
using Warhammer.Mvc.Abstract;

namespace Warhammer.Mvc.Concrete
{
    public class LinkGenerator : ILinkGenerator
    {
        public string ResolveCreoleLinks(string htmlContent)
        {
            return htmlContent;
        }



        public string CreoleLinksToHtml(string htmlContent)
        {
            return htmlContent;
        }



    }
}