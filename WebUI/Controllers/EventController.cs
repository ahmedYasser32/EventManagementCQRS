﻿using CQRS.Application.Features.Events.Models;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CQRS.Application.Features.Events.Queries;
using CQRS.Application.Features.Events.Commands.Create;
using Microsoft.AspNetCore.Mvc.Rendering;
using CQRS.Application.Features.Sources.Queries;
using CQRS.Application.Features.Events.Commands.Update;
using CQRS.Application.Features.Events.Commands.Delete;

namespace WebUI.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext db;
        private ISender _mediator = null!;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        public EventController(ApplicationDbContext db)
        {

            this.db = db;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<EventListDTO> list = await Mediator.Send(new GetEventsQuery());
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            EventDTO Event = await Mediator.Send(new GetEventDetailQuery() { Id = id });
            return View(Event);
        }

        public async Task<IActionResult> Create()
        {
            // get your drop downs with select list object
            ViewData["Source"] = new SelectList(await Mediator.Send(new GetSourcesQuery()), "Id", "Name");
            ViewData["PhotoAlbum"] = new SelectList(await Mediator.Send(new GetPhotoAlbumsQuery()), "Id", "Title");
            ViewData["Category"] = new MultiSelectList(await Mediator.Send(new GetCategoriesQuery()), "Id", "Name");

            return View(new CreateEventCommand());
        }
        public async Task<IActionResult> Update(int Id)
        {
            // get your drop downs with select list object
            ViewData["Source"] = new SelectList(await Mediator.Send(new GetSourcesQuery()), "Id", "Name");
            ViewData["PhotoAlbum"] = new SelectList(await Mediator.Send(new GetPhotoAlbumsQuery()), "Id", "Title");
            ViewData["Category"] = new MultiSelectList(await Mediator.Send(new GetCategoriesQuery()), "Id", "Name");

            return View(new UpdateEventCommand { Id = Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateEventCommand command , IFormFile? CoverPhotoFormFile) 
        {

                command.CoverPhotoFormFile = CoverPhotoFormFile;


                if (ModelState.IsValid)
            {
                await Mediator.Send(command);
                return RedirectToAction(nameof(Index));

            }

            else
            {
                var errors = String.Join("|", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                Console.WriteLine(errors);

                ViewData["Source"] = new SelectList(await Mediator.Send(new GetSourcesQuery()), "Id", "Name", command.SourceId);
                ViewData["PhotoAlbum"] = new SelectList(await Mediator.Send(new GetPhotoAlbumsQuery()), "Id", "Title", command.PhotoAlbumId);
                ViewData["Category"] = new MultiSelectList(await Mediator.Send(new GetCategoriesQuery()), "Id", "Name", command.CategoriesId);

                return View(command);
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteEventCommand() { Id = id });
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to delete. ");
            }
            return RedirectToAction(nameof(Index));
        }



            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(CreateEventCommand command, IFormFile? CoverPhotoFormFile) {

                command.CoverPhotoFormFile = CoverPhotoFormFile;

                if (ModelState.IsValid)
                {
                    await Mediator.Send(command);
                    return RedirectToAction(nameof(Index));

                }

                else
                {
                    var errors = String.Join("|", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));

                    Console.WriteLine(errors);

                    ViewData["Source"] = new SelectList(await Mediator.Send(new GetSourcesQuery()), "Id", "Name", command.SourceId);
                    ViewData["PhotoAlbum"] = new SelectList(await Mediator.Send(new GetPhotoAlbumsQuery()), "Id", "Title", command.PhotoAlbumId);
                    ViewData["Category"] = new MultiSelectList(await Mediator.Send(new GetCategoriesQuery()), "Id", "Name", command.CategoriesId);

                    return View(command);
                }

            }

        }
    } 
