using ImageServer.Data;
using ImageServer.Models;
using ImageServer.Services.ImageService;
using ImageServer.Services.NoteService;
using ImageServer.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageServer.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IImageService imageService;
        private readonly INoteService noteService;

        public UserController(IUserService userService, IImageService imageService, INoteService noteService )
        {
            this.userService = userService;
            this.imageService = imageService;
            this.noteService = noteService;
        }

        [HttpGet("/notes")]
        public async Task<ActionResult<ServiceResponce<List<User>>>> GetAllNotes()
        {
            return Ok(await noteService.GetAllNotes());
        }
        [HttpGet("/notes/{owner}")]
        public async Task<ActionResult<ServiceResponce<List<User>>>> GetAllNotesByOwner(string owner)
        {
            return Ok(await noteService.GetNotesByOwner(owner));
        }
        [HttpPost("/notes")]
        public async Task<ActionResult<ServiceResponce<List<User>>>> AddNote(Note note)
        {
            return Ok(await noteService.AddNote(note));
        }
        [HttpDelete("/notes/{id}")]
        public async Task<ActionResult<ServiceResponce<List<User>>>> DeleteNodeById(int id)
        {
            return Ok(await noteService.DeleteNoteById(id));
        }
        [HttpPut("/notes")]
        public async Task<ActionResult<ServiceResponce<List<User>>>> UpdateNode(Note note)
        {
            return Ok(await noteService.UpdateNote(note));
        }
        [HttpGet("")]
        public async Task<ActionResult<ServiceResponce<List<User>>>> Get()
        {
            return Ok( await userService.GetAllUsers());
        }

        [HttpGet("{login}")]
        public async  Task<ActionResult<ServiceResponce<User>>> Get(string login) 
        {
            return Ok(await userService.GetUserByLogin(login));
        }

        [HttpPost("")]
        public async Task<ActionResult<ServiceResponce<List<User>>>> AddUser(User user) 
        {
            return Ok(await userService.AddUser(user));
        }

        [HttpDelete("")]
        public async Task<ActionResult<ServiceResponce<List<User>>>> DeleteUserByLogin(string login) 
        {
            return Ok(await userService.DeleteUserByLogin(login));
        }

        [HttpPut("")]
        public async Task<ActionResult<ServiceResponce<List<User>>>> UpdateUser(User user)
        {
            return Ok(await userService.UpdateUser(user));
        }

        [HttpGet("/images")]
        public async Task<ActionResult<ServiceResponce<List<Image>>>> GetAll()
        {
            return Ok(await imageService.GetAllImages());
        }
        [HttpGet("/images/{name}")]
        public async Task<ActionResult<ServiceResponce<Image>>> GetImageByName(string name)
        {
            return Ok(await imageService.GetImageByName(name));
        }
        [HttpPost("/images")]
        public async Task<ActionResult<ServiceResponce<List<Image>>>> AddImage(Image image)
        {
            return Ok(await imageService.AddImage(image));
        }
        [HttpPut("/images")]
        public async Task<ActionResult<ServiceResponce<List<Image>>>> UpdateImage(Image image)
        {
            return Ok(await imageService.UpdateImage(image));
        }
        [HttpDelete("/images/{id}")]
        public async Task<ActionResult<ServiceResponce<List<Image>>>> DeleteImage(int id)
        {
            return Ok(await imageService.DeleteImageById(id));
        }
    }
}
