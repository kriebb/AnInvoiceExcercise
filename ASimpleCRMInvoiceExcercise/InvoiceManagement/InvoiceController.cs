using Backend.API.Domain.InvoiceManagement;
using Backend.API.Domain.Services;
using Backend.API.Domain.Services.InvoiceManagement;
using Backend.API.Dtos.InvoiceManagement;
using Backend.API.Infrastructure.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.InvoiceManagement
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper<Invoice, InvoiceDto> _invoiceMapper;

        public InvoiceController(IInvoiceRepository invoiceRepository, 
            IMapper<Invoice,InvoiceDto> invoiceMapper
            )
        {
            _invoiceRepository = invoiceRepository;
            _invoiceMapper = invoiceMapper;
        }
        // POST: api/Invoice - Een factuur met factuurlijnen te maken
        [HttpPost]
        public void Post([FromBody] InvoiceDto value)
        {
            var invoice = _invoiceMapper.Map(value);
            _invoiceRepository.Add(invoice);
        }

        // PUT: api/Invoice/5 - Een status te updaten van een factuur  
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string invoiceState)
        {
            //TODO: "We should talk about the state of an invoice; It' wasn't in the exercise... So what is this state exactly?"
            var invoice = _invoiceRepository.Get(id);
            invoice.Summary += "State Added: " + invoiceState + System.Environment.NewLine;


        }

    }


}
