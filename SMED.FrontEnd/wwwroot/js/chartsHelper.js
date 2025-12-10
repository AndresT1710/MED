// ========================================
// CHARTS HELPER - Sistema de Reportes SMED
// ========================================

// Almacenamiento global de instancias de gráficos
window.chartInstances = {
    genderChart: null,
    locationChart: null,
    ageChart: null,
    referralChart: null,
    serviceChart: null,
    areaChart: null
};

// ========================================
// FUNCIÓN 1: Gráfico de Género (Pie Chart)
// ========================================
window.renderGenderChart = function (elementId, labels, data, colors) {
    try {
        console.log('Renderizando gráfico de género...', { elementId, labels, data });

        const canvas = document.getElementById(elementId);
        if (!canvas) {
            console.error(`Elemento ${elementId} no encontrado`);
            return;
        }

        const ctx = canvas.getContext('2d');
        if (!ctx) {
            console.error(`No se pudo obtener el contexto 2D para ${elementId}`);
            return;
        }

        // Destruir gráfico anterior si existe
        if (window.chartInstances.genderChart) {
            window.chartInstances.genderChart.destroy();
        }

        window.chartInstances.genderChart = new Chart(ctx, {
            type: 'pie',
            data: {
                labels: labels,
                datasets: [{
                    data: data,
                    backgroundColor: colors || [
                        'rgba(59, 130, 246, 0.8)',
                        'rgba(239, 68, 68, 0.8)',
                        'rgba(16, 185, 129, 0.8)'
                    ],
                    borderColor: '#ffffff',
                    borderWidth: 2
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        position: 'bottom',
                        labels: {
                            font: {
                                size: 13,
                                family: 'Arial'
                            },
                            padding: 15
                        }
                    },
                    title: {
                        display: true,
                        text: 'Distribución de Atenciones por Género',
                        font: {
                            size: 16,
                            weight: 'bold'
                        },
                        padding: {
                            top: 10,
                            bottom: 20
                        }
                    },
                    tooltip: {
                        callbacks: {
                            label: function (context) {
                                const label = context.label || '';
                                const value = context.parsed || 0;
                                const total = context.dataset.data.reduce((a, b) => a + b, 0);
                                const percentage = ((value / total) * 100).toFixed(1);
                                return `${label}: ${value} (${percentage}%)`;
                            }
                        }
                    }
                }
            }
        });

        console.log('Gráfico de género renderizado correctamente');
    } catch (error) {
        console.error('Error en renderGenderChart:', error);
    }
};

// ========================================
// FUNCIÓN 2: Gráfico de Ubicación (Bar Chart)
// ========================================
window.renderLocationChart = function (elementId, locations, counts) {
    try {
        console.log('Renderizando gráfico de ubicación...', { elementId, locations, counts });

        const canvas = document.getElementById(elementId);
        if (!canvas) {
            console.error(`Elemento ${elementId} no encontrado`);
            return;
        }

        const ctx = canvas.getContext('2d');
        if (!ctx) {
            console.error(`No se pudo obtener el contexto 2D para ${elementId}`);
            return;
        }

        // Destruir gráfico anterior si existe
        if (window.chartInstances.locationChart) {
            window.chartInstances.locationChart.destroy();
        }

        window.chartInstances.locationChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: locations,
                datasets: [{
                    label: 'Número de Atenciones',
                    data: counts,
                    backgroundColor: 'rgba(16, 185, 129, 0.7)',
                    borderColor: 'rgba(16, 185, 129, 1)',
                    borderWidth: 2,
                    borderRadius: 5
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    y: {
                        beginAtZero: true,
                        title: {
                            display: true,
                            text: 'Cantidad de Atenciones',
                            font: {
                                size: 13,
                                weight: 'bold'
                            }
                        },
                        ticks: {
                            stepSize: 1
                        },
                        grid: {
                            color: 'rgba(0, 0, 0, 0.05)'
                        }
                    },
                    x: {
                        title: {
                            display: true,
                            text: 'Ubicaciones (Ciudad, Provincia)',
                            font: {
                                size: 13,
                                weight: 'bold'
                            }
                        },
                        ticks: {
                            maxRotation: 45,
                            minRotation: 45,
                            font: {
                                size: 10
                            }
                        },
                        grid: {
                            display: false
                        }
                    }
                },
                plugins: {
                    title: {
                        display: true,
                        text: 'Top 10 Ubicaciones por Atenciones',
                        font: {
                            size: 16,
                            weight: 'bold'
                        },
                        padding: {
                            top: 10,
                            bottom: 20
                        }
                    },
                    legend: {
                        display: false
                    },
                    tooltip: {
                        callbacks: {
                            title: function (context) {
                                return context[0].label;
                            },
                            label: function (context) {
                                return `Atenciones: ${context.parsed.y}`;
                            }
                        }
                    }
                }
            }
        });

        console.log('Gráfico de ubicación renderizado correctamente');
    } catch (error) {
        console.error('Error en renderLocationChart:', error);
    }
};

