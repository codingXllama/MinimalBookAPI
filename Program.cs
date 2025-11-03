
namespace MinimalBookAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();


            //Creating a list of books

            var books = new List<Book>
            {
                new() {Id=1,Title = "The HungerGames", Author = "Ronaldo"},
                new() { Id = 2, Title = "HarryPoter", Author = "Kaka" },
                new() {Id = 3, Title = "Ready Player One", Author="Ernest Cline"},
                new() {Id = 4, Title ="How to be Rich", Author="Robert Smith"}
            };

            //Get Method is HTTP request
            app.MapGet("/book", () =>
            {
                return books;
            });

            //using the lamda expression {} => ();
            app.MapGet("/book/{id}", (int id) =>
            {
                //books ID, b - is the same as the ID given in the API.
                return books.Find(b => b.Id == id);
            });

            app.Run();

        }
    }

    class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
    }
}


