using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNet.Mvc;

namespace ViewComponentSample.Components
{
    public class ProfileLinksViewComponent : ViewComponent
    {
        private readonly IProfileLinkManager _profileLinkManager;
        
        public ProfileLinksViewComponent(IProfileLinkManager profileLinkManager)
        {
            if (profileLinkManager == null)
            {
                throw new ArgumentNullException("profileLinkManager");
            }
            
            _profileLinkManager = profileLinkManager;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var profileLinks = await _profileLinkManager.GetAllAsync();            
            return View(profileLinks);
        }
    }
}