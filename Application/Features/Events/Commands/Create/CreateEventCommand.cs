using CQRS.Application.Features.Events.Models;
using Domain.Entities;
using Infrastructure;
using MediatR;
using static CQRS.Application.Common.Helper.FilesHelper;

namespace CQRS.Application.Features.Events.Commands.Create
{
    public class CreateEventCommand : EventDTO, IRequest<string>
    {
        public class Handler : IRequestHandler<CreateEventCommand, string>
        {

            private readonly ApplicationDbContext db;
            public Handler(ApplicationDbContext db)
            {

                this.db = db;
            }


            public async Task<string> Handle(CreateEventCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.CoverPhotoPath != null)
                    {
                        request.CoverPhotoPath = await SaveFiles(request.CoverPhoto);
                    }

                    else
                    {
                        request.CoverPhotoPath = " ";
                    }


                    var entity = new Event()
                    {
                        Address = request.Address,
                        Content = request.Content,
                        ArabicTitle = request.ArabicTitle,
                        Title = request.Title,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        CoverPhotoPath = request.CoverPhotoPath,
                        Source = db.Source.Find(request.Source.Id),
                        PhotoAlbum = db.PhotoAlbum.Find(request.PhotoAlbum.Id)
                    };

                    List<Category> Categories = new();
                    entity.Categories = Categories;

                    if (request.Categories != null)
                    {
                        foreach (CategoryDTO cat in request.Categories)
                        {
                            int CategoryId = cat.Id;
                            Console.WriteLine(CategoryId);
                            
                            var category = db.Category.Find(CategoryId);

                            if (category != null)
                            {
                                entity.Categories.Add(category);
                            }

                        }
                    }

                    await db.Event.AddAsync(entity);

                    await db.SaveChangesAsync();

                    return "Event Created Succesfully";

                }

                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
