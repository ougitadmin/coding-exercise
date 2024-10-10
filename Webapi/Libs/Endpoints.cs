using Microsoft.AspNetCore.Mvc;
using Webapi.Schemas;
using Webapi.Libs.User.Endpoints;
using Webapi.Libs.Test.Endpoints;
using System.ComponentModel;

namespace Webapi.Libs {
    public static class Endpoints {
        public static void ConfigureEndpoints(this WebApplication app) {
            app.MapPost("/user/login",
                    async ([FromBody] LoginSchema loginRequest, [FromServices] Login userLogin)
                        => await userLogin.LoginAsync(loginRequest)
                )
                .WithName("User/Login")
                .WithOpenApi();

            app.MapGet("/test/for-each",
                    async ([FromQuery, DefaultValue(10)] int? limit)
                        => Results.Ok(await ForEachAsyncEndpoint.ForEachAsync(limit ?? 5)))
                .WithName("Test/ForEach")
                .WithOpenApi();

            app.MapGet("/test/for",
                    async ([FromQuery, DefaultValue(10)] int? limit)
                        => Results.Ok(await ForAsyncEndpoint.ForAsync(limit ?? 5)))
                .WithName("Test/For")
                .WithOpenApi();

            app.MapGet("/test/linq",
                    async ([FromQuery, DefaultValue(10)] int? limit)
                        => Results.Ok(await LinqAsyncEndpoint.LinqAsync(limit ?? 5)))
                .WithName("Test/LINQ")
                .WithOpenApi();
            
            app.MapGet("/test/switch",
                    async ([FromQuery, DefaultValue(10)] int? limit)
                        => Results.Ok(await SwitchAsyncEndpoint.SwitchAsync(limit ?? 5)))
                .WithName("Test/Switch")
                .WithOpenApi();
            
            app.MapGet("/test/if-else-if-else",
                    async ([FromQuery, DefaultValue(10)] int? limit)
                        => Results.Ok(await IfElseIfElseAsyncEndpoint.IfElseIfElseAsync(limit ?? 5)))
                .WithName("Test/IfElseIfElse")
                .WithOpenApi();
            
            app.MapGet("/test/switch-as-linq", 
                    async ([FromQuery, DefaultValue(10)] int? limit)
                        => Results.Ok(await SwitchAsLinqAsyncEndpoint.SwitchAsLinqAsync(limit ?? 5)))
                .WithName("Test/SwitchAsLinq")
                .WithOpenApi();
        }
    }
}