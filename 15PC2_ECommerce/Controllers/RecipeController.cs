using _15PC2_ECommerce.DTOs.RecipeDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace _15PC2_ECommerce.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RecipeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new ResultRecipeDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Index(ResultRecipeDTO model)
        {
            if (string.IsNullOrWhiteSpace(model.Prompt))
            {
                ModelState.AddModelError("", "Lütfen malzemeleri girin.");
                return View(model);
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://chatgpt-42.p.rapidapi.com/gpt4"),
                    Headers =
                    {
                        { "x-rapidapi-key", "2e21af8214mshcc76234e6e9bc00p1ba553jsn0731fa49af5d" },
                        { "x-rapidapi-host", "chatgpt-42.p.rapidapi.com" },
                    },
                    Content = new StringContent(JsonSerializer.Serialize(new
                    {
                        messages = new[]
                        {
                            new { role = "user", content = $"{model.Prompt}. Bu ürünlerle yapılabilecek üç yemek söyle. Yemeğin malzemelerini ve tarifini yaz." }
                        },
                        web_access = false
                    }), Encoding.UTF8, "application/json")
                };

                using var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var body = await response.Content.ReadAsStringAsync();

                // JSON parse edip Answer'a yazıyoruz
                using var doc = JsonDocument.Parse(body);
                if (doc.RootElement.TryGetProperty("result", out var result))
                {
                    model.Answer = result.GetString();
                }
                else
                {
                    model.Answer = "Tarif bulunamadı.";
                }
            }
            catch (Exception ex)
            {
                model.Answer = $"API çağrısı sırasında hata oluştu: {ex.Message}";
            }

            ModelState.Clear();

            return View(model);
        }
    }
}
