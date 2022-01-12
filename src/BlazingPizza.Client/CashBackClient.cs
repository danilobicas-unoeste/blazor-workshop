using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazingPizza.Client
{
    public class CashBackClient
    {
        private readonly HttpClient httpClient;

        public CashBackClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<CashBack>> GetCashBacks() =>
            await httpClient.GetFromJsonAsync<IEnumerable<CashBack>>("cashback");

        public async Task<IEnumerable<OrderWithStatus>> GetOrders() =>
            await httpClient.GetFromJsonAsync<IEnumerable<OrderWithStatus>>("orders");


        public async Task<OrderWithStatus> GetOrder(int orderId) =>
            await httpClient.GetFromJsonAsync<OrderWithStatus>($"orders/{orderId}");


        public async Task<int> PlaceOrder(Order order)
        {
            var response = await httpClient.PostAsJsonAsync("orders", order);
            response.EnsureSuccessStatusCode();
            var orderId = await response.Content.ReadFromJsonAsync<int>();
            CashBack cashback = new CashBack();
            cashback.OrderId = orderId;
            cashback.Value = Math.Round((order.GetTotalPrice() * (10 / 100)),2);
            var responsecashback = await httpClient.PostAsJsonAsync("cashback", cashback);
            return orderId;
        }

        public async Task SubscribeToNotifications(NotificationSubscription subscription)
        {
            var response = await httpClient.PutAsJsonAsync("notifications/subscribe", subscription);
            response.EnsureSuccessStatusCode();
        }
    }
}
