using EticaretApi.Persistence;

var builder = WebApplication.CreateBuilder(args);

//kend� olusturdugumuz ver�y� tet�kl�yoz 
builder.Services.AddPersistenceServices();

//Crospolitikalar�
//builder.Services.AddCors(options=>options.AddDefaultPolicy(policy=>policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())); //burada her yerden ver� al�r kullan�lmaz bu 
builder.Services.AddCors(options=>options.AddDefaultPolicy(policy=>policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod())); //sadece bu s�teden gelen ver�ler� al�cakt�r 

builder.Services.AddControllers();
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