using EticaretApi.Persistence;

var builder = WebApplication.CreateBuilder(args);

//kendý olusturdugumuz verýyý tetýklýyoz 
builder.Services.AddPersistenceServices();

//Crospolitikalarý
//builder.Services.AddCors(options=>options.AddDefaultPolicy(policy=>policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())); //burada her yerden verý alýr kullanýlmaz bu 
builder.Services.AddCors(options=>options.AddDefaultPolicy(policy=>policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod())); //sadece bu sýteden gelen verýlerý alýcaktýr 

builder.Services.AddControllers();
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