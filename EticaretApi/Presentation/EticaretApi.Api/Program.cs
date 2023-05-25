using EticaretApi.Application.Validaters._Product;
using EticaretApi.Infrastructure;
using EticaretApi.Infrastructure.Filters;
using EticaretApi.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

//kendý olusturdugumuz verýyý tetýklýyoz 
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureService();
//Crospolitikalarý
//builder.Services.AddCors(options=>options.AddDefaultPolicy(policy=>policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())); //burada her yerden verý alýr kullanýlmaz bu 
builder.Services.AddCors(options=>options.AddDefaultPolicy(policy=>policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod())); //sadece bu sýteden gelen verýlerý alýcaktýr 


//Validation
builder.Services.AddControllers().AddFluentValidation(c=>c.RegisterValidatorsFromAssemblyContaining<CreateProductValidater>());
//builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()).AddFluentValidation(configration => configration.RegisterValidatorsFromAssemblyContaining<CreateProductValidater>())
  //  .ConfigureApiBehaviorOptions(option => option.SuppressModelStateInvalidFilter = true);//fluent valýdater ý aktýflestýrdýk sadece product creat ýcýn degýl dýgerlerýnýde yazýnca ayný dýzýnde olduklarý ýcýn hepsýný ceker
//burada true yaptýgýmýz ýcýn filter olusturmamýz gerekýr býr servister filter o zmn Infrastructure de olusturcaz
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Uste yapmýs oldugumu cors polýtýkasýnýn mýdel waresýný ekledýk buraya 
app.UseCors();
//wwwroot ýcýn kullanýlýr 
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#region Cros Politikasý nedir

//Apiler uzerýnden browserýn almýs oldugu dosal same-orgýn polýcy onlemýný hafýfletme polýtýkasýdýr

//Egerký clýent uygulamasý browser da calýsýyorsa burada cros polýtýkasý soz konusudur 
//Browserlarýn bu davranýsýna same-orgin polýtc denýr
//Browserlar dogal olarak almýs olduklarý Same-orgin polýcy onlemýný asabýlmnek için clýent uygulamasýnýn ýstek gonderdýgi hedef sýteye orgýne endpoýnte api e oncelýklý gýdýp bu sýteden gelecek olan requestlere ýzýn olup olmadýgýný soracakardýndan ýstegý ya ýptal edýcek ya da ýzýn verýcektýr.

#endregion

#region Filter olusrturma

//burada true yaptýgýmýz ýcýn filter olusturmamýz gerekýr býr servister filter o zmn Infrastructure de olusturcaz içinde bý class olsuturup classý IAsyncActionFilter dan turetcez 
/*
  if(!context.ModelState.IsValid) => hata var ýse 
            {
                var eror=context.ModelState   =>hatalarý al dýk 
                    .Where(x => x.Value.Errors.Any())  =>degerý eror olanlarý sectýk 
                    .ToDictionary(e => e.Key, e => e.Value.Errors.Select(e =>e.ErrorMessage)).ToArray(); =>ToDictionary turunden key value seklýnde erorlarýn mesajýný cektýk array e attýk 

                context.Result=new BadRequestObjectResult(eror); //gerýye dondurduk 
            }
            await next();   =>burasý bý sonraký fýltera bakar 
 */

//sonra burasý bý servýs oldugu ýcýn eklemek lazým builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()) bunun eklenmesý gerek

#endregion