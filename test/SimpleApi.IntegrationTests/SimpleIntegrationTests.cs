using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using SimpleApi.Models;
using Xunit;

namespace SimpleApi.IntegrationTests
{
    public class SimpleIntegrationTests
    {
        private readonly TestServer _testServer;
        private readonly HttpClient _testClient;

        public SimpleIntegrationTests()
        {
            _testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _testClient = _testServer.CreateClient();
        }

        [Fact]
        public async Task TestValuesPostAndGetAll()
        {
            var valueModel = new ValueModel 
            {
                Value = "Test value"
            };

            await PostValueModel(valueModel);

            var valueModels = await GetAllValueModels();

            Assert.Equal(1, valueModels.Count);
            AssertValueModelInList(0,valueModels,valueModel);
        }

        [Fact]
        public async Task TestValuesPostTwiceAndGetAll()
        {
            // arrange
            var valueModel = new ValueModel
            {
                Value = "Test value"
            };
            var valueModel2 = new ValueModel
            {
                Value = "Test value 2"
            };

            // act
            await PostValueModel(valueModel);
            await PostValueModel(valueModel2);
            
            // asset
            var valueModels = await GetAllValueModels();

            Assert.Equal(2, valueModels.Count);
            AssertValueModelInList(0,valueModels,valueModel);
            AssertValueModelInList(1,valueModels,valueModel2);
        }

        private async Task PostValueModel(ValueModel valueModel)
        {
            var stringContent = SerializeToString(valueModel);

            var postMessage = await _testClient.PostAsync("/api/values", stringContent);
            postMessage.EnsureSuccessStatusCode();
        }     

        private StringContent SerializeToString(ValueModel valueModel)
        {
            return new StringContent(
                JsonConvert.SerializeObject(valueModel),
                UnicodeEncoding.UTF8,
                "application/json"
            );
        }

        private async Task<IList<ValueModel>> GetAllValueModels()
        {
            var getMessage = await _testClient.GetAsync("/api/values");
            getMessage.EnsureSuccessStatusCode();

            var raw = await getMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ValueModel>>(raw);
        }

        private void AssertValueModelInList(int position, IList<ValueModel> actual, ValueModel expected)
        {
            Assert.Equal(expected.Value, actual[position].Value);
            Assert.Equal(position, actual[position].Id);
        }
    }
}
