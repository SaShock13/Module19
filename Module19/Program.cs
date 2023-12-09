using Module19.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
var app = builder.Build();

app.UseMvc(r =>
{
    r.MapRoute(
    name: "default",
    template: "{controller=People}/{action=Index}");
}
    );

app.Run();
