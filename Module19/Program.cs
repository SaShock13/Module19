var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



//app.MapGet("/", () => "Hello World!");
//app.UseWelcomePage();
app.Run(async (context) =>
    {
        //var response = context.Response;
        //response.StatusCode = 404;
        //response.ContentType = "text/html; charset=utf-8"; ;
        //await context.Response.WriteAsync("<h2>Hello METANIT.COM</h2><h3>Welcome to ASP.NET Core</h3>");


        context.Response.ContentType = "text/html; charset=utf-8";
        var stringBuilder = new System.Text.StringBuilder("<table>");

        foreach (var header in context.Request.Headers)
        {
            stringBuilder.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
        }
        stringBuilder.Append("</table>");
        await context.Response.WriteAsync(stringBuilder.ToString());
    }
    ) ;




app.Run();
