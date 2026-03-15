using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RentAMovie.Data;
using RentAMovie.Services;
using RentAMovie.Models;

//Създаване на цялото приложение
//builder - обект, през него ние си конфигурираме нещата за сайта
//(базата, аутентикацията за логване и др такива)
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); //казва това приложение изполва mvc (Controler + View)

builder.Services.AddDbContext<AppDbContext>(options =>
{
    //взима connection stringa за връзката към базата ни от appsetting.json-a
    var cs = builder.Configuration.GetConnectionString("DefaultConnection");
    //свързва с mysql, този ред казва използвай Mysql, aвтоматично ми разпознай версия на сървъра
    options.UseMySql(cs, ServerVersion.AutoDetect(cs));
});

builder.Services.AddScoped<UserService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/Login";
    });

builder.Services.AddAuthorization();

var app = builder.Build(); //създава самото приложение на база на това което имаме по дефоут при създаване на проекта

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Movies.Any())
    {
        context.Movies.AddRange(
            new Movie
            {
                Genre = "Action",
                Title = "The Dark Knight",
                Year = 2008,
                PricePerDay = 3.99m,
                ImageUrl = "https://image.tmdb.org/t/p/w500/qJ2tW6WMUDux911r6m7haRef0WH.jpg",
                AvailableCopies = 5
            },
            new Movie
            {
                Genre = "Sci-Fi",
                Title = "Inception",
                Year = 2010,
                PricePerDay = 4.99m,
                ImageUrl = "https://image.tmdb.org/t/p/w500/9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg",
                AvailableCopies = 4
            },
            new Movie
            {
                Genre = "Drama",
                Title = "The Shawshank Redemption",
                Year = 1994,
                PricePerDay = 2.99m,
                ImageUrl = "https://image.tmdb.org/t/p/w500/q6y0Go1tsGEsmtFryDOJo3dEmqu.jpg",
                AvailableCopies = 6
            },
            new Movie
            {
                Genre = "Crime",
                Title = "The Godfather",
                Year = 1972,
                PricePerDay = 3.49m,
                ImageUrl = "https://image.tmdb.org/t/p/w500/3bhkrj58Vtu7enYsRolD1fZdja1.jpg",
                AvailableCopies = 3
            },
            new Movie
            {
                Genre = "Sci-Fi",
                Title = "The Matrix",
                Year = 1999,
                PricePerDay = 3.99m,
                ImageUrl = "https://image.tmdb.org/t/p/w500/f89U3ADr1oiB1s9GkdPOEpXUk5H.jpg",
                AvailableCopies = 7
            },
            new Movie
            {
                Genre = "Adventure",
                Title = "The Lord of the Rings: The Fellowship of the Ring",
                Year = 2001,
                PricePerDay = 4.49m,
                ImageUrl = "https://image.tmdb.org/t/p/w500/6oom5QYQ2yQTMJIbnvbkBL9cHo6.jpg",
                AvailableCopies = 4
            },
            new Movie
            {
                Genre = "Action",
                Title = "Pulp Fiction",
                Year = 1994,
                PricePerDay = 3.99m,
                ImageUrl = "https://image.tmdb.org/t/p/w500/d5iIlFn5s0ImszYzBPb8JPIfbXD.jpg",
                AvailableCopies = 5
            },
            new Movie
            {
                Genre = "Drama",
                Title = "Forrest Gump",
                Year = 1994,
                PricePerDay = 3.49m,
                ImageUrl = "https://image.tmdb.org/t/p/w500/arw2vcBveWOVZr6pxd9XTd1TdQa.jpg",
                AvailableCopies = 8
            },
            new Movie
            {
                Genre = "Sci-Fi",
                Title = "Interstellar",
                Year = 2014,
                PricePerDay = 5.99m,
                ImageUrl = "https://image.tmdb.org/t/p/w500/gEU2QniE6E77NI6lCU6MxlNBvIx.jpg",
                AvailableCopies = 3
            }
        );
        context.SaveChanges();
    }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts(); // ако сме качили сайта да не се ползва през visual studito, а си е в интернет че ще ползва https за следващите 30 дни,
                   // това не е важно за момента, просто дефоутно ви го създава проекта
}

app.UseHttpsRedirection();  //-> Ако някой отвори сайта http://localhost:7122/ -> автоматично ще ви пренасочи към https://localhost:7122/
app.UseStaticFiles(); // за да може да ни зареждат файловете които не са динамично генерирани(класовете които ползваме)
app.UseRouting(); // приложението ни ще се ориентира с този ред и ще разбира към кой url и към controller да ти отиде

app.UseAuthentication();
app.UseAuthorization(); // това ни трябва за логин на един на един потребител и да може работин 


//регистрира/мапва всички статични файловр (css, js, избображения) според начина по който сме си натройли проекта 
app.MapControllerRoute( // дефиницията за route(маршрута) mvc котролерите ни 
    name: "default", 
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets(); // свързва ви route, идеята му е правилно да ви работят ресурсите(класовте които правим според конфигурацията)


app.Run(); //стартира приложението и започва да чака заяки 
