using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StackExchange.Redis;
using Talabaat.Core.Entity;
using Talabaat.Core.Reposatory.Interfaces;

namespace Talabaat.Repository.Repository
{
    public class BasketReposatry : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketReposatry(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket),
                TimeSpan.FromDays(30));
            if (!created) return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}
