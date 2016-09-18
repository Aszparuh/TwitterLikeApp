namespace Twitter.Web.ViewModels.Home
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mappings;

    public class TweetViewModel : IMapFrom<Tweet>, IHaveCustomMappings
    {
        public string Content { get; set; }

        public string ApplicationUserId { get; set; }

        public string AuthorName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Tweet, TweetViewModel>()
                .ForMember(x => x.AuthorName, opt => opt.MapFrom(x => x.Author.UserName));
        }
    }
}