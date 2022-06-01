using ImageServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageServer.Services.NoteService
{
    public interface INoteService
    {
        Task<ServiceResponce<List<Note>>> GetAllNotes();
        Task<ServiceResponce<List<Note>>> GetNotesByOwner(string owner);
        Task<ServiceResponce<List<Note>>> DeleteNoteById(int id);
        Task<ServiceResponce<List<Note>>> AddNote(Note note);
        Task<ServiceResponce<List<Note>>> UpdateNote(Note note);
    }
}