// ========================================
// FUNCIÓN 3: Gráfico de Edad (Stacked Bar Chart)
// ========================================
window.renderAgeChart = function (elementId, ageRanges, departments, data) {
    try {
        console.log('Renderizando gráfico de edad...', { elementId, ageRanges, departments, data });

        const canvas = document.getElementById(elementId);
        if (!canvas) {
            console.error(`Elemento ${elementId} no encontrado`);
            return;
        }

        const ctx = canvas.getContext('2d');
        if (!ctx) {
            console.error(`No se pudo obtener el contexto 2D para ${elementId}`);
            return;
        }

        // Destruir gráfico anterior si existe
        if (window.chartInstances.ageChart) {
            window.chartInstances.ageChart.destroy();
        }

        const backgroundColors = [
            'rgba(255, 99, 132, 0.7)',
            'rgba(54, 162, 235, 0.7)',
            'rgba(255, 206, 86, 0.7)',
            'rgba(75, 192, 192, 0.7)',
            'rgba(153, 102, 255, 0.7)',
            'rgba(255, 159, 64, 0.7)',
            'rgba(199, 199, 199, 0.7)',
            'rgba(83, 102, 255, 0.7)'
        ];

        const datasets = departments.map((dept, index) => ({
            label: dept,
            data: ageRanges.map(range => {
                const rangeData = data[range];
                return rangeData && rangeData[dept] ? rangeData[dept] : 0;
            }),
            backgroundColor: backgroundColors[index % backgroundColors.length],
            borderColor: backgroundColors[index % backgroundColors.length].replace('0.7', '1'),
            borderWidth: 1
        }));

        window.chartInstances.ageChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ageRanges,
                datasets: datasets
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    x: {
                        stacked: true,
                        title: {
                            display: true,
                            text: 'Rangos de Edad',
                            font: {
                                size: 13,
                                weight: 'bold'
                            }
                        },
                        grid: {
                            display: false
                        }
                    },
                    y: {
                        stacked: true,
                        beginAtZero: true,
                        title: {
                            display: true,
                            text: 'Cantidad de Atenciones',
                            font: {
                                size: 13,
                                weight: 'bold'
                            }
                        },
                        ticks: {
                            stepSize: 5
                        },
                        grid: {
                            color: 'rgba(0, 0, 0, 0.05)'
                        }
                    }
                },
                plugins: {
                    title: {
                        display: true,
                        text: 'Atenciones por Rango de Edad y Departamento',
                        font: {
                            size: 16,
                            weight: 'bold'
                        },
                        padding: {
                            top: 10,
                            bottom: 20
                        }
                    },
                    legend: {
                        position: 'bottom',
                        labels: {
                            font: {
                                size: 11
                            },
                            padding: 10,
                            boxWidth: 15
                        }
                    },
                    tooltip: {
                        callbacks: {
                            label: function (context) {
                                const label = context.dataset.label || '';
                                const value = context.parsed.y || 0;
                                return `${label}: ${value} atenciones`;
                            }
                        }
                    }
                }
            }
        });

        console.log('Gráfico de edad renderizado correctamente');
    } catch (error) {
        console.error('Error en renderAgeChart:', error);
    }
};

// ========================================
// FUNCIÓN 4: Gráfico de Derivaciones (Horizontal Bar Chart)
// ========================================
window.renderReferralChart = function (elementId, referrals) {
    try {
        console.log('Renderizando gráfico de derivaciones...', { elementId, referrals });

        const canvas = document.getElementById(elementId);
        if (!canvas) {
            console.error(`Elemento ${elementId} no encontrado`);
            return;
        }

        const ctx = canvas.getContext('2d');
        if (!ctx) {
            console.error(`No se pudo obtener el contexto 2D para ${elementId}`);
            return;
        }

        // Destruir gráfico anterior si existe
        if (window.chartInstances.referralChart) {
            window.chartInstances.referralChart.destroy();
        }

        const labels = referrals.map(r => `${r.from} → ${r.to}`);
        const data = referrals.map(r => r.count);

        window.chartInstances.referralChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Cantidad de Derivaciones',
                    data: data,
                    backgroundColor: 'rgba(239, 68, 68, 0.7)',
                    borderColor: 'rgba(239, 68, 68, 1)',
                    borderWidth: 2,
                    borderRadius: 5
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                indexAxis: 'y',
                scales: {
                    x: {
                        beginAtZero: true,
                        title: {
                            display: true,
                            text: 'Cantidad de Derivaciones',
                            font: {
                                size: 13,
                                weight: 'bold'
                            }
                        },
                        ticks: {
                            stepSize: 1
                        },
                        grid: {
                            color: 'rgba(0, 0, 0, 0.05)'
                        }
                    },
                    y: {
                        title: {
                            display: true,
                            text: 'Flujo de Derivación',
                            font: {
                                size: 13,
                                weight: 'bold'
                            }
                        },
                        ticks: {
                            font: {
                                size: 10
                            }
                        },
                        grid: {
                            display: false
                        }
                    }
                },
                plugins: {
                    title: {
                        display: true,
                        text: 'Derivaciones entre Departamentos',
                        font: {
                            size: 16,
                            weight: 'bold'
                        },
                        padding: {
                            top: 10,
                            bottom: 20
                        }
                    },
                    legend: {
                        display: false
                    },
                    tooltip: {
                        callbacks: {
                            title: function (context) {
                                return context[0].label;
                            },
                            label: function (context) {
                                return `Derivaciones: ${context.parsed.x}`;
                            }
                        }
                    }
                }
            }
        });

        console.log('Gráfico de derivaciones renderizado correctamente');
    } catch (error) {
        console.error('Error en renderReferralChart:', error);
    }
};

// ========================================
// FUNCIÓN 5: Gráfico de Servicios (Horizontal Bar Chart)
// ========================================
window.renderServiceChart = function (elementId, departments, totals) {
    try {
        console.log('Renderizando gráfico de servicios...', { elementId, departments, totals });

        const canvas = document.getElementById(elementId);
        if (!canvas) {
            console.error(`Elemento ${elementId} no encontrado`);
            return;
        }

        const ctx = canvas.getContext('2d');
        if (!ctx) {
            console.error(`No se pudo obtener el contexto 2D para ${elementId}`);
            return;
        }

        // Destruir gráfico anterior si existe
        if (window.chartInstances.serviceChart) {
            window.chartInstances.serviceChart.destroy();
        }

        window.chartInstances.serviceChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: departments,
                datasets: [{
                    label: 'Total de Servicios',
                    data: totals,
                    backgroundColor: 'rgba(54, 162, 235, 0.7)',
                    borderColor: 'rgba(54, 162, 235, 1)',
                    borderWidth: 2,
                    borderRadius: 5
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                indexAxis: 'y',
                scales: {
                    x: {
                        beginAtZero: true,
                        title: {
                            display: true,
                            text: 'Cantidad de Servicios',
                            font: {
                                size: 13,
                                weight: 'bold'
                            }
                        },
                        ticks: {
                            stepSize: 5
                        },
                        grid: {
                            color: 'rgba(0, 0, 0, 0.05)'
                        }
                    },
                    y: {
                        title: {
                            display: true,
                            text: 'Profesional',
                            font: {
                                size: 13,
                                weight: 'bold'
                            }
                        },
                        grid: {
                            display: false
                        }
                    }
                },
                plugins: {
                    title: {
                        display: true,
                        text: 'Servicios Realizados por Departamento',
                        font: {
                            size: 16,
                            weight: 'bold'
                        },
                        padding: {
                            top: 10,
                            bottom: 20
                        }
                    },
                    legend: {
                        display: false
                    },
                    tooltip: {
                        callbacks: {
                            title: function (context) {
                                return context[0].label;
                            },
                            label: function (context) {
                                return `Servicios: ${context.parsed.x}`;
                            }
                        }
                    }
                }
            }
        });

        console.log('Gráfico de servicios renderizado correctamente');
    } catch (error) {
        console.error('Error en renderServiceChart:', error);
    }
};

