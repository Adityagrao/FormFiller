using FormFiller.DTOs;
using iText.Forms;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Pdf.Canvas;

namespace FormFiller.Services

{
    public class FormServices
    {
        public FormServices()
        {

        }
        public List<string> ExtractFormFields(string templatePath)
        {
            var fieldNames = new List<string>();

            using (var pdfReader = new PdfReader(templatePath))
            using (var pdfDoc = new PdfDocument(pdfReader))
            {
                // Get the AcroForm from the PDF
                var form = PdfAcroForm.GetAcroForm(pdfDoc, false);
                var fields = form.GetAllFormFields();

                // Collect field names
                foreach (var field in fields)
                {
                    fieldNames.Add(field.Key);
                }
            }

            return fieldNames;
        }

        public byte[] GeneratePDFDocumentUsingAcro(FormTemplate formTemplate, Dictionary<string, string> userData)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var pdfWriter = new PdfWriter(memoryStream))
                using (var pdfReader = new PdfReader(formTemplate.FilePath))
                using (var pdfDoc = new PdfDocument(pdfReader, pdfWriter))
                {
                    // Get the AcroForm object from the PDF
                    var form = PdfAcroForm.GetAcroForm(pdfDoc, true);

                    // Iterate over mappings and fill fields if they exist in the PDF
                    foreach (var fieldMapping in formTemplate.Fields)
                    {
                        // Check if the field exists in the PDF
                        if (form.GetField(fieldMapping.FormFieldName) != null)
                        {
                            if (userData.TryGetValue(fieldMapping.FieldName, out var value))
                            {
                                // Set the value for the form field
                                form.GetField(fieldMapping.FormFieldName).SetValue(value);
                            }
                        }
                    }

                    // Optionally flatten the form to make fields read-only
                    form.FlattenFields();

                    pdfDoc.Close();
                }

                // Return the PDF content as a byte array
                return memoryStream.ToArray();
            }
        }

       public byte[] GeneratePDFDocument(FormTemplate formTemplate, Dictionary<string, string> userData)
        {
            using var memoryStream = new MemoryStream();
            using (var pdfReader = new PdfReader(formTemplate.FilePath))
            using (var pdfWriter = new PdfWriter(memoryStream))
            using (var pdfDoc = new PdfDocument(pdfReader, pdfWriter))
            {
                // Flatten the form to convert interactive fields into static content
                var acroForm = PdfAcroForm.GetAcroForm(pdfDoc, true);
                acroForm.FlattenFields();

                // Add text to the PDF at the specified positions
                foreach (var fieldMapping in formTemplate.Fields)
                {
                    if (userData.TryGetValue(fieldMapping.FieldName, out var value))
                    {
                        // Get the specified page
                        var pdfPage = pdfDoc.GetPage(fieldMapping.PageNumber);

                        if (pdfPage == null)
                        {
                            throw new InvalidOperationException($"Page {fieldMapping.PageNumber} does not exist in the PDF.");
                        }

                        // Use PdfCanvas to draw text directly on the page content layer
                        var canvas = new PdfCanvas(pdfPage);

                        canvas.BeginText()
                            .SetFontAndSize(PdfFontFactory.CreateFont(StandardFonts.HELVETICA), 10) // Font and size
                            .MoveText(fieldMapping.XCoordinate, fieldMapping.YCoordinate) // Position
                            .ShowText(value) // Text to display
                            .EndText();
                    }
                }

                pdfDoc.Close();
            }

            return memoryStream.ToArray();
        }

    }
}