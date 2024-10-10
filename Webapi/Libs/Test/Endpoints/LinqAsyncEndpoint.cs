using System.ComponentModel;

namespace Webapi.Libs.Test.Endpoints {
    public class LinqAsyncEndpoint : TestEndpointsBase {
        public static async Task<List<object>> LinqAsync([DefaultValue(10)] int limit = 5) {
            var random = new Random();

            var responses = await Task.Run(() =>
                Names.Select((name, index) => new {
                        name,
                        age = Ages[index],
                        active = random.Next(2) == 0,
                        isAdult = Ages[index] >= 18
                    })
                    .Take(limit)
                    .Cast<object>()
                    .ToList()
            );

            return responses;
        }
    }
}