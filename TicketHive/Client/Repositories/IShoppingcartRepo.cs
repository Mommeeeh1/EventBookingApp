﻿using TicketHive.Shared.Models;

namespace TicketHive.Client.Repositories
{
    public interface IShoppingcartRepo
    {
        public Task CreateCart();
        public Task<List<BookingModel>> GetCartFromLocalStorage();

        public Task RemoveFromCart(int eventId);

        public Task<bool> CheckIfItemExists(BookingModel booking);
        public Task AddToCart(BookingModel booking);

        public Task<bool> CheckIfCartExists();
        public Task RemoveCart();

        public Task UpdateCart(List<BookingModel> bookings);
    }
}
