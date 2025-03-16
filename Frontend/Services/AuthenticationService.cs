using Blazored.LocalStorage;
using Frontend.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.DTOs.Autenticacion;
using Shared.DTOs.Usuarios;
using System.Net.Http.Json;

public class AuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly CustomAuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthenticationService(HttpClient httpClient, CustomAuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        _localStorage = localStorage;
    }

    public async Task<AuthenticationResultDTO> RegisterAsync(UsuarioDTO usuario)
    {
        var response = await _httpClient.PostAsJsonAsync("api/gateway/authentication/register", usuario);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<AuthenticationResultDTO>();

            if (result.IsSuccess)
            {
                await _localStorage.SetItemAsync("authToken", result.Token);
                await _authenticationStateProvider.MarkUserAsAuthenticatedAsync(result.Token);
            }

            return result;
        }
        return new AuthenticationResultDTO { IsSuccess = false, Message = "Error en el registro" };
    }


    public async Task<AuthenticationResultDTO> LoginAsync(CredencialesDTO credenciales)
    {
        var response = await _httpClient.PostAsJsonAsync("api/gateway/authentication/login", credenciales);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<AuthenticationResultDTO>();
            if (result.IsSuccess)
            {
                await _localStorage.SetItemAsync("authToken", result.Token);
                await _authenticationStateProvider.MarkUserAsAuthenticatedAsync(result.Token);
            }
            return result;
        }
        return new AuthenticationResultDTO { IsSuccess = false, Message = "Credenciales inválidas" };
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
        await _authenticationStateProvider.MarkUserAsLoggedOutAsync();
    }
}
