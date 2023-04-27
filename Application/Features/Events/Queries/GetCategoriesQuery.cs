using AutoMapper;
using AutoMapper.QueryableExtensions;
using CQRS.Application.Features.Events.Models;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.Features.Events.Queries
{
    public class GetPhotoAlbumsQuery : IRequest<IEnumerable<PhotoAlbumDTO>>
    {
  


    }
    public class PhotoAlbumHandler : IRequestHandler<GetPhotoAlbumsQuery, IEnumerable<PhotoAlbumDTO>>
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext db;

        public PhotoAlbumHandler(IMapper mapper, ApplicationDbContext db)
        {
            this.db = db;
            this.mapper = mapper;

        }

        public async Task<IEnumerable<PhotoAlbumDTO>> Handle(GetPhotoAlbumsQuery request, CancellationToken cancellationToken)
        {
            try
            {

                IEnumerable<PhotoAlbumDTO> result = await db.PhotoAlbum.ProjectTo<PhotoAlbumDTO>(mapper.ConfigurationProvider).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                return new List<PhotoAlbumDTO>();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
