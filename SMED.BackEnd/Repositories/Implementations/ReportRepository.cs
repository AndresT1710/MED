using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ReportRepository : IReportRepository
    {
        private readonly SGISContext _context;
        private readonly IWebHostEnvironment _environment;

        // Colores corporativos UTA
        private readonly BaseColor UNIVERSITY_RED = new BaseColor(109, 19, 18);
        private readonly BaseColor UNIVERSITY_WHITE = new BaseColor(255, 255, 254);
        private readonly BaseColor DARK_GRAY = new BaseColor(64, 64, 64);
        private readonly BaseColor MEDIUM_GRAY = new BaseColor(128, 128, 128);
        private readonly BaseColor LIGHT_GRAY = new BaseColor(240, 240, 240);

        public ReportRepository(SGISContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<ReportResultDTO> GenerateMedicalCareReportAsync(ReportRequestDTO request)
        {
            try
            {
                var medicalCares = await GetMedicalCaresByFilterAsync(request);

                using var memoryStream = new MemoryStream();
                var document = new Document(PageSize.A4, 40, 40, 50, 40);
                var writer = PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                // ===== ENCABEZADO UTA =====
                AddUtAHeader(document, "REPORTE DE ATENCIONES MÉDICAS");

                // Información del reporte
                var infoFont = new Font(Font.HELVETICA, 10, Font.NORMAL, DARK_GRAY);
                var periodText = $"Período: {(request.StartDate?.ToString("dd/MM/yyyy") ?? "Inicio")} - {(request.EndDate?.ToString("dd/MM/yyyy") ?? "Fin")}";
                document.Add(new Paragraph(periodText, infoFont));
                document.Add(new Paragraph($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}", infoFont));
                document.Add(new Paragraph($"Total de registros: {medicalCares.Count}", infoFont));
                document.Add(new Paragraph("\n"));

                // Tabla de atenciones
                if (medicalCares.Any())
                {
                    var table = new PdfPTable(5)
                    {
                        WidthPercentage = 100,
                        SpacingBefore = 10f,
                        SpacingAfter = 10f
                    };
                    table.SetWidths(new float[] { 20, 25, 25, 15, 15 });

                    // Encabezados
                    table.AddCell(CreateUtAHeaderCell("Fecha"));
                    table.AddCell(CreateUtAHeaderCell("Paciente"));
                    table.AddCell(CreateUtAHeaderCell("Profesional"));
                    table.AddCell(CreateUtAHeaderCell("Área"));
                    table.AddCell(CreateUtAHeaderCell("Ubicación"));

                    // Datos con alternancia de colores
                    bool alternate = false;
                    foreach (var care in medicalCares)
                    {
                        table.AddCell(CreateUtADataCell(care.CareDate.ToString("dd/MM/yyyy"), alternate));
                        table.AddCell(CreateUtADataCell(care.Patient?.PersonNavigation != null ?
                            $"{care.Patient.PersonNavigation.FirstName} {care.Patient.PersonNavigation.LastName}" : "N/A", alternate));
                        table.AddCell(CreateUtADataCell(care.HealthProfessional?.PersonNavigation != null ?
                            $"{care.HealthProfessional.PersonNavigation.FirstName} {care.HealthProfessional.PersonNavigation.LastName}" : "N/A", alternate));
                        table.AddCell(CreateUtADataCell(care.LocationNavigation?.Name ?? "N/A", alternate));
                        table.AddCell(CreateUtADataCell(care.PlaceOfAttentionNavigation?.Name ?? "N/A", alternate));
                        alternate = !alternate;
                    }

                    document.Add(table);
                }
                else
                {
                    var noDataFont = new Font(Font.HELVETICA, 12, Font.ITALIC, DARK_GRAY);
                    document.Add(new Paragraph("No se encontraron atenciones médicas con los filtros aplicados.", noDataFont));
                }

                // ===== PIE DE PÁGINA =====
                AddUtAFooter(document);

                document.Close();

                return new ReportResultDTO
                {
                    FileName = $"Reporte_Atenciones_{DateTime.Now:yyyyMMddHHmmss}.pdf",
                    Content = memoryStream.ToArray(),
                    ContentType = "application/pdf",
                    ReportTitle = "Reporte de Atenciones Médicas"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generando reporte de atenciones médicas: {ex.Message}");
                throw;
            }
        }

        public async Task<ReportResultDTO> GeneratePatientReportAsync(ReportRequestDTO request)
        {
            try
            {
                var patientData = await GetPatientReportDataAsync(request);

                using var memoryStream = new MemoryStream();
                var document = new Document(PageSize.A4, 40, 40, 50, 40);
                var writer = PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                // ===== ENCABEZADO UTA =====
                AddUtAHeader(document, "REPORTE DE PACIENTES");

                var infoFont = new Font(Font.HELVETICA, 10, Font.NORMAL, DARK_GRAY);
                document.Add(new Paragraph($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}", infoFont));
                document.Add(new Paragraph($"Total de pacientes: {patientData.Count}", infoFont));
                document.Add(new Paragraph("\n"));

                foreach (var patient in patientData)
                {
                    var patientHeaderFont = new Font(Font.HELVETICA, 12, Font.BOLD, UNIVERSITY_RED);
                    document.Add(new Paragraph($"PACIENTE: {patient.FullName}", patientHeaderFont));

                    var patientInfo = new PdfPTable(2)
                    {
                        WidthPercentage = 100,
                        SpacingBefore = 5f,
                        SpacingAfter = 10f
                    };
                    patientInfo.SetWidths(new float[] { 30, 70 });

                    AddUtATableRow(patientInfo, "Cédula:", patient.DocumentNumber);
                    AddUtATableRow(patientInfo, "Fecha Nacimiento:", patient.BirthDate?.ToString("dd/MM/yyyy") ?? "N/A");
                    AddUtATableRow(patientInfo, "Email:", patient.Email ?? "N/A");
                    AddUtATableRow(patientInfo, "Teléfono:", patient.Phone ?? "N/A");
                    AddUtATableRow(patientInfo, "Grupo Sanguíneo:", patient.BloodGroup ?? "N/A");
                    AddUtATableRow(patientInfo, "Estado Civil:", patient.MaritalStatus ?? "N/A");
                    AddUtATableRow(patientInfo, "Seguros Médicos:", string.Join(", ", patient.MedicalInsurances));
                    AddUtATableRow(patientInfo, "Total Consultas:", patient.TotalConsultations.ToString());
                    AddUtATableRow(patientInfo, "Última Consulta:", patient.LastConsultation?.ToString("dd/MM/yyyy") ?? "N/A");

                    document.Add(patientInfo);

                    if (patient.Consultations.Any())
                    {
                        var consultTitle = new Paragraph("ÚLTIMAS CONSULTAS", new Font(Font.HELVETICA, 10, Font.BOLD, DARK_GRAY))
                        {
                            SpacingBefore = 5f
                        };
                        document.Add(consultTitle);

                        var consultTable = new PdfPTable(3)
                        {
                            WidthPercentage = 100,
                            SpacingBefore = 5f,
                            SpacingAfter = 20f
                        };
                        consultTable.SetWidths(new float[] { 25, 35, 40 });

                        consultTable.AddCell(CreateUtAHeaderCell("Fecha"));
                        consultTable.AddCell(CreateUtAHeaderCell("Área"));
                        consultTable.AddCell(CreateUtAHeaderCell("Profesional"));

                        bool alternate = false;
                        foreach (var consult in patient.Consultations.Take(5))
                        {
                            consultTable.AddCell(CreateUtADataCell(consult.CareDate.ToString("dd/MM/yyyy"), alternate));
                            consultTable.AddCell(CreateUtADataCell(consult.Area, alternate));
                            consultTable.AddCell(CreateUtADataCell(consult.Professional, alternate));
                            alternate = !alternate;
                        }

                        document.Add(consultTable);
                    }

                    // Línea separadora entre pacientes
                    document.Add(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.5f, 100f, MEDIUM_GRAY, Element.ALIGN_CENTER, 5f)));
                    document.Add(new Paragraph(" "));
                }

                // ===== PIE DE PÁGINA =====
                AddUtAFooter(document);

                document.Close();

                return new ReportResultDTO
                {
                    FileName = $"Reporte_Pacientes_{DateTime.Now:yyyyMMddHHmmss}.pdf",
                    Content = memoryStream.ToArray(),
                    ContentType = "application/pdf",
                    ReportTitle = "Reporte de Pacientes"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generando reporte de pacientes: {ex.Message}");
                throw;
            }
        }

        public async Task<ReportResultDTO> GenerateProfessionalReportAsync(ReportRequestDTO request)
        {
            try
            {
                Console.WriteLine($"=== GenerateProfessionalReportAsync STARTED ===");
                Console.WriteLine($"Request: Type={request.ReportType}, Start={request.StartDate}, End={request.EndDate}");

                // PASO 1: Obtener datos
                Console.WriteLine("PASO 1: Llamando a GetProfessionalReportDataAsync...");
                var professionalData = await GetProfessionalReportDataAsync(request);
                Console.WriteLine($"PASO 1 COMPLETADO: Obtenidos {professionalData.Count} profesionales");

                // PASO 2: Crear PDF
                Console.WriteLine("PASO 2: Creando documento PDF...");
                using var memoryStream = new MemoryStream();
                var document = new Document(PageSize.A4, 40, 40, 50, 40);
                var writer = PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                // ===== ENCABEZADO UTA =====
                Console.WriteLine("Agregando encabezado...");
                AddUtAHeader(document, "REPORTE DE PROFESIONALES");

                var infoFont = new Font(Font.HELVETICA, 10, Font.NORMAL, DARK_GRAY);
                document.Add(new Paragraph($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}", infoFont));
                document.Add(new Paragraph($"Total de profesionales: {professionalData.Count}", infoFont));
                document.Add(new Paragraph("\n"));

                // PASO 3: Procesar cada profesional
                Console.WriteLine($"PASO 3: Procesando {professionalData.Count} profesionales...");
                foreach (var professional in professionalData)
                {
                    Console.WriteLine($"Procesando profesional: {professional.FullName}");

                    var profHeaderFont = new Font(Font.HELVETICA, 12, Font.BOLD, UNIVERSITY_RED);
                    document.Add(new Paragraph($"PROFESIONAL: {professional.FullName}", profHeaderFont));

                    var profInfo = new PdfPTable(2)
                    {
                        WidthPercentage = 100,
                        SpacingBefore = 5f,
                        SpacingAfter = 10f
                    };
                    profInfo.SetWidths(new float[] { 30, 70 });

                    AddUtATableRow(profInfo, "Matrícula:", professional.RegistrationNumber);
                    AddUtATableRow(profInfo, "Tipo:", professional.ProfessionalType);
                    AddUtATableRow(profInfo, "Email:", professional.Email ?? "N/A");
                    AddUtATableRow(profInfo, "Total Consultas:", professional.TotalConsultations.ToString());

                    document.Add(profInfo);

                    // Consultas por área
                    if (professional.ConsultationsByArea.Any())
                    {
                        var areaTitle = new Paragraph("CONSULTAS POR ÁREA", new Font(Font.HELVETICA, 10, Font.BOLD, DARK_GRAY))
                        {
                            SpacingBefore = 5f
                        };
                        document.Add(areaTitle);

                        var areaTable = new PdfPTable(2)
                        {
                            WidthPercentage = 100,
                            SpacingBefore = 5f,
                            SpacingAfter = 10f
                        };
                        areaTable.SetWidths(new float[] { 70, 30 });

                        areaTable.AddCell(CreateUtAHeaderCell("Área"));
                        areaTable.AddCell(CreateUtAHeaderCell("Cantidad"));

                        bool alternate = false;
                        foreach (var area in professional.ConsultationsByArea)
                        {
                            areaTable.AddCell(CreateUtADataCell(area.Key, alternate));
                            areaTable.AddCell(CreateUtADataCell(area.Value.ToString(), alternate));
                            alternate = !alternate;
                        }

                        document.Add(areaTable);
                    }

                    // Línea separadora entre profesionales
                    document.Add(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.5f, 100f, MEDIUM_GRAY, Element.ALIGN_CENTER, 5f)));
                    document.Add(new Paragraph(" "));
                }

                // ===== PIE DE PÁGINA =====
                Console.WriteLine("Agregando pie de página...");
                AddUtAFooter(document);

                document.Close();

                Console.WriteLine($"=== GenerateProfessionalReportAsync COMPLETADO EXITOSAMENTE ===");
                Console.WriteLine($"Tamaño del PDF: {memoryStream.Length} bytes");

                return new ReportResultDTO
                {
                    FileName = $"Reporte_Profesionales_{DateTime.Now:yyyyMMddHHmmss}.pdf",
                    Content = memoryStream.ToArray(),
                    ContentType = "application/pdf",
                    ReportTitle = "Reporte de Profesionales"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== ERROR en GenerateProfessionalReportAsync ===");
                Console.WriteLine($"Mensaje: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner Stack Trace: {ex.InnerException.StackTrace}");
                }

                throw;
            }
        }

        public async Task<ReportResultDTO> GenerateStatisticalReportAsync(ReportRequestDTO request)
        {
            try
            {
                var startDate = request.StartDate ?? DateTime.Now.AddMonths(-1);
                var endDate = request.EndDate ?? DateTime.Now;

                var statisticalData = await GetStatisticalDataAsync(startDate, endDate);

                using var memoryStream = new MemoryStream();
                var document = new Document(PageSize.A4, 40, 40, 50, 40);
                var writer = PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                // ===== ENCABEZADO UTA =====
                AddUtAHeader(document, "REPORTE ESTADÍSTICO");

                var infoFont = new Font(Font.HELVETICA, 10, Font.NORMAL, DARK_GRAY);
                document.Add(new Paragraph($"Período: {statisticalData.Period}", infoFont));
                document.Add(new Paragraph($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}", infoFont));
                document.Add(new Paragraph("\n"));

                // Estadísticas generales
                var statsTitle = new Paragraph("ESTADÍSTICAS GENERALES", new Font(Font.HELVETICA, 12, Font.BOLD, UNIVERSITY_RED))
                {
                    SpacingAfter = 10f
                };
                document.Add(statsTitle);

                var statsTable = new PdfPTable(2)
                {
                    WidthPercentage = 100,
                    SpacingBefore = 10f,
                    SpacingAfter = 20f
                };
                statsTable.SetWidths(new float[] { 50, 50 });

                AddUtATableRow(statsTable, "Total de Atenciones:", statisticalData.TotalConsultations.ToString());
                AddUtATableRow(statsTable, "Pacientes Únicos:", statisticalData.TotalPatients.ToString());
                AddUtATableRow(statsTable, "Profesionales Activos:", statisticalData.TotalProfessionals.ToString());
                AddUtATableRow(statsTable, "Áreas con Atención:", statisticalData.ConsultationsByArea.Count.ToString());

                document.Add(statsTable);

                // Consultas por área
                if (statisticalData.ConsultationsByArea.Any())
                {
                    var areaTitle = new Paragraph("DISTRIBUCIÓN POR ÁREA", new Font(Font.HELVETICA, 12, Font.BOLD, UNIVERSITY_RED))
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 10f
                    };
                    document.Add(areaTitle);

                    var areaTable = new PdfPTable(2)
                    {
                        WidthPercentage = 100,
                        SpacingBefore = 5f,
                        SpacingAfter = 20f
                    };
                    areaTable.SetWidths(new float[] { 70, 30 });

                    areaTable.AddCell(CreateUtAHeaderCell("Área"));
                    areaTable.AddCell(CreateUtAHeaderCell("Consultas"));

                    bool alternate = false;
                    foreach (var area in statisticalData.ConsultationsByArea.OrderByDescending(x => x.Value))
                    {
                        areaTable.AddCell(CreateUtADataCell(area.Key, alternate));
                        areaTable.AddCell(CreateUtADataCell(area.Value.ToString(), alternate));
                        alternate = !alternate;
                    }

                    document.Add(areaTable);
                }

                // Top profesionales
                if (statisticalData.TopProfessionals.Any())
                {
                    var topTitle = new Paragraph("TOP 10 PROFESIONALES", new Font(Font.HELVETICA, 12, Font.BOLD, UNIVERSITY_RED))
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 10f
                    };
                    document.Add(topTitle);

                    var topTable = new PdfPTable(2)
                    {
                        WidthPercentage = 100,
                        SpacingBefore = 5f,
                        SpacingAfter = 20f
                    };
                    topTable.SetWidths(new float[] { 70, 30 });

                    topTable.AddCell(CreateUtAHeaderCell("Profesional"));
                    topTable.AddCell(CreateUtAHeaderCell("Consultas"));

                    bool alternate = false;
                    foreach (var professional in statisticalData.TopProfessionals.OrderByDescending(x => x.Value).Take(10))
                    {
                        topTable.AddCell(CreateUtADataCell(professional.Key, alternate));
                        topTable.AddCell(CreateUtADataCell(professional.Value.ToString(), alternate));
                        alternate = !alternate;
                    }

                    document.Add(topTable);
                }

                // Consultas por mes
                if (statisticalData.ConsultationsByMonth.Any())
                {
                    var monthTitle = new Paragraph("EVOLUCIÓN MENSUAL", new Font(Font.HELVETICA, 12, Font.BOLD, UNIVERSITY_RED))
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 10f
                    };
                    document.Add(monthTitle);

                    var monthTable = new PdfPTable(2)
                    {
                        WidthPercentage = 100,
                        SpacingBefore = 5f,
                        SpacingAfter = 20f
                    };
                    monthTable.SetWidths(new float[] { 50, 50 });

                    monthTable.AddCell(CreateUtAHeaderCell("Mes/Año"));
                    monthTable.AddCell(CreateUtAHeaderCell("Consultas"));

                    bool alternate = false;
                    foreach (var month in statisticalData.ConsultationsByMonth)
                    {
                        monthTable.AddCell(CreateUtADataCell(month.Key, alternate));
                        monthTable.AddCell(CreateUtADataCell(month.Value.ToString(), alternate));
                        alternate = !alternate;
                    }

                    document.Add(monthTable);
                }

                // ===== PIE DE PÁGINA =====
                AddUtAFooter(document);

                document.Close();

                return new ReportResultDTO
                {
                    FileName = $"Reporte_Estadistico_{DateTime.Now:yyyyMMddHHmmss}.pdf",
                    Content = memoryStream.ToArray(),
                    ContentType = "application/pdf",
                    ReportTitle = "Reporte Estadístico"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generando reporte estadístico: {ex.Message}");
                throw;
            }
        }

        public async Task<ReportResultDTO> GenerateProfessionalDetailReportAsync(ReportRequestDTO request)
        {
            try
            {
                var professionalData = await GetProfessionalDetailDataAsync(request);

                using var memoryStream = new MemoryStream();
                var document = new Document(PageSize.A4, 40, 40, 50, 40);
                var writer = PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                AddUtAHeader(document, "REPORTE DETALLADO DE PROFESIONAL");

                var infoFont = new Font(Font.HELVETICA, 10, Font.NORMAL, DARK_GRAY);
                document.Add(new Paragraph($"Profesional: {professionalData.ProfessionalName}", infoFont));
                document.Add(new Paragraph($"Período: {(request.StartDate?.ToString("dd/MM/yyyy") ?? "Inicio")} - {(request.EndDate?.ToString("dd/MM/yyyy") ?? "Fin")}", infoFont));
                document.Add(new Paragraph($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}", infoFont));
                document.Add(new Paragraph("\n"));

                // Estadísticas del profesional
                var statsTable = new PdfPTable(2)
                {
                    WidthPercentage = 100,
                    SpacingBefore = 10f,
                    SpacingAfter = 20f
                };
                statsTable.SetWidths(new float[] { 50, 50 });

                AddUtATableRow(statsTable, "Total Atenciones:", professionalData.TotalConsultations.ToString());
                AddUtATableRow(statsTable, "Pacientes Atendidos:", professionalData.UniquePatients.ToString());
                AddUtATableRow(statsTable, "Áreas de Trabajo:", professionalData.ConsultationsByArea.Count.ToString());
                AddUtATableRow(statsTable, "Ubicaciones:", professionalData.ConsultationsByLocation.Count.ToString());

                document.Add(statsTable);

                // Atenciones por área
                if (professionalData.ConsultationsByArea.Any())
                {
                    var areaTitle = new Paragraph("ATENCIONES POR ÁREA", new Font(Font.HELVETICA, 12, Font.BOLD, UNIVERSITY_RED))
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 10f
                    };
                    document.Add(areaTitle);

                    var areaTable = new PdfPTable(2)
                    {
                        WidthPercentage = 100,
                        SpacingBefore = 5f,
                        SpacingAfter = 20f
                    };
                    areaTable.SetWidths(new float[] { 70, 30 });

                    areaTable.AddCell(CreateUtAHeaderCell("Área"));
                    areaTable.AddCell(CreateUtAHeaderCell("Atenciones"));

                    bool alternate = false;
                    foreach (var area in professionalData.ConsultationsByArea.OrderByDescending(x => x.Value))
                    {
                        areaTable.AddCell(CreateUtADataCell(area.Key, alternate));
                        areaTable.AddCell(CreateUtADataCell(area.Value.ToString(), alternate));
                        alternate = !alternate;
                    }

                    document.Add(areaTable);
                }

                // Atenciones por ubicación
                if (professionalData.ConsultationsByLocation.Any())
                {
                    var locationTitle = new Paragraph("ATENCIONES POR UBICACIÓN", new Font(Font.HELVETICA, 12, Font.BOLD, UNIVERSITY_RED))
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 10f
                    };
                    document.Add(locationTitle);

                    var locationTable = new PdfPTable(2)
                    {
                        WidthPercentage = 100,
                        SpacingBefore = 5f,
                        SpacingAfter = 20f
                    };
                    locationTable.SetWidths(new float[] { 70, 30 });

                    locationTable.AddCell(CreateUtAHeaderCell("Ubicación"));
                    locationTable.AddCell(CreateUtAHeaderCell("Atenciones"));

                    bool alternate = false;
                    foreach (var location in professionalData.ConsultationsByLocation.OrderByDescending(x => x.Value))
                    {
                        locationTable.AddCell(CreateUtADataCell(location.Key, alternate));
                        locationTable.AddCell(CreateUtADataCell(location.Value.ToString(), alternate));
                        alternate = !alternate;
                    }

                    document.Add(locationTable);
                }

                // Últimas atenciones
                if (professionalData.RecentConsultations.Any())
                {
                    var recentTitle = new Paragraph("ÚLTIMAS ATENCIONES", new Font(Font.HELVETICA, 12, Font.BOLD, UNIVERSITY_RED))
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 10f
                    };
                    document.Add(recentTitle);

                    var recentTable = new PdfPTable(4)
                    {
                        WidthPercentage = 100,
                        SpacingBefore = 5f,
                        SpacingAfter = 20f
                    };
                    recentTable.SetWidths(new float[] { 20, 30, 25, 25 });

                    recentTable.AddCell(CreateUtAHeaderCell("Fecha"));
                    recentTable.AddCell(CreateUtAHeaderCell("Paciente"));
                    recentTable.AddCell(CreateUtAHeaderCell("Área"));
                    recentTable.AddCell(CreateUtAHeaderCell("Ubicación"));

                    bool alternate = false;
                    foreach (var consult in professionalData.RecentConsultations.Take(10))
                    {
                        recentTable.AddCell(CreateUtADataCell(consult.CareDate.ToString("dd/MM/yyyy"), alternate));
                        recentTable.AddCell(CreateUtADataCell(consult.PatientName, alternate));
                        recentTable.AddCell(CreateUtADataCell(consult.Area, alternate));
                        recentTable.AddCell(CreateUtADataCell(consult.Location, alternate));
                        alternate = !alternate;
                    }

                    document.Add(recentTable);
                }

                AddUtAFooter(document);
                document.Close();

                return new ReportResultDTO
                {
                    FileName = $"Reporte_Profesional_Detalle_{DateTime.Now:yyyyMMddHHmmss}.pdf",
                    Content = memoryStream.ToArray(),
                    ContentType = "application/pdf",
                    ReportTitle = "Reporte Detallado de Profesional"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generando reporte detallado de profesional: {ex.Message}");
                throw;
            }
        }

        public async Task<ReportResultDTO> GenerateLocationReportAsync(ReportRequestDTO request)
        {
            try
            {
                var locationData = await GetLocationReportDataAsync(request);

                using var memoryStream = new MemoryStream();
                var document = new Document(PageSize.A4, 40, 40, 50, 40);
                var writer = PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                AddUtAHeader(document, "REPORTE POR UBICACIÓN");

                var infoFont = new Font(Font.HELVETICA, 10, Font.NORMAL, DARK_GRAY);
                document.Add(new Paragraph($"Ubicación: {locationData.LocationName}", infoFont));
                document.Add(new Paragraph($"Período: {(request.StartDate?.ToString("dd/MM/yyyy") ?? "Inicio")} - {(request.EndDate?.ToString("dd/MM/yyyy") ?? "Fin")}", infoFont));
                document.Add(new Paragraph($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}", infoFont));
                document.Add(new Paragraph("\n"));

                // Estadísticas de la ubicación
                var statsTable = new PdfPTable(2)
                {
                    WidthPercentage = 100,
                    SpacingBefore = 10f,
                    SpacingAfter = 20f
                };
                statsTable.SetWidths(new float[] { 50, 50 });

                AddUtATableRow(statsTable, "Total Atenciones:", locationData.TotalConsultations.ToString());
                AddUtATableRow(statsTable, "Pacientes Únicos:", locationData.UniquePatients.ToString());
                AddUtATableRow(statsTable, "Profesionales Activos:", locationData.UniqueProfessionals.ToString());
                AddUtATableRow(statsTable, "Días con Atención:", locationData.DaysWithConsultations.Count.ToString());

                document.Add(statsTable);

                // Días con más atenciones
                if (locationData.DaysWithMostConsultations.Any())
                {
                    var daysTitle = new Paragraph("DÍAS CON MÁS ATENCIONES", new Font(Font.HELVETICA, 12, Font.BOLD, UNIVERSITY_RED))
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 10f
                    };
                    document.Add(daysTitle);

                    var daysTable = new PdfPTable(2)
                    {
                        WidthPercentage = 100,
                        SpacingBefore = 5f,
                        SpacingAfter = 20f
                    };
                    daysTable.SetWidths(new float[] { 50, 50 });

                    daysTable.AddCell(CreateUtAHeaderCell("Fecha"));
                    daysTable.AddCell(CreateUtAHeaderCell("Atenciones"));

                    bool alternate = false;
                    foreach (var day in locationData.DaysWithMostConsultations.OrderByDescending(x => x.Value).Take(10))
                    {
                        daysTable.AddCell(CreateUtADataCell(day.Key.ToString("dd/MM/yyyy"), alternate));
                        daysTable.AddCell(CreateUtADataCell(day.Value.ToString(), alternate));
                        alternate = !alternate;
                    }

                    document.Add(daysTable);
                }

                // Profesionales en esta ubicación
                if (locationData.ProfessionalsByConsultations.Any())
                {
                    var profTitle = new Paragraph("PROFESIONALES EN ESTA UBICACIÓN", new Font(Font.HELVETICA, 12, Font.BOLD, UNIVERSITY_RED))
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 10f
                    };
                    document.Add(profTitle);

                    var profTable = new PdfPTable(2)
                    {
                        WidthPercentage = 100,
                        SpacingBefore = 5f,
                        SpacingAfter = 20f
                    };
                    profTable.SetWidths(new float[] { 70, 30 });

                    profTable.AddCell(CreateUtAHeaderCell("Profesional"));
                    profTable.AddCell(CreateUtAHeaderCell("Atenciones"));

                    bool alternate = false;
                    foreach (var prof in locationData.ProfessionalsByConsultations.OrderByDescending(x => x.Value))
                    {
                        profTable.AddCell(CreateUtADataCell(prof.Key, alternate));
                        profTable.AddCell(CreateUtADataCell(prof.Value.ToString(), alternate));
                        alternate = !alternate;
                    }

                    document.Add(profTable);
                }

                AddUtAFooter(document);
                document.Close();

                return new ReportResultDTO
                {
                    FileName = $"Reporte_Ubicacion_{DateTime.Now:yyyyMMddHHmmss}.pdf",
                    Content = memoryStream.ToArray(),
                    ContentType = "application/pdf",
                    ReportTitle = "Reporte por Ubicación"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generando reporte por ubicación: {ex.Message}");
                throw;
            }
        }

        public async Task<ReportResultDTO> GenerateAreaReportAsync(ReportRequestDTO request)
        {
            try
            {
                var areaData = await GetAreaReportDataAsync(request);

                using var memoryStream = new MemoryStream();
                var document = new Document(PageSize.A4, 40, 40, 50, 40);
                var writer = PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                AddUtAHeader(document, "REPORTE POR ÁREA");

                var infoFont = new Font(Font.HELVETICA, 10, Font.NORMAL, DARK_GRAY);
                document.Add(new Paragraph($"Área: {areaData.AreaName}", infoFont));
                document.Add(new Paragraph($"Período: {(request.StartDate?.ToString("dd/MM/yyyy") ?? "Inicio")} - {(request.EndDate?.ToString("dd/MM/yyyy") ?? "Fin")}", infoFont));
                document.Add(new Paragraph($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}", infoFont));
                document.Add(new Paragraph("\n"));

                // Estadísticas del área
                var statsTable = new PdfPTable(2)
                {
                    WidthPercentage = 100,
                    SpacingBefore = 10f,
                    SpacingAfter = 20f
                };
                statsTable.SetWidths(new float[] { 50, 50 });

                AddUtATableRow(statsTable, "Total Atenciones:", areaData.TotalConsultations.ToString());
                AddUtATableRow(statsTable, "Pacientes Únicos:", areaData.UniquePatients.ToString());
                AddUtATableRow(statsTable, "Profesionales Activos:", areaData.UniqueProfessionals.ToString());
                AddUtATableRow(statsTable, "Semanas con Atención:", areaData.WeeksWithConsultations.Count.ToString());

                document.Add(statsTable);

                // Semanas con más atenciones
                if (areaData.WeeksWithMostConsultations.Any())
                {
                    var weeksTitle = new Paragraph("SEMANAS CON MÁS ATENCIONES", new Font(Font.HELVETICA, 12, Font.BOLD, UNIVERSITY_RED))
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 10f
                    };
                    document.Add(weeksTitle);

                    var weeksTable = new PdfPTable(2)
                    {
                        WidthPercentage = 100,
                        SpacingBefore = 5f,
                        SpacingAfter = 20f
                    };
                    weeksTable.SetWidths(new float[] { 50, 50 });

                    weeksTable.AddCell(CreateUtAHeaderCell("Semana"));
                    weeksTable.AddCell(CreateUtAHeaderCell("Atenciones"));

                    bool alternate = false;
                    foreach (var week in areaData.WeeksWithMostConsultations.OrderByDescending(x => x.Value).Take(8))
                    {
                        weeksTable.AddCell(CreateUtADataCell(week.Key, alternate));
                        weeksTable.AddCell(CreateUtADataCell(week.Value.ToString(), alternate));
                        alternate = !alternate;
                    }

                    document.Add(weeksTable);
                }

                // Días de la semana con más atenciones
                if (areaData.ConsultationsByDayOfWeek.Any())
                {
                    var daysTitle = new Paragraph("ATENCIONES POR DÍA DE LA SEMANA", new Font(Font.HELVETICA, 12, Font.BOLD, UNIVERSITY_RED))
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 10f
                    };
                    document.Add(daysTitle);

                    var daysTable = new PdfPTable(2)
                    {
                        WidthPercentage = 100,
                        SpacingBefore = 5f,
                        SpacingAfter = 20f
                    };
                    daysTable.SetWidths(new float[] { 50, 50 });

                    daysTable.AddCell(CreateUtAHeaderCell("Día"));
                    daysTable.AddCell(CreateUtAHeaderCell("Atenciones"));

                    bool alternate = false;
                    var dayNames = new Dictionary<int, string>
                    {
                        { 1, "Lunes" }, { 2, "Martes" }, { 3, "Miércoles" },
                        { 4, "Jueves" }, { 5, "Viernes" }, { 6, "Sábado" }, { 0, "Domingo" }
                    };

                    foreach (var day in areaData.ConsultationsByDayOfWeek.OrderByDescending(x => x.Value))
                    {
                        var dayName = dayNames.ContainsKey(day.Key) ? dayNames[day.Key] : $"Día {day.Key}";
                        daysTable.AddCell(CreateUtADataCell(dayName, alternate));
                        daysTable.AddCell(CreateUtADataCell(day.Value.ToString(), alternate));
                        alternate = !alternate;
                    }

                    document.Add(daysTable);
                }

                AddUtAFooter(document);
                document.Close();

                return new ReportResultDTO
                {
                    FileName = $"Reporte_Area_{DateTime.Now:yyyyMMddHHmmss}.pdf",
                    Content = memoryStream.ToArray(),
                    ContentType = "application/pdf",
                    ReportTitle = "Reporte por Área"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generando reporte por área: {ex.Message}");
                throw;
            }
        }

        public async Task<ReportResultDTO> GenerateTopPatientReportAsync(ReportRequestDTO request)
        {
            try
            {
                var topPatientsData = await GetTopPatientsDataAsync(request);

                using var memoryStream = new MemoryStream();
                var document = new Document(PageSize.A4, 40, 40, 50, 40);
                var writer = PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                AddUtAHeader(document, "REPORTE DE PACIENTES CON MÁS ATENCIONES");

                var infoFont = new Font(Font.HELVETICA, 10, Font.NORMAL, DARK_GRAY);
                document.Add(new Paragraph($"Período: {(request.StartDate?.ToString("dd/MM/yyyy") ?? "Inicio")} - {(request.EndDate?.ToString("dd/MM/yyyy") ?? "Fin")}", infoFont));
                document.Add(new Paragraph($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}", infoFont));
                document.Add(new Paragraph("\n"));

                // Top pacientes
                if (topPatientsData.Any())
                {
                    var topTable = new PdfPTable(4)
                    {
                        WidthPercentage = 100,
                        SpacingBefore = 10f,
                        SpacingAfter = 20f
                    };
                    topTable.SetWidths(new float[] { 10, 40, 25, 25 });

                    topTable.AddCell(CreateUtAHeaderCell("#"));
                    topTable.AddCell(CreateUtAHeaderCell("Paciente"));
                    topTable.AddCell(CreateUtAHeaderCell("Total Atenciones"));
                    topTable.AddCell(CreateUtAHeaderCell("Última Atención"));

                    bool alternate = false;
                    int rank = 1;
                    foreach (var patient in topPatientsData.OrderByDescending(x => x.TotalConsultations).Take(20))
                    {
                        topTable.AddCell(CreateUtADataCell(rank.ToString(), alternate));
                        topTable.AddCell(CreateUtADataCell(patient.FullName, alternate));
                        topTable.AddCell(CreateUtADataCell(patient.TotalConsultations.ToString(), alternate));
                        topTable.AddCell(CreateUtADataCell(patient.LastConsultation?.ToString("dd/MM/yyyy") ?? "N/A", alternate));
                        alternate = !alternate;
                        rank++;
                    }

                    document.Add(topTable);
                }
                else
                {
                    var noDataFont = new Font(Font.HELVETICA, 12, Font.ITALIC, DARK_GRAY);
                    document.Add(new Paragraph("No se encontraron pacientes con atenciones en el período seleccionado.", noDataFont));
                }

                AddUtAFooter(document);
                document.Close();

                return new ReportResultDTO
                {
                    FileName = $"Reporte_Top_Pacientes_{DateTime.Now:yyyyMMddHHmmss}.pdf",
                    Content = memoryStream.ToArray(),
                    ContentType = "application/pdf",
                    ReportTitle = "Reporte de Pacientes con Más Atenciones"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generando reporte de top pacientes: {ex.Message}");
                throw;
            }
        }

        // ===== MÉTODOS AUXILIARES UTA =====

        private void AddUtAHeader(Document document, string reportTitle)
        {
            var headerTable = new PdfPTable(3)
            {
                WidthPercentage = 100,
                SpacingAfter = 20f
            };
            headerTable.SetWidths(new float[] { 20, 60, 20 });

            // Logo
            var logoCell = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Padding = 8,
                BackgroundColor = UNIVERSITY_RED
            };

            try
            {
                var logoPath = Path.Combine(_environment.ContentRootPath, "Resources", "Images", "logo-uta.jpg");
                if (File.Exists(logoPath))
                {
                    var logo = Image.GetInstance(logoPath);
                    logo.ScaleToFit(70, 70);
                    logo.Alignment = Image.ALIGN_CENTER;
                    logoCell.AddElement(logo);
                }
                else
                {
                    var fallbackLogo = new Paragraph("UTA",
                        new Font(FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, Font.BOLD, UNIVERSITY_WHITE)))
                    {
                        Alignment = Element.ALIGN_CENTER
                    };
                    logoCell.AddElement(fallbackLogo);
                }
            }
            catch
            {
                var fallbackLogo = new Paragraph("UTA",
                    new Font(FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, Font.BOLD, UNIVERSITY_WHITE)))
                {
                    Alignment = Element.ALIGN_CENTER
                };
                logoCell.AddElement(fallbackLogo);
            }

            var textCell = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Padding = 10,
                BackgroundColor = UNIVERSITY_RED
            };

            var universityName = new Paragraph("UNIVERSIDAD TÉCNICA DE AMBATO",
                new Font(FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, Font.BOLD, UNIVERSITY_WHITE)))
            {
                Alignment = Element.ALIGN_CENTER
            };
            var systemName = new Paragraph("SISTEMA DE REPORTES MÉDICOS",
                new Font(FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, Font.BOLD, UNIVERSITY_WHITE)))
            {
                SpacingBefore = 5f,
                Alignment = Element.ALIGN_CENTER
            };
            var title = new Paragraph(reportTitle,
                new Font(FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, Font.BOLD, UNIVERSITY_WHITE)))
            {
                SpacingBefore = 5f,
                Alignment = Element.ALIGN_CENTER
            };

            textCell.AddElement(universityName);
            textCell.AddElement(systemName);
            textCell.AddElement(title);

            var emptyCell = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                BackgroundColor = UNIVERSITY_RED
            };

            headerTable.AddCell(logoCell);
            headerTable.AddCell(textCell);
            headerTable.AddCell(emptyCell);

            document.Add(headerTable);
        }

        private void AddUtAFooter(Document document)
        {
            var footerLine = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(1f, 100f, MEDIUM_GRAY, Element.ALIGN_CENTER, -2f)))
            {
                SpacingBefore = 20f,
                SpacingAfter = 10f
            };
            document.Add(footerLine);

            var footer = new Paragraph($"Reporte generado automáticamente - Universidad Técnica de Ambato © {DateTime.Now.Year}",
                new Font(Font.HELVETICA, 8, Font.NORMAL, MEDIUM_GRAY))
            {
                Alignment = Element.ALIGN_CENTER
            };
            document.Add(footer);
        }

        private PdfPCell CreateUtAHeaderCell(string text)
        {
            var cell = new PdfPCell(new Phrase(text, new Font(Font.HELVETICA, 10, Font.BOLD, UNIVERSITY_WHITE)))
            {
                BackgroundColor = UNIVERSITY_RED,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 8f,
                Border = Rectangle.BOX,
                BorderColor = UNIVERSITY_RED
            };
            return cell;
        }

        private PdfPCell CreateUtADataCell(string text, bool alternate = false)
        {
            var cell = new PdfPCell(new Phrase(text, new Font(Font.HELVETICA, 9, Font.NORMAL, DARK_GRAY)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 8f,
                Border = Rectangle.BOX,
                BorderColor = BaseColor.LightGray
            };

            // Alternar colores de fondo para mejor legibilidad
            if (alternate)
            {
                cell.BackgroundColor = LIGHT_GRAY;
            }

            return cell;
        }

        private void AddUtATableRow(PdfPTable table, string label, string value)
        {
            table.AddCell(CreateUtAHeaderCell(label));
            table.AddCell(CreateUtADataCell(value));
        }

        // ===== MÉTODOS DE DATOS NUEVOS =====

        public async Task<ProfessionalDetailReportDTO> GetProfessionalDetailDataAsync(ReportRequestDTO request)
        {
            if (!request.HealthProfessionalId.HasValue)
                throw new ArgumentException("Se requiere un profesional específico para este reporte");

            var consultations = await _context.MedicalCares
                .Where(mc => mc.HealthProfessionalId == request.HealthProfessionalId.Value)
                .Include(mc => mc.LocationNavigation)
                .Include(mc => mc.PlaceOfAttentionNavigation)
                .Include(mc => mc.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(mc => mc.HealthProfessional)
                    .ThenInclude(hp => hp.PersonNavigation)
                .ToListAsync();

            if (request.StartDate.HasValue)
                consultations = consultations.Where(mc => mc.CareDate >= request.StartDate.Value).ToList();
            if (request.EndDate.HasValue)
                consultations = consultations.Where(mc => mc.CareDate <= request.EndDate.Value).ToList();

            var professional = consultations.FirstOrDefault()?.HealthProfessional;

            return new ProfessionalDetailReportDTO
            {
                ProfessionalName = professional?.PersonNavigation != null ?
                    $"{professional.PersonNavigation.FirstName} {professional.PersonNavigation.LastName}" : "Profesional no encontrado",
                TotalConsultations = consultations.Count,
                UniquePatients = consultations.Select(mc => mc.PatientId).Distinct().Count(),
                ConsultationsByArea = consultations
                    .GroupBy(mc => mc.LocationNavigation?.Name ?? "Sin área")
                    .ToDictionary(g => g.Key, g => g.Count()),
                ConsultationsByLocation = consultations
                    .GroupBy(mc => mc.PlaceOfAttentionNavigation?.Name ?? "Sin ubicación")
                    .ToDictionary(g => g.Key, g => g.Count()),
                RecentConsultations = consultations
                    .OrderByDescending(mc => mc.CareDate)
                    .Take(20)
                    .Select(mc => new ProfessionalConsultationDTO
                    {
                        CareDate = mc.CareDate,
                        PatientName = mc.Patient?.PersonNavigation != null ?
                            $"{mc.Patient.PersonNavigation.FirstName} {mc.Patient.PersonNavigation.LastName}" : "Sin paciente",
                        Area = mc.LocationNavigation?.Name ?? "Sin área",
                        Location = mc.PlaceOfAttentionNavigation?.Name ?? "Sin ubicación"
                    })
                    .ToList()
            };
        }

        public async Task<LocationReportDTO> GetLocationReportDataAsync(ReportRequestDTO request)
        {
            if (!request.PlaceOfAttentionId.HasValue)
                throw new ArgumentException("Se requiere una ubicación específica para este reporte");

            var consultations = await _context.MedicalCares
                .Where(mc => mc.PlaceOfAttentionId == request.PlaceOfAttentionId.Value)
                .Include(mc => mc.LocationNavigation)
                .Include(mc => mc.PlaceOfAttentionNavigation)
                .Include(mc => mc.Patient)
                .Include(mc => mc.HealthProfessional)
                    .ThenInclude(hp => hp.PersonNavigation)
                .ToListAsync();

            if (request.StartDate.HasValue)
                consultations = consultations.Where(mc => mc.CareDate >= request.StartDate.Value).ToList();
            if (request.EndDate.HasValue)
                consultations = consultations.Where(mc => mc.CareDate <= request.EndDate.Value).ToList();

            var location = consultations.FirstOrDefault()?.PlaceOfAttentionNavigation;

            var daysWithConsultations = consultations
                .GroupBy(mc => mc.CareDate.Date)
                .Select(g => g.Key)
                .ToList();

            return new LocationReportDTO
            {
                LocationName = location?.Name ?? "Ubicación no encontrada",
                TotalConsultations = consultations.Count,
                UniquePatients = consultations.Select(mc => mc.PatientId).Distinct().Count(),
                UniqueProfessionals = consultations.Select(mc => mc.HealthProfessionalId).Distinct().Count(),
                DaysWithConsultations = daysWithConsultations,
                DaysWithMostConsultations = consultations
                    .GroupBy(mc => mc.CareDate.Date)
                    .ToDictionary(g => g.Key, g => g.Count()),
                ProfessionalsByConsultations = consultations
                    .GroupBy(mc => mc.HealthProfessional?.PersonNavigation != null ?
                        $"{mc.HealthProfessional.PersonNavigation.FirstName} {mc.HealthProfessional.PersonNavigation.LastName}" : "Sin profesional")
                    .ToDictionary(g => g.Key, g => g.Count())
            };
        }

        public async Task<AreaReportDTO> GetAreaReportDataAsync(ReportRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Area))
                throw new ArgumentException("Se requiere un área específica para este reporte");

            var consultations = await _context.MedicalCares
                .Where(mc => mc.LocationNavigation != null && mc.LocationNavigation.Name == request.Area)
                .Include(mc => mc.LocationNavigation)
                .Include(mc => mc.PlaceOfAttentionNavigation)
                .Include(mc => mc.Patient)
                .Include(mc => mc.HealthProfessional)
                .ToListAsync();

            if (request.StartDate.HasValue)
                consultations = consultations.Where(mc => mc.CareDate >= request.StartDate.Value).ToList();
            if (request.EndDate.HasValue)
                consultations = consultations.Where(mc => mc.CareDate <= request.EndDate.Value).ToList();

            var weeksWithConsultations = consultations
                .Select(mc => System.Globalization.ISOWeek.GetWeekOfYear(mc.CareDate))
                .Distinct()
                .ToList();

            return new AreaReportDTO
            {
                AreaName = request.Area,
                TotalConsultations = consultations.Count,
                UniquePatients = consultations.Select(mc => mc.PatientId).Distinct().Count(),
                UniqueProfessionals = consultations.Select(mc => mc.HealthProfessionalId).Distinct().Count(),
                WeeksWithConsultations = weeksWithConsultations,
                WeeksWithMostConsultations = consultations
                    .GroupBy(mc => $"Sem {System.Globalization.ISOWeek.GetWeekOfYear(mc.CareDate)}/{mc.CareDate.Year}")
                    .ToDictionary(g => g.Key, g => g.Count()),
                ConsultationsByDayOfWeek = consultations
                    .GroupBy(mc => (int)mc.CareDate.DayOfWeek)
                    .ToDictionary(g => g.Key, g => g.Count())
            };
        }

        public async Task<List<PatientReportDTO>> GetTopPatientsDataAsync(ReportRequestDTO request)
        {
            var patientData = await GetPatientReportDataAsync(request);
            return patientData
                .Where(p => p.TotalConsultations > 0)
                .OrderByDescending(p => p.TotalConsultations)
                .ToList();
        }

        // ===== MÉTODOS DE DATOS EXISTENTES =====

        public async Task<StatisticalReportDTO> GetStatisticalDataAsync(DateTime? startDate, DateTime? endDate, bool includeAllRecords = true)
        {
            var consultationsQuery = _context.MedicalCares
                .Include(mc => mc.LocationNavigation)
                .Include(mc => mc.HealthProfessional)
                    .ThenInclude(hp => hp.PersonNavigation)
                .AsQueryable();

            // Solo aplicar filtros de fecha si no se quieren todos los registros
            if (!includeAllRecords && startDate.HasValue && endDate.HasValue)
            {
                consultationsQuery = consultationsQuery.Where(mc => mc.CareDate >= startDate.Value && mc.CareDate <= endDate.Value);
            }

            var consultations = await consultationsQuery.ToListAsync();

            var uniquePatients = consultations.Select(mc => mc.PatientId).Distinct().Count();
            var uniqueProfessionals = consultations.Select(mc => mc.HealthProfessionalId).Distinct().Count();

            var consultationsByArea = consultations
                .GroupBy(mc => mc.LocationNavigation?.Name ?? "Sin área")
                .ToDictionary(g => g.Key, g => g.Count());

            var consultationsByMonth = consultations
                .GroupBy(mc => new { mc.CareDate.Year, mc.CareDate.Month })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month)
                .ToDictionary(
                    g => $"{g.Key.Month:00}/{g.Key.Year}",
                    g => g.Count()
                );

            var topProfessionals = consultations
                .GroupBy(mc => new {
                    mc.HealthProfessionalId,
                    Name = mc.HealthProfessional != null ?
                        $"{mc.HealthProfessional.PersonNavigation?.FirstName} {mc.HealthProfessional.PersonNavigation?.LastName}"
                        : "Sin profesional"
                })
                .OrderByDescending(g => g.Count())
                .Take(10)
                .ToDictionary(
                    g => g.Key.Name,
                    g => g.Count()
                );

            var period = includeAllRecords ? "Todos los registros" :
                (startDate.HasValue && endDate.HasValue ? $"{startDate:dd/MM/yyyy} - {endDate:dd/MM/yyyy}" : "Sin período definido");

            return new StatisticalReportDTO
            {
                Period = period,
                TotalConsultations = consultations.Count,
                TotalPatients = uniquePatients,
                TotalProfessionals = uniqueProfessionals,
                ConsultationsByArea = consultationsByArea,
                ConsultationsByMonth = consultationsByMonth,
                TopProfessionals = topProfessionals
            };
        }

        public async Task<List<PatientReportDTO>> GetPatientReportDataAsync(ReportRequestDTO request)
        {
            var query = _context.Persons
                .Include(p => p.PersonDocument)
                .Include(p => p.PersonPhone)
                .Include(p => p.PersonBloodGroup)
                    .ThenInclude(pbg => pbg.BloodGroupNavigation)
                .Include(p => p.PersonMaritalStatus)
                    .ThenInclude(pms => pms.MaritalStatusNavigation)
                .Include(p => p.PersonMedicalInsurances)
                    .ThenInclude(pmi => pmi.MedicalInsuranceNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.DocumentNumber))
            {
                query = query.Where(p => p.PersonDocument != null &&
                    p.PersonDocument.DocumentNumber.Contains(request.DocumentNumber));
            }

            var persons = await query.ToListAsync();
            var result = new List<PatientReportDTO>();

            foreach (var person in persons)
            {
                var consultations = await _context.MedicalCares
                    .Where(mc => mc.PatientId == person.Id)
                    .Include(mc => mc.LocationNavigation)
                    .Include(mc => mc.HealthProfessional)
                        .ThenInclude(hp => hp.PersonNavigation)
                    .OrderByDescending(mc => mc.CareDate)
                    .ToListAsync();

                if (request.StartDate.HasValue)
                {
                    consultations = consultations.Where(mc => mc.CareDate >= request.StartDate.Value).ToList();
                }
                if (request.EndDate.HasValue)
                {
                    consultations = consultations.Where(mc => mc.CareDate <= request.EndDate.Value).ToList();
                }

                var patientReport = new PatientReportDTO
                {
                    FullName = $"{person.FirstName} {person.LastName}",
                    DocumentNumber = person.PersonDocument?.DocumentNumber ?? "Sin documento",
                    BirthDate = person.BirthDate,
                    Email = person.Email,
                    Phone = person.PersonPhone?.Mobile ?? person.PersonPhone?.Landline,
                    BloodGroup = person.PersonBloodGroup?.BloodGroupNavigation?.Name,
                    MaritalStatus = person.PersonMaritalStatus?.MaritalStatusNavigation?.Name,
                    MedicalInsurances = person.PersonMedicalInsurances?
                        .Select(pmi => pmi.MedicalInsuranceNavigation?.Name)
                        .Where(name => !string.IsNullOrEmpty(name))
                        .ToList() ?? new List<string>(),
                    TotalConsultations = consultations.Count,
                    LastConsultation = consultations.FirstOrDefault()?.CareDate,
                    Consultations = consultations.Select(c => new ConsultationSummaryDTO
                    {
                        CareDate = c.CareDate,
                        Area = c.LocationNavigation?.Name ?? "Sin área",
                        Professional = c.HealthProfessional?.PersonNavigation != null ?
                            $"{c.HealthProfessional.PersonNavigation.FirstName} {c.HealthProfessional.PersonNavigation.LastName}"
                            : "Sin profesional"
                    }).ToList()
                };

                result.Add(patientReport);
            }

            return result;
        }

        public async Task<List<ProfessionalReportDTO>> GetProfessionalReportDataAsync(ReportRequestDTO request)
        {
            try
            {
                Console.WriteLine($"GetProfessionalReportDataAsync called with:");
                Console.WriteLine($"HealthProfessionalId: {request.HealthProfessionalId}");
                Console.WriteLine($"StartDate: {request.StartDate}");
                Console.WriteLine($"EndDate: {request.EndDate}");
                Console.WriteLine($"IncludeAllRecords: {request.IncludeAllRecords}");

                // PASO 1: Obtener profesionales de forma separada
                var professionalsQuery = _context.HealthProfessionals
                    .Where(h => h.HealthProfessionalTypeId != null && h.RegistrationNumber != null)
                    .Include(h => h.PersonNavigation)
                    .Include(h => h.HealthProfessionalTypeNavigation)
                    .AsQueryable();

                if (request.HealthProfessionalId.HasValue)
                {
                    professionalsQuery = professionalsQuery.Where(h => h.HealthProfessionalId == request.HealthProfessionalId.Value);
                }

                var professionals = await professionalsQuery.ToListAsync();
                Console.WriteLine($"Found {professionals.Count} professionals");

                var result = new List<ProfessionalReportDTO>();

                // PASO 2: Procesar cada profesional por separado
                foreach (var professional in professionals)
                {
                    Console.WriteLine($"Processing professional: {professional.HealthProfessionalId}");

                    // Construir nombre completo
                    var fullName = "Sin nombre";
                    if (professional.PersonNavigation != null)
                    {
                        var nameParts = new List<string>();
                        if (!string.IsNullOrWhiteSpace(professional.PersonNavigation.FirstName))
                            nameParts.Add(professional.PersonNavigation.FirstName);
                        if (!string.IsNullOrWhiteSpace(professional.PersonNavigation.MiddleName))
                            nameParts.Add(professional.PersonNavigation.MiddleName);
                        if (!string.IsNullOrWhiteSpace(professional.PersonNavigation.LastName))
                            nameParts.Add(professional.PersonNavigation.LastName);
                        if (!string.IsNullOrWhiteSpace(professional.PersonNavigation.SecondLastName))
                            nameParts.Add(professional.PersonNavigation.SecondLastName);

                        fullName = string.Join(" ", nameParts);
                    }

                    // Obtener consultas del profesional
                    var consultationsQuery = _context.MedicalCares
                        .Where(mc => mc.HealthProfessionalId == professional.HealthProfessionalId)
                        .Include(mc => mc.LocationNavigation)
                        .Include(mc => mc.Patient)
                            .ThenInclude(p => p.PersonNavigation)
                        .AsQueryable();

                    // MODIFICACIÓN: Solo aplicar filtros de fecha si IncludeAllRecords es false
                    if (!request.IncludeAllRecords)
                    {
                        if (request.StartDate.HasValue)
                        {
                            consultationsQuery = consultationsQuery.Where(mc => mc.CareDate >= request.StartDate.Value);
                        }
                        if (request.EndDate.HasValue)
                        {
                            consultationsQuery = consultationsQuery.Where(mc => mc.CareDate <= request.EndDate.Value);
                        }
                    }

                    var consultations = await consultationsQuery.ToListAsync();
                    Console.WriteLine($"Professional {fullName} has {consultations.Count} consultations");

                    // Procesar datos de consultas
                    var consultationsByArea = consultations
                        .GroupBy(mc => mc.LocationNavigation?.Name ?? "Sin área")
                        .ToDictionary(g => g.Key, g => g.Count());

                    var consultationsByMonth = consultations
                        .GroupBy(mc => new { mc.CareDate.Year, mc.CareDate.Month })
                        .ToDictionary(
                            g => $"{g.Key.Month:00}/{g.Key.Year}",
                            g => g.Count()
                        );

                    var recentConsultations = consultations
                        .OrderByDescending(mc => mc.CareDate)
                        .Take(10)
                        .Select(mc => new PatientConsultationDTO
                        {
                            PatientName = mc.Patient?.PersonNavigation != null ?
                                $"{mc.Patient.PersonNavigation.FirstName} {mc.Patient.PersonNavigation.LastName}"
                                : "Sin paciente",
                            CareDate = mc.CareDate,
                            Area = mc.LocationNavigation?.Name ?? "Sin área"
                        })
                        .ToList();

                    var professionalReport = new ProfessionalReportDTO
                    {
                        FullName = fullName,
                        RegistrationNumber = professional.RegistrationNumber ?? "Sin matrícula",
                        ProfessionalType = professional.HealthProfessionalTypeNavigation?.Name ?? "Sin tipo",
                        Email = professional.PersonNavigation?.Email,
                        TotalConsultations = consultations.Count,
                        ConsultationsByArea = consultationsByArea,
                        ConsultationsByMonth = consultationsByMonth,
                        RecentConsultations = recentConsultations
                    };

                    result.Add(professionalReport);
                }

                Console.WriteLine($"Returning {result.Count} professional reports");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR in GetProfessionalReportDataAsync: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        private async Task<List<MedicalCare>> GetMedicalCaresByFilterAsync(ReportRequestDTO request)
        {
            var query = _context.MedicalCares
                .Include(mc => mc.LocationNavigation)
                .Include(mc => mc.PlaceOfAttentionNavigation)
                .Include(mc => mc.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(mc => mc.HealthProfessional)
                    .ThenInclude(hp => hp.PersonNavigation)
                .AsQueryable();

            if (request.StartDate.HasValue)
            {
                query = query.Where(mc => mc.CareDate >= request.StartDate.Value);
            }
            if (request.EndDate.HasValue)
            {
                query = query.Where(mc => mc.CareDate <= request.EndDate.Value);
            }
            if (request.LocationId.HasValue)
            {
                query = query.Where(mc => mc.LocationId == request.LocationId.Value);
            }
            if (request.HealthProfessionalId.HasValue)
            {
                query = query.Where(mc => mc.HealthProfessionalId == request.HealthProfessionalId.Value);
            }
            if (!string.IsNullOrEmpty(request.Area))
            {
                query = query.Where(mc => mc.LocationNavigation != null &&
                    mc.LocationNavigation.Name.Contains(request.Area));
            }
            if (request.PlaceOfAttentionId.HasValue)
            {
                query = query.Where(mc => mc.PlaceOfAttentionId == request.PlaceOfAttentionId.Value);
            }

            return await query.OrderByDescending(mc => mc.CareDate).ToListAsync();
        }
    }
}