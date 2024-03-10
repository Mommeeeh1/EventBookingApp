﻿using TicketHive.Shared.Models;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using TicketHive.Client.Pages;

namespace TicketHive.Client.Repositories
{
    public class ShoppingcartRepo : IShoppingcartRepo
    {
        private readonly ILocalStorageService localStorage;

        public ShoppingcartRepo(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        /// <summary>
        /// Creates an empty cart in localstorage
        /// </summary>
        /// <param name="bookings"></param>
        /// <returns></returns>
        public async Task CreateCart()
        {
            List<BookingModel> empty = new();
            string bookingsJson = JsonConvert.SerializeObject(empty);

            if (!await localStorage.ContainKeyAsync("cart"))
            {
                await localStorage.SetItemAsync("cart", bookingsJson);
            }
        }

        /// <summary>
        /// Returns List contained in cart item in localstorage
        /// </summary>
        /// <returns></returns>
        public async Task<List<BookingModel>> GetCartFromLocalStorage()
        {
            string? jsonString = await localStorage.GetItemAsync<string>("cart");

            if (jsonString != null)
            {
                return JsonConvert.DeserializeObject<List<BookingModel>>(jsonString);
            }
            return null;
        }

        /// <summary>
        /// Checks if list of bookingmodel in cart in localstorage contains provided booking and returns a bool
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        public async Task<bool> CheckIfItemExists(BookingModel booking)
        {
            List<BookingModel> bookings = await GetCartFromLocalStorage();

            foreach (var item in bookings)
            {
                if (item.EventModelId == booking.EventModelId)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Adds a booking to the cart in localstorage
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        public async Task AddToCart(BookingModel booking)
        {
            string bookingsJson = await localStorage.GetItemAsync<string>("cart");

            List<BookingModel>? localStorageBookings = JsonConvert.DeserializeObject<List<BookingModel>>(bookingsJson);

            localStorageBookings.Add(booking);
            string updatedBookingsJson = JsonConvert.SerializeObject(localStorageBookings);

            await localStorage.SetItemAsync("cart", updatedBookingsJson);
        }

        /// <summary>
        /// Removes an item from cart in localstorage
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public async Task RemoveFromCart(int eventId)
        {

            List<BookingModel>? localStorageBookings = await GetCartFromLocalStorage();

            localStorageBookings.RemoveAll(b => b.EventModelId == eventId);

            string updatedBookingsJson = JsonConvert.SerializeObject(localStorageBookings);

            await localStorage.SetItemAsync("cart", updatedBookingsJson);

        }

        /// <summary>
        /// Checks if localstorage contains cart item and returns a bool based on result
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckIfCartExists()
        {
            if (await localStorage.ContainKeyAsync("cart"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes cart from localstorage
        /// </summary>
        /// <returns></returns>
        public async Task RemoveCart()
        {
            await localStorage.RemoveItemAsync("cart");
        }

        /// <summary>
        /// Sets list of bookingmodel in cart in localstorage to provided list
        /// </summary>
        /// <param name="bookings"></param>
        /// <returns></returns>
        public async Task UpdateCart(List<BookingModel> bookings)
        {
            var cart = await GetCartFromLocalStorage();

            List<BookingModel> updatedBookings = bookings;

            string updatedBookingsJson = JsonConvert.SerializeObject(updatedBookings);

            await localStorage.SetItemAsync("cart", updatedBookings);
        }
    }
}
