namespace Webapi.Libs.Test.Endpoints {
    public class IfElseIfElseAsyncEndpoint : TestEndpointsBase {
        public static async Task<List<object>> IfElseIfElseAsync(int limit = 5) {
            var random = new Random();
            var responses = await Task.Run(() =>
                Names.Select((name, index) => {
                        string colour;
                        var randomValue = random.Next(3);
                        if (randomValue == 0) {
                            colour = ShirtColours[0];
                        }
                        else if (randomValue == 1) {
                            colour = ShirtColours[1];
                        }
                        else if (randomValue == 2) {
                            colour = ShirtColours[2];
                        }
                        else {
                            colour = ShirtColours[3];
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