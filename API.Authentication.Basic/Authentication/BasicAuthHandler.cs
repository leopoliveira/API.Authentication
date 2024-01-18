using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

using API.Authentication.Core.Repositories;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace API.Authentication.Basic.Authentication
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        // Basic Auth format: basic base64<userName:password>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            const string AUTHORIZATION_STRING = "Authorization";
            const string UNAUTHORIZED_MSG = "Check the header! Authentication is needed.";
            const string AUTH_HEADER_INITIAL_STRING = "basic ";
            const string BASIC_AUTH_SEPARATOR = ":";

            if (!Request.Headers.ContainsKey(AUTHORIZATION_STRING))
            {
                return Task.FromResult(AuthenticateResult.Fail(UNAUTHORIZED_MSG));
            }

            string authHeader = Request.Headers[AUTHORIZATION_STRING];

            if (string.IsNullOrWhiteSpace(authHeader))
            {
                return Task.FromResult(AuthenticateResult.Fail(UNAUTHORIZED_MSG));
            }

            if (!authHeader.StartsWith(AUTH_HEADER_INITIAL_STRING, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.Fail(UNAUTHORIZED_MSG));
            }

            string token = authHeader.Substring(AUTH_HEADER_INITIAL_STRING.Length).Trim();

            // expected format: userName:password
            string credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));

            string[] credentials = credentialString.Split(BASIC_AUTH_SEPARATOR);

            if (credentials?.Length != 2)
            {
                return Task.FromResult(AuthenticateResult.Fail(UNAUTHORIZED_MSG));
            }

            string userName = credentials[0];
            string password = credentials[1];

            bool authenticated = new UserRepository().Authenticate(userName, password);

            if (!authenticated)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));
            }

            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userName) };
            var identity = new ClaimsIdentity(claims, "Basic");
            var principal = new ClaimsPrincipal(identity);

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name)));
        }
    }
}
