using BaseProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace WebApp.Template.UserCards
{
    //<user-card app-user =/> 
    public class UserCardTagHelper : TagHelper
    {
        public AppUser AppUser { get; set; }

        private readonly IHttpContextAccessor httpContextAccessor;
        public UserCardTagHelper( IHttpContextAccessor httpContextAccessor)
        {  
            this.httpContextAccessor = httpContextAccessor;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            UserCardTemplate userCardTemplate;
            if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                userCardTemplate = new PrimeUserCardTemplate();
            }
            else
            {
                userCardTemplate = new DefaultUserCardTemplate();
            }
            userCardTemplate.SetUser(AppUser);

            //Çıktıda yazılacak
            output.Content.SetHtmlContent(userCardTemplate.Build());

            
        }
    }
}
