namespace Webapi.Libs.Test.Endpoints {
    public class SwitchAsyncEndpoint : TestEndpointsBase {
        public static async Task<List<object>> SwitchAsync(int limit = 5) {
            var random = new Random();
            var responses = await Task.Run(() =>
                Names.Select((name, index) => {
                        string colour;
                        switch (random.Next(3)) {
                            case 0:
                                colour = ShirtColours[0];
                                break;
                            case 1:
                                colour = ShirtColours[1];
                                break;
                            case 2:
                                colour = ShirtColours[2];
                                break;
                            default:
                                colour = ShirtColours[3];
                                break;
                        }
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