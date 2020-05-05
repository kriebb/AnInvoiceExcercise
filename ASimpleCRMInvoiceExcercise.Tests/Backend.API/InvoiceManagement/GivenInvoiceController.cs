using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Backend.API.CustomerManagement;
using Backend.API.Data.Generator;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Domain.InvoiceManagement;
using Backend.API.Domain.Services.CustomerManagement;
using Backend.API.Domain.Services.InvoiceManagement;
using Backend.API.Dtos.CustomerManagement;
using Backend.API.Dtos.InvoiceManagement;
using Backend.API.ErrorManagement;
using Backend.API.ErrorManagement.Impl;
using Backend.API.Infrastructure.Mappings.BootstrapAutoMapper;
using Backend.API.InvoiceManagement;
using Bogus;
using FluentAssertions;
using FluentAssertions.Common;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Backend.API.Tests.Backend.API.InvoiceManagement
{
    /*
     *  - Een factuur met factuurlijnen te maken
        - Een status te updaten van een factuur  
     *
     */
    public class GivenInvoiceController
    {
        private InvoiceController _sut;
        private Guid _notExistingId;
        private Guid _existingId;
        private IInvoiceRepository _invoiceRepository;

        public GivenInvoiceController()
        {
            var fixture = new Fixture();

            var mapperFactory = new AutoMapperFactory();

            _invoiceRepository = Substitute.For<IInvoiceRepository>();


            IErrorService errorService = new ErrorService();
            fixture.Inject(_invoiceRepository);
            fixture.Inject(errorService);
            fixture.Customize(new AutoNSubstituteCustomization() { ConfigureMembers = true, GenerateDelegates = true });
            fixture.Inject(mapperFactory.CreateMapper<InvoiceItem, Invoice>()); //Is tested in other class, so used for easyness at the moment
            fixture.Inject(mapperFactory.CreateMapper<Invoice, InvoiceItem>());
            fixture.Inject(new ControllerContext());
            _sut = fixture.Create<InvoiceController>();
        }

        [Fact]
        public async Task AddingAnInvoice_ShouldBeSaved()
        {
            var invoiceItem = ApiDtoGenerator.InvoiceItemGenerator().Generate();
            var apiResult = await _sut.Post(invoiceItem);

            using (new AssertionScope())
            {
                apiResult.Result.Should().BeOfType<CreatedAtActionResult>();

                await _invoiceRepository.Received(1).AddAsync(Arg.Any<Invoice>());
            }
        }
        [Fact]
        public async Task AddingAnNullInvoice_ShouldReturnABadRequest()
        {
            var apiResult = await _sut.Post(null);

            apiResult.Result.Should().BeOfType<BadRequestResult>();
        }
        [Fact]
        public async Task UpdatingAnInvoiceState_ShouldBeAddedToTheSummary()
        {
            var invoice = DomainGenerator.InvoiceGenerator().Generate();
            _invoiceRepository.Get(invoice.Id).Returns(invoice);

            var apiResult = await _sut.Put(invoice.Id, "someState");

            using (new AssertionScope())
            {
                apiResult.Result.Should().BeOfType<OkObjectResult>();
                invoice.Summary.Should().Contain("someState");
            }
        }
        [Theory]
        [InlineData(null), InlineData(" "), InlineData("")]
        public async Task UpdatingAnInvoiceStateAsNull_StringEmpty_WhiteSpace_ShouldReturnBadRequest(string emptyStatus)
        {
            var invoice = DomainGenerator.InvoiceGenerator().Generate();
            _invoiceRepository.Get(invoice.Id).Returns(invoice);

            var apiResult = await _sut.Put(invoice.Id, emptyStatus);

            apiResult.Result.Should().BeOfType<BadRequestResult>();

        }
    }
}
