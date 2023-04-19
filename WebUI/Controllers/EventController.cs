using CQRS.Application.Features.Events.Models;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CQRS.Application.Features.Events.Queries;

namespace WebUI.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IMediator mediator;

        public EventController(IMediator mediator, ApplicationDbContext db)
        {

            this.mediator = mediator;
            this.db = db;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<EventListDTO> list = await mediator.Send(new GetEventsQuery());
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        { 
            EventDTO Event = await mediator.Send(new GetEventDetailQuery() {Id = id});
            return View(Event);
        } 
        
      
    }
}