// ========================================
// FUNCIÓN 6: Gráfico de Áreas (Bar Chart) - CORREGIDA
// ========================================
window.renderAreaChart = function (elementId, areas, totals) {
    try {
        console.log('Renderizando gráfico de áreas...', { elementId, areas, totals });

        // Validaciones iniciales
        if (!Array.isArray(areas) || !Array.isArray(totals)) {
            console.error('Datos inválidos para el gráfico de áreas', { areas, totals });
            return;
        }

        if (areas.length === 0 || totals.length === 0) {
            console.warn('No hay datos para mostrar en el gráfico de áreas');
            return;
        }

        const canvas = document.getElementById(elementId);
        if (!canvas) {
            console.error(`Elemento ${elementId} no encontrado`);
            // Intentar nuevamente después de un breve retraso
            setTimeout(() => {
                const retryCanvas = document.getElementById(elementId);
                if (retryCanvas) {
                    console.log(`Elemento ${elementId} encontrado en reintento`);
                    renderAreaChart(elementId, areas, totals);
                }
            }, 100);
            return;
        }

        const ctx = canvas.getContext('2d');
        if (!ctx) {
            console.error(`No se pudo obtener el contexto 2D para ${elementId}`);
            return;
        }

        // Limpiar canvas antes de renderizar
        ctx.clearRect(0, 0, canvas.width, canvas.height);

        // Destruir gráfico anterior si existe
        if (window.chartInstances.areaChart) {
            try {
                window.chartInstances.areaChart.destroy();
            } catch (e) {
                console.warn('Error al destruir gráfico anterior:', e);
            }
            window.chartInstances.areaChart = null;
        }

        // Crear gradiente de color
        const gradient = ctx.createLinearGradient(0, 0, 0, canvas.height);
        gradient.addColorStop(0, 'rgba(111, 66, 193, 0.9)');
        gradient.addColorStop(1, 'rgba(111, 66, 193, 0.4)');

        // Preparar datos para el gráfico
        const displayAreas = areas.map((area, index) => {
            // Acortar nombres largos para mejor visualización
            if (area.length > 30) {
                return area.substring(0, 27) + '...';
            }
            return area;
        });

        const displayTotals = totals.map(total => total || 0);

        // Configurar el gráfico
        const chartConfig = {
            type: 'bar',
            data: {
                labels: displayAreas,
                datasets: [{
                    label: 'Total de Atenciones',
                    data: displayTotals,
                    backgroundColor: gradient,
                    borderColor: 'rgba(111, 66, 193, 1)',
                    borderWidth: 2,
                    borderRadius: 8,
                    hoverBackgroundColor: 'rgba(111, 66, 193, 0.8)',
                    hoverBorderColor: 'rgba(111, 66, 193, 1)',
                    hoverBorderWidth: 3
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    y: {
                        beginAtZero: true,
                        title: {
                            display: true,
                            text: 'Cantidad de Atenciones',
                            font: {
                                size: 13,
                                weight: 'bold'
                            },
                            color: '#666'
                        },
                        ticks: {
                            precision: 0,
                            font: {
                                size: 11
                            },
                            color: '#666'
                        },
                        grid: {
                            color: 'rgba(0, 0, 0, 0.05)',
                            drawBorder: false
                        }
                    },
                    x: {
                        title: {
                            display: true,
                            text: 'Áreas Médicas',
                            font: {
                                size: 13,
                                weight: 'bold'
                            },
                            color: '#666'
                        },
                        ticks: {
                            font: {
                                size: 11
                            },
                            color: '#666',
                            maxRotation: 45,
                            minRotation: 45
                        },
                        grid: {
                            display: false
                        }
                    }
                },
                plugins: {
                    title: {
                        display: true,
                        text: 'Top 10 Áreas por Cantidad de Atenciones',
                        font: {
                            size: 16,
                            weight: 'bold'
                        },
                        color: '#333',
                        padding: {
                            top: 10,
                            bottom: 20
                        }
                    },
                    legend: {
                        display: false
                    },
                    tooltip: {
                        backgroundColor: 'rgba(0, 0, 0, 0.8)',
                        titleColor: '#ffffff',
                        bodyColor: '#ffffff',
                        borderColor: 'rgba(111, 66, 193, 1)',
                        borderWidth: 1,
                        cornerRadius: 6,
                        padding: 12,
                        callbacks: {
                            label: function (context) {
                                const value = context.parsed.y;
                                const total = displayTotals.reduce((a, b) => a + b, 0);
                                const percentage = total > 0 ? ((value / total) * 100).toFixed(1) : 0;
                                return `${value} atenciones (${percentage}%)`;
                            },
                            title: function (tooltipItems) {
                                // Mostrar el nombre completo del área
                                const index = tooltipItems[0].dataIndex;
                                return areas[index] || displayAreas[index];
                            }
                        }
                    }
                },
                animation: {
                    duration: 1000,
                    easing: 'easeOutQuart'
                },
                interaction: {
                    intersect: false,
                    mode: 'index'
                },
                elements: {
                    bar: {
                        borderWidth: 2,
                        borderSkipped: false
                    }
                }
            }
        };

        // Crear y almacenar la instancia del gráfico
        window.chartInstances.areaChart = new Chart(ctx, chartConfig);

        console.log('Gráfico de áreas renderizado correctamente');

        // Forzar actualización del canvas
        canvas.style.display = 'none';
        canvas.offsetHeight; // Trigger reflow
        canvas.style.display = 'block';

    } catch (error) {
        console.error('Error en renderAreaChart:', error);
        console.error('Stack trace:', error.stack);
    }
};

// ========================================
// FUNCIÓN 7: Mini gráficos para tabla de áreas
// ========================================
window.renderAreaMiniCharts = function () {
    try {
        console.log('Renderizando mini gráficos de área...');

        // Verificar que Chart.js esté cargado
        if (typeof Chart === 'undefined') {
            console.error('Chart.js no está cargado');
            // Intentar cargarlo dinámicamente
            return;
        }

        // Esperar a que el DOM esté listo
        if (document.readyState !== 'complete') {
            setTimeout(window.renderAreaMiniCharts, 100);
            return;
        }

        const dataElements = document.querySelectorAll('[id^="data-"]');
        console.log(`Encontrados ${dataElements.length} elementos de datos para mini gráficos`);

        dataElements.forEach((element, index) => {
            try {
                const id = element.id.replace('data-', '');
                const canvasId = `miniChart-${id}`;
                const canvas = document.getElementById(canvasId);

                if (!canvas) {
                    console.warn(`Canvas no encontrado: ${canvasId}`);
                    return;
                }

                // Verificar si ya existe un gráfico en este canvas
                if (canvas._chart) {
                    try {
                        canvas._chart.destroy();
                    } catch (e) {
                        console.warn('Error al destruir gráfico anterior:', e);
                    }
                    canvas._chart = null;
                }

                // Limpiar canvas
                const ctx = canvas.getContext('2d');
                if (!ctx) {
                    console.warn(`No se pudo obtener contexto 2D para ${canvasId}`);
                    return;
                }

                ctx.clearRect(0, 0, canvas.width, canvas.height);

                // Obtener datos
                let months, values;
                try {
                    months = JSON.parse(element.getAttribute('data-months') || '[]');
                    values = JSON.parse(element.getAttribute('data-values') || '[]');
                } catch (e) {
                    console.warn(`Error parseando datos para ${canvasId}:`, e);
                    return;
                }

                if (!Array.isArray(months) || !Array.isArray(values)) {
                    console.warn(`Datos inválidos para ${canvasId}`);
                    return;
                }

                if (months.length === 0 || values.length === 0) {
                    console.warn(`Sin datos para ${canvasId}`);
                    return;
                }

                // Formatear meses para mostrar (solo último segmento del año-mes)
                const formattedMonths = months.map(month => {
                    if (month && typeof month === 'string') {
                        const parts = month.split('-');
                        if (parts.length >= 2) {
                            const monthNum = parseInt(parts[1]);
                            const monthNames = ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'];
                            return monthNames[monthNum - 1] || parts[1];
                        }
                    }
                    return month || '';
                });

                // Determinar color basado en la tendencia
                const total = values.reduce((a, b) => a + b, 0);
                const lastValue = values[values.length - 1] || 0;
                const secondLastValue = values[values.length - 2] || 0;
                const avgValue = total / values.length;

                let lineColor, fillColor;
                if (lastValue > secondLastValue) {
                    // Tendencia positiva
                    lineColor = 'rgba(40, 167, 69, 0.9)';
                    fillColor = 'rgba(40, 167, 69, 0.1)';
                } else if (lastValue < secondLastValue) {
                    // Tendencia negativa
                    lineColor = 'rgba(220, 53, 69, 0.9)';
                    fillColor = 'rgba(220, 53, 69, 0.1)';
                } else {
                    // Estable
                    lineColor = 'rgba(108, 117, 125, 0.9)';
                    fillColor = 'rgba(108, 117, 125, 0.1)';
                }

                // Crear mini gráfico
                const chart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: formattedMonths,
                        datasets: [{
                            data: values,
                            backgroundColor: fillColor,
                            borderColor: lineColor,
                            borderWidth: 2,
                            pointRadius: 2,
                            pointBackgroundColor: lineColor,
                            pointBorderColor: '#ffffff',
                            pointBorderWidth: 1,
                            tension: 0.3,
                            fill: true
                        }]
                    },
                    options: {
                        responsive: true,
                        maintainAspectRatio: false,
                        plugins: {
                            legend: {
                                display: false
                            },
                            tooltip: {
                                enabled: true,
                                mode: 'index',
                                intersect: false,
                                backgroundColor: 'rgba(0, 0, 0, 0.8)',
                                titleColor: '#ffffff',
                                bodyColor: '#ffffff',
                                cornerRadius: 4,
                                padding: 8,
                                callbacks: {
                                    title: function (tooltipItems) {
                                        const index = tooltipItems[0].dataIndex;
                                        const originalMonth = months[index] || '';
                                        return `Mes: ${formattedMonths[index]} (${originalMonth})`;
                                    },
                                    label: function (context) {
                                        return `Atenciones: ${context.parsed.y}`;
                                    }
                                }
                            }
                        },
                        scales: {
                            x: {
                                display: false,
                                grid: {
                                    display: false
                                }
                            },
                            y: {
                                display: false,
                                grid: {
                                    display: false
                                },
                                beginAtZero: true
                            }
                        },
                        elements: {
                            line: {
                                borderWidth: 2,
                                tension: 0.3
                            },
                            point: {
                                radius: 2,
                                hoverRadius: 4
                            }
                        },
                        interaction: {
                            intersect: false,
                            mode: 'index'
                        },
                        layout: {
                            padding: {
                                top: 2,
                                bottom: 2,
                                left: 2,
                                right: 2
                            }
                        }
                    }
                });

                // Almacenar referencia al gráfico en el canvas
                canvas._chart = chart;

                console.log(`Mini gráfico ${index + 1}/${dataElements.length} creado: ${canvasId}`);

            } catch (error) {
                console.error(`Error creando mini gráfico ${index + 1}:`, error);
            }
        });

        console.log('Mini gráficos de área renderizados correctamente');

    } catch (error) {
        console.error('Error en renderAreaMiniCharts:', error);
        console.error('Stack trace:', error.stack);
    }
};

