using System.ComponentModel;

namespace Webapi.Libs.Test.Endpoints {
    public class ForEachAsyncEndpoint : TestEndpointsBase {
        public static async Task<List<object>> ForEachAsync([DefaultValue(10)] int limit = 5) {
            var responses = new List<object>();
            var random = new Random();

            await Task.Run(() => {
                foreach (var name in Names.Take(limit)) {
                    var index = Array.IndexOf(Names, name);
                    var response = new {
                        name,
                        age = Ages[index],
                        active = random.Next(2) == 0,
                        isAdult = Ages[index] >= 18
                    };
                    responses.Add(response);
                }
            });

            return responses;
        }
    }
}