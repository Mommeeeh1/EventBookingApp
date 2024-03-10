﻿using System.ComponentModel;
using System.Net.Http.Json;
using System.Text.Json;
using TicketHive.Shared;
using TicketHive.Shared.Models;

namespace TicketHive.Client.Repositories
{
    public class EventRepo : IEventRepo
    {
        private readonly HttpClient httpClient;

        public List<EventModel> Events { get; set; } = new();

        public EventRepo(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Sends GET request to in order to fetch all events
        /// </summary>
        /// <returns></returns>
        public async Task<List<EventModel>> GetAllEvents()
        {

            var response = await httpClient.GetAsync("api/events");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<EventModel>>();
            }
            return null;
        }

        /// <summary>
        /// Sends GET request to controller in order to fetch all eventviews
        /// </summary>
        /// <returns></returns>
        public async Task<List<EventViewModel>> GetAllEventViews()
        {
            var response = await httpClient.GetAsync("api/events/views");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<EventViewModel>>();
            }
            return null;
        }

        /// <summary>
        /// Sends GET request to API in order to fetch event with provided ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>/returns>
        public async Task<EventModel?> GetEvent(int id)
        {
            var response = await httpClient.GetAsync($"api/Events/{id}");

            if (response.IsSuccessStatusCode)
            {
                var foundEvent = await response.Content.ReadFromJsonAsync<EventModel>();
                return (foundEvent);
            }
            return null;
        }

        /// <summary>
        /// Sends GET request to API in order to fetch eventview with provided ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EventViewModel> GetEventView(int id)
        {
            var response = await httpClient.GetAsync($"api/Events/views/{id}");

            if (response.IsSuccessStatusCode)
            {
                var foundEvent = await response.Content.ReadFromJsonAsync<EventViewModel>();
                return (foundEvent);
            }
            return null;
        }

        /// <summary>
        /// Sends HTTP Post request to controller in order to add event
        /// </summary>
        /// <param name="eventToAdd"></param>
        /// <returns></returns>
        public async Task AddEvent(EventModel eventToAdd)
        {
            var result = await httpClient.PostAsJsonAsync("api/Events", eventToAdd);

        }

        /// <summary>
        /// Sends Delete request to controller in order to remove an event
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveEvent(int id)
        {
            var result = await httpClient.DeleteAsync($"api/Events/{id}");
        }
        
        /// <summary>
        /// Sends Put request to controller in order to update available tickets property for event
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task UpdateAvailableEventTickets(int eventModelId, int quantity)
        {
            ChangeAvailableTicketsModel changeAvailableTicketsModel = new()
            {
                EventModelId = eventModelId,
                Quantity = quantity
            };
            await httpClient.PutAsJsonAsync($"api/Events/availabletickets", changeAvailableTicketsModel);
        }

    }
}
