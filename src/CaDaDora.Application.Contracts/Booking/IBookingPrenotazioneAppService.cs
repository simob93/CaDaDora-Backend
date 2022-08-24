using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace CaDaDora.Booking
{
    public interface IBookingPrenotazioneAppService : IApplicationService
    {
        Task CreateFromFileAsync(IFormFile file);

        Task AggiornaTassaDiSoggiornoAsync(AggiornaTasseDiSoggiornoDto input);

        Task<byte[]> GeneratePdfAsync();
    }
}
