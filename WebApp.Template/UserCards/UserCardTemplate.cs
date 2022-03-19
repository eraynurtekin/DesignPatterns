using BaseProject.Models;
using System;
using System.Text;

namespace WebApp.Template.UserCards
{
    //Algoritmanın yapısını iskeletini UserCardTemplate belirliyor!!!
    public abstract class UserCardTemplate
    {
        protected AppUser AppUser { get; set; }

        public void SetUser(AppUser appUser)
        {
            AppUser = appUser;
        }

        //Metodların hangi sırada ve başka işlemler yapılacaksa yapılacak yer.

        public string Build()
        {
            if (AppUser == null) throw new ArgumentNullException(nameof(AppUser));

            var sb = new StringBuilder();

            sb.Append("<div class='card'>");

            sb.Append(SetPicture());
            sb.Append($@"<div class='card-body'>

            <h5>{AppUser.UserName}</h5>
            <p>{AppUser.Description}</p>");

            sb.Append(SetFooter());

            sb.Append("</div>");
            sb.Append("</div>");

            return sb.ToString();
        }

        //Subclasslara yollayacaklarımız:

        protected abstract string SetFooter();
        protected abstract string SetPicture();
    }
}
