using DevStudyNotes.API.Entities;
using DevStudyNotes.API.Models;
using DevStudyNotes.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevStudyNotes.API.Controllers
{
    [ApiController]
    [Route("api/study-notes")]
    public class StudyNotesController : ControllerBase
    {
        private readonly StudyNoteDbContext _context;

        public StudyNotesController(StudyNoteDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Recupera todos as notas.
        /// </summary>
        /// <returns>Lista com todas as notas</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        public IActionResult GetAll()
        {
            var studyNotes = _context.StudyNotes
                .Include(s => s.Reactions)
                .ToList();

            return Ok(studyNotes);
        }

        /// <summary>
        /// Recupera a nota por id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto com os valores da nota informada.</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var studyNote = _context.StudyNotes
                .Include(s => s.Reactions)
                .SingleOrDefault(s => s.Id == id);
            if (studyNote == null)
                return NotFound();
            
            return Ok(studyNote);
        }

        /// <summary>
        /// Cadastrar uma nota de estudo
        /// </summary>
        /// <remarks>
        /// {"title": "Estudos .Net 7", "description": "Estudar as novas atualizações do .Net 7", isPublic: true}
        /// </remarks>
        /// <param name="model">Dados de uma nota de estudo</param>
        /// <returns>Objeto recém-criado</returns>
        /// <response code="201">Sucesso</response>
        [HttpPost]
        public IActionResult Post(AddStudyNoteInputModel model)
        {
            var studyNote = new StudyNote(model.Title, model.Description, model.IsPublic);
             
            _context.StudyNotes.Add(studyNote);
            _context.SaveChanges();

            return CreatedAtAction("GetById", new
            {
                id = studyNote.Id
            }, model);
        }

        /// <summary>
        /// Cadastrar uma nova reação
        /// </summary>
        /// <remarks>{"isPositive": true}</remarks>
        /// <param name="id">Id da nota para criar a reação.</param>
        /// <param name="model"></param>
        /// <returns>No content</returns>
        [HttpPost("{id}/reactions")]
        public IActionResult PostReaction(int id, AddReactionStudyNoteInputModel model)
        {
            var studyNote = _context.StudyNotes.SingleOrDefault(s => s.Id == id);

            if (studyNote == null)
                return BadRequest();

            studyNote.AddReaction(model.IsPositive);

            _context.SaveChanges();     

            return NoContent();
        }
    }
}
