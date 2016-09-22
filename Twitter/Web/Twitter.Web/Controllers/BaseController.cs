namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
    using Infrastructure.Mappings;

    public abstract class BaseController : Controller
    {
        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }
    }
}