using System;
using System.Threading.Tasks;
using Backend.API.Domain.InvoiceManagement;
using Backend.API.Domain.Services.InvoiceManagement;
using Backend.API.Dtos.InvoiceManagement;
using Backend.API.ErrorManagement;
using Backend.API.Infrastructure.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.InvoiceManagement
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    //TODO: add business service layer. To much responsibilities for a controller
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper<InvoiceItem, Invoice> _invoiceMapper;
        private readonly IMapper<Invoice, InvoiceItem> _invoiceItemMapper;

        private readonly IErrorService _errorService;

        public InvoiceController(IInvoiceRepository invoiceRepository,
            IMapper<InvoiceItem, Invoice> invoiceMapper,
            IErrorService errorService, IMapper<Invoice, InvoiceItem> invoiceItemMapper)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceMapper = invoiceMapper;
            _errorService = errorService;
            _invoiceItemMapper = invoiceItemMapper;
        }

        // POST: api/Invoice - Een factuur met factuurlijnen te maken
        [HttpPost]
        public async Task<ActionResult<InvoiceItem>> Post([FromBody] InvoiceItem value)
        {
            try
            {
                if (value == null) //TODO: add validators
                    return new BadRequestResult();

                var newInvoice = _invoiceMapper.Map(value);
                await _invoiceRepository.AddAsync(newInvoice);

                return CreatedAtAction(nameof(InvoiceItem), new { id = newInvoice.Id }, value);
            }
            catch (Exception e) //TODO middleware            
            {
                return _errorService.Catch(e);
            }
        }

        // PUT: api/Invoice/5 - Een status te updaten van een factuur  
        [HttpPut("{id}")]
        public async Task<ActionResult<InvoiceItem>> Put(long id, [FromBody] string invoiceState)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(invoiceState)) //TODO: add validators
                    return new BadRequestResult();

                //TODO: "We should talk about the state of an invoice; It' wasn't in the exercise... So what is this state exactly?"
                var invoice =  _invoiceRepository.Get(id);
                invoice.Summary += "State Added: " + invoiceState + System.Environment.NewLine;

                await _invoiceRepository.UpdateAsync(invoice);

                var invoiceItem = _invoiceItemMapper.Map(invoice);

                return new OkObjectResult(invoiceItem);
            }
            catch (Exception e) //TODO middleware            
            {
                return _errorService.Catch(e);
            }
        }
    }
}
