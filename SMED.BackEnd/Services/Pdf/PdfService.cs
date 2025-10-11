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

        public async Task<byte[]> GenerateClinicalHistoryPdfAsync(ClinicalHistoryDTO clinicalHistory)
        {
            try
            {
                using var memoryStream = new MemoryStream();

                // Configurar documento A4 con márgenes
                var document = new Document(PageSize.A4, 40, 40, 100, 40);
                PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                // Colores corporativos
                var universityRed = new BaseColor(109, 19, 18);
                var universityWhite = new BaseColor(255, 255, 254);
                var darkGray = new BaseColor(64, 64, 64);
                var mediumGray = new BaseColor(128, 128, 128);
                var lightGray = new BaseColor(240, 240, 240);


                // Configurar fuentes
                var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, universityWhite);
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, universityRed);
                var subtitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, darkGray);
                var textFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, darkGray);
                var labelFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, darkGray);
                var smallFont = FontFactory.GetFont(FontFactory.HELVETICA, 8, mediumGray);

                // ===== ENCABEZADO =====
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
                    BackgroundColor = universityRed
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
                            FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, universityWhite))
                        {
                            Alignment = Element.ALIGN_CENTER
                        };
                        logoCell.AddElement(fallbackLogo);
                    }
                }
                catch
                {
                    var fallbackLogo = new Paragraph("UTA",
                        FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, universityWhite))
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
                    BackgroundColor = universityRed
                };

                var universityName = new Paragraph("UNIVERSIDAD TÉCNICA DE AMBATO", headerFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                var systemName = new Paragraph("HISTORIA CLÍNICA",
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

                // ===== INFORMACIÓN BÁSICA =====
                AddClinicalHistoryBasicInfo(document, clinicalHistory, titleFont, labelFont, textFont, lightGray);

                // ===== SECCIONES DE LA HISTORIA CLÍNICA =====
                AddClinicalHistorySections(document, clinicalHistory, titleFont, subtitleFont, labelFont, textFont, lightGray);

                // ===== PIE DE PÁGINA =====
                var footerLine = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(1f, 100f, mediumGray, Element.ALIGN_CENTER, -2f)))
                {
                    SpacingBefore = 20f,
                    SpacingAfter = 10f
                };
                document.Add(footerLine);

                var footer = new Paragraph($"Historia Clínica generada automáticamente - Universidad Técnica de Ambato © {DateTime.Now.Year}", smallFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(footer);

                document.Close();
                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generando PDF de historia clínica: {ex.Message}");
                throw;
            }
        }

        private void AddClinicalHistoryBasicInfo(Document document, ClinicalHistoryDTO clinicalHistory, Font titleFont, Font labelFont, Font textFont, BaseColor lightGray)
        {
            var sectionTitle = new Paragraph("INFORMACIÓN BÁSICA", titleFont)
            {
                SpacingAfter = 10f
            };
            document.Add(sectionTitle);

            var basicTable = new PdfPTable(2)
            {
                WidthPercentage = 100,
                SpacingAfter = 20f
            };
            basicTable.SetWidths(new float[] { 30, 70 });

            // Información del paciente
            var patient = clinicalHistory.Patient?.Person;
            AddTableRow(basicTable, "Paciente:", $"{patient?.FirstName} {patient?.LastName}".Trim(), labelFont, textFont, lightGray);
            AddTableRow(basicTable, "Cédula:", patient?.Document?.DocumentNumber ?? "N/A", labelFont, textFont);
            AddTableRow(basicTable, "Fecha Nacimiento:", patient?.BirthDate?.ToString("dd/MM/yyyy") ?? "N/A", labelFont, textFont, lightGray);

            // Información de la historia clínica
            AddTableRow(basicTable, "N° Historia:", clinicalHistory.HistoryNumber ?? "N/A", labelFont, textFont);
            AddTableRow(basicTable, "Fecha Creación:", clinicalHistory.CreationDate?.ToString("dd/MM/yyyy") ?? "N/A", labelFont, textFont, lightGray);
            AddTableRow(basicTable, "Estado:", clinicalHistory.IsActive == true ? "Activa" : "Inactiva", labelFont, textFont);

            document.Add(basicTable);

            // Observaciones Generales
            if (!string.IsNullOrWhiteSpace(clinicalHistory.GeneralObservations))
            {
                AddSectionTitle(document, "OBSERVACIONES GENERALES", titleFont);
                var observations = new Paragraph(clinicalHistory.GeneralObservations, textFont)
                {
                    SpacingAfter = 15f
                };
                document.Add(observations);
            }
        }


        private void AddClinicalHistorySections(Document document, ClinicalHistoryDTO clinicalHistory, Font titleFont, Font subtitleFont, Font labelFont, Font textFont, BaseColor lightGray)
        {
            // SECCIÓN: GENERALES
            AddMainSectionTitle(document, "GENERALES", titleFont);

            // Antecedentes Personales
            if (clinicalHistory.PersonalHistories?.Any() == true)
            {
                AddSubSectionTitle(document, "ANTECEDENTES PERSONALES", subtitleFont);
                foreach (var item in clinicalHistory.PersonalHistories)
                {
                    AddDetailCard(document, new List<string>
            {
                $"Enfermedad: {item.DiseaseName}",
                $"Tipo: {item.DiseaseTypeName}",
                $"Descripción: {item.Description}",
                $"Fecha: {item.RegistrationDate}"
            }, labelFont, textFont, lightGray);
                }
            }

            // Antecedentes Quirúrgicos
            if (clinicalHistory.SurgeryHistories?.Any() == true)
            {
                AddSubSectionTitle(document, "ANTECEDENTES QUIRÚRGICOS", subtitleFont);
                foreach (var surgery in clinicalHistory.SurgeryHistories)
                {
                    AddDetailCard(document, new List<string>
            {
                $"Nombre de Cirugía: {surgery.SurgeryName}",
                $"Descripción: {surgery.Description}",
                $"Fecha de Registro: {surgery.RegistrationDate?.ToShortDateString()}",
                $"Fecha de Cirugía: {surgery.SurgeryDate?.ToShortDateString()}"
            }, labelFont, textFont, lightGray);
                }
            }

            // Antecedentes de Alergias
            if (clinicalHistory.AllergyHistories?.Any() == true)
            {
                AddSubSectionTitle(document, "ANTECEDENTES DE ALERGIAS", subtitleFont);
                foreach (var allergy in clinicalHistory.AllergyHistories)
                {
                    AddDetailCard(document, new List<string>
            {
                $"Nombre de Alergia: {allergy.AllergyName}",
                $"Fecha de Registro: {allergy.RegistrationDate?.ToShortDateString()}"
            }, labelFont, textFont, lightGray);
                }
            }

            // Antecedentes de Hábitos
            if (clinicalHistory.HabitHistories?.Any() == true)
            {
                AddSubSectionTitle(document, "ANTECEDENTES DE HÁBITOS", subtitleFont);
                foreach (var habit in clinicalHistory.HabitHistories)
                {
                    AddDetailCard(document, new List<string>
            {
                $"Nombre de Hábito: {habit.HabitName}",
                $"Fecha de Registro: {habit.RecordDate?.ToShortDateString()}"
            }, labelFont, textFont, lightGray);
                }
            }

            // Antecedentes Familiares
            if (clinicalHistory.FamilyHistories?.Any() == true)
            {
                AddSubSectionTitle(document, "ANTECEDENTES FAMILIARES", subtitleFont);
                foreach (var family in clinicalHistory.FamilyHistories)
                {
                    AddDetailCard(document, new List<string>
            {
                $"Parentesco: {family.RelationshipName}",
                $"Nombre de Enfermedad: {family.DiseaseName}",
                $"Tipo de Enfermedad: {family.DiseaseTypeName}",
                $"Edad de Aparición: {family.appearanceAge}",
                $"Descripción: {family.Description}",
                $"Fecha de Registro: {family.RegistrationDate?.ToShortDateString()}"
            }, labelFont, textFont, lightGray);
                }
            }

            // SECCIÓN: OBSTÉTRICOS
            AddMainSectionTitle(document, "OBSTÉTRICOS", titleFont);

            // Antecedentes Obstétricos
            if (clinicalHistory.ObstetricHistory != null)
            {
                AddSubSectionTitle(document, "ANTECEDENTES OBSTÉTRICOS", subtitleFont);
                var obstetricData = new List<string>
        {
            $"Embarazo Actual: {(clinicalHistory.ObstetricHistory.CurrentPregnancy == true ? "Sí" : "No")}",
            $"Embarazos Anteriores: {(clinicalHistory.ObstetricHistory.PreviousPregnancies == true ? "Sí" : "No")}",
            $"Partos: {(clinicalHistory.ObstetricHistory.Deliveries == true ? "Sí" : "No")}",
            $"Abortos: {(clinicalHistory.ObstetricHistory.Abortions == true ? "Sí" : "No")}",
            $"Cesáreas: {(clinicalHistory.ObstetricHistory.CSections == true ? "Sí" : "No")}",
            $"Nacidos Vivos: {(clinicalHistory.ObstetricHistory.LiveBirths > 0 ? "Sí" : "No")}",
            $"Nacidos Muertos: {(clinicalHistory.ObstetricHistory.Stillbirths > 0 ? "Sí" : "No")}",
            $"Hijos Vivos: {(clinicalHistory.ObstetricHistory.LivingChildren > 0 ? "Sí" : "No")}",
            $"Lactancia: {(clinicalHistory.ObstetricHistory.Breastfeeding == true ? "Sí" : "No")}",
            $"Edad Gestacional: {clinicalHistory.ObstetricHistory.GestionalAge} semanas",
            $"Fecha Estimada de Parto: {clinicalHistory.ObstetricHistory.ExpectedDeliveryDate?.ToShortDateString()}"
        };
                AddDetailCard(document, obstetricData, labelFont, textFont, lightGray);
            }

            // SECCIÓN: GINECOLÓGICOS
            AddMainSectionTitle(document, "GINECOLÓGICOS", titleFont);

            // Antecedentes Ginecológicos
            if (clinicalHistory.GynecologicalHistory != null)
            {
                AddSubSectionTitle(document, "ANTECEDENTES GINECOLÓGICOS", subtitleFont);
                var gynecologicalData = new List<string>
        {
            $"Desarrollo Ginecológico: {clinicalHistory.GynecologicalHistory.GynecologicalDevelopment}",
            $"Menarquia: {(clinicalHistory.GynecologicalHistory.Menarche.HasValue ? clinicalHistory.GynecologicalHistory.Menarche.Value.ToShortDateString() : "No registrado")}",
            $"Pubarquia: {(clinicalHistory.GynecologicalHistory.Pubarche.HasValue ? clinicalHistory.GynecologicalHistory.Pubarche.Value.ToShortDateString() : "No registrado")}",
            $"Ciclos Menstruales: {clinicalHistory.GynecologicalHistory.MenstrualCycles}",
            $"Última Menstruación: {(clinicalHistory.GynecologicalHistory.LastMenstruation.HasValue ? clinicalHistory.GynecologicalHistory.LastMenstruation.Value.ToShortDateString() : "No registrado")}",
            $"Métodos Anticonceptivos: {clinicalHistory.GynecologicalHistory.ContraceptiveMethods}"
        };
                AddDetailCard(document, gynecologicalData, labelFont, textFont, lightGray);
            }

            // SECCIÓN: NUTRICIONISTAS
            AddMainSectionTitle(document, "NUTRICIONISTAS", titleFont);

            // Actividades Deportivas
            if (clinicalHistory.SportsActivitiesHistory != null)
            {
                AddSubSectionTitle(document, "ACTIVIDADES DEPORTIVAS", subtitleFont);
                var sportsData = new List<string>
        {
            $"Descripción: {clinicalHistory.SportsActivitiesHistory.Description}",
            $"Minutos por Día: {clinicalHistory.SportsActivitiesHistory.MinutesPerDay} minutos",
            $"Número de Días por Semana: {clinicalHistory.SportsActivitiesHistory.NumberOfDays} días",
            $"Fecha de Registro: {clinicalHistory.SportsActivitiesHistory.RegistrationDate?.ToShortDateString()}",
            $"Actividad Física: {clinicalHistory.SportsActivitiesHistory.SportActivityName}"
        };
                AddDetailCard(document, sportsData, labelFont, textFont, lightGray);
            }

            // Estilo de Vida
            if (clinicalHistory.LifeStyleHistory != null)
            {
                AddSubSectionTitle(document, "ESTILO DE VIDA", subtitleFont);
                var lifestyleData = new List<string>
        {
            $"Estilo de Vida: {clinicalHistory.LifeStyleHistory.LifeStyleName}",
            $"Descripción: {clinicalHistory.LifeStyleHistory.Description}",
            $"Fecha de Registro: {clinicalHistory.LifeStyleHistory.RegistrationDate?.ToShortDateString()}"
        };
                AddDetailCard(document, lifestyleData, labelFont, textFont, lightGray);
            }

            // Hábitos Dietéticos
            if (clinicalHistory.DietaryHabitsHistory != null)
            {
                AddSubSectionTitle(document, "HÁBITOS DIETÉTICOS", subtitleFont);
                var dietaryData = new List<string>
        {
            $"Descripción: {clinicalHistory.DietaryHabitsHistory.Description}",
            $"Fecha de Registro: {clinicalHistory.DietaryHabitsHistory.RegistrationDate?.ToShortDateString()}"
        };
                AddDetailCard(document, dietaryData, labelFont, textFont, lightGray);
            }

            // Hábitos de Sueño
            if (clinicalHistory.SleepHabitHistory != null)
            {
                AddSubSectionTitle(document, "HÁBITOS DE SUEÑO", subtitleFont);
                var sleepData = new List<string>
        {
            $"Hábito de Sueño: {clinicalHistory.SleepHabitHistory.SleepHabitName}",
            $"Descripción: {clinicalHistory.SleepHabitHistory.Description}",
            $"Fecha de Registro: {clinicalHistory.SleepHabitHistory.RecordDate?.ToShortDateString()}"
        };
                AddDetailCard(document, sleepData, labelFont, textFont, lightGray);
            }

            // Consumo de Alimentos
            if (clinicalHistory.FoodConsumptionHistory != null)
            {
                AddSubSectionTitle(document, "CONSUMO DE ALIMENTOS", subtitleFont);
                var foodData = new List<string>
        {
            $"Nombre del Alimento: {clinicalHistory.FoodConsumptionHistory.FoodName}",
            $"Hora: {clinicalHistory.FoodConsumptionHistory.Hour}",
            $"Lugar: {clinicalHistory.FoodConsumptionHistory.Place}",
            $"Cantidad: {clinicalHistory.FoodConsumptionHistory.Amount}",
            $"Descripción: {clinicalHistory.FoodConsumptionHistory.Description}",
            $"Fecha de Registro: {clinicalHistory.FoodConsumptionHistory.RegistrationDate?.ToShortDateString()}"
        };
                AddDetailCard(document, foodData, labelFont, textFont, lightGray);
            }

            // Consumo de Agua
            if (clinicalHistory.WaterConsumptionHistory != null)
            {
                AddSubSectionTitle(document, "CONSUMO DE AGUA", subtitleFont);
                var waterData = new List<string>
        {
            $"Cantidad: {clinicalHistory.WaterConsumptionHistory.Amount}",
            $"Frecuencia: {clinicalHistory.WaterConsumptionHistory.Frequency}",
            $"Descripción: {clinicalHistory.WaterConsumptionHistory.Description}",
            $"Fecha de Registro: {clinicalHistory.WaterConsumptionHistory.RegistrationDate?.ToShortDateString()}"
        };
                AddDetailCard(document, waterData, labelFont, textFont, lightGray);
            }

            // SECCIÓN: PSICOLOGÍA
            AddMainSectionTitle(document, "PSICOLOGÍA", titleFont);

            // Antecedente de Medicamentos
            if (clinicalHistory.MedicationHistories?.Any() == true)
            {
                AddSubSectionTitle(document, "ANTECEDENTE DE MEDICAMENTOS", subtitleFont);
                foreach (var medication in clinicalHistory.MedicationHistories)
                {
                    AddDetailCard(document, new List<string>
            {
                $"Medicamento: {medication.MedicineName}",
                $"Fecha de Registro: {medication.ConsumptionDate?.ToString("dd/MM/yyyy")}"
            }, labelFont, textFont, lightGray);
                }
            }

            // Antecedente Psicopsiquiátrico
            if (clinicalHistory.PsychopsychiatricHistories?.Any() == true)
            {
                AddSubSectionTitle(document, "ANTECEDENTE PSICOPSIQUIÁTRICO", subtitleFont);
                foreach (var psych in clinicalHistory.PsychopsychiatricHistories)
                {
                    AddDetailCard(document, new List<string>
            {
                $"Tipo: {psych.Type}",
                $"Actor: {psych.Actor}",
                $"Fecha: {psych.HistoryDate?.ToString("dd/MM/yyyy")}",
                $"Estado: {psych.HistoryState}"
            }, labelFont, textFont, lightGray);
                }
            }

            // Historial de Problema Actual
            if (clinicalHistory.CurrentProblemHistories?.Any() == true)
            {
                AddSubSectionTitle(document, "HISTORIAL DE PROBLEMA ACTUAL", subtitleFont);
                var currentProblem = clinicalHistory.CurrentProblemHistories.FirstOrDefault();
                var problemData = new List<string>();

                if (!string.IsNullOrWhiteSpace(currentProblem?.AppearanceEvolution))
                    problemData.Add($"Aparición y Evolución: {currentProblem.AppearanceEvolution}");

                if (!string.IsNullOrWhiteSpace(currentProblem?.TriggeringFactors))
                    problemData.Add($"Factores Desencadenantes: {currentProblem.TriggeringFactors}");

                if (!string.IsNullOrWhiteSpace(currentProblem?.FrequencyIntensitySymptoms))
                    problemData.Add($"Frecuencia e Intensidad de Síntomas: {currentProblem.FrequencyIntensitySymptoms}");

                if (!string.IsNullOrWhiteSpace(currentProblem?.Impact))
                    problemData.Add($"Impacto: {currentProblem.Impact}");

                if (problemData.Any())
                    AddDetailCard(document, problemData, labelFont, textFont, lightGray);
            }

            // Hábitos Tóxicos
            if (clinicalHistory.ToxicHabitHistories?.Any() == true)
            {
                AddSubSectionTitle(document, "HÁBITOS TÓXICOS", subtitleFont);
                foreach (var habit in clinicalHistory.ToxicHabitHistories)
                {
                    AddDetailCard(document, new List<string>
            {
                $"Hábito Tóxico: {habit.Description}",
                $"Fecha de Registro: {habit.RecordDate?.ToString("dd/MM/yyyy")}"
            }, labelFont, textFont, lightGray);
                }
            }

            // Antecedente Psicosexual
            if (clinicalHistory.PsychosexualHistories?.Any() == true)
            {
                AddSubSectionTitle(document, "ANTECEDENTE PSICOSEXUAL", subtitleFont);
                var psychosexual = clinicalHistory.PsychosexualHistories.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(psychosexual?.Description))
                {
                    AddDetailCard(document, new List<string> { $"Descripción: {psychosexual.Description}" }, labelFont, textFont, lightGray);
                }
            }

            // Historial Laboral
            if (clinicalHistory.WorkHistories?.Any() == true)
            {
                AddSubSectionTitle(document, "HISTORIAL LABORAL", subtitleFont);
                var workHistory = clinicalHistory.WorkHistories.FirstOrDefault();
                var workData = new List<string>();

                if (!string.IsNullOrWhiteSpace(workHistory?.Experience))
                    workData.Add($"Experiencia: {workHistory.Experience}");

                if (!string.IsNullOrWhiteSpace(workHistory?.Stability))
                    workData.Add($"Estabilidad: {workHistory.Stability}");

                if (!string.IsNullOrWhiteSpace(workHistory?.SatisfactionLevel))
                    workData.Add($"Nivel de Satisfacción: {workHistory.SatisfactionLevel}");

                if (workData.Any())
                    AddDetailCard(document, workData, labelFont, textFont, lightGray);
            }

            // SECCIÓN: FISIOTERAPIA
            AddMainSectionTitle(document, "FISIOTERAPIA", titleFont);

            // Antecedentes Traumatológicos
            if (clinicalHistory.TraumaticHistories?.Any() == true)
            {
                AddSubSectionTitle(document, "ANTECEDENTES TRAUMATOLÓGICOS", subtitleFont);
                foreach (var traumatic in clinicalHistory.TraumaticHistories)
                {
                    AddDetailCard(document, new List<string>
            {
                $"Descripción: {traumatic.Description}"
            }, labelFont, textFont, lightGray);
                }
            }

            // SECCIÓN: ESTIMULACIÓN TEMPRANA
            AddMainSectionTitle(document, "ESTIMULACIÓN TEMPRANA", titleFont);

            // Antecedentes Prenatales
            if (clinicalHistory.PrenatalHistories?.Any() == true)
            {
                AddSubSectionTitle(document, "ANTECEDENTES PRENATALES", subtitleFont);
                var prenatal = clinicalHistory.PrenatalHistories.FirstOrDefault();
                var prenatalData = new List<string>
        {
            $"Producto de Número de Gestas: {prenatal?.NumberOfDeeds ?? 0}",
            $"Embarazo Planificado: {(prenatal?.PlannedPregnancy == true ? "Sí" : "No")}",
            $"Exposición a Radiación: {(prenatal?.RadiationExposure == true ? "Sí" : "No")}",
            $"Sufrimiento Fetal: {(prenatal?.FetalSuffering == true ? "Sí" : "No")}",
            $"Número de Controles: {prenatal?.NumberOfControls ?? 0}",
            $"Número de Ecografías: {prenatal?.NumberOfUltrasounds ?? 0}"
        };

                if (!string.IsNullOrWhiteSpace(prenatal?.MedicationsOrVitamins))
                    prenatalData.Add($"Medicamentos o Vitaminas: {prenatal.MedicationsOrVitamins}");

                if (!string.IsNullOrWhiteSpace(prenatal?.ComplicationsDuringPregnancy))
                    prenatalData.Add($"Complicaciones durante el Embarazo: {prenatal.ComplicationsDuringPregnancy}");

                AddDetailCard(document, prenatalData, labelFont, textFont, lightGray);
            }

            // Antecedentes Perinatales
            if (clinicalHistory.PerinatalHistories?.Any() == true)
            {
                AddSubSectionTitle(document, "ANTECEDENTES PERINATALES", subtitleFont);
                var perinatal = clinicalHistory.PerinatalHistories.FirstOrDefault();
                var perinatalData = new List<string>
        {
            $"Tipo de Parto: {perinatal?.TypeOfBirth}",
            $"Número de Semanas: {perinatal?.NumberOfWeeks ?? 0}",
            $"Lloró al nacer: {(perinatal?.BirthCry == true ? "Sí" : "No")}"
        };

                if (!string.IsNullOrWhiteSpace(perinatal?.Apgar))
                    perinatalData.Add($"Apgar: {perinatal.Apgar}");

                if (!string.IsNullOrWhiteSpace(perinatal?.AuditoryScreen))
                    perinatalData.Add($"Tamiz Auditivo: {perinatal.AuditoryScreen}");

                if (!string.IsNullOrWhiteSpace(perinatal?.ResuscitationManeuvers))
                    perinatalData.Add($"Maniobras de Reanimación: {perinatal.ResuscitationManeuvers}");

                if (!string.IsNullOrWhiteSpace(perinatal?.PlaceOfCare))
                    perinatalData.Add($"Lugar de Atención: {perinatal.PlaceOfCare}");

                if (!string.IsNullOrWhiteSpace(perinatal?.MetabolicScreen))
                    perinatalData.Add($"Tamiz Metabólico: {perinatal.MetabolicScreen}");

                if (!string.IsNullOrWhiteSpace(perinatal?.ComplicationsDuringChildbirth))
                    perinatalData.Add($"Complicaciones durante el Parto/Cesárea: {perinatal.ComplicationsDuringChildbirth}");

                AddDetailCard(document, perinatalData, labelFont, textFont, lightGray);
            }

            // Antecedentes Postnatales
            if (clinicalHistory.PostnatalHistories?.Any() == true)
            {
                AddSubSectionTitle(document, "ANTECEDENTES POSTNATALES", subtitleFont);
                var postnatal = clinicalHistory.PostnatalHistories.FirstOrDefault();
                var postnatalData = new List<string>();

                if (!string.IsNullOrWhiteSpace(postnatal?.Description))
                    postnatalData.Add($"Descripción: {postnatal.Description}");

                // Inmunizaciones
                postnatalData.Add("Inmunizaciones:");
                postnatalData.Add($"• BCG: {(postnatal?.Bcg == true ? "Sí" : "No")}");
                postnatalData.Add($"• Rotavirus: {(postnatal?.Rotavirus == true ? "Sí" : "No")}");
                postnatalData.Add($"• Pentavalente: {(postnatal?.Pentavalente == true ? "Sí" : "No")}");
                postnatalData.Add($"• Influenza: {(postnatal?.Influenza == true ? "Sí" : "No")}");
                postnatalData.Add($"• Varicela: {(postnatal?.Varicela == true ? "Sí" : "No")}");
                postnatalData.Add($"• Hepatitis B: {(postnatal?.HepatitisB == true ? "Sí" : "No")}");
                postnatalData.Add($"• Triple Viral: {(postnatal?.TripleViral == true ? "Sí" : "No")}");
                postnatalData.Add($"• Polio Virus: {(postnatal?.PolioVirus == true ? "Sí" : "No")}");
                postnatalData.Add($"• Neumococo: {(postnatal?.Neumococo == true ? "Sí" : "No")}");

                if (!string.IsNullOrWhiteSpace(postnatal?.Observations))
                    postnatalData.Add($"Observaciones: {postnatal.Observations}");

                AddDetailCard(document, postnatalData, labelFont, textFont, lightGray);
            }

            // Antecedentes Neuropsicológicos
            if (clinicalHistory.NeuropsychologicalHistories?.Any() == true)
            {
                AddSubSectionTitle(document, "ANTECEDENTES NEUROPSICOLÓGICOS", subtitleFont);
                var neuropsychological = clinicalHistory.NeuropsychologicalHistories.FirstOrDefault();
                var neuroData = new List<string>();

                if (!string.IsNullOrWhiteSpace(neuropsychological?.HomeConduct))
                    neuroData.Add($"Conducta en Casa: {neuropsychological.HomeConduct}");

                if (!string.IsNullOrWhiteSpace(neuropsychological?.SchoolConduct))
                    neuroData.Add($"Conducta en la Escuela: {neuropsychological.SchoolConduct}");

                if (!string.IsNullOrWhiteSpace(neuropsychological?.Leverage))
                    neuroData.Add($"Aprovechamiento: {neuropsychological.Leverage}");

                if (!string.IsNullOrWhiteSpace(neuropsychological?.DreamObservation))
                    neuroData.Add($"Observación Sueño: {neuropsychological.DreamObservation}");

                if (!string.IsNullOrWhiteSpace(neuropsychological?.SightObservation))
                    neuroData.Add($"Observación Vista: {neuropsychological.SightObservation}");

                if (!string.IsNullOrWhiteSpace(neuropsychological?.SpeechObservation))
                    neuroData.Add($"Observación Habla: {neuropsychological.SpeechObservation}");

                if (!string.IsNullOrWhiteSpace(neuropsychological?.HearingObservation))
                    neuroData.Add($"Observación Escucha: {neuropsychological.HearingObservation}");

                neuroData.Add($"Desmayos: {(neuropsychological?.Faintings == true ? "Sí" : "No")}");

                if (!string.IsNullOrWhiteSpace(neuropsychological?.ObservationDifferentAbility))
                    neuroData.Add($"Observaciones de Capacidades Diferentes: {neuropsychological.ObservationDifferentAbility}");

                if (!string.IsNullOrWhiteSpace(neuropsychological?.Observation))
                    neuroData.Add($"Observaciones Generales: {neuropsychological.Observation}");

                AddDetailCard(document, neuroData, labelFont, textFont, lightGray);
            }

            // Hitos de Desarrollo
            if (clinicalHistory.DevelopmentRecords?.Any() == true)
            {
                AddSubSectionTitle(document, "HITOS DE DESARROLLO", subtitleFont);
                foreach (var record in clinicalHistory.DevelopmentRecords)
                {
                    var developmentData = new List<string>
            {
                $"Hito: {record.DevelopmentMilestone}",
                $"Rango de Edad: {record.AgeRange}"
            };

                    if (!string.IsNullOrWhiteSpace(record.Observations))
                        developmentData.Add($"Observaciones: {record.Observations}");

                    AddDetailCard(document, developmentData, labelFont, textFont, lightGray);
                }
            }

            // Exámenes Neurológicos
            if (clinicalHistory.NeurologicalExams?.Any() == true)
            {
                AddSubSectionTitle(document, "EXÁMENES NEUROLÓGICOS", subtitleFont);
                foreach (var exam in clinicalHistory.NeurologicalExams)
                {
                    var examData = new List<string>
            {
                $"Tipo de Examen: {exam.NeurologicalExamTypeName}",
                $"Examen: {exam.Name}",
                $"Fecha: {exam.ExamDate?.ToString("dd/MM/yyyy")}"
            };

                    if (!string.IsNullOrWhiteSpace(exam.Description))
                        examData.Add($"Descripción: {exam.Description}");

                    if (!string.IsNullOrWhiteSpace(exam.LinkPdf))
                        examData.Add($"Enlace PDF: {exam.LinkPdf}");

                    AddDetailCard(document, examData, labelFont, textFont, lightGray);
                }
            }
        }

        // Métodos auxiliares
        private void AddMainSectionTitle(Document document, string title, Font titleFont)
        {
            var section = new Paragraph(title, titleFont)
            {
                SpacingBefore = 20f,
                SpacingAfter = 10f,
                Alignment = Element.ALIGN_CENTER
            };
            document.Add(section);
        }
        private void AddSubSectionTitle(Document document, string title, Font subtitleFont)
        {
            var section = new Paragraph(title, subtitleFont)
            {
                SpacingBefore = 15f,
                SpacingAfter = 8f
            };
            document.Add(section);
        }

        private void AddDetailCard(Document document, List<string> items, Font labelFont, Font textFont, BaseColor lightGray)
        {
            var table = new PdfPTable(1)
            {
                WidthPercentage = 100,
                SpacingAfter = 10f
            };

            var cell = new PdfPCell()
            {
                Border = Rectangle.BOX,
                BorderColor = BaseColor.LightGray,
                BorderWidth = 1f,
                BackgroundColor = lightGray,
                Padding = 8f
            };

            foreach (var item in items)
            {
                var phrase = new Phrase(item, textFont);
                cell.AddElement(phrase);
            }

            table.AddCell(cell);
            document.Add(table);
        }


        private void AddSectionTitle(Document document, string title, Font titleFont)
        {
            var section = new Paragraph(title, titleFont)
            {
                SpacingBefore = 15f,
                SpacingAfter = 8f
            };
            document.Add(section);
        }

        private void AddSimpleTableRow(Document document, string label, string value, Font labelFont, Font textFont)
        {
            var table = new PdfPTable(2)
            {
                WidthPercentage = 100,
                SpacingAfter = 5f
            };
            table.SetWidths(new float[] { 40, 60 });

            var labelCell = new PdfPCell(new Phrase(label, labelFont))
            {
                Border = Rectangle.NO_BORDER,
                Padding = 4,
                VerticalAlignment = Element.ALIGN_TOP
            };

            var valueCell = new PdfPCell(new Phrase(value ?? "No disponible", textFont))
            {
                Border = Rectangle.NO_BORDER,
                Padding = 4,
                VerticalAlignment = Element.ALIGN_TOP
            };

            table.AddCell(labelCell);
            table.AddCell(valueCell);
            document.Add(table);
        }



        //---------------------------------------------------------------------------------------------------------------------------------------------
        //PERSON
        //---------------------------------------------------------------------------------------------------------------------------------------------

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