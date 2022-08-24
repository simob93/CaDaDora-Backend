using CaDaDora.Booking;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CaDaDora.Controllers.Booking
{
    [Area("Prenotazioni Booking")]
    [ControllerName("BookingPrenotazione")]
    [Route("api/booking-riservation")]
    public class BookingPrenotazioneController : CaDaDoraController
    {

        private readonly IBookingPrenotazioneAppService _bookingPrenotazioneAppService;

        public BookingPrenotazioneController(IBookingPrenotazioneAppService bookingPrenotazioneAppService)
        {
            _bookingPrenotazioneAppService = bookingPrenotazioneAppService;
        }

        /// <summary>
        /// Aggiorna/inserisce le prenotazioni booking tramite file 
        /// </summary>
        [HttpPost("upload")]
        public async Task CreateFromFileAsync(IFormFile file)
        {
            await _bookingPrenotazioneAppService.CreateFromFileAsync(file);
        }

        [HttpPost("report")]
        public async Task<IActionResult> GeneratePdfAsync()
        {
            return File(await _bookingPrenotazioneAppService.GeneratePdfAsync(), "application/pdf", "my_file.pdf");
        }

        [HttpPost("aggiorna-tasse-soggiorno")]
        public async Task AggiornaTassaDiSoggiornoAsync(AggiornaTasseDiSoggiornoDto input)
        {
            await _bookingPrenotazioneAppService.AggiornaTassaDiSoggiornoAsync(input);
        }
    }
}
