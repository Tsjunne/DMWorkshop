using DMWorkshop.DTO.Login;
using DMWorkshop.Model.Login;
using MediatR;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DMWorkshop.Handlers.Login
{
    public class LoginWithFacebookCommandHandler : IRequestHandler<LoginWithFacebookCommand, AuthorizationToken>
    {
        private readonly IMongoDatabase _database;
        private FacebookService _facebookService;
        private JwtHandler _jwtHandler;

        public LoginWithFacebookCommandHandler(IConfiguration configuration, IMongoDatabase database)
        {
            _facebookService = new FacebookService();
            _jwtHandler = new JwtHandler(configuration);
            _database = database;
        }

        public async Task<AuthorizationToken> Handle(LoginWithFacebookCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(command.FacebookToken))
            {
                throw new Exception("Token is null or empty");
            }

            var facebookUser = await _facebookService.GetUserFromFacebook(command.FacebookToken, cancellationToken);

            var user = await _database.GetCollection<User>("users").AsQueryable()
                .Where(c => c.Email == facebookUser.Email)
                .SingleAsync(cancellationToken);

            return CreateAccessTokens(user);
        }

        private AuthorizationToken CreateAccessTokens(User user)
        {
            var accessToken = _jwtHandler.CreateAccessToken(user.Id, user.Email, user.Role);
            var refreshToken = _jwtHandler.CreateRefreshToken(user.Id);

            return new AuthorizationToken { AccessToken = accessToken, RefreshToken = refreshToken };
        }
    }
}
