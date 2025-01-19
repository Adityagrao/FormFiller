using FormFiller.DTOs;

using System.Text.Json;

namespace FormFiller.Services
{
    public class MappingService
    {
        private readonly FormMappings _formMappings;

        public MappingService()
        {
            var json = File.ReadAllText("Services/mappings.json");
            _formMappings = JsonSerializer.Deserialize<FormMappings>(json);
        }

        public FormTemplate GetFormTemplate(string formName)
        {
            return _formMappings.Forms.FirstOrDefault(f => f.FormName == formName);
        }

        public List<string> GetFormTemplateNames()
        {
            return _formMappings.Forms.Select(f => f.FormName).ToList();
        }
    }
}
