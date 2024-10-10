using System.ComponentModel;

namespace Webapi.Libs.Test.Endpoints {
    public class ForAsyncEndpoint : TestEndpointsBase {
        public static async Task<List<object>> ForAsync([DefaultValue(10)] int limit = 5) {
            var responses = new List<object>();

            await Task.Run(() => {
                for (var i = 0; i < Math.Min(limit, Names.Length); i++) {
                    var response = new {
                        name = Names[i],
                        age = Ages[i],
                        isAdult = Ages[i] >= 18
                    };
                    responses.Add(response);
                }
            });

            return responses;
        }
    }
}