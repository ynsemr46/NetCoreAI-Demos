using NetCoreAI.Project03_RapidApi.ViewModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;


var client = new HttpClient();
List<ApiSeriesViewModel> seriesList = new List<ApiSeriesViewModel>();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/series/"),
    Headers =
    {
        { "x-rapidapi-key", "Kendirapidapikeyiniz" },
        { "x-rapidapi-host", "imdb-top-100-movies.p.rapidapi.com" },
    },
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    seriesList = JsonConvert.DeserializeObject<List<ApiSeriesViewModel>>(body);
    foreach (var series in seriesList)
    {
        Console.WriteLine($"Rank: {series.rank}, Title: {series.title}, Rating: {series.rating}, Year: {series.year}");
    }
}

Console.ReadLine();