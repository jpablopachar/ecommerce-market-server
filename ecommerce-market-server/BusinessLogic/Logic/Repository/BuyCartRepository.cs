using System.Text.Json;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace BusinessLogic.Logic.Repository
{
    public class BuyCartRepository(IConnectionMultiplexer redis) : IBuyCartRepository
    {
        private readonly IDatabase _database = redis.GetDatabase();

        public async Task<bool> DeleteBuyCartAsync(string cartId)
        {
            return await _database.KeyDeleteAsync(cartId);
        }

        public async Task<BuyCart?> GetBuyCartByIdAsync(string cartId)
        {
            var data = await _database.StringGetAsync(cartId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<BuyCart>(data!);
        }

        public async Task<BuyCart?> UpdateBuyCartAsync(BuyCart buyCart)
        {
            var status = await _database.StringSetAsync(buyCart.Id, JsonSerializer.Serialize(buyCart), TimeSpan.FromDays(30));

            if (!status) return null!;

            return await GetBuyCartByIdAsync(buyCart.Id!);
        }
    }
}