using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace CatalogoProdutos.Api.Auth;

public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly string _usuario;
    private readonly string _senha;

    public BasicAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IConfiguration configuration)
        : base(options, logger, encoder)
    {
        _usuario = configuration["Admin:Usuario"] ?? throw new ArgumentNullException("Admin:Usuario configuração está ausente");
        _senha = configuration["Admin:Senha"] ?? throw new ArgumentNullException("Admin:Senha configuração está ausente");
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("Cabeçalho de Autorização ausente");

        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);

            var usuario = credentials[0];
            var senha = credentials[1];

            // Valida o usuário e senha
            if (usuario != _usuario || senha != _senha)
                return AuthenticateResult.Fail("Usuário ou senha inválidos");

            var claims = new[] { new Claim(ClaimTypes.Name, usuario) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
        catch
        {
            return AuthenticateResult.Fail("Cabeçalho de Autorização Inválido");
        }
    }
}