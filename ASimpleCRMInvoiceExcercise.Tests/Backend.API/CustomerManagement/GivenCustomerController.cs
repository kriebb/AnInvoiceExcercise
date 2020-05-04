using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Kernel;
using Backend.API.CustomerManagement;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Domain.Services.CustomerManagement;
using Backend.API.Dtos.CustomerManagement;
using Backend.API.ErrorManagement;
using Backend.API.ErrorManagement.Impl;
using Backend.API.Infrastructure.Mappings;
using Backend.API.Infrastructure.Mappings.BootstrapAutoMapper;
using Backend.API.Tests.Backend.API.InvoiceManagement;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Xunit;
using NSubstitute;
using NSubstitute.Exceptions;

namespace Backend.API.Tests.Backend.API.CustomerManagement
{

    /*
     *  - Klanten aan te maken
        - Een contactgegeven (email/telefoon) toe te wijzen aan een klant
     */
    public class GivenCustomerController
    {
        private CustomerController _sut;
        private long _notExistingId;
        private long _existingId;

        public GivenCustomerController()
        {
            var fixture = new Fixture();

            var mapperFactory = new AutoMapperFactory();
            var customerRepository = Substitute.For<ICustomerRepository>(); //maybe forse a dictionary implementation instead of an inline mock


            _notExistingId = 0;
            var customer = Generator.CustomerGenerator().Generate();
            _existingId = customer.Id;

            Dictionary<long, Customer> repo = new Dictionary<long, Customer>();
            repo.Add(_notExistingId, null);
            repo.Add(_existingId, customer);


            customerRepository.GetAsync(0).Returns(Task.FromResult(null as Customer));
            customerRepository.GetAsync(customer.Id).Returns(Task.FromResult(repo[customer.Id]));
            customerRepository
                .When(cr =>cr.UpdateAsync(Arg.Any<Customer>()))
                .Do(callInfo =>
           {
               var callInfoCustomer = callInfo.Arg<Customer>();
               if (callInfoCustomer.Id == 0)
               {
                   while (!repo.TryAdd(callInfoCustomer.Id, callInfoCustomer))
                   {
                       callInfoCustomer.Id = new Faker().Random.Number(0, int.MaxValue);
                   }
               }
               else
               {
                   if (repo.ContainsKey(callInfoCustomer.Id))
                       repo[callInfoCustomer.Id] = callInfoCustomer;
                   else
                   {
                       throw new ArgumentException("Couldnt update repo, wasn't added");
                   }
               }
           });

            IErrorService errorService = new ErrorService();
            fixture.Inject(customerRepository);
            fixture.Inject(errorService);
            fixture.Customize(new AutoNSubstituteCustomization() { ConfigureMembers = true, GenerateDelegates = true });
            fixture.Inject(mapperFactory.CreateMapper<CustomerItem, Customer>()); //Is tested in other class, so used for easyness at the moment
            fixture.Inject(mapperFactory.CreateMapper<ContactInfoItem, ContactInfo>());
            fixture.Inject(new ControllerContext());
            _sut = fixture.Create<CustomerController>();
        }
        [Fact]
        public async Task WhenAddingNullAsCustomer_ShouldReturnBadRequest()
        {
            var apiResult = await _sut.Post(null as CustomerItem);
            Assert.IsType<BadRequestResult>(apiResult.Result);


        }
        [Fact]
        public async Task WhenAddingAValidCustomer_ShouldReturnOK()
        {
            var nonExistingCustomer = Generator.CustomerItemGenerator().Generate();
            nonExistingCustomer.Id = _notExistingId;

            var apiResult = await _sut.Post(nonExistingCustomer);
            Assert.IsType<CreatedAtActionResult>(apiResult.Result);

        }

        [Fact]
        public async Task WhenAddingContactInfoToNonExistingCustomer_ShouldReturnNotFound()
        {
            var contactInfoItem = Generator.ContactInfoItemGenerator();
            var apiResult = await _sut.Put(_notExistingId, contactInfoItem);
            Assert.IsType<NotFoundResult>(apiResult.Result);
        }

        [Fact]
        public async Task WhenAddingNullAsContactDataToACustomer_ShouldReturnBadRequest()
        {
            var apiResult = await _sut.Put(_notExistingId, null as ContactInfoItem);
            Assert.IsType<BadRequestResult>(apiResult.Result);
        }

        [Fact]
        public async Task WhenAddingValidContactDataToACustomer_ItShouldReturnOIk()
        {
            var contactInfoItem = Generator.ContactInfoItemGenerator();

            var apiResult = await _sut.Put(_existingId, contactInfoItem);
            Assert.IsType<OkResult>(apiResult.Result);
        }

        [Fact]
        public async Task WhenAddingDuplicateContactDataToACustomer_ItShouldReturnBadRequest()
        {
            var contactInfoItem1 = Generator.ContactInfoItemGenerator().Generate();

            var apiResult1 = await _sut.Put(_existingId, contactInfoItem1);
            Assert.IsType<OkResult>(apiResult1.Result);
            var apiResult2 = await _sut.Put(_existingId, contactInfoItem1);
            Assert.IsType<BadRequestObjectResult>(apiResult2.Result);
        }


    }
}
