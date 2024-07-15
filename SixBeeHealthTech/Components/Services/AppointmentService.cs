using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using SixBeeHealthTech.Components.Models;

public class AppointmentService
{
    private readonly HttpClient _httpClient;

    public AppointmentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Appointment>> GetAppointmentsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Appointment>>("api/appointments");
    }

    public async Task CreateAppointmentAsync(Appointment appointment)
    {
        await _httpClient.PostAsJsonAsync("api/appointments", appointment);
    }

    public async Task UpdateAppointmentAsync(Appointment appointment)
    {
        await _httpClient.PutAsJsonAsync($"api/appointments/{appointment.Id}", appointment);
    }

    public async Task DeleteAppointmentAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/appointments/{id}");
    }
}
