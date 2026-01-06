using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace Waterloo.Clients.StanmoreClient;

public class StanmoreClient : IStanmoreClient
{
    private readonly string _url;
    private readonly HttpClient _httpClient;
    private readonly TokenProvider _tokenProvider;

    public StanmoreClient(
        HttpClient httpClient, 
        TokenProvider tokenProvider, 
        IOptions<StanmoreOptions> stanmoreOptions)
    {
        _httpClient = httpClient ??
           throw new ArgumentNullException(nameof(httpClient));
        _tokenProvider = tokenProvider ??
            throw new ArgumentNullException(nameof(tokenProvider));

        ArgumentNullException.ThrowIfNull(stanmoreOptions, nameof(stanmoreOptions));

        _url = $"{stanmoreOptions.Value.BaseUrl}/{stanmoreOptions.Value.PremiumUser}";
    }

    public async Task<Result<bool>> IsUserPremiumAsync(Guid userId)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", _tokenProvider.CreateToken());

        try
        {
            var response = await _httpClient.GetAsync($"{_url}?userId={userId}");
            if(response.IsSuccessStatusCode) {
                return Result.Success(await response.Content.ReadFromJsonAsync<bool>());
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            return Result.Failure<bool>(
               $"User premium status for {userId} failed {response.StatusCode}: {errorContent}");
        }
        catch(Exception ex)
        {
            return Result
                .Failure<bool>($"Exception getting user {userId} premium status : {ex.Message}");
        }
    }
}
