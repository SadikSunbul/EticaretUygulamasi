using EticaretApi.Application.Validaters._Product;
using EticaretApi.Infrastructure;
using EticaretApi.Infrastructure.Filters;
using EticaretApi.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

//kend� olusturdugumuz ver�y� tet�kl�yoz 
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureService();
//Crospolitikalar�
//builder.Services.AddCors(options=>options.AddDefaultPolicy(policy=>policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())); //burada her yerden ver� al�r kullan�lmaz bu 
builder.Services.AddCors(options=>options.AddDefaultPolicy(policy=>policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod())); //sadece bu s�teden gelen ver�ler� al�cakt�r 


//Validation
builder.Services.AddControllers().AddFluentValidation(c=>c.RegisterValidatorsFromAssemblyContaining<CreateProductValidater>());
//builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()).AddFluentValidation(configration => configration.RegisterValidatorsFromAssemblyContaining<CreateProductValidater>())
  //  .ConfigureApiBehaviorOptions(option => option.SuppressModelStateInvalidFilter = true);//fluent val�dater � akt�flest�rd�k sadece product creat �c�n deg�l d�gerler�n�de yaz�nca ayn� d�z�nde olduklar� �c�n heps�n� ceker
//burada true yapt�g�m�z �c�n filter olusturmam�z gerek�r b�r servister filter o zmn Infrastructure de olusturcaz
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Uste yapm�s oldugumu cors pol�t�kas�n�n m�del wares�n� ekled�k buraya 
app.UseCors();
//wwwroot �c�n kullan�l�r 
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#region Cros Politikas� nedir

//Apiler uzer�nden browser�n alm�s oldugu dosal same-org�n pol�cy onlem�n� haf�fletme pol�t�kas�d�r

//Egerk� cl�ent uygulamas� browser da cal�s�yorsa burada cros pol�t�kas� soz konusudur 
//Browserlar�n bu davran�s�na same-orgin pol�tc den�r
//Browserlar dogal olarak alm�s olduklar� Same-orgin pol�cy onlem�n� asab�lmnek i�in cl�ent uygulamas�n�n �stek gonderd�gi hedef s�teye org�ne endpo�nte api e oncel�kl� g�d�p bu s�teden gelecek olan requestlere �z�n olup olmad�g�n� soracakard�ndan �steg� ya �ptal ed�cek ya da �z�n ver�cekt�r.

#endregion

#region Filter olusrturma

//burada true yapt�g�m�z �c�n filter olusturmam�z gerek�r b�r servister filter o zmn Infrastructure de olusturcaz i�inde b� class olsuturup class� IAsyncActionFilter dan turetcez 
/*
  if(!context.ModelState.IsValid) => hata var �se 
            {
                var eror=context.ModelState   =>hatalar� al d�k 
                    .Where(x => x.Value.Errors.Any())  =>deger� eror olanlar� sect�k 
                    .ToDictionary(e => e.Key, e => e.Value.Errors.Select(e =>e.ErrorMessage)).ToArray(); =>ToDictionary turunden key value sekl�nde erorlar�n mesaj�n� cekt�k array e att�k 

                context.Result=new BadRequestObjectResult(eror); //ger�ye dondurduk 
            }
            await next();   =>buras� b� sonrak� f�ltera bakar 
 */

//sonra buras� b� serv�s oldugu �c�n eklemek laz�m builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()) bunun eklenmes� gerek

#endregion