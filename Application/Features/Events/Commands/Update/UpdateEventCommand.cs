using CQRS.Application.Features.Events.Models;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using MediatR;
using static CQRS.Application.Common.Helper.FilesHelper;
using Domain.Entities;

namespace CQRS.Application.Features.Events.Commands.Update;

public class UpdateEventCommand : EventDTO, IRequest<string>
{
    public class Handler : IRequestHandler<UpdateEventCommand, string>
    {

        private readonly ApplicationDbContext db;
        public Handler(ApplicationDbContext db)
        {

            this.db = db;
        }

        public async Task<string> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            try
            { 
                var entity = await db.Event.Where(e => e.Id == request.Id).Include(x => x.Categories).FirstOrDefaultAsync();

                if (entity == null) { 

                    throw new Exception("Event Not Found!");
                }

                if (request.CoverPhoto != null)
                {
                    
                    request.CoverPhotoPath = await SaveFiles(request.CoverPhoto);

                    if (entity.CoverPhotoPath != null)
                    {

                        File.Delete(entity.CoverPhotoPath);
                    }

                    entity.CoverPhotoPath = request.CoverPhotoPath;
                }

                entity.Address = request.Address;
                entity.Content = request.Content;
                entity.ArabicTitle = request.ArabicTitle;
                entity.Title = request.Title;
                entity.StartDate = request.StartDate;
                entity.EndDate = request.EndDate;
                entity.CoverPhotoPath = request.CoverPhotoPath;
                entity.Source = db.Source.Find(request.Source.Id);
                entity.PhotoAlbum = db.PhotoAlbum.Find(request.PhotoAlbum.Id);
               

                if (request.Categories != null)
                {
                    List<Category> Categories = new();

                    entity.Categories = Categories;

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

                db.Event.Update(entity);

                await db.SaveChangesAsync();

                return "Event Created Succesfully";
            }
            catch (Exception ex) { return ex.Message; }
    }
    }
}
