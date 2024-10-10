namespace Webapi.Libs.Test.Endpoints {
    public class SwitchAsLinqAsyncEndpoint : TestEndpointsBase {
        public static async Task<List<object>> SwitchAsLinqAsync(int limit = 5) {
            var random = new Random();
            var responses = await Task.Run(() =>
                Names.Select((name, index) => {
                        var colour = random.Next(3) switch {
                            0 => ShirtColours[0],
                            1 => ShirtColours[1],
                            2 => ShirtColours[2],
                            _ => ShirtColours[3]
                        };
                        return new {
                            name,
                            age = Ages[index],
                            tShirtColour = colour,
                            isAdult = Ages[index] >= 18
                        };
                    })
                    .Take(limit)
                    .Cast<object>()
                    .ToList()
            );

            return responses;
        }
    }
}