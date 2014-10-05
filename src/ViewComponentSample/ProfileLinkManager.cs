using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ViewComponentSample 
{
    public interface IProfileLinkManager
    {
        Task<IEnumerable<ProfileLink>> GetAllAsync();
    }
    
    public class ProfileLinkManager : IProfileLinkManager
    {
        private static readonly IEnumerable<ProfileLink> _profileLinks = new List<ProfileLink> 
        {
            new ProfileLink { Name = "Twitter", Url = "http://twitter.com/tourismgeek", FaName = "twitter" },
            new ProfileLink { Name = "linkedIn", Url = "http://www.linkedin.com/in/tugberk", FaName = "linkedin" },
            new ProfileLink { Name = "GitHub", Url = "http://github.com/tugberkugurlu", FaName = "github" },
            new ProfileLink { Name = "Stackoverflow", Url = "http://stackoverflow.com/users/463785/tugberk", FaName = "stack-exchange" }
        };
        
        public Task<IEnumerable<ProfileLink>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<ProfileLink>>(_profileLinks);
        }
    }
    
    public class ProfileLink
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string FaName { get; set; }
    }
}