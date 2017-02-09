using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http.Authentication;
using Newtonsoft.Json.Linq;

namespace AspNetCore.Authentication.ExactOnline
{
    public class ExactOnlineHandler : OAuthHandler<ExactOnlineAuthenticationOptions>
    {
        public ExactOnlineHandler(HttpClient backchannel) : base(backchannel)
        {
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            //TODO read the token for values
            //api/v1/current/Me
            var request = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // = "application/json, text/javascript, */*; q=0.01";
            var response = await Backchannel.SendAsync(request, Context.RequestAborted);


            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"An error occurred when retrieving user information ({response.StatusCode}). Please check if the authentication information is correct and the corresponding Google+ API is enabled.");
            }

            var content = await response.Content.ReadAsStringAsync();
            var payload = JObject.Parse(content)["d"]["results"][0] as JObject;

            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, properties, Options.AuthenticationScheme);
            var context = new OAuthCreatingTicketContext(ticket, Context, Options, Backchannel, tokens, payload);

            identity.AddClaim(new Claim("urn:exactonline:access_token_expires_at",  DateTime.UtcNow.AddSeconds(int.Parse(tokens.ExpiresIn)).ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz"), ClaimValueTypes.DateTime, Options.ClaimsIssuer));
            identity.AddClaim(new Claim("urn:exactonline:access_token", tokens.AccessToken, ClaimValueTypes.String, Options.ClaimsIssuer));
            identity.AddClaim(new Claim("urn:exactonline:refresh_token", tokens.RefreshToken, ClaimValueTypes.String, Options.ClaimsIssuer));

            var identifier = payload.Value<string>("UserID");
            if (!string.IsNullOrEmpty(identifier))
            {
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, identifier, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var givenName = payload.Value<string>("UserName");
            if (!string.IsNullOrEmpty(givenName))
            {
                identity.AddClaim(new Claim(ClaimTypes.GivenName, givenName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var familyName = payload.Value<string>("LastName");
            if (!string.IsNullOrEmpty(familyName))
            {
                identity.AddClaim(new Claim(ClaimTypes.Surname, familyName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var name = payload.Value<string>("FullName"); //FirstName
            if (!string.IsNullOrEmpty(name))
            {
                identity.AddClaim(new Claim("urn:exactonline:fullname", name, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var email = payload.Value<string>("Email");
            if (!string.IsNullOrEmpty(email))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, email, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var division = payload.Value<string>("CurrentDivision");
            if (!string.IsNullOrEmpty(division))
            {
                identity.AddClaim(new Claim("urn:exactonline:division", division, ClaimValueTypes.String, Options.ClaimsIssuer));
            }
            var company = payload.Value<string>("DivisionCustomerName");
            if (!string.IsNullOrEmpty(company))
            {
                identity.AddClaim(new Claim("urn:exactonline:company", company, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var mugshot = payload.Value<string>("PictureUrl");
            if (!string.IsNullOrEmpty(mugshot))
            {
                identity.AddClaim(new Claim("urn:exactonline:mugshot", mugshot.Replace("OptimizeForWeb=1", "OptimizeForWeb=0"), ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            await Options.Events.CreatingTicket(context);
            return context.Ticket;
        }

    }
}