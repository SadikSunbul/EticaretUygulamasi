using EticaretApi.Domain.Entities._Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApi.Application.Features.Commends._AppUser.LoginUser
{
    public class LoginUserCommendHandler : IRequestHandler<LoginUserCommendRequest, LoginUserCommendResponse>
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public LoginUserCommendHandler(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<LoginUserCommendResponse> Handle(LoginUserCommendRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.UserNameOrMailk);
            if (user==null)
            {
                user = await userManager.FindByEmailAsync(request.UserNameOrMailk);
            }
            if (user==null)
            {
                throw new Exception("Kullanıcı adı veya email hatalı...");
            }

           var result =await signInManager.CheckPasswordSignInAsync(user, request.Password,false);

            if (result.Succeeded) //Authentication basarılı
            {
                // .... Yetkılerı belırlememız gerekır
            }
            return new();
        }
    }
}
