using _15PC2_ECommerce.Context;
using _15PC2_ECommerce.DTOs.ForecastDTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;

namespace _15PC2_ECommerce.Services.ForecastServices
{
    public class ForecastService
    {
        private readonly AppDbContext _context;
        private readonly MLContext _mlContext;

        public ForecastService(AppDbContext context)
        {
            _context = context;
            _mlContext = new MLContext();
        }

        public async Task<List<CityForecastResult>> GetForecastWithHistoryAsync()
        {
            // 🔹 10 şehir
            var cities = new[]
            {
                "İstanbul", "İzmir", "Ankara", "Eskişehir", "Bursa",
                "Trabzon", "Konya", "Mardin", "Antalya", "Erzurum"
            };

            var results = new List<CityForecastResult>();

            foreach (var city in cities)
            {               
                var cityData = await _context.Orders
                    .Include(o => o.Customer)
                    .Where(o => o.Customer.City == city && o.OrderDate.Year == 2025)
                    .GroupBy(o => o.OrderDate.Month)
                    .Select(g => new OrderForecastData
                    {
                        City = city,
                        MonthNumber = g.Key,                       
                        OrderCount = g.Count()
                    })
                    .OrderBy(x => x.MonthNumber)
                    .ToListAsync();

                for (int m = 1; m <= 12; m++)
                {
                    if (!cityData.Any(x => x.MonthNumber == m))
                    {
                        cityData.Add(new OrderForecastData
                        {
                            City = city,
                            MonthNumber = m,
                            OrderCount = 0
                        });
                    }
                }

                cityData = cityData.OrderBy(x => x.MonthNumber).ToList();

                var dataView = _mlContext.Data.LoadFromEnumerable(cityData);

                var forecastingPipeline = _mlContext.Forecasting.ForecastBySsa(
                    outputColumnName: nameof(OrderForecastPrediction.ForecastedOrders),
                    inputColumnName: nameof(OrderForecastData.OrderCount),
                    windowSize: 3,
                    seriesLength: 12,
                    trainSize: cityData.Count,
                    horizon: 3,
                    confidenceLevel: 0.95f,
                    confidenceLowerBoundColumn: "LowerBound",
                    confidenceUpperBoundColumn: "UpperBound"
                );

                var model = forecastingPipeline.Fit(dataView);

                var forecastEngine = model.CreateTimeSeriesEngine<OrderForecastData, OrderForecastPrediction>(_mlContext);

                var prediction = forecastEngine.Predict();

                results.Add(new CityForecastResult
                {
                    City = city,
                    HistoricalOrders = cityData.Select(x => x.OrderCount).ToArray(),
                    ForecastedOrders = prediction.ForecastedOrders
                });
            }

            return results;
        }
    }
}
