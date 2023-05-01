using CQRS.Application.Features.Events.Models;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using MediatR;
using static CQRS.Application.Common.Helper.FilesHelper;
using Domain.Entities;

namespace CQRS.Application.Features.Events.Commands.Update;

public class UpdateEventCommand : EventDTO, IRequest<EventDTO>

{
    public int? SourceId { get; set; }
    public int? PhotoAlbumId { get; set; }
    public List<int>? CategoriesId { get; set; }

    public class Handler : IRequestHandler<UpdateEventCommand, EventDTO>
    {
       

        private readonly ApplicationDbContext db;
        public Handler(ApplicationDbContext db)
        {

            this.db = db;
        }

        public async Task<EventDTO> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            try
            { 
                var entity = await db.Event.Where(e => e.Id == request.Id).Include(x => x.Categories).FirstOrDefaultAsync();

                if (entity == null) { 

                    throw new Exception("Event Not Found!");
                }

                if (request.CoverPhotoFormFile != null)
                {
                    
                    

                    if (entity.CoverPhoto != null)
                    {

                        File.Delete(entity.CoverPhoto);
                    }

                    entity.CoverPhoto = request.CoverPhoto;
                }

                entity.Address = request.Address;
                entity.Content = request.Content;
                entity.ArabicTitle = request.ArabicTitle;
                entity.Title = request.Title;
                entity.StartDate = request.StartDate;
                entity.EndDate = request.EndDate;
                entity.CoverPhoto = request.CoverPhoto;
                entity.Source = db.Source.Find(request.SourceId);
                entity.PhotoAlbum = db.PhotoAlbum.Find(request.PhotoAlbumId);

                request.CoverPhoto = await SaveFiles(request.CoverPhotoFormFile);

                if (request.CategoriesId != null)
                {

                    List<Category> Categories = new();
                    entity.Categories = Categories;

                    if (request.CategoriesId != null)
                    {
                        foreach (int CategoryId in request.CategoriesId)
                        {

                            var category = db.Category.Find(CategoryId);

                            if (category != null)
                            {
                                entity.Categories.Add(category);
                            }

                        }
                    }
                }

                db.Event.Update(entity);

                await db.SaveChangesAsync();

                return request;
            }
            catch (Exception ex) { return null; }
    }
    }
}
