using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
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

        //---------------------------------------------------------------------------------------------------------------------------------------------
        //History Clinical PDF Generation
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<byte[]> GenerateClinicalHistoryPdfAsync(ClinicalHistoryDTO clinicalHistory)
        {
            try
            {
                using var memoryStream = new MemoryStream();

                // Configurar documento A4 con márgenes
                var document = new Document(PageSize.A4, 40, 40, 50, 40);
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

        //---------------------------------------------------------------------------------------------------------------------------------------------
        //Person PDF Generation
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<byte[]> GeneratePersonPdfAsync(PersonDTO persona)
        {
            try
            {
                using var memoryStream = new MemoryStream();

                // Configurar documento A4 con márgenes
                var document = new Document(PageSize.A4, 40, 40, 50, 40);
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

        //---------------------------------------------------------------------------------------------------------------------------------------------
        //NURSING PDF Generation
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<byte[]> GenerateNursingPdfAsync(MedicalCareDTO nursingCare)
        {
            try
            {
                using var memoryStream = new MemoryStream();

                // Configurar documento A4 con márgenes
                var document = new Document(PageSize.A4, 40, 40, 50, 40);
                PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                // Colores corporativos de la universidad
                var universityRed = new BaseColor(109, 19, 18);
                var universityWhite = new BaseColor(255, 255, 254);
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
                var tableHeaderFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, universityWhite);

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

                // Cargar logo
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
                catch (Exception ex)
                {
                    Console.WriteLine($"Error cargando logo: {ex.Message}");
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
                var systemName = new Paragraph("SISTEMA MÉDICO - ENFERMERÍA",
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
                var subtitle = new Paragraph("REPORTE DE ATENCIÓN DE ENFERMERÍA", subtitleFont)
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

                // ===== INFORMACIÓN DE LA ATENCIÓN =====
                AddNursingInfoSection(document, nursingCare, titleFont, labelFont, textFont, lightGray, universityRed);

                // ===== MOTIVO DE CONSULTA =====
                AddReasonForConsultationSection(document, nursingCare, titleFont, textFont, lightGray);

                // ===== SIGNOS VITALES =====
                AddVitalSignsSection(document, nursingCare, titleFont, textFont, lightGray, universityRed, tableHeaderFont);

                // ===== SERVICIOS MÉDICOS =====
                AddMedicalServicesSection(document, nursingCare, titleFont, textFont, lightGray, universityRed, tableHeaderFont);

                // ===== PROCEDIMIENTOS MÉDICOS =====
                AddMedicalProceduresSection(document, nursingCare, titleFont, textFont, lightGray, universityRed, tableHeaderFont);

                // ===== RESUMEN DE ATENCIÓN =====
                AddSummarySection(document, nursingCare, titleFont, textFont, lightGray, universityRed);

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
                Console.WriteLine($"Error generando PDF de enfermería: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }
        }


        private void AddNursingInfoSection(Document document, MedicalCareDTO nursingCare, Font titleFont, Font labelFont, Font textFont, BaseColor lightGray, BaseColor universityRed)
        {
            var sectionTitle = new Paragraph("INFORMACIÓN DE LA ATENCIÓN", titleFont)
            {
                SpacingAfter = 10f
            };
            document.Add(sectionTitle);

            var infoTable = new PdfPTable(2)
            {
                WidthPercentage = 100,
                SpacingAfter = 20f
            };
            infoTable.SetWidths(new float[] { 35, 65 });

            AddTableRow(infoTable, "ID Atención:", nursingCare.CareId.ToString(), labelFont, textFont, lightGray);
            AddTableRow(infoTable, "Paciente:", nursingCare.NamePatient ?? "No disponible", labelFont, textFont);
            AddTableRow(infoTable, "ID Paciente:", nursingCare.PatientId.ToString(), labelFont, textFont, lightGray);
            AddTableRow(infoTable, "Área:", nursingCare.Area ?? "Enfermería", labelFont, textFont);
            AddTableRow(infoTable, "Ubicación:", nursingCare.NamePlace ?? "No disponible", labelFont, textFont, lightGray);
            AddTableRow(infoTable, "Profesional:", nursingCare.NameHealthProfessional ?? "No disponible", labelFont, textFont);
            AddTableRow(infoTable, "Fecha de Atención:", nursingCare.CareDate.ToString("dd/MM/yyyy HH:mm"), labelFont, textFont, lightGray);

            document.Add(infoTable);
        }

        private void AddReasonForConsultationSection(Document document, MedicalCareDTO nursingCare, Font titleFont, Font textFont, BaseColor lightGray)
        {
            if (nursingCare.ReasonForConsultation != null && !string.IsNullOrEmpty(nursingCare.ReasonForConsultation.Description))
            {
                var sectionTitle = new Paragraph("MOTIVO DE CONSULTA", titleFont)
                {
                    SpacingBefore = 15f,
                    SpacingAfter = 10f
                };
                document.Add(sectionTitle);

                var reasonTable = new PdfPTable(1)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 20f
                };

                var reasonCell = new PdfPCell(new Phrase(nursingCare.ReasonForConsultation.Description, textFont))
                {
                    Border = Rectangle.BOX,
                    BorderColor = lightGray,
                    Padding = 10,
                    BackgroundColor = BaseColor.White
                };
                reasonTable.AddCell(reasonCell);

                document.Add(reasonTable);
            }
        }

        private void AddVitalSignsSection(Document document, MedicalCareDTO nursingCare, Font titleFont, Font textFont, BaseColor lightGray, BaseColor universityRed, Font tableHeaderFont)
        {
            if (nursingCare.VitalSigns != null)
            {
                var sectionTitle = new Paragraph("SIGNOS VITALES", titleFont)
                {
                    SpacingBefore = 15f,
                    SpacingAfter = 10f
                };
                document.Add(sectionTitle);

                var vitalSignsTable = new PdfPTable(4)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 20f
                };
                vitalSignsTable.SetWidths(new float[] { 25, 25, 25, 25 });

                var vitalSigns = nursingCare.VitalSigns;

                // Primera fila
                AddVitalSignCell(vitalSignsTable, "PESO", $"{vitalSigns.Weight?.ToString("F1") ?? "N/A"} kg", tableHeaderFont, textFont, universityRed);
                AddVitalSignCell(vitalSignsTable, "TALLA", $"{vitalSigns.Height?.ToString("F1") ?? "N/A"} cm", tableHeaderFont, textFont, universityRed);
                AddVitalSignCell(vitalSignsTable, "IMC", $"{vitalSigns.Icm?.ToString("F1") ?? "N/A"}", tableHeaderFont, textFont, universityRed);
                AddVitalSignCell(vitalSignsTable, "PRESIÓN ARTERIAL", vitalSigns.BloodPressure ?? "N/A", tableHeaderFont, textFont, universityRed);

                // Segunda fila
                AddVitalSignCell(vitalSignsTable, "TEMPERATURA", $"{vitalSigns.Temperature?.ToString("F1") ?? "N/A"} °C", tableHeaderFont, textFont, universityRed);
                AddVitalSignCell(vitalSignsTable, "PAM", $"{vitalSigns.MeanArterialPressure?.ToString("F1") ?? "N/A"}", tableHeaderFont, textFont, universityRed);
                AddVitalSignCell(vitalSignsTable, "FC", $"{vitalSigns.HeartRate?.ToString() ?? "N/A"} lpm", tableHeaderFont, textFont, universityRed);
                AddVitalSignCell(vitalSignsTable, "FR", $"{vitalSigns.RespiratoryRate?.ToString() ?? "N/A"} rpm", tableHeaderFont, textFont, universityRed);

                // Tercera fila
                AddVitalSignCell(vitalSignsTable, "SAT O2", $"{vitalSigns.OxygenSaturation?.ToString("F1") ?? "N/A"} %", tableHeaderFont, textFont, universityRed);
                AddVitalSignCell(vitalSignsTable, "GLUCOSA", $"{vitalSigns.BloodGlucose?.ToString("F1") ?? "N/A"} mg/dl", tableHeaderFont, textFont, universityRed);
                AddVitalSignCell(vitalSignsTable, "HEMOGLOBINA", $"{vitalSigns.Hemoglobin?.ToString("F1") ?? "N/A"} g/dl", tableHeaderFont, textFont, universityRed);
                AddVitalSignCell(vitalSignsTable, "P. ABDOMINAL", $"{vitalSigns.AbdominalCircumference?.ToString("F1") ?? "N/A"} cm", tableHeaderFont, textFont, universityRed);

                document.Add(vitalSignsTable);
            }
        }

        private void AddMedicalServicesSection(Document document, MedicalCareDTO nursingCare, Font titleFont, Font textFont, BaseColor lightGray, BaseColor universityRed, Font tableHeaderFont)
        {
            if (nursingCare.MedicalServices != null && nursingCare.MedicalServices.Any())
            {
                var sectionTitle = new Paragraph($"SERVICIOS MÉDICOS ({nursingCare.MedicalServices.Count})", titleFont)
                {
                    SpacingBefore = 15f,
                    SpacingAfter = 10f
                };
                document.Add(sectionTitle);

                var servicesTable = new PdfPTable(6)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 20f
                };
                servicesTable.SetWidths(new float[] { 15, 20, 15, 15, 15, 20 });

                // Encabezados de la tabla
                AddTableHeaderCell(servicesTable, "FECHA", tableHeaderFont, universityRed);
                AddTableHeaderCell(servicesTable, "TIPO SERVICIO", tableHeaderFont, universityRed);
                AddTableHeaderCell(servicesTable, "DIAGNÓSTICO", tableHeaderFont, universityRed);
                AddTableHeaderCell(servicesTable, "OBSERVACIONES", tableHeaderFont, universityRed);
                AddTableHeaderCell(servicesTable, "RECOMENDACIONES", tableHeaderFont, universityRed);
                AddTableHeaderCell(servicesTable, "PROFESIONAL", tableHeaderFont, universityRed);

                foreach (var service in nursingCare.MedicalServices)
                {
                    AddTableCell(servicesTable, service.ServiceDate?.ToString("dd/MM/yyyy HH:mm") ?? "N/A", textFont);
                    AddTableCell(servicesTable, service.ServiceType ?? "N/A", textFont);
                    AddTableCell(servicesTable, service.Diagnosis ?? "N/A", textFont);
                    AddTableCell(servicesTable, service.Observations ?? "N/A", textFont);
                    AddTableCell(servicesTable, service.Recommendations ?? "N/A", textFont);
                    AddTableCell(servicesTable, service.HealthProfessionalName ?? "N/A", textFont);
                }

                document.Add(servicesTable);
            }
        }

        private void AddMedicalProceduresSection(Document document, MedicalCareDTO nursingCare, Font titleFont, Font textFont, BaseColor lightGray, BaseColor universityRed, Font tableHeaderFont)
        {
            if (nursingCare.MedicalProcedures != null && nursingCare.MedicalProcedures.Any())
            {
                var sectionTitle = new Paragraph($"PROCEDIMIENTOS MÉDICOS ({nursingCare.MedicalProcedures.Count})", titleFont)
                {
                    SpacingBefore = 15f,
                    SpacingAfter = 10f
                };
                document.Add(sectionTitle);

                var proceduresTable = new PdfPTable(8)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 20f
                };
                proceduresTable.SetWidths(new float[] { 12, 12, 15, 12, 12, 12, 10, 15 });

                // Encabezados de la tabla
                AddTableHeaderCell(proceduresTable, "FECHA", tableHeaderFont, universityRed);
                AddTableHeaderCell(proceduresTable, "TIPO", tableHeaderFont, universityRed);
                AddTableHeaderCell(proceduresTable, "PROCEDIMIENTO", tableHeaderFont, universityRed);
                AddTableHeaderCell(proceduresTable, "PERSONAL SALUD", tableHeaderFont, universityRed);
                AddTableHeaderCell(proceduresTable, "MÉDICO TRATANTE", tableHeaderFont, universityRed);
                AddTableHeaderCell(proceduresTable, "UBICACIÓN", tableHeaderFont, universityRed);
                AddTableHeaderCell(proceduresTable, "ESTADO", tableHeaderFont, universityRed);
                AddTableHeaderCell(proceduresTable, "OBSERVACIONES", tableHeaderFont, universityRed);

                foreach (var procedure in nursingCare.MedicalProcedures)
                {
                    AddTableCell(proceduresTable, procedure.ProcedureDate.ToString("dd/MM/yyyy HH:mm"), textFont);
                    AddTableCell(proceduresTable, procedure.TypeOfProcedureName ?? "N/A", textFont);
                    AddTableCell(proceduresTable, procedure.SpecificProcedureName ?? "N/A", textFont);
                    AddTableCell(proceduresTable, procedure.HealthProfessionalName ?? "N/A", textFont);
                    AddTableCell(proceduresTable, procedure.TreatingPhysicianName ?? "N/A", textFont);
                    AddTableCell(proceduresTable, procedure.LocationName ?? "N/A", textFont);

                    // Celda de estado con color
                    var statusCell = new PdfPCell(new Phrase(procedure.Status ?? "N/A", textFont))
                    {
                        Border = Rectangle.BOX,
                        BorderColor = lightGray,
                        Padding = 5,
                        BackgroundColor = GetStatusColor(procedure.Status)
                    };
                    proceduresTable.AddCell(statusCell);

                    AddTableCell(proceduresTable, procedure.Observations ?? "N/A", textFont);
                }

                document.Add(proceduresTable);
            }
        }


        private void AddSummarySection(Document document, MedicalCareDTO nursingCare, Font titleFont, Font textFont, BaseColor lightGray, BaseColor universityRed)
        {
            var sectionTitle = new Paragraph("RESUMEN DE ATENCIÓN", titleFont)
            {
                SpacingBefore = 15f,
                SpacingAfter = 10f
            };
            document.Add(sectionTitle);

            var summaryTable = new PdfPTable(3)
            {
                WidthPercentage = 100,
                SpacingAfter = 20f
            };
            summaryTable.SetWidths(new float[] { 33, 34, 33 });

            var hasVitalSigns = HasValidVitalSigns(nursingCare.VitalSigns);
            var servicesCount = nursingCare.MedicalServices?.Count ?? 0;
            var proceduresCount = nursingCare.MedicalProcedures?.Count ?? 0;
            var hasServices = servicesCount > 0;
            var hasProcedures = proceduresCount > 0;

            Console.WriteLine($"DEBUG - hasVitalSigns: {hasVitalSigns}");

            // SOLUCIÓN: Usar caracteres ASCII simples que siempre funcionan
            var vitalSignsValue = hasVitalSigns ? "SI" : "NO";
            // O alternativamente: 
            // var vitalSignsValue = hasVitalSigns ? "[X]" : "[ ]";
            // var vitalSignsValue = hasVitalSigns ? "CON" : "SIN";

            Console.WriteLine($"DEBUG - vitalSignsValue: {vitalSignsValue}");

            AddSummaryCard(summaryTable, "SIGNOS VITALES", vitalSignsValue, universityRed, textFont, hasVitalSigns);
            AddSummaryCard(summaryTable, "SERVICIOS MÉDICOS", servicesCount.ToString(), universityRed, textFont, hasServices);
            AddSummaryCard(summaryTable, "PROCEDIMIENTOS", proceduresCount.ToString(), universityRed, textFont, hasProcedures);

            document.Add(summaryTable);
            Console.WriteLine("DEBUG - Tabla de resumen agregada al documento");
        }

        // Métodos auxiliares específicos para Nursing PDF
        private bool HasValidVitalSigns(VitalSignsDTO vitalSigns)
        {
            if (vitalSigns == null)
                return false;

            // Verificar si al menos un campo de signos vitales tiene valor
            return vitalSigns.Weight.HasValue ||
                   vitalSigns.Height.HasValue ||
                   vitalSigns.Icm.HasValue ||
                   !string.IsNullOrEmpty(vitalSigns.BloodPressure) ||
                   vitalSigns.Temperature.HasValue ||
                   vitalSigns.MeanArterialPressure.HasValue ||
                   vitalSigns.HeartRate.HasValue ||
                   vitalSigns.RespiratoryRate.HasValue ||
                   vitalSigns.OxygenSaturation.HasValue ||
                   vitalSigns.BloodGlucose.HasValue ||
                   vitalSigns.Hemoglobin.HasValue ||
                   vitalSigns.AbdominalCircumference.HasValue;
        }
        private void AddVitalSignCell(PdfPTable table, string title, string value, Font titleFont, Font valueFont, BaseColor headerColor)
        {
            // Celda de título
            var titleCell = new PdfPCell(new Phrase(title, titleFont))
            {
                BackgroundColor = headerColor,
                Border = Rectangle.BOX,
                BorderColor = BaseColor.White,
                Padding = 8,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            table.AddCell(titleCell);

            // Celda de valor
            var valueCell = new PdfPCell(new Phrase(value, valueFont))
            {
                BackgroundColor = BaseColor.White,
                Border = Rectangle.BOX,
                BorderColor = BaseColor.LightGray,
                Padding = 8,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            table.AddCell(valueCell);
        }

        private void AddTableHeaderCell(PdfPTable table, string text, Font font, BaseColor backgroundColor)
        {
            var cell = new PdfPCell(new Phrase(text, font))
            {
                BackgroundColor = backgroundColor,
                Border = Rectangle.BOX,
                BorderColor = BaseColor.White,
                Padding = 8,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            table.AddCell(cell);
        }

        private void AddTableCell(PdfPTable table, string text, Font font)
        {
            var cell = new PdfPCell(new Phrase(text, font))
            {
                Border = Rectangle.BOX,
                BorderColor = BaseColor.LightGray,
                Padding = 6,
                HorizontalAlignment = Element.ALIGN_LEFT
            };
            table.AddCell(cell);
        }

        private void AddSummaryCard(PdfPTable table, string title, string value, BaseColor color, Font textFont, bool hasData = true)
        {
            try
            {
                Console.WriteLine($"DEBUG - AddSummaryCard: title='{title}', value='{value}', hasData={hasData}");

                var cardCell = new PdfPCell()
                {
                    Border = Rectangle.BOX,
                    BorderColor = BaseColor.LightGray,
                    Padding = 15,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE, // Añadir esta línea
                    BackgroundColor = hasData ? new BaseColor(248, 249, 250) : new BaseColor(233, 236, 239)
                };

                // SOLUCIÓN: Usar una fuente que soporte Unicode y caracteres especiales
                var titleFontStyle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, hasData ? color : BaseColor.Gray);

                // Para el valor, usar una fuente que soporte caracteres especiales
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var valueFontStyle = new Font(baseFont, 16, Font.BOLD, hasData ? BaseColor.Black : BaseColor.Gray);

                var titleParagraph = new Paragraph(title, titleFontStyle)
                {
                    Alignment = Element.ALIGN_CENTER
                };

                var valueParagraph = new Paragraph(value, valueFontStyle)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingBefore = 8f // Aumentar espacio
                };

                cardCell.AddElement(titleParagraph);
                cardCell.AddElement(valueParagraph);

                Console.WriteLine($"DEBUG - Celda creada para: {title}");
                table.AddCell(cardCell);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR en AddSummaryCard: {ex.Message}");
                // Agregar una celda de fallback
                var fallbackCell = new PdfPCell(new Phrase($"{title}: {value}", textFont))
                {
                    Border = Rectangle.BOX,
                    Padding = 10,
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                table.AddCell(fallbackCell);
            }
        }


        private BaseColor GetStatusColor(string status)
        {
            return status?.ToLower() switch
            {
                "realizado" => new BaseColor(40, 167, 69), // Verde
                "pendiente" => new BaseColor(255, 193, 7),  // Amarillo
                "cancelado" => new BaseColor(220, 53, 69),  // Rojo
                _ => new BaseColor(108, 117, 125)           // Gris
            };
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------
        // MEDICAL CARE PDF Generation
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<byte[]> GenerateMedicalCarePdfAsync(MedicalCareDTO medicalCare)
        {
            try
            {
                using var memoryStream = new MemoryStream();

                // Configurar documento A4 con márgenes
                var document = new Document(PageSize.A4, 40, 40, 50, 40);
                PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                // Colores corporativos de la universidad
                var universityRed = new BaseColor(109, 19, 18);
                var universityWhite = new BaseColor(255, 255, 254);
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
                var tableHeaderFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, universityWhite);

                // ===== ENCABEZADO CON LOGO =====
                AddHeader(document, headerFont, universityRed, universityWhite);

                // ===== SUBTÍTULO DE LA FICHA =====
                var subtitle = new Paragraph("REPORTE DE ATENCIÓN MÉDICA", subtitleFont)
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

                // ===== INFORMACIÓN GENERAL =====
                AddMedicalCareInfoSection(document, medicalCare, titleFont, labelFont, textFont, lightGray);

                // ===== MOTIVO DE CONSULTA =====
                if (medicalCare.ReasonForConsultation != null)
                {
                    AddReasonForConsultationSection(document, medicalCare, titleFont, textFont, lightGray);
                }

                // ===== APARATOS Y SISTEMAS =====
                if (medicalCare.ReviewSystemDevices != null && medicalCare.ReviewSystemDevices.Any())
                {
                    AddSystemsDevicesSection(document, medicalCare, titleFont, textFont, lightGray, universityRed, tableHeaderFont);
                }

                // ===== EXAMEN FÍSICO =====
                if (medicalCare.PhysicalExams != null && medicalCare.PhysicalExams.Any())
                {
                    AddPhysicalExamSection(document, medicalCare, titleFont, textFont, lightGray, universityRed, tableHeaderFont);
                }

                // ===== DATOS ADICIONALES =====
                if (medicalCare.AdditionalData != null)
                {
                    AddAdditionalDataSection(document, medicalCare, titleFont, textFont, lightGray);
                }

                // ===== DIAGNÓSTICOS =====
                if (medicalCare.Diagnoses != null && medicalCare.Diagnoses.Any())
                {
                    AddDiagnosesSection(document, medicalCare, titleFont, textFont, lightGray, universityRed, tableHeaderFont);
                }

                // ===== TRATAMIENTOS =====
                AddTreatmentsSection(document, medicalCare, titleFont, textFont, lightGray, universityRed, tableHeaderFont);

                // ===== PROCEDIMIENTOS MÉDICOS =====
                if (medicalCare.MedicalProcedures != null && medicalCare.MedicalProcedures.Any())
                {
                    AddMedicalProceduresSection(document, medicalCare, titleFont, textFont, lightGray, universityRed, tableHeaderFont);
                }

                // ===== DERIVACIONES =====
                if (medicalCare.Referrals != null && medicalCare.Referrals.Any())
                {
                    AddReferralsSection(document, medicalCare, titleFont, textFont, lightGray, universityRed, tableHeaderFont);
                }

                // ===== EVOLUCIONES =====
                if (medicalCare.Evolutions != null && medicalCare.Evolutions.Any())
                {
                    AddEvolutionsSection(document, medicalCare, titleFont, textFont, lightGray, universityRed, tableHeaderFont);
                }

                // ===== SERVICIOS MÉDICOS =====
                if (medicalCare.MedicalServices != null && medicalCare.MedicalServices.Any())
                {
                    AddMedicalServicesSection(document, medicalCare, titleFont, textFont, lightGray, universityRed, tableHeaderFont);
                }

                // ===== RESUMEN DE ATENCIÓN =====
                AddMedicalCareSummarySection(document, medicalCare, titleFont, textFont, lightGray, universityRed);

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
                Console.WriteLine($"Error generando PDF de atención médica: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        // ===== MÉTODOS AUXILIARES PARA MEDICAL CARE PDF =====

        private void AddHeader(Document document, Font headerFont, BaseColor universityRed, BaseColor universityWhite)
        {
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

            // Cargar logo
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando logo: {ex.Message}");
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
            var systemName = new Paragraph("SISTEMA MÉDICO - ATENCIÓN MÉDICA",
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
        }

        private void AddMedicalCareInfoSection(Document document, MedicalCareDTO medicalCare, Font titleFont, Font labelFont, Font textFont, BaseColor lightGray)
        {
            var sectionTitle = new Paragraph("INFORMACIÓN GENERAL", titleFont)
            {
                SpacingAfter = 10f
            };
            document.Add(sectionTitle);

            var infoTable = new PdfPTable(2)
            {
                WidthPercentage = 100,
                SpacingAfter = 20f
            };
            infoTable.SetWidths(new float[] { 35, 65 });

            AddTableRow(infoTable, "ID Atención:", medicalCare.CareId.ToString(), labelFont, textFont, lightGray);
            AddTableRow(infoTable, "Paciente:", medicalCare.NamePatient ?? "No disponible", labelFont, textFont);
            AddTableRow(infoTable, "ID Paciente:", medicalCare.PatientId.ToString(), labelFont, textFont, lightGray);
            AddTableRow(infoTable, "Área:", medicalCare.Area ?? "Medicina General", labelFont, textFont);
            AddTableRow(infoTable, "Ubicación:", medicalCare.NamePlace ?? "No disponible", labelFont, textFont, lightGray);
            AddTableRow(infoTable, "Profesional:", medicalCare.NameHealthProfessional ?? "No disponible", labelFont, textFont);
            AddTableRow(infoTable, "Fecha de Atención:", medicalCare.CareDate.ToString("dd/MM/yyyy HH:mm"), labelFont, textFont, lightGray);

            document.Add(infoTable);
        }

        private void AddSystemsDevicesSection(Document document, MedicalCareDTO medicalCare, Font titleFont, Font textFont, BaseColor lightGray, BaseColor universityRed, Font tableHeaderFont)
        {
            // CORREGIR: Usar ReviewSystemDevices en lugar de SystemsDevices
            if (medicalCare.ReviewSystemDevices != null && medicalCare.ReviewSystemDevices.Any())
            {
                var sectionTitle = new Paragraph($"APARATOS Y SISTEMAS ({medicalCare.ReviewSystemDevices.Count})", titleFont)
                {
                    SpacingBefore = 15f,
                    SpacingAfter = 10f
                };
                document.Add(sectionTitle);

                var systemsTable = new PdfPTable(3)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 20f
                };
                systemsTable.SetWidths(new float[] { 35, 20, 45 });

                // Encabezados
                AddTableHeaderCell(systemsTable, "SISTEMA/APARATO", tableHeaderFont, universityRed);
                AddTableHeaderCell(systemsTable, "ESTADO", tableHeaderFont, universityRed);
                AddTableHeaderCell(systemsTable, "OBSERVACIONES", tableHeaderFont, universityRed);

                // Iteramos las revisiones - CORREGIR: usar ReviewSystemDevices
                foreach (var review in medicalCare.ReviewSystemDevices)
                {
                    AddTableCell(systemsTable, review.SystemName ?? "N/A", textFont);
                    AddTableCell(systemsTable, review.State ?? "N/A", textFont);
                    AddTableCell(systemsTable, review.Observations ?? "N/A", textFont);
                }

                document.Add(systemsTable);
            }
            else
            {
                // Opcional: Mostrar mensaje si no hay datos
                var noDataParagraph = new Paragraph("APARATOS Y SISTEMAS: No se registraron datos", textFont)
                {
                    SpacingBefore = 15f,
                    SpacingAfter = 10f
                };
                document.Add(noDataParagraph);
            }
        }


        private void AddPhysicalExamSection(Document document, MedicalCareDTO medicalCare, Font titleFont, Font textFont, BaseColor lightGray, BaseColor universityRed, Font tableHeaderFont)
        {
            var sectionTitle = new Paragraph($"EXAMEN FÍSICO ({medicalCare.PhysicalExams.Count})", titleFont)
            {
                SpacingBefore = 15f,
                SpacingAfter = 10f
            };
            document.Add(sectionTitle);

            var examTable = new PdfPTable(3)
            {
                WidthPercentage = 100,
                SpacingAfter = 20f
            };
            examTable.SetWidths(new float[] { 30, 35, 35 });

            // Encabezados
            AddTableHeaderCell(examTable, "REGIÓN", tableHeaderFont, universityRed);
            AddTableHeaderCell(examTable, "EVIDENCIA PATOLÓGICA", tableHeaderFont, universityRed);
            AddTableHeaderCell(examTable, "OBSERVACIÓN", tableHeaderFont, universityRed);

            foreach (var exam in medicalCare.PhysicalExams)
            {
                AddTableCell(examTable, exam.RegionName ?? "N/A", textFont);
                AddTableCell(examTable, exam.PathologicalEvidenceName ?? "N/A", textFont);
                AddTableCell(examTable, exam.Observation ?? "N/A", textFont);
            }

            document.Add(examTable);
        }

        private void AddAdditionalDataSection(Document document, MedicalCareDTO medicalCare, Font titleFont, Font textFont, BaseColor lightGray)
        {
            if (!string.IsNullOrWhiteSpace(medicalCare.AdditionalData?.Observacion))
            {
                var sectionTitle = new Paragraph("DATOS ADICIONALES", titleFont)
                {
                    SpacingBefore = 15f,
                    SpacingAfter = 10f
                };
                document.Add(sectionTitle);

                var dataTable = new PdfPTable(1)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 20f
                };

                var dataCell = new PdfPCell(new Phrase(medicalCare.AdditionalData.Observacion, textFont))
                {
                    Border = Rectangle.BOX,
                    BorderColor = lightGray,
                    Padding = 10,
                    BackgroundColor = BaseColor.White
                };
                dataTable.AddCell(dataCell);

                document.Add(dataTable);
            }
        }

        private void AddDiagnosesSection(Document document, MedicalCareDTO medicalCare, Font titleFont, Font textFont, BaseColor lightGray, BaseColor universityRed, Font tableHeaderFont)
        {
            var sectionTitle = new Paragraph($"DIAGNÓSTICOS ({medicalCare.Diagnoses.Count})", titleFont)
            {
                SpacingBefore = 15f,
                SpacingAfter = 10f
            };
            document.Add(sectionTitle);

            var diagnosesTable = new PdfPTable(5)
            {
                WidthPercentage = 100,
                SpacingAfter = 20f
            };
            diagnosesTable.SetWidths(new float[] { 15, 30, 20, 15, 20 });

            // Encabezados
            AddTableHeaderCell(diagnosesTable, "CIE10", tableHeaderFont, universityRed);
            AddTableHeaderCell(diagnosesTable, "DENOMINACIÓN", tableHeaderFont, universityRed);
            AddTableHeaderCell(diagnosesTable, "TIPO", tableHeaderFont, universityRed);
            AddTableHeaderCell(diagnosesTable, "RECURRENCIA", tableHeaderFont, universityRed);
            AddTableHeaderCell(diagnosesTable, "MOTIVACIÓN", tableHeaderFont, universityRed);

            foreach (var diagnosis in medicalCare.Diagnoses)
            {
                AddTableCell(diagnosesTable, diagnosis.Cie10 ?? "N/A", textFont);
                AddTableCell(diagnosesTable, diagnosis.Denomination ?? "N/A", textFont);
                AddTableCell(diagnosesTable, diagnosis.DiagnosticTypeName ?? "N/A", textFont);
                AddTableCell(diagnosesTable, diagnosis.Recurrence ?? "N/A", textFont);
                AddTableCell(diagnosesTable, diagnosis.DiagnosisMotivation ?? "N/A", textFont);
            }

            document.Add(diagnosesTable);
        }

        private void AddTreatmentsSection(Document document, MedicalCareDTO medicalCare, Font titleFont, Font textFont, BaseColor lightGray, BaseColor universityRed, Font tableHeaderFont)
        {
            BaseColor darkGray = new BaseColor(80, 80, 80);


            bool hasTreatments = (medicalCare.PharmacologicalTreatments != null && medicalCare.PharmacologicalTreatments.Any()) ||
                                 (medicalCare.NonPharmacologicalTreatments != null && medicalCare.NonPharmacologicalTreatments.Any()) ||
                                 (medicalCare.Indications != null && medicalCare.Indications.Any());

            if (!hasTreatments) return;

            var sectionTitle = new Paragraph("TRATAMIENTOS", titleFont)
            {
                SpacingBefore = 15f,
                SpacingAfter = 10f
            };
            document.Add(sectionTitle);

            // Tratamientos Farmacológicos
            if (medicalCare.PharmacologicalTreatments != null && medicalCare.PharmacologicalTreatments.Any())
            {
                var pharmaTitle = new Paragraph("Tratamiento Farmacológico", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, darkGray))
                {
                    SpacingBefore = 5f,
                    SpacingAfter = 8f
                };
                document.Add(pharmaTitle);

                var pharmaTable = new PdfPTable(5)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 15f
                };
                pharmaTable.SetWidths(new float[] { 25, 15, 20, 20, 20 });

                AddTableHeaderCell(pharmaTable, "MEDICAMENTO", tableHeaderFont, universityRed);
                AddTableHeaderCell(pharmaTable, "DOSIS", tableHeaderFont, universityRed);
                AddTableHeaderCell(pharmaTable, "FRECUENCIA", tableHeaderFont, universityRed);
                AddTableHeaderCell(pharmaTable, "DURACIÓN", tableHeaderFont, universityRed);
                AddTableHeaderCell(pharmaTable, "VÍA", tableHeaderFont, universityRed);

                foreach (var treatment in medicalCare.PharmacologicalTreatments)
                {
                    AddTableCell(pharmaTable, treatment.MedicineName ?? "N/A", textFont);
                    AddTableCell(pharmaTable, treatment.Dose.ToString(), textFont);
                    AddTableCell(pharmaTable, treatment.Frequency ?? "N/A", textFont);
                    AddTableCell(pharmaTable, treatment.Duration ?? "N/A", textFont);
                    AddTableCell(pharmaTable, treatment.ViaAdmission ?? "N/A", textFont);
                }

                document.Add(pharmaTable);
            }

            // Tratamientos No Farmacológicos
            if (medicalCare.NonPharmacologicalTreatments != null && medicalCare.NonPharmacologicalTreatments.Any())
            {
                var nonPharmaTitle = new Paragraph("Tratamiento No Farmacológico", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, darkGray))
                {
                    SpacingBefore = 5f,
                    SpacingAfter = 8f
                };
                document.Add(nonPharmaTitle);

                foreach (var treatment in medicalCare.NonPharmacologicalTreatments)
                {
                    var treatmentTable = new PdfPTable(1)
                    {
                        WidthPercentage = 100,
                        SpacingAfter = 5f
                    };

                    var treatmentCell = new PdfPCell(new Phrase(treatment.Description ?? "N/A", textFont))
                    {
                        Border = Rectangle.BOX,
                        BorderColor = lightGray,
                        Padding = 8,
                        BackgroundColor = new BaseColor(248, 249, 250)
                    };
                    treatmentTable.AddCell(treatmentCell);
                    document.Add(treatmentTable);
                }

                document.Add(new Paragraph(" ") { SpacingAfter = 10f });
            }

            // Indicaciones
            if (medicalCare.Indications != null && medicalCare.Indications.Any())
            {
                var indicationsTitle = new Paragraph("Indicaciones", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, darkGray))
                {
                    SpacingBefore = 5f,
                    SpacingAfter = 8f
                };
                document.Add(indicationsTitle);

                foreach (var indication in medicalCare.Indications)
                {
                    var indicationTable = new PdfPTable(1)
                    {
                        WidthPercentage = 100,
                        SpacingAfter = 5f
                    };

                    var indicationCell = new PdfPCell(new Phrase(indication.Description ?? "N/A", textFont))
                    {
                        Border = Rectangle.BOX,
                        BorderColor = lightGray,
                        Padding = 8,
                        BackgroundColor = new BaseColor(248, 249, 250)
                    };
                    indicationTable.AddCell(indicationCell);
                    document.Add(indicationTable);
                }
            }
        }

        private void AddReferralsSection(Document document, MedicalCareDTO medicalCare, Font titleFont, Font textFont, BaseColor lightGray, BaseColor universityRed, Font tableHeaderFont)
        {
            var sectionTitle = new Paragraph($"DERIVACIONES ({medicalCare.Referrals.Count})", titleFont)
            {
                SpacingBefore = 15f,
                SpacingAfter = 10f
            };
            document.Add(sectionTitle);

            var referralsTable = new PdfPTable(2)
            {
                WidthPercentage = 100,
                SpacingAfter = 20f
            };
            referralsTable.SetWidths(new float[] { 30, 70 });

            AddTableHeaderCell(referralsTable, "FECHA DE DERIVACIÓN", tableHeaderFont, universityRed);
            AddTableHeaderCell(referralsTable, "MOTIVO", tableHeaderFont, universityRed);

            foreach (var referral in medicalCare.Referrals)
            {
                AddTableCell(referralsTable, referral.DateOfReferral?.ToString("dd/MM/yyyy") ?? "N/A", textFont);
                AddTableCell(referralsTable, referral.Description ?? "N/A", textFont);
            }

            document.Add(referralsTable);
        }

        private void AddEvolutionsSection(Document document, MedicalCareDTO medicalCare, Font titleFont, Font textFont, BaseColor lightGray, BaseColor universityRed, Font tableHeaderFont)
        {
            var sectionTitle = new Paragraph($"EVOLUCIONES ({medicalCare.Evolutions.Count})", titleFont)
            {
                SpacingBefore = 15f,
                SpacingAfter = 10f
            };
            document.Add(sectionTitle);

            var evolutionsTable = new PdfPTable(2)
            {
                WidthPercentage = 100,
                SpacingAfter = 20f
            };
            evolutionsTable.SetWidths(new float[] { 70, 30 });

            AddTableHeaderCell(evolutionsTable, "OBSERVACIÓN", tableHeaderFont, universityRed);
            AddTableHeaderCell(evolutionsTable, "% MEJORA", tableHeaderFont, universityRed);

            foreach (var evolution in medicalCare.Evolutions)
            {
                AddTableCell(evolutionsTable, evolution.Description ?? "N/A", textFont);
                AddTableCell(evolutionsTable, $"{evolution.Percentage}%", textFont);
            }

            document.Add(evolutionsTable);
        }

        private void AddMedicalCareSummarySection(Document document, MedicalCareDTO medicalCare, Font titleFont, Font textFont, BaseColor lightGray, BaseColor universityRed)
        {
            var sectionTitle = new Paragraph("RESUMEN DE ATENCIÓN", titleFont)
            {
                SpacingBefore = 15f,
                SpacingAfter = 10f
            };
            document.Add(sectionTitle);

            var summaryTable = new PdfPTable(5)
            {
                WidthPercentage = 100,
                SpacingAfter = 20f
            };
            summaryTable.SetWidths(new float[] { 20, 20, 20, 20, 20 });

            // Contar elementos
            var systemsCount = medicalCare.ReviewSystemDevices?.Count ?? 0;
            var examsCount = medicalCare.PhysicalExams?.Count ?? 0;
            var diagnosesCount = medicalCare.Diagnoses?.Count ?? 0;
            var proceduresCount = medicalCare.MedicalProcedures?.Count ?? 0;
            var evolutionsCount = medicalCare.Evolutions?.Count ?? 0;

            AddSummaryCard(summaryTable, "SISTEMAS", systemsCount.ToString(), universityRed, textFont);
            AddSummaryCard(summaryTable, "EXÁMENES", examsCount.ToString(), universityRed, textFont);
            AddSummaryCard(summaryTable, "DIAGNÓSTICOS", diagnosesCount.ToString(), universityRed, textFont);
            AddSummaryCard(summaryTable, "PROCEDIMIENTOS", proceduresCount.ToString(), universityRed, textFont);
            AddSummaryCard(summaryTable, "EVOLUCIONES", evolutionsCount.ToString(), universityRed, textFont);

            document.Add(summaryTable);
        }



        //---------------------------------------------------------------------------------------------------------------------------------------------
        // PHYSIOTHERAPY PDF Generation
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<byte[]> GeneratePhysiotherapyPdfAsync(MedicalCareDTO physioCare)
        {
            try
            {
                using var memoryStream = new MemoryStream();

                // Configurar documento A4 con márgenes
                var document = new Document(PageSize.A4, 40, 40, 50, 40);
                PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                // Colores corporativos de la universidad
                var universityRed = new BaseColor(109, 19, 18);
                var universityWhite = new BaseColor(255, 255, 254);
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
                var tableHeaderFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, universityWhite);

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

                // Cargar logo
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
                catch (Exception ex)
                {
                    Console.WriteLine($"Error cargando logo: {ex.Message}");
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
                var systemName = new Paragraph("SISTEMA MÉDICO - FISIOTERAPIA",
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
                var subtitle = new Paragraph("REPORTE DE ATENCIÓN DE FISIOTERAPIA", subtitleFont)
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

                // ===== INFORMACIÓN GENERAL =====
                AddPhysioInfoSection(document, physioCare, titleFont, labelFont, textFont, lightGray, universityRed);

                // ===== MOTIVO DE CONSULTA =====
                AddPhysioReasonForConsultationSection(document, physioCare, titleFont, textFont, lightGray);

                // ===== ENFERMEDAD ACTUAL =====
                AddCurrentIllnessSection(document, physioCare, titleFont, labelFont, textFont, lightGray, universityRed, tableHeaderFont);

                // ===== ESCALAS DE DOLOR =====
                AddPainScalesSection(document, physioCare, titleFont, textFont, lightGray, universityRed, tableHeaderFont);

                // ===== EVALUACIONES =====
                AddEvaluationsSection(document, physioCare, titleFont, textFont, lightGray, universityRed, tableHeaderFont);

                // ===== PRUEBAS ESPECIALES =====
                AddSpecialTestsSection(document, physioCare, titleFont, textFont, lightGray, universityRed, tableHeaderFont);

                // ===== EXÁMENES COMPLEMENTARIOS =====
                AddComplementaryExamsSection(document, physioCare, titleFont, textFont, lightGray, universityRed, tableHeaderFont);

                // ===== SESIONES DE FISIOTERAPIA =====
                AddPhysioSessionsSection(document, physioCare, titleFont, textFont, lightGray, universityRed, tableHeaderFont);

                // ===== RESUMEN DE ATENCIÓN =====
                AddPhysioSummarySection(document, physioCare, titleFont, textFont, lightGray, universityRed);

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
                Console.WriteLine($"Error generando PDF de fisioterapia: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        private void AddPhysioInfoSection(Document document, MedicalCareDTO physioCare, Font titleFont, Font labelFont, Font textFont, BaseColor lightGray, BaseColor universityBlue)
        {
            var sectionTitle = new Paragraph("INFORMACIÓN GENERAL", titleFont)
            {
                SpacingAfter = 10f
            };
            document.Add(sectionTitle);

            var infoTable = new PdfPTable(2)
            {
                WidthPercentage = 100,
                SpacingAfter = 20f
            };
            infoTable.SetWidths(new float[] { 35, 65 });

            AddTableRow(infoTable, "ID Atención:", physioCare.CareId.ToString(), labelFont, textFont, lightGray);
            AddTableRow(infoTable, "Paciente:", physioCare.NamePatient ?? "No disponible", labelFont, textFont);
            AddTableRow(infoTable, "ID Paciente:", physioCare.PatientId.ToString(), labelFont, textFont, lightGray);
            AddTableRow(infoTable, "Área:", physioCare.Area ?? "Fisioterapia", labelFont, textFont);
            AddTableRow(infoTable, "Ubicación:", physioCare.NamePlace ?? "No disponible", labelFont, textFont, lightGray);
            AddTableRow(infoTable, "Profesional:", physioCare.NameHealthProfessional ?? "No disponible", labelFont, textFont);
            AddTableRow(infoTable, "Fecha de Atención:", physioCare.CareDate.ToString("dd/MM/yyyy HH:mm"), labelFont, textFont, lightGray);

            document.Add(infoTable);
        }

        private void AddPhysioReasonForConsultationSection(Document document, MedicalCareDTO physioCare, Font titleFont, Font textFont, BaseColor lightGray)
        {
            if (physioCare.ReasonForConsultation != null && !string.IsNullOrEmpty(physioCare.ReasonForConsultation.Description))
            {
                var sectionTitle = new Paragraph("MOTIVO DE CONSULTA", titleFont)
                {
                    SpacingBefore = 15f,
                    SpacingAfter = 10f
                };
                document.Add(sectionTitle);

                var reasonTable = new PdfPTable(1)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 20f
                };

                var reasonCell = new PdfPCell(new Phrase(physioCare.ReasonForConsultation.Description, textFont))
                {
                    Border = Rectangle.BOX,
                    BorderColor = lightGray,
                    Padding = 10,
                    BackgroundColor = BaseColor.White
                };
                reasonTable.AddCell(reasonCell);

                document.Add(reasonTable);
            }
        }


        private void AddCurrentIllnessSection(Document document, MedicalCareDTO physioCare, Font titleFont, Font labelFont, Font textFont, BaseColor lightGray, BaseColor universityBlue, Font tableHeaderFont)
        {
            if (physioCare.CurrentIllnesses != null && physioCare.CurrentIllnesses.Any())
            {
                var sectionTitle = new Paragraph("ENFERMEDAD ACTUAL", titleFont)
                {
                    SpacingBefore = 15f,
                    SpacingAfter = 10f
                };
                document.Add(sectionTitle);

                foreach (var illness in physioCare.CurrentIllnesses)
                {
                    var illnessTable = new PdfPTable(2)
                    {
                        WidthPercentage = 100,
                        SpacingAfter = 20f
                    };
                    illnessTable.SetWidths(new float[] { 50, 50 });

                    AddTableRow(illnessTable, "Tiempo de Evolución:", illness.EvolutionTime ?? "N/A", labelFont, textFont, lightGray);
                    AddTableRow(illnessTable, "Localización:", illness.Localization ?? "N/A", labelFont, textFont);
                    AddTableRow(illnessTable, "Intensidad:", illness.Intensity ?? "N/A", labelFont, textFont, lightGray);
                    AddTableRow(illnessTable, "Factores Agravantes:", illness.AggravatingFactors ?? "N/A", labelFont, textFont);
                    AddTableRow(illnessTable, "Factores Atenuantes:", illness.MitigatingFactors ?? "N/A", labelFont, textFont, lightGray);
                    AddTableRow(illnessTable, "Dolor Nocturno:", illness.NocturnalPain ?? "N/A", labelFont, textFont);
                    AddTableRow(illnessTable, "Debilidad:", illness.Weakness ?? "N/A", labelFont, textFont, lightGray);
                    AddTableRow(illnessTable, "Parestesias:", illness.Paresthesias ?? "N/A", labelFont, textFont);

                    if (!string.IsNullOrWhiteSpace(illness.ComplementaryExams))
                    {
                        AddTableRow(illnessTable, "Exámenes Complementarios:", illness.ComplementaryExams, labelFont, textFont, lightGray);
                    }

                    document.Add(illnessTable);
                }
            }

        }

        private void AddPainScalesSection(Document document, MedicalCareDTO physioCare, Font titleFont, Font textFont, BaseColor lightGray, BaseColor universityBlue, Font tableHeaderFont)
        {
            if (physioCare.PainScales != null && physioCare.PainScales.Any())
            {
                var sectionTitle = new Paragraph($"ESCALAS DE DOLOR ({physioCare.PainScales.Count})", titleFont)
                {
                    SpacingBefore = 15f,
                    SpacingAfter = 10f
                };
                document.Add(sectionTitle);

                var painTable = new PdfPTable(4)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 20f
                };
                painTable.SetWidths(new float[] { 25, 25, 25, 25 });

                // Encabezados de la tabla
                AddTableHeaderCell(painTable, "MOMENTO DEL DOLOR", tableHeaderFont, universityBlue);
                AddTableHeaderCell(painTable, "ACTIVIDAD", tableHeaderFont, universityBlue);
                AddTableHeaderCell(painTable, "ESCALA DE INTENSIDAD", tableHeaderFont, universityBlue);
                AddTableHeaderCell(painTable, "OBSERVACIÓN", tableHeaderFont, universityBlue);

                foreach (var pain in physioCare.PainScales)
                {
                    AddTableCell(painTable, pain.PainMomentName ?? "N/A", textFont);
                    AddTableCell(painTable, pain.ActionName ?? "N/A", textFont);
                    AddTableCell(painTable, pain.ScaleDescription ?? "N/A", textFont);
                    AddTableCell(painTable, pain.Observation ?? "N/A", textFont);
                }

                document.Add(painTable);
            }
        }

        private void AddEvaluationsSection(Document document, MedicalCareDTO physioCare, Font titleFont, Font textFont, BaseColor lightGray, BaseColor universityBlue, Font tableHeaderFont)
        {
            var hasSkinEvaluations = physioCare.SkinEvaluations?.Any() ?? false;
            var hasOsteoarticularEvaluations = physioCare.OsteoarticularEvaluations?.Any() ?? false;
            var hasMedicalEvaluations = physioCare.MedicalEvaluations?.Any() ?? false;
            var hasNeuromuscularEvaluations = physioCare.NeuromuscularEvaluations?.Any() ?? false;
            var hasPosturalEvaluations = physioCare.PosturalEvaluations?.Any() ?? false;

            if (hasSkinEvaluations || hasOsteoarticularEvaluations || hasMedicalEvaluations || hasNeuromuscularEvaluations || hasPosturalEvaluations)
            {
                var sectionTitle = new Paragraph("EVALUACIONES FISIOTERAPÉUTICAS", titleFont)
                {
                    SpacingBefore = 15f,
                    SpacingAfter = 10f
                };
                document.Add(sectionTitle);

                // Evaluaciones de Piel
                if (hasSkinEvaluations)
                {
                    var skinTitle = new Paragraph($"Evaluación de Piel ({physioCare.SkinEvaluations.Count})",
                        FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, universityBlue))
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 5f
                    };
                    document.Add(skinTitle);

                    var skinTable = new PdfPTable(5)
                    {
                        WidthPercentage = 100,
                        SpacingAfter = 15f
                    };
                    skinTable.SetWidths(new float[] { 20, 20, 20, 20, 20 });

                    AddTableHeaderCell(skinTable, "COLOR", tableHeaderFont, universityBlue);
                    AddTableHeaderCell(skinTable, "EDEMA", tableHeaderFont, universityBlue);
                    AddTableHeaderCell(skinTable, "ESTADO", tableHeaderFont, universityBlue);
                    AddTableHeaderCell(skinTable, "TUMEFACCIÓN", tableHeaderFont, universityBlue);
                    AddTableHeaderCell(skinTable, "FECHA", tableHeaderFont, universityBlue);

                    foreach (var skin in physioCare.SkinEvaluations)
                    {
                        AddTableCell(skinTable, skin.ColorName ?? "N/A", textFont);
                        AddTableCell(skinTable, skin.EdemaName ?? "N/A", textFont);
                        AddTableCell(skinTable, skin.StatusName ?? "N/A", textFont);
                        AddTableCell(skinTable, skin.SwellingName ?? "N/A", textFont);
                        AddTableCell(skinTable, skin.EvaluationDate?.ToString("dd/MM/yyyy") ?? "N/A", textFont);
                    }

                    document.Add(skinTable);
                }

                // Evaluaciones Osteoarticulares
                if (hasOsteoarticularEvaluations)
                {
                    var osteoTitle = new Paragraph($"Evaluación Osteoarticular ({physioCare.OsteoarticularEvaluations.Count})",
                        FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, universityBlue))
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 5f
                    };
                    document.Add(osteoTitle);

                    var osteoTable = new PdfPTable(2)
                    {
                        WidthPercentage = 100,
                        SpacingAfter = 15f
                    };
                    osteoTable.SetWidths(new float[] { 50, 50 });

                    AddTableHeaderCell(osteoTable, "ESTADO ARTICULAR", tableHeaderFont, universityBlue);
                    AddTableHeaderCell(osteoTable, "AMPLITUD ARTICULAR", tableHeaderFont, universityBlue);

                    foreach (var osteo in physioCare.OsteoarticularEvaluations)
                    {
                        AddTableCell(osteoTable, osteo.JointConditionName ?? "N/A", textFont);
                        AddTableCell(osteoTable, osteo.JointRangeOfMotionName ?? "N/A", textFont);
                    }

                    document.Add(osteoTable);
                }

                // Evaluaciones Médicas (Articular/Muscular)
                if (hasMedicalEvaluations)
                {
                    var medicalTitle = new Paragraph($"Evaluación Articular/Muscular ({physioCare.MedicalEvaluations.Count})",
                        FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, universityBlue))
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 5f
                    };
                    document.Add(medicalTitle);

                    var medicalTable = new PdfPTable(4)
                    {
                        WidthPercentage = 100,
                        SpacingAfter = 15f
                    };
                    medicalTable.SetWidths(new float[] { 20, 20, 20, 40 });

                    AddTableHeaderCell(medicalTable, "TIPO VALORACIÓN", tableHeaderFont, universityBlue);
                    AddTableHeaderCell(medicalTable, "POSICIÓN CORPORAL", tableHeaderFont, universityBlue);
                    AddTableHeaderCell(medicalTable, "UBICACIÓN", tableHeaderFont, universityBlue);
                    AddTableHeaderCell(medicalTable, "DESCRIPCIÓN", tableHeaderFont, universityBlue);

                    foreach (var evaluation in physioCare.MedicalEvaluations)
                    {
                        AddTableCell(medicalTable, evaluation.TypeOfMedicalEvaluationName ?? "N/A", textFont);
                        AddTableCell(medicalTable, evaluation.MedicalEvaluationPositionName ?? "N/A", textFont);
                        AddTableCell(medicalTable, evaluation.MedicalEvaluationMembersName ?? "N/A", textFont);
                        AddTableCell(medicalTable, evaluation.Description ?? "N/A", textFont);
                    }

                    document.Add(medicalTable);
                }

                // Evaluaciones Neuromusculares
                if (hasNeuromuscularEvaluations)
                {
                    var neuroTitle = new Paragraph($"Evaluación Neuromuscular ({physioCare.NeuromuscularEvaluations.Count})",
                        FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, universityBlue))
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 5f
                    };
                    document.Add(neuroTitle);

                    var neuroTable = new PdfPTable(3)
                    {
                        WidthPercentage = 100,
                        SpacingAfter = 15f
                    };
                    neuroTable.SetWidths(new float[] { 33, 33, 34 });

                    AddTableHeaderCell(neuroTable, "TONO", tableHeaderFont, universityBlue);
                    AddTableHeaderCell(neuroTable, "TROFISMO", tableHeaderFont, universityBlue);
                    AddTableHeaderCell(neuroTable, "FUERZA", tableHeaderFont, universityBlue);

                    foreach (var neuro in physioCare.NeuromuscularEvaluations)
                    {
                        AddTableCell(neuroTable, neuro.ShadeName ?? "N/A", textFont);
                        AddTableCell(neuroTable, neuro.TrophismName ?? "N/A", textFont);
                        AddTableCell(neuroTable, neuro.StrengthName ?? "N/A", textFont);
                    }

                    document.Add(neuroTable);
                }

                // Evaluaciones Posturales
                if (hasPosturalEvaluations)
                {
                    var posturalTitle = new Paragraph($"Evaluación Postural ({physioCare.PosturalEvaluations.Count})",
                        FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, universityBlue))
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 5f
                    };
                    document.Add(posturalTitle);

                    var posturalTable = new PdfPTable(4)
                    {
                        WidthPercentage = 100,
                        SpacingAfter = 15f
                    };
                    posturalTable.SetWidths(new float[] { 20, 15, 30, 35 });

                    AddTableHeaderCell(posturalTable, "VISTA", tableHeaderFont, universityBlue);
                    AddTableHeaderCell(posturalTable, "GRADO (%)", tableHeaderFont, universityBlue);
                    AddTableHeaderCell(posturalTable, "ALINEACIÓN CORPORAL", tableHeaderFont, universityBlue);
                    AddTableHeaderCell(posturalTable, "OBSERVACIÓN", tableHeaderFont, universityBlue);

                    foreach (var postural in physioCare.PosturalEvaluations)
                    {
                        AddTableCell(posturalTable, postural.ViewName ?? "N/A", textFont);
                        AddTableCell(posturalTable, postural.Grade.ToString() ?? "N/A", textFont);
                        AddTableCell(posturalTable, postural.BodyAlignment ?? "N/A", textFont);
                        AddTableCell(posturalTable, postural.Observation ?? "N/A", textFont);
                    }

                    document.Add(posturalTable);
                }
            }
        }

        private void AddSpecialTestsSection(Document document, MedicalCareDTO physioCare, Font titleFont, Font textFont, BaseColor lightGray, BaseColor universityBlue, Font tableHeaderFont)
        {
            if (physioCare.SpecialTests != null && physioCare.SpecialTests.Any())
            {
                var sectionTitle = new Paragraph($"PRUEBAS ESPECIALES ({physioCare.SpecialTests.Count})", titleFont)
                {
                    SpacingBefore = 15f,
                    SpacingAfter = 10f
                };
                document.Add(sectionTitle);

                var testsTable = new PdfPTable(3)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 20f
                };
                testsTable.SetWidths(new float[] { 30, 20, 50 });

                // Encabezados de la tabla
                AddTableHeaderCell(testsTable, "NOMBRE DE LA PRUEBA", tableHeaderFont, universityBlue);
                AddTableHeaderCell(testsTable, "RESULTADO", tableHeaderFont, universityBlue);
                AddTableHeaderCell(testsTable, "OBSERVACIONES", tableHeaderFont, universityBlue);

                foreach (var test in physioCare.SpecialTests)
                {
                    AddTableCell(testsTable, test.Test ?? "N/A", textFont);

                    // Celda de resultado con color
                    var resultCell = new PdfPCell(new Phrase(test.ResultTypeName ?? "N/A", textFont))
                    {
                        Border = Rectangle.BOX,
                        BorderColor = lightGray,
                        Padding = 5,
                        BackgroundColor = GetTestResultColor(test.ResultTypeName)
                    };
                    testsTable.AddCell(resultCell);

                    AddTableCell(testsTable, test.Observations ?? "N/A", textFont);
                }

                document.Add(testsTable);
            }
        }

        private void AddComplementaryExamsSection(Document document, MedicalCareDTO physioCare, Font titleFont, Font textFont, BaseColor lightGray, BaseColor universityBlue, Font tableHeaderFont)
        {
            if (physioCare.ComplementaryExams != null && physioCare.ComplementaryExams.Any())
            {
                var sectionTitle = new Paragraph($"EXÁMENES COMPLEMENTARIOS ({physioCare.ComplementaryExams.Count})", titleFont)
                {
                    SpacingBefore = 15f,
                    SpacingAfter = 10f
                };
                document.Add(sectionTitle);

                var examsTable = new PdfPTable(4)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 20f
                };
                examsTable.SetWidths(new float[] { 25, 15, 35, 25 });

                // Encabezados de la tabla
                AddTableHeaderCell(examsTable, "NOMBRE DEL EXAMEN", tableHeaderFont, universityBlue);
                AddTableHeaderCell(examsTable, "FECHA", tableHeaderFont, universityBlue);
                AddTableHeaderCell(examsTable, "DESCRIPCIÓN", tableHeaderFont, universityBlue);
                AddTableHeaderCell(examsTable, "DOCUMENTO", tableHeaderFont, universityBlue);

                foreach (var exam in physioCare.ComplementaryExams)
                {
                    AddTableCell(examsTable, exam.Exam ?? "N/A", textFont);
                    AddTableCell(examsTable, exam.ExamDate.ToString("dd/MM/yyyy"), textFont);
                    AddTableCell(examsTable, exam.Descriptions ?? "N/A", textFont);

                    // Celda de documento
                    var docCell = new PdfPCell(new Phrase(!string.IsNullOrWhiteSpace(exam.PdfLink) ? "Disponible" : "No disponible", textFont))
                    {
                        Border = Rectangle.BOX,
                        BorderColor = lightGray,
                        Padding = 5,
                        BackgroundColor = !string.IsNullOrWhiteSpace(exam.PdfLink) ? new BaseColor(220, 248, 198) : new BaseColor(255, 243, 205)
                    };
                    examsTable.AddCell(docCell);
                }

                document.Add(examsTable);
            }
        }

        private void AddPhysioSessionsSection(Document document, MedicalCareDTO physioCare, Font titleFont, Font textFont, BaseColor lightGray, BaseColor universityBlue, Font tableHeaderFont)
        {
            if (physioCare.Sessions != null && physioCare.Sessions.Any())
            {
                var sectionTitle = new Paragraph($"SESIONES DE FISIOTERAPIA ({physioCare.Sessions.Count})", titleFont)
                {
                    SpacingBefore = 15f,
                    SpacingAfter = 10f
                };
                document.Add(sectionTitle);

                var sessionsTable = new PdfPTable(5)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 20f
                };
                sessionsTable.SetWidths(new float[] { 15, 25, 25, 15, 20 });

                // Encabezados de la tabla
                AddTableHeaderCell(sessionsTable, "FECHA", tableHeaderFont, universityBlue);
                AddTableHeaderCell(sessionsTable, "DESCRIPCIÓN", tableHeaderFont, universityBlue);
                AddTableHeaderCell(sessionsTable, "TRATAMIENTO", tableHeaderFont, universityBlue);
                AddTableHeaderCell(sessionsTable, "ALTA MÉDICA", tableHeaderFont, universityBlue);
                AddTableHeaderCell(sessionsTable, "OBSERVACIONES", tableHeaderFont, universityBlue);

                foreach (var session in physioCare.Sessions)
                {
                    AddTableCell(sessionsTable, session.Date?.ToString("dd/MM/yyyy") ?? "N/A", textFont);
                    AddTableCell(sessionsTable, session.Description ?? "N/A", textFont);
                    AddTableCell(sessionsTable, session.Treatment ?? "N/A", textFont);

                    // Celda de alta médica
                    var dischargeCell = new PdfPCell(new Phrase((session.MedicalDischarge ?? false) ? "Sí" : "No", textFont))
                    {
                        Border = Rectangle.BOX,
                        BorderColor = lightGray,
                        Padding = 5,
                        BackgroundColor = (session.MedicalDischarge ?? false) ? new BaseColor(40, 167, 69) : new BaseColor(108, 117, 125),
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    dischargeCell.Phrase.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, BaseColor.White);
                    sessionsTable.AddCell(dischargeCell);

                    AddTableCell(sessionsTable, session.Observations ?? "N/A", textFont);
                }

                document.Add(sessionsTable);
            }
        }

        private void AddPhysioSummarySection(Document document, MedicalCareDTO physioCare, Font titleFont, Font textFont, BaseColor lightGray, BaseColor universityBlue)
        {
            var sectionTitle = new Paragraph("RESUMEN DE ATENCIÓN", titleFont)
            {
                SpacingBefore = 15f,
                SpacingAfter = 10f
            };
            document.Add(sectionTitle);

            var summaryTable = new PdfPTable(4)
            {
                WidthPercentage = 100,
                SpacingAfter = 20f
            };
            summaryTable.SetWidths(new float[] { 25, 25, 25, 25 });

            var hasCurrentIllness = physioCare.CurrentIllnesses != null;
            var painScalesCount = physioCare.PainScales?.Count ?? 0;
            var totalEvaluations = GetTotalPhysioEvaluations(physioCare);
            var sessionsCount = physioCare.Sessions?.Count ?? 0;

            AddSummaryCard(summaryTable, "ENFERMEDAD ACTUAL", hasCurrentIllness ? "SI" : "NO", universityBlue, textFont, hasCurrentIllness);
            AddSummaryCard(summaryTable, "ESCALAS DE DOLOR", painScalesCount.ToString(), universityBlue, textFont, painScalesCount > 0);
            AddSummaryCard(summaryTable, "EVALUACIONES", totalEvaluations.ToString(), universityBlue, textFont, totalEvaluations > 0);
            AddSummaryCard(summaryTable, "SESIONES", sessionsCount.ToString(), universityBlue, textFont, sessionsCount > 0);

            document.Add(summaryTable);
        }


        // Métodos auxiliares específicos para Physiotherapy PDF
        private BaseColor GetTestResultColor(string resultType)
        {
            return resultType?.ToLower() switch
            {
                "positivo" => new BaseColor(220, 53, 69),  // Rojo
                "negativo" => new BaseColor(40, 167, 69),  // Verde
                "normal" => new BaseColor(40, 167, 69),    // Verde
                "anormal" => new BaseColor(255, 193, 7),   // Amarillo
                _ => new BaseColor(108, 117, 125)          // Gris
            };
        }
        private int GetTotalPhysioEvaluations(MedicalCareDTO physioCare)
        {
            return (physioCare.SkinEvaluations?.Count ?? 0) +
                   (physioCare.OsteoarticularEvaluations?.Count ?? 0) +
                   (physioCare.MedicalEvaluations?.Count ?? 0) +
                   (physioCare.NeuromuscularEvaluations?.Count ?? 0) +
                   (physioCare.PosturalEvaluations?.Count ?? 0) +
                   (physioCare.SpecialTests?.Count ?? 0) +
                   (physioCare.ComplementaryExams?.Count ?? 0);
        }


    }
}