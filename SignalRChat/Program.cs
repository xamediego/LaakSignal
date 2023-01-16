using SignalRChat.Socket;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = "Allowed Cors";

builder.Services.AddCors(options =>
{
    options.AddPolicy(allowedOrigins,
        policy =>
        {
            policy.WithOrigins(builder.Configuration.GetValue<string>("ShowApiCors") ?? "https://localhost:7001")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            
            policy.WithOrigins(builder.Configuration.GetValue<string>("FrontEndCors") ?? "http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddSignalR();

builder.Services.AddControllers();

var app = builder.Build();

app.UseStaticFiles();

app.UseDeveloperExceptionPage();

app.UseCors(allowedOrigins);

app.UseRouting();

app.UseHttpsRedirection();

app.UseEndpoints(endpoints => { endpoints.MapHub<ShowHub>("/chatApi/showChat"); });

app.Run();
