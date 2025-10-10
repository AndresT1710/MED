using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Services
{
    public class PdfService
    {
        private readonly IWebHostEnvironment _environment;

        public PdfService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<byte[]> GeneratePersonPdfAsync(PersonDTO persona)
        {
            try
            {
                using var memoryStream = new MemoryStream();

                // Configurar documento A4 con márgenes
                var document = new Document(PageSize.A4, 40, 40, 100, 40);
                PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                // Colores corporativos de la universidad
                var universityRed = new BaseColor(109, 19, 18);    // #6D1312
                var universityWhite = new BaseColor(255, 255, 254); // #FFFFFE
                var darkGray = new BaseColor(64, 64, 64);
                var mediumGray = new BaseColor(128, 128, 128);
                var lightGray = new BaseColor(240, 240, 240);

                // Configurar fuentes
                var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, universityWhite);
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, universityRed);
                var subtitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, darkGray);
                var textFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, darkGray);
                var labelFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, darkGray);
                var smallFont = FontFactory.GetFont(FontFactory.HELVETICA, 8, mediumGray);

                // ===== ENCABEZADO CON LOGO =====
                var headerTable = new PdfPTable(3)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 20f
                };
                headerTable.SetWidths(new float[] { 20, 60, 20 });

                // Celda para el logo (izquierda)
                var logoCell = new PdfPCell()
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Padding = 8,
                    BackgroundColor = universityRed
                };

                // Cargar logo desde Resources del BackEnd
                try
                {
                    var logoPath = Path.Combine(_environment.ContentRootPath, "Resources", "Images", "logo-uta.jpg");

                    Console.WriteLine($"Buscando logo en: {logoPath}");
                    Console.WriteLine($"ContentRootPath: {_environment.ContentRootPath}");
                    Console.WriteLine($"Directorio existe: {Directory.Exists(Path.GetDirectoryName(logoPath))}");
                    Console.WriteLine($"Archivo existe: {File.Exists(logoPath)}");

                    if (File.Exists(logoPath))
                    {
                        var logo = Image.GetInstance(logoPath);
                        logo.ScaleToFit(70, 70);
                        logo.Alignment = Image.ALIGN_CENTER;
                        logoCell.AddElement(logo);
                        Console.WriteLine($"✅ Logo cargado exitosamente desde: {logoPath}");
                    }
                    else
                    {
                        throw new FileNotFoundException($"❌ Logo no encontrado en: {logoPath}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ ERROR cargando logo: {ex.Message}");
                    Console.WriteLine($"❌ StackTrace: {ex.StackTrace}");

                    // Fallback elegante
                    var fallbackLogo = new Paragraph("UTA",
                        FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, universityWhite))
                    {
                        Alignment = Element.ALIGN_CENTER
                    };
                    logoCell.AddElement(fallbackLogo);
                }

                // Resto del código SIN CAMBIOS...
                var textCell = new PdfPCell()
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Padding = 10,
                    BackgroundColor = universityRed
                };

                var universityName = new Paragraph("UNIVERSIDAD TÉCNICA DE AMBATO", headerFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                var systemName = new Paragraph("SISTEMA MÉDICO",
                    FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, universityWhite))
                {
                    SpacingBefore = 5f,
                    Alignment = Element.ALIGN_CENTER
                };

                textCell.AddElement(universityName);
                textCell.AddElement(systemName);

                var emptyCell = new PdfPCell()
                {
                    Border = Rectangle.NO_BORDER,
                    BackgroundColor = universityRed
                };

                headerTable.AddCell(logoCell);
                headerTable.AddCell(textCell);
                headerTable.AddCell(emptyCell);

                document.Add(headerTable);

                // ===== SUBTÍTULO DE LA FICHA =====
                var subtitle = new Paragraph("FICHA DE PACIENTE", subtitleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 8f
                };
                document.Add(subtitle);

                // Fecha de generación
                var dateGenerated = new Paragraph($"Fecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm}", smallFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 15f
                };
                document.Add(dateGenerated);

                // ===== LÍNEA SEPARADORA =====
                var line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2f, 100f, universityRed, Element.ALIGN_CENTER, -2f)))
                {
                    SpacingAfter = 20f
                };
                document.Add(line);

                // ===== INFORMACIÓN PRINCIPAL EN TABLAS =====
                AddPersonalInfoSection(document, persona, titleFont, labelFont, textFont, lightGray, universityRed);
                AddContactInfoSection(document, persona, titleFont, labelFont, textFont, lightGray, universityRed);
                AddAddressSection(document, persona, titleFont, labelFont, textFont, lightGray, universityRed);
                AddAdditionalInfoSection(document, persona, titleFont, labelFont, textFont, lightGray, universityRed);

                // ===== PIE DE PÁGINA =====
                var footerLine = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(1f, 100f, mediumGray, Element.ALIGN_CENTER, -2f)))
                {
                    SpacingBefore = 20f,
                    SpacingAfter = 10f
                };
                document.Add(footerLine);

                var footer = new Paragraph($"Documento generado automáticamente por el Sistema Médico\nUniversidad Técnica de Ambato © {DateTime.Now.Year}", smallFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(footer);

                document.Close();
                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generando PDF: {ex.Message}");
                throw;
            }
        }

        // Los métodos privados se mantienen EXACTAMENTE IGUAL...
        private void AddPersonalInfoSection(Document document, PersonDTO persona, Font titleFont, Font labelFont, Font textFont, BaseColor lightGray, BaseColor universityRed)
        {
            var sectionTitle = new Paragraph("INFORMACIÓN PERSONAL", titleFont)
            {
                SpacingAfter = 10f
            };
            document.Add(sectionTitle);

            var personalTable = new PdfPTable(2)
            {
                WidthPercentage = 100,
                SpacingAfter = 20f
            };
            personalTable.SetWidths(new float[] { 35, 65 });

            AddTableRow(personalTable, "Nombre completo:", $"{persona.FirstName} {persona.MiddleName} {persona.LastName} {persona.SecondLastName}".Trim(), labelFont, textFont, lightGray);
            AddTableRow(personalTable, "Cédula:", persona.Document?.DocumentNumber ?? "No disponible", labelFont, textFont);
            AddTableRow(personalTable, "Tipo Documento:", persona.Document?.DocumentTypeName ?? "No disponible", labelFont, textFont, lightGray);
            AddTableRow(personalTable, "Fecha Nacimiento:", persona.BirthDate?.ToString("dd/MM/yyyy") ?? "No disponible", labelFont, textFont);
            AddTableRow(personalTable, "Email:", persona.Email ?? "No disponible", labelFont, textFont, lightGray);
            AddTableRow(personalTable, "Estado Civil:", persona.MaritalStatus?.Name ?? "No disponible", labelFont, textFont);
            AddTableRow(personalTable, "Lateralidad:", persona.Laterality?.NameLaterality ?? "No disponible", labelFont, textFont, lightGray);
            AddTableRow(personalTable, "Religión:", persona.Religion?.Name ?? "No disponible", labelFont, textFont);
            AddTableRow(personalTable, "Grupo Sanguíneo:", persona.BloodGroup?.Name ?? "No disponible", labelFont, textFont, lightGray);
            AddTableRow(personalTable, "Nivel Instrucción:", persona.EducationLevel?.Name ?? "No disponible", labelFont, textFont);

            document.Add(personalTable);
        }

        private void AddContactInfoSection(Document document, PersonDTO persona, Font titleFont, Font labelFont, Font textFont, BaseColor lightGray, BaseColor universityRed)
        {
            var sectionTitle = new Paragraph("INFORMACIÓN DE CONTACTO", titleFont)
            {
                SpacingAfter = 10f
            };
            document.Add(sectionTitle);

            var contactTable = new PdfPTable(2)
            {
                WidthPercentage = 100,
                SpacingAfter = 20f
            };
            contactTable.SetWidths(new float[] { 35, 65 });

            AddTableRow(contactTable, "Teléfono Móvil:", persona.Phone?.Mobile ?? "No disponible", labelFont, textFont, lightGray);
            AddTableRow(contactTable, "Teléfono Convencional:", persona.Phone?.Landline ?? "No disponible", labelFont, textFont);

            document.Add(contactTable);
        }

        private void AddAddressSection(Document document, PersonDTO persona, Font titleFont, Font labelFont, Font textFont, BaseColor lightGray, BaseColor universityRed)
        {
            var sectionTitle = new Paragraph("DIRECCIÓN Y UBICACIÓN", titleFont)
            {
                SpacingAfter = 10f
            };
            document.Add(sectionTitle);

            var addressTable = new PdfPTable(2)
            {
                WidthPercentage = 100,
                SpacingAfter = 20f
            };
            addressTable.SetWidths(new float[] { 35, 65 });

            if (persona.Address?.Any() == true)
            {
                var address = persona.Address.First();
                AddTableRow(addressTable, "Calle Principal:", address.MainStreet ?? "No disponible", labelFont, textFont, lightGray);
                AddTableRow(addressTable, "Calle Secundaria 1:", address.SecondaryStreet1 ?? "No disponible", labelFont, textFont);
                AddTableRow(addressTable, "Calle Secundaria 2:", address.SecondaryStreet2 ?? "No disponible", labelFont, textFont, lightGray);
                AddTableRow(addressTable, "Número Casa:", address.HouseNumber ?? "No disponible", labelFont, textFont);
                AddTableRow(addressTable, "Referencia:", address.Reference ?? "No disponible", labelFont, textFont, lightGray);
            }
            else
            {
                AddTableRow(addressTable, "Dirección:", "No disponible", labelFont, textFont, lightGray);
            }

            AddTableRow(addressTable, "Ciudad:", persona.Residence?.CityName ?? "No disponible", labelFont, textFont);
            AddTableRow(addressTable, "Provincia:", persona.Residence?.ProvinceName ?? "No disponible", labelFont, textFont, lightGray);

            document.Add(addressTable);
        }

        private void AddAdditionalInfoSection(Document document, PersonDTO persona, Font titleFont, Font labelFont, Font textFont, BaseColor lightGray, BaseColor universityRed)
        {
            if (persona.HealthProfessional != null)
            {
                var professionalTitle = new Paragraph("INFORMACIÓN PROFESIONAL", titleFont)
                {
                    SpacingAfter = 10f
                };
                document.Add(professionalTitle);

                var professionalTable = new PdfPTable(2)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 20f
                };
                professionalTable.SetWidths(new float[] { 35, 65 });

                AddTableRow(professionalTable, "Tipo Profesional:", persona.HealthProfessional.NameTypeProfessional ?? "No disponible", labelFont, textFont, lightGray);
                AddTableRow(professionalTable, "Número de Registro:", persona.HealthProfessional.RegistrationNumber ?? "No disponible", labelFont, textFont);

                document.Add(professionalTable);
            }

            var listsTitle = new Paragraph("INFORMACIÓN ADICIONAL", titleFont)
            {
                SpacingAfter = 10f
            };
            document.Add(listsTitle);

            var listsTable = new PdfPTable(2)
            {
                WidthPercentage = 100,
                SpacingAfter = 15f
            };
            listsTable.SetWidths(new float[] { 35, 65 });

            AddTableRow(listsTable, "Seguros Médicos:",
                persona.MedicalInsurance?.Any() == true ?
                string.Join(", ", persona.MedicalInsurance.Select(i => i.Name)) : "No disponible",
                labelFont, textFont, lightGray);

            AddTableRow(listsTable, "Profesiones:",
                persona.Professions?.Any() == true ?
                string.Join(", ", persona.Professions.Select(p => p.Name)) : "No disponible",
                labelFont, textFont);

            AddTableRow(listsTable, "Actividades Laborales:",
                persona.LaborActivity?.Any() == true ?
                string.Join(", ", persona.LaborActivity.Select(a => a.Name)) : "No disponible",
                labelFont, textFont, lightGray);

            document.Add(listsTable);
        }

        private void AddTableRow(PdfPTable table, string label, string value, Font labelFont, Font textFont, BaseColor backgroundColor = null)
        {
            var labelCell = new PdfPCell(new Phrase(label, labelFont))
            {
                Border = Rectangle.NO_BORDER,
                Padding = 8,
                PaddingBottom = 6,
                BackgroundColor = backgroundColor,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            var valueCell = new PdfPCell(new Phrase(value ?? "No disponible", textFont))
            {
                Border = Rectangle.NO_BORDER,
                Padding = 8,
                PaddingBottom = 6,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            table.AddCell(labelCell);
            table.AddCell(valueCell);
        }
    }
}