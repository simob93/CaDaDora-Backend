using CaDaDora.Personalizzazioni;
using CaDaDora.ValueObjects;
using ExcelDataReader;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;
using Volo.Abp.Uow;

namespace CaDaDora.Booking
{
    public class BookingPrenotazioneAppService : CaDaDoraAppService, IBookingPrenotazioneAppService
    {
        private readonly IBookingPrenotazioneRepository _bookingPrenotazioneRepository;
        private readonly IPersonalizzazioniRepository _personalizzazioniRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IConfiguration _configuration;

        public BookingPrenotazioneAppService(
            IConfiguration configuration,
            IUnitOfWorkManager unitOfWorkManager,
            IPersonalizzazioniRepository personalizzazioniRepository,
            IBookingPrenotazioneRepository bookingPrenotazioneRepository)
        {
            _bookingPrenotazioneRepository = bookingPrenotazioneRepository;
            _personalizzazioniRepository = personalizzazioniRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _configuration = configuration;
        }

        private async Task<byte[]> GeneraPdfAsync()
        {

            var elencoPrenotazioni = await _bookingPrenotazioneRepository.GetListAsync(p => p.PeriodoPrenotato.DataInizio >= DateOnly.FromDateTime(DateTime.Now));
            elencoPrenotazioni = elencoPrenotazioni.OrderBy(p => p.PeriodoPrenotato.DataInizio).ToList();
            byte[] buffer;
            using (var stream = new MemoryStream())
            {
                using (var writer = new PdfWriter(stream))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        pdf.SetDefaultPageSize(PageSize.A4.Rotate());
                        var document = new Document(pdf).SetFontSize(10);
                        var titolo = new Paragraph("Situazione prenotazioni al " + DateOnly.FromDateTime(DateTime.Now).ToString("dd/MM/yyyy")).SetFontSize(15);

                        document.Add(titolo);

                        SolidLine line = new SolidLine(1f);
                        LineSeparator lineSeparator = new LineSeparator(line);
                        document.Add(lineSeparator);

                        float[] colWidths = { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                        var colonne = new[] { "Nome ospite", "Arrivo", "Partenza", "N° Notti", "N° Persone", "N° Adulti", "N° Bambini", "Prezzo", "Commissioni", "Tassa sogg." };
                        Table table = new Table(colWidths, true);
                        table.SetMarginTop(10);
                        table.SetWidth(UnitValue.CreatePercentValue(100));

                        foreach (var col in colonne)
                        {
                            var cellHeader = new Cell().Add(new Paragraph(col).SetMarginLeft(5));
                            cellHeader.SetBackgroundColor(ColorConstants.LIGHT_GRAY);
                            table.AddCell(cellHeader);
                        }
                        decimal totaleAppartamento = 0;
                        decimal totaleCommissioni = 0;
                        foreach (var prenotazione in elencoPrenotazioni)
                        {

                            table.AddCell(new Paragraph(prenotazione.Nominativo.Cognome + " " + prenotazione.Nominativo.Nome).SetMarginLeft(5));
                            table.AddCell(new Paragraph(prenotazione.PeriodoPrenotato.DataInizio.ToString("dd/MM/yyyy")).SetMarginLeft(5));
                            table.AddCell(new Paragraph(prenotazione.PeriodoPrenotato.DataFine?.ToString("dd/MM/yyyy")).SetMarginLeft(5));
                            table.AddCell(new Paragraph(prenotazione.NumeroDiNotti.ToString()).SetMarginLeft(5));
                            table.AddCell(new Paragraph(prenotazione.NumeroPersone.ToString()).SetMarginLeft(5));
                            table.AddCell(new Paragraph(prenotazione.NumeroAdulti.HasValue ? prenotazione.NumeroAdulti.ToString() : String.Empty).SetMarginLeft(5));

                            var str = string.Empty;
                            if (prenotazione.NumeroBambini.HasValue)
                            {
                                str = prenotazione.NumeroBambini.Value.ToString();
                                if (!String.IsNullOrWhiteSpace(prenotazione.EtaBambini))
                                {
                                    str += " (";
                                    var idx = 0;
                                    foreach (var eta in prenotazione.EtaBambini.Split(","))
                                    {
                                        if (idx > 0)
                                        {
                                            str += ";";
                                        }
                                        str += eta.Trim();
                                        idx++;
                                    }
                                    str += ")";
                                }
                            }
                            table.AddCell(new Paragraph(str).SetMarginLeft(5));
                            table.AddCell(new Paragraph("€ " + prenotazione.CostoAppartamento.ToString("N2")).SetMarginLeft(5));
                            table.AddCell(new Paragraph(prenotazione.CostoCommissione.HasValue ? "€ " + prenotazione.CostoCommissione.Value.ToString("N2") : "").SetMarginLeft(5));
                            table.AddCell(new Paragraph("€ " + prenotazione.CostoTassaDiSoggiorno.ToString("N2")).SetMarginLeft(5));
                            totaleAppartamento += prenotazione.CostoAppartamento;
                            totaleCommissioni += prenotazione.CostoCommissione.HasValue ? prenotazione.CostoCommissione.Value : 0;

                        }
                        table.AddCell(new Paragraph(String.Empty));
                        table.AddCell(new Paragraph(String.Empty));
                        table.AddCell(new Paragraph(String.Empty));
                        table.AddCell(new Paragraph(String.Empty));
                        table.AddCell(new Paragraph(String.Empty));
                        table.AddCell(new Paragraph(String.Empty));
                        table.AddCell(new Paragraph("Totale").SetBold());
                        table.AddCell(new Paragraph("€ " + totaleAppartamento.ToString("N2")).SetMarginLeft(5));
                        table.AddCell(new Paragraph("€ " + totaleCommissioni.ToString("N2")).SetMarginLeft(5));
                        table.AddCell(new Paragraph(String.Empty));
                        table.AddCell(new Paragraph(String.Empty));

                        document.Add(table);
                        document.Close();
                    }
                }
                buffer = stream.ToArray();
            }
            return buffer;
        }

        private async Task SendPrenotazioneOnTelegramBotAsync()
        {
            var botClient = new TelegramBotClient(_configuration["Telegram:ApiKey"]);
            var pdfFile = await GeneraPdfAsync();

            await botClient.SendDocumentAsync(
                    _configuration["Telegram:ChatId"],
                    new InputOnlineFile(new MemoryStream(pdfFile), "Elenco Prenotazioni.pdf"),
                    caption: "Inivio prenotazioni aggiornate"
                );
        }

        public async Task CreateFromFileAsync(IFormFile file)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var elencoPrenotazioni = new List<BookingPrenotazioneDto>();
            var personalizzazioni = (await _personalizzazioniRepository.GetListAsync()).FirstOrDefault();

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 1;
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    int count = 0;
                    while (reader.Read()) //Each row of the file
                    {
                        if (count > 0)
                        {
                            var prenotazioneBookingId = long.Parse(reader.GetValue(0).ToString());
                            var prenotatoDa = reader.GetString(1);
                            var cognome = prenotatoDa.Split(",")[0].Trim();
                            var nome = prenotatoDa.Split(",")[1].Trim();
                            var dataPrenotazione = DateTime.Parse(reader.GetValue(5).ToString());
                            var arrivo = DateTime.Parse(reader.GetValue(3).ToString());
                            var partenza = DateTime.Parse(reader.GetValue(4).ToString());
                            var stato = reader.GetValue(6).ToString();
                            var persone = int.Parse(reader.GetValue(8).ToString());
                            var adulti = (int?)(reader.GetValue(9) != null ? int.Parse(reader.GetValue(9).ToString()) : null);
                            var bambini = (int?)(reader.GetValue(10) != null ? int.Parse(reader.GetValue(10).ToString()) : null);
                            var etaBambini = reader.GetValue(11) != null ? reader.GetValue(11).ToString() : null;
                            var prezzo = decimal.Parse(reader.GetValue(12).ToString().Split(" ")[0]);
                            var commissione = (decimal?)(!string.IsNullOrWhiteSpace(reader.GetString(14)) ? decimal.Parse(reader.GetValue(14).ToString().Split(" ")[0]) : null);

                            elencoPrenotazioni.Add(new BookingPrenotazioneDto
                            {
                                Nome = nome,
                                Cognome = cognome,
                                DataInizio = arrivo.ClearTime(),
                                DataFine = partenza.ClearTime(),
                                Stato = stato,
                                PrenotazioneBookingId = prenotazioneBookingId,
                                CostoAppartamento = prezzo,
                                CostoCommissione = commissione,
                                NumeroPersone = persone,
                                NumeroAdulti = adulti,
                                NumeroBambini = bambini,
                                EtaBambini = etaBambini,
                                DataPrenotazione = dataPrenotazione

                            });
                        }
                        count++;
                    }
                }
            }

            var daInviare = false;
            var daEliminare = new List<Guid>();

            foreach (var prenotazione in elencoPrenotazioni)
            {
                var prenotazioneDb = await _bookingPrenotazioneRepository.FindAsync(p => p.PrenotazioneBookingId == prenotazione.PrenotazioneBookingId);
                if (prenotazioneDb == null)
                {
                    if (prenotazione.Stato != "cancelled_by_guest")
                    {
                        // aggiungo il dato a DB
                        await _bookingPrenotazioneRepository.InsertAsync(
                            new BookingPrenotazione(
                                GuidGenerator.Create(),
                                prenotazione.PrenotazioneBookingId,
                                Nominativo.Crea(prenotazione.Nome, prenotazione.Cognome),
                                PeriodoDateOnly.Crea(DateOnly.FromDateTime(prenotazione.DataInizio), DateOnly.FromDateTime(prenotazione.DataFine)),
                                prenotazione.DataPrenotazione,
                                prenotazione.NumeroPersone,
                                prenotazione.NumeroAdulti,
                                prenotazione.NumeroBambini,
                                prenotazione.EtaBambini,
                                prenotazione.CostoAppartamento,
                                prenotazione.CostoCommissione,
                                personalizzazioni.ImpostaDiSoggiorno,
                                personalizzazioni.CostoTransazioneBancaria));
                        daInviare = true;
                    }
                }
                else
                {
                    if (prenotazione.Stato == "cancelled_by_guest")
                    {
                        daEliminare.Add(prenotazioneDb.Id);
                    }
                    else
                    {

                        if (!prenotazioneDb.PeriodoPrenotato.ValueEquals(PeriodoDateOnly.Crea(DateOnly.FromDateTime(prenotazione.DataInizio), DateOnly.FromDateTime(prenotazione.DataFine))))
                        {
                            daInviare = true;
                        }

                        // modifico il dato 
                        prenotazioneDb.Modifica(
                                PeriodoDateOnly.Crea(DateOnly.FromDateTime(prenotazione.DataInizio), DateOnly.FromDateTime(prenotazione.DataFine)),
                                prenotazione.NumeroPersone,
                                prenotazione.NumeroAdulti,
                                prenotazione.NumeroBambini,
                                prenotazione.EtaBambini,
                                prenotazione.CostoAppartamento,
                                prenotazione.CostoCommissione,
                                personalizzazioni.ImpostaDiSoggiorno);

                        await _bookingPrenotazioneRepository.UpdateAsync(prenotazioneDb);
                    }
                }

            }

            if (daEliminare.Count > 0)
            {
                await _bookingPrenotazioneRepository.DeleteManyAsync(daEliminare);
                daInviare = true;
            }

            await _unitOfWorkManager.Current.SaveChangesAsync();

            if (daInviare)
            {
                await SendPrenotazioneOnTelegramBotAsync();
            }
        }

        public async Task<byte[]> GeneratePdfAsync()
        {
            return await GeneraPdfAsync();
        }

        public async Task AggiornaTassaDiSoggiornoAsync(AggiornaTasseDiSoggiornoDto input)
        {
            var elencoPrenotazioni = await _bookingPrenotazioneRepository.GetListAsync(p => p.PeriodoPrenotato.DataInizio >= DateOnly.FromDateTime(DateTime.Now));
            foreach (var prenotazione in elencoPrenotazioni)
            {
                prenotazione.AggiornaTassaDiSoggiorno(input.Importo);
                await _bookingPrenotazioneRepository.UpdateAsync(prenotazione);
            }
        }
    }
}
