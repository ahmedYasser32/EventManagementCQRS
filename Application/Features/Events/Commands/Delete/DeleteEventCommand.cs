using CQRS.Application.Features.Events.Models;
using Domain.Entities;
using Infrastructure;
using MediatR;

namespace CQRS.Application.Features.Events.Commands.Delete
{
    public class DeleteEventCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
    public class Handler : IRequestHandler<DeleteEventCommand, string>
    {
        private readonly ApplicationDbContext db;
        public Handler(ApplicationDbContext db)
        {

            this.db = db;
        }

        public async Task<string> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var entity = db.Event.Find(request.Id);

                if (entity == null) throw new Exception("Event does not exist");

                if (entity.CoverPhotoPath != null)
                {
                    File.Delete(entity.CoverPhotoPath);
                }
                db.Event.Remove(entity);

                await db.SaveChangesAsync();

                return "Deleted";

            }

            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}
