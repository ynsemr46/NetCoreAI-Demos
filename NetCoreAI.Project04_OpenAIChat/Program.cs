using System.Text;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        // Hugging Face tokenini buraya yaz
        var hfToken = "hf_********";

        Console.WriteLine("Hoşgeldiniz");
        Console.Write("Lütfen sorunuzu yazınız: ");
        var prompt = Console.ReadLine();

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {hfToken}");

        // Hugging Face chat completion request body
        var requestBody = new
        {
            model = "openai/gpt-oss-120b:nebius",
            messages = new[]
            {
                new { role = "system", content = "You are a helpful assistant." },
                new { role = "user", content = prompt }
            },
            max_tokens = 300
        };

        var jsonRequestBody = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PostAsync(
                "https://router.huggingface.co/v1/chat/completions",
                content
            );

            var jsonResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var responseObject = JsonSerializer.Deserialize<JsonElement>(jsonResponse);
                var answer = responseObject
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                Console.WriteLine($"Cevap: {answer}");
            }
            else
            {
                Console.WriteLine($"Hata: {response.StatusCode} -> {jsonResponse}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata: {ex.Message}");
        }
    }
}
