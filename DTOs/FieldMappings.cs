namespace FormFiller.DTOs
{
    public class FieldMapping
    {
        public string FieldName { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public string FormFieldName { get; set; }
        public int PageNumber { get; set; }
    }

    public class FormTemplate
    {
        public string FormName { get; set; }
        public string FilePath { get; set; }
        public List<FieldMapping> Fields { get; set; }
    }

    public class FormMappings
    {
        public List<FormTemplate> Forms { get; set; }
    }

}