// ========================================
// FUNCIONES AUXILIARES MEJORADAS
// ========================================

// Verificar si un elemento existe en el DOM
window.checkElementExists = function (elementId) {
    const exists = document.getElementById(elementId) !== null;
    console.log(`Verificando elemento ${elementId}: ${exists}`);
    return exists;
};

// Esperar a que un elemento esté disponible en el DOM
window.waitForElement = function (elementId, timeout = 5000) {
    return new Promise((resolve, reject) => {
        const startTime = Date.now();

        const check = () => {
            const element = document.getElementById(elementId);
            if (element) {
                resolve(element);
            } else if (Date.now() - startTime > timeout) {
                reject(new Error(`Elemento ${elementId} no encontrado después de ${timeout}ms`));
            } else {
                setTimeout(check, 100);
            }
        };

        check();
    });
};

// Destruir todos los gráficos
window.destroyAllCharts = function () {
    console.log('Destruyendo todos los gráficos...');
    Object.keys(window.chartInstances).forEach(chartKey => {
        if (window.chartInstances[chartKey]) {
            try {
                window.chartInstances[chartKey].destroy();
                console.log(`Gráfico ${chartKey} destruido`);
            } catch (e) {
                console.warn(`Error al destruir gráfico ${chartKey}:`, e);
            }
            window.chartInstances[chartKey] = null;
        }
    });

    // También destruir mini gráficos
    const canvases = document.querySelectorAll('canvas');
    canvases.forEach(canvas => {
        if (canvas._chart) {
            try {
                canvas._chart.destroy();
            } catch (e) {
                // Ignorar errores al destruir
            }
            canvas._chart = null;
        }
    });
};

