namespace Webapi.Schemas;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

public record LoginSchema(
    [property: Required, DefaultValue("carlfearby@me.com"), SwaggerSchema] string Email,
    [property: Required, DefaultValue("password1234!"), SwaggerSchema] string Password
);