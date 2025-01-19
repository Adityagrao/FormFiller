using FormFiller.Models;
using FormFiller.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormFiller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController(PdfFormFillerContext context, MappingService formMappingService, FormServices formServices) : ControllerBase
    {
        private readonly PdfFormFillerContext _context = context;
        private readonly MappingService _formMappingService = formMappingService;
        private readonly FormServices _formServices = formServices;

        [HttpGet]
        public ActionResult<List<string>> GetFormTemplateNames()
        {
            var formNames = _formMappingService.GetFormTemplateNames();
            return Ok(formNames);
        }

        [HttpPost("{formName}")]
        public async Task<IActionResult> GeneratePDF(string formName, [FromQuery] int userId)
        {
            // Load form template
            var formTemplate = _formMappingService.GetFormTemplate(formName);
            if (formTemplate == null)
                return NotFound($"Form '{formName}' not found.");

            // Retrieve user data
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound($"User with ID {userId} not found.");

            // Map user data to a dictionary
            var userData = new Dictionary<string, string>
            {
                { "LastName", user.LastName },
                { "FirstName", user.FirstName },
                { "DateOfBirth", user.DateOfBirth.ToString("yyyy-MM-dd") },
                { "PostalCode", user.PostalCode },
                { "StreetName", user.StreetName },
                { "HouseNumberAddition", user.HouseNumberAddition }
            };

            // Generate PDF as byte array
            var pdfBytes = _formServices.GeneratePDFDocument(formTemplate, userData);           

            // Return PDF as a file
            return File(pdfBytes, "application/pdf", $"{formTemplate.FormName}.pdf");
        }      
        
    }
}