// Destruir un gráfico específico
window.destroyChart = function (chartName) {
    if (window.chartInstances[chartName]) {
        try {
            window.chartInstances[chartName].destroy();
            console.log(`Gráfico ${chartName} destruido`);
        } catch (e) {
            console.warn(`Error al destruir gráfico ${chartName}:`, e);
        }
        window.chartInstances[chartName] = null;
    }
};

// Redimensionar todos los gráficos
window.resizeAllCharts = function () {
    Object.keys(window.chartInstances).forEach(chartKey => {
        if (window.chartInstances[chartKey]) {
            try {
                window.chartInstances[chartKey].resize();
            } catch (e) {
                console.warn(`Error al redimensionar gráfico ${chartKey}:`, e);
            }
        }
    });
};

// Función de diagnóstico mejorada
window.diagnoseCharts = function () {
    console.log('=== DIAGNÓSTICO DE GRÁFICOS ===');
    console.log('Chart.js cargado:', typeof Chart !== 'undefined');
    console.log('Chart versión:', Chart?.version || 'No disponible');
    console.log('Instancias de gráficos:', Object.keys(window.chartInstances).filter(k => window.chartInstances[k]));

    const elements = ['genderChart', 'locationChart', 'ageChart', 'referralChart', 'serviceChart', 'areaChart'];
    elements.forEach(id => {
        const element = document.getElementById(id);
        const exists = element !== null;
        console.log(`Elemento ${id}:`, {
            existe: exists,
            tipo: element?.tagName,
            dimensiones: exists ? `${element.clientWidth}x${element.clientHeight}` : 'N/A'
        });
    });

    // Verificar mini gráficos
    const miniCharts = document.querySelectorAll('[id^="miniChart-"]');
    console.log(`Mini gráficos encontrados: ${miniCharts.length}`);

    console.log('================================');
};

// Inicializar listeners para redimensionamiento
if (typeof window !== 'undefined') {
    window.addEventListener('resize', function () {
        if (window.resizeAllChartsTimeout) {
            clearTimeout(window.resizeAllChartsTimeout);
        }
        window.resizeAllChartsTimeout = setTimeout(window.resizeAllCharts, 250);
    });
}

console.log('chartsHelper.js cargado correctamente');
console.log('Chart.js disponible:', typeof Chart !== 'undefined');
console.log('Chart versión:', Chart?.version || 'No disponible');

// Exportar funciones para Blazor
window.smedCharts = {
    renderGenderChart: window.renderGenderChart,
    renderLocationChart: window.renderLocationChart,
    renderAgeChart: window.renderAgeChart,
    renderReferralChart: window.renderReferralChart,
    renderServiceChart: window.renderServiceChart,
    renderAreaChart: window.renderAreaChart,
    renderAreaMiniCharts: window.renderAreaMiniCharts,
    destroyAllCharts: window.destroyAllCharts,
    diagnoseCharts: window.diagnoseCharts
};