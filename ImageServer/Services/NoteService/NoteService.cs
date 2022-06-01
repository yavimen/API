using ImageServer.Data;
using ImageServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageServer.Services.NoteService
{
    public class NoteService : INoteService
    {
        private readonly DataContext dataContext;
        public NoteService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<ServiceResponce<List<Note>>> AddNote(Note note)
        {
            if(note.Description.Contains("&"))
                return new ServiceResponce<List<Note>> { Success = false, Message = "& not allow in descriptions!" };
            if (note.Owner == "admin4308")
                return new ServiceResponce<List<Note>> { Success = false, Message = "You cant add new note" };
            if (dataContext.Notes.Contains(note))
                return new ServiceResponce<List<Note>> { Success = false, Message = "Note has this id in db" };
            await dataContext.Notes.AddAsync(note);
            await dataContext.SaveChangesAsync();
            return new ServiceResponce<List<Note>> { Data = dataContext.Notes.ToList() };
        }

        public async Task<ServiceResponce<List<Note>>> DeleteNoteById(int id)
        {
            var item = await dataContext.Notes.FirstOrDefaultAsync(note => note.Id == id);
            if (item == default(Note))
                return new ServiceResponce<List<Note>> { Success = false, Message = "We havn't this note" };
            dataContext.Notes.Remove(item);
            await dataContext.SaveChangesAsync();
            var serviceResponce = new ServiceResponce<List<Note>>();
            serviceResponce.Data = await dataContext.Notes.ToListAsync();
            return serviceResponce;
        }

        public async Task<ServiceResponce<List<Note>>> GetAllNotes()
        {
            return new ServiceResponce<List<Note>>(){Data = await dataContext.Notes.ToListAsync()};
        }

        public async Task<ServiceResponce<List<Note>>> GetNotesByOwner(string owner)
        {
            var list = await dataContext.Notes.Where(item => item.Owner == owner).Select(item => item).ToListAsync();
            return new ServiceResponce<List<Note>>() { Data = list };
        }

        public async Task<ServiceResponce<List<Note>>> UpdateNote(Note note)
        {
           if(dataContext.Notes.FirstOrDefault(item=>item.Id==note.Id) == null)
                return new ServiceResponce<List<Note>> { Success = false, Message = "We havn't this note" };
            var item = dataContext.Notes.FirstOrDefault(item => item.Id == note.Id);
            item.Title = note.Title;
            item.Description = note.Description;
            item.Owner = note.Owner;
            await dataContext.SaveChangesAsync();
            return new ServiceResponce<List<Note>>() { Data = await dataContext.Notes.ToListAsync() };
        }
    }
}
