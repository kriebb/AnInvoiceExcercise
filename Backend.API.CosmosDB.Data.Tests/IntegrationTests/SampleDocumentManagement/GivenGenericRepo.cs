using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.API.CosmosDB.Data.Services.Infrastructure.Impl;
using Backend.API.CosmosDB.Data.Tests.IntegrationTests.Infrastructure;
using Bogus;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Backend.API.CosmosDB.Data.Tests.IntegrationTests.SampleDocumentManagement
{
    //TODO: automate DOCKER around cosmos db emulation https://github.com/Azure/azure-cosmos-db-emulator-docker
    //https://docs.microsoft.com/en-gb/azure/cosmos-db/local-emulator#developing-with-the-emulator
    //https://github.com/joudot/CosmosDB-EmulatorTesting-VSTS
    //cfr. mongo2go.exe



    /*https://hub.docker.com/r/microsoft/azure-cosmosdb-emulator/
     *
     docker rm -existingContainerId
      cmd
set containerName=azure-cosmosdb-emulator
set hostDirectory=%LOCALAPPDATA%\azure-cosmosdb-emulator.hostd
md %hostDirectory% 2>nul
      docker run --name %containerName% --memory 2GB --mount "type=bind,source=%hostDirectory%,destination=C:\CosmosDB.Emulator\bind-mount"  --interactive --tty -p 8081:8081 -p 8900:8900 -p 8901:8901 -p 8979:8979 -p 10250:10250 -p 10251:10251 -p 10252:10252 -p 10253:10253 -p 10254:10254 -p 10255:10255 -p 10256:10256 -p 10350:10350 microsoft/azure-cosmosdb-emulator
     
     cd %LOCALAPPDATA%\azure-cosmosdb-emulator.hostd
powershell .\importcert.ps1
     */
    public class GivenGenericRepo : IClassFixture<CosmosDBFixture>
    {
        private readonly CosmosDBFixture _cosmosDbFixture;
        private DocumentDbRepository<SampleDocument> _sut;

        public GivenGenericRepo(CosmosDBFixture cosmosDbFixture)
        {
            _cosmosDbFixture = cosmosDbFixture ?? throw new ArgumentNullException(nameof(cosmosDbFixture));
            if (_cosmosDbFixture != null)
                _sut = new DocumentDbRepository<SampleDocument>(_cosmosDbFixture.CosmosDbConfig);
        }

        [Fact]
        public async Task WhenAddingASampleDocument_ShouldBeRetrieved()
        {
            var faker = new Faker();

            var newSampleDocumentCreated = new SampleDocument()
            {
                SampleString = faker.Random.String(1, 20),
                SampleCollection = faker.Random.WordsArray(1, 20).ToList()

            };

            var sampleDocumentCreated = await _sut.CreateAsync(newSampleDocumentCreated);

            var sampleDocumentGetted = _sut.GetById(sampleDocumentCreated.Id);


            using (new AssertionScope())
            {
                sampleDocumentCreated.Should().NotBe(null);
                sampleDocumentCreated.SampleString.Should().Be(newSampleDocumentCreated.SampleString);
                sampleDocumentCreated.SampleCollection.Should()
                    .BeEquivalentTo(newSampleDocumentCreated.SampleCollection);


                sampleDocumentGetted.Should().NotBe(null);
                sampleDocumentGetted.SampleString.Should().Be(newSampleDocumentCreated.SampleString);
                sampleDocumentGetted.SampleCollection.Should()
                    .BeEquivalentTo(newSampleDocumentCreated.SampleCollection);

            }


        }

        [Fact]
        public async Task WhenUpdatingASampleDocument_UpdatedDocumentShouldBeRetrieved()
        {
            var faker = new Faker();

            string sampleDocumentCreatedId;

            using (new ArrangeAction())
            {
                var newSampleDocumentCreated = new SampleDocument()
                {
                    SampleString = faker.Random.Words(3),
                    SampleCollection = faker.Random.WordsArray(1, 20).ToList()

                };

                var sampleDocumentCreated = await _sut.CreateAsync(newSampleDocumentCreated);
                sampleDocumentCreatedId = sampleDocumentCreated.Id;
            }

            var sampleDocumentToBeUpdated = new SampleDocument(); //new instance.
            sampleDocumentToBeUpdated.Id = sampleDocumentCreatedId;
            sampleDocumentToBeUpdated.SampleString = faker.Random.Words(4);
            sampleDocumentToBeUpdated.SampleCollection = faker.Random.WordsArray(21, 40).ToList();

            var sampleDocumentIsUpdated = await _sut.UpdateAsync(sampleDocumentToBeUpdated.Id, sampleDocumentToBeUpdated);


            using (new AssertionScope())
            {
                //should be the same as the updated one
                sampleDocumentIsUpdated.Should().NotBe(null);
                sampleDocumentIsUpdated.SampleString.Should().Be(sampleDocumentToBeUpdated.SampleString);
                sampleDocumentIsUpdated.SampleCollection.Should()
                    .BeEquivalentTo(sampleDocumentToBeUpdated.SampleCollection);
            }
        }
        [Fact]
        public async Task WhenDeleting_ShouldntBeAbleToGet()
        {
            var faker = new Faker();

            var newSampleDocumentCreated = new SampleDocument()
            {
                SampleString = faker.Random.Words(4),
                SampleCollection = faker.Random.WordsArray(1, 20).ToList()

            };

            var sampleDocumentCreated = await _sut.CreateAsync(newSampleDocumentCreated);


            await _sut.DeleteAsync(sampleDocumentCreated.Id);

            var deletedDocument = _sut.GetById(sampleDocumentCreated.Id);

            deletedDocument.Should().Be(null);

        }
    }
}