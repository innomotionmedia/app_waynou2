// Copyright (c) Microsoft Corporation. All Rights Reserved.
// Licensed under the MIT License.

//using Microsoft.AspNetCore.Datasync;
//using Microsoft.EntityFrameworkCore;
//using TodoAppService.NET6.Db;
//
//var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//
//if (connectionString == null)
//{
//    throw new ApplicationException("DefaultConnection is not set");
//}
//
//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddDatasyncControllers();
//
//var app = builder.Build();
//
//// Initialize the database
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    await context.InitializeDatabaseAsync().ConfigureAwait(false);
//}
//
//// Configure and run the web service.
//app.MapControllers();
//app.Run();



using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Datasync;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using System.Net;
using TodoAppService.NET6.Db;

var builder = WebApplication.CreateBuilder(args);
IdentityModelEventSource.ShowPII = true;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null)
{
    throw new ApplicationException("DefaultConnection is not set");
}

ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApi(options =>
           {
               builder.Configuration.Bind("AzureAdB2C", options);
               options.TokenValidationParameters.NameClaimType = "name";
           },
   options => { builder.Configuration.Bind("AzureAdB2C", options); });
// End of the Microsoft Identity platform block    

builder.Services.AddControllers();

builder.Services.AddDbContext<MigrationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatasyncControllers();

var app = builder.Build();
// Initialize the database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MigrationDbContext>();
    //await context.InitializeDatabaseAsync().ConfigureAwait(false);
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
//app.MapRazorPages();
app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.Run();





/*
Add the tool to facilitate EF work. EF power tool
Create the db context. keep only the context and delete the generated classes
hide the old context for this sample purpose
using the new context generate a migration, existing tables will be deteccted as create. We need to delete this migration as it is our baseline.
add the new classes MyItem on this example
generate new migration. The MyItem will be coded as a migration.
apply the migration to the DB
test the result.

 

*/
