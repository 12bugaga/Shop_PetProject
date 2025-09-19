using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Shop.API.Filtres;

public class BasicAuthenticationFilter : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        /// <summary>
        /// authenticate service
        /// </summary>

        /// <summary>
        /// Authentication Handle
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        /// <param name="encoder"></param>
        /// <param name="clock"></param>
        public BasicAuthenticationFilter(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        /// <summary>
        /// Authentification handler
        /// </summary>
        /// <returns>AuthenticateResult</returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            bool isAuth = false;
            string brandCode = string.Empty;
            try
            {
                var authHeaderVal = AuthenticationHeaderValue.Parse(Request.Headers["authToken"]);
                if (authHeaderVal.Scheme.Equals("pla",
                    StringComparison.OrdinalIgnoreCase) &&
                authHeaderVal.Parameter != null)
                {
                    var token = authHeaderVal.Parameter;
                    isAuth = true;
                }
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (!isAuth)
                return AuthenticateResult.Fail("Invalid Authorization token");

            Response.Headers.Add("BrandCode", brandCode);
            var identity = new ClaimsIdentity(null, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
}