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


            //Creating a list of Books

            var MyBooks = new List<Book>
            {
                new() {Id=1,Title = "The HungerGames", Author = "Ronaldo"},
                new() { Id = 2, Title = "HarryPoter", Author = "Kaka" },
                new() {Id = 3, Title = "Ready Player One", Author="Ernest Cline"},
                new() {Id = 4, Title ="How to be Rich", Author="Robert Smith"}
            };

            //Get Method is HTTP request
            app.MapGet("/book", () =>
            {
                return MyBooks;
            });

            //using the lamda expression {} => ();
            app.MapGet("/book/{id}", (int id) =>
            {
                //MyBooks ID, b - is the same as the ID given in the API.
                var book = MyBooks.Find(b => b.Id == id);
                if (book is null)
                    return Results.NotFound("Sorry, this book does not exist");

                return Results.Ok(book);
            });

            //Adding a book uses the POST method
            app.MapPost("/book", (Book book) =>
            {
                // you can add the book to a DB here, but we're not doing that. We're just adding the book to a list
                MyBooks.Add(book);
                return MyBooks;
            });

            //Updating a book using the PUT request not POST.
            app.MapPut("/book/{id}", (Book updatedBook, int id) =>
            {
                //MyBooks ID, b - is the same as the ID given in the API.
                var book = MyBooks.Find(b => b.Id == id);
                if (book is null)
                    return Results.NotFound("Sorry, this book does not exist");

                //updating the book properties.
                book.Title = updatedBook.Title;
                book.Author = updatedBook.Author;

                return Results.Ok(book);
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


