// Función DEFINITIVA para abrir PDF en nueva pestaña Y permitir descarga
window.abrirPdfEnNuevaPestana = function (base64DataUrl, fileName) {
    try {
        // Valores por defecto
        fileName = fileName || 'documento.pdf';

        console.log("🔵 Iniciando proceso PDF:", fileName);

        // Validar formato
        if (!base64DataUrl || !base64DataUrl.startsWith('data:application/pdf')) {
            console.error("❌ Formato de datos inválido");
            alert("Error: Formato de PDF inválido");
            return false;
        }

        // FORZAR extensión .pdf de manera explícita
        if (!fileName.toLowerCase().endsWith('.pdf')) {
            fileName = fileName + '.pdf';
        }

        // Asegurar que el nombre sea válido
        fileName = fileName.trim();

        console.log("📝 Nombre final del archivo:", fileName);

        // Extraer base64
        const base64Data = base64DataUrl.split(',')[1];
        if (!base64Data) {
            console.error("❌ No se pudo extraer base64");
            return false;
        }

        // Decodificar
        const byteCharacters = atob(base64Data);
        const byteNumbers = new Array(byteCharacters.length);
        for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }

        // Crear Blob CON tipo MIME explícito
        const byteArray = new Uint8Array(byteNumbers);
        const blob = new Blob([byteArray], {
            type: 'application/pdf'
        });
        const blobUrl = URL.createObjectURL(blob);

        console.log("✅ PDF creado, tamaño:", blob.size, "bytes, tipo:", blob.type);

        // ===== PASO 1: ABRIR EN NUEVA PESTAÑA (PRIORITARIO) =====
        console.log("🌐 Abriendo en nueva pestaña...");
        const newWindow = window.open(blobUrl, '_blank');

        if (!newWindow) {
            console.warn("⚠️ Popup bloqueado, intentando descarga directa...");

            // Si no se puede abrir ventana, descargar directamente
            const link = document.createElement('a');
            link.href = blobUrl;
            link.download = fileName; // Nombre con .pdf
            link.type = 'application/pdf';
            link.style.display = 'none';
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);

            setTimeout(function () {
                URL.revokeObjectURL(blobUrl);
            }, 1000);

            return true;
        }

        // Configurar título de la nueva ventana
        try {
            newWindow.document.title = fileName;
        } catch (e) {
            console.log("No se pudo configurar título de ventana");
        }

        // ===== PASO 2: OFRECER DESCARGA CON NOMBRE Y TIPO CORRECTO =====
        setTimeout(function () {
            console.log("💾 Iniciando descarga automática con nombre:", fileName);

            const link = document.createElement('a');
            link.href = blobUrl;
            link.download = fileName; // Nombre completo con .pdf
            link.type = 'application/pdf'; // Tipo MIME explícito
            link.rel = 'noopener noreferrer';
            link.style.display = 'none';

            // Agregar al DOM
            document.body.appendChild(link);

            // Forzar click
            link.click();

            // Remover del DOM
            document.body.removeChild(link);

            console.log("✅ Descarga iniciada con extensión .pdf");
        }, 800);

        // ===== LIMPIEZA DE MEMORIA =====
        setTimeout(function () {
            URL.revokeObjectURL(blobUrl);
            console.log("🧹 Limpieza automática completada");
        }, 30000);

        console.log("✅ Proceso completado exitosamente");
        return true;

    } catch (err) {
        console.error("❌ Error crítico:", err);
        alert("Error al procesar PDF: " + err.message);
        return false;
    }
};

// Función alternativa: solo vista previa (sin descarga)
window.abrirPdfSoloVista = function (base64DataUrl, fileName) {
    try {
        fileName = fileName || 'documento.pdf';

        if (!fileName.toLowerCase().endsWith('.pdf')) {
            fileName += '.pdf';
        }

        const base64Data = base64DataUrl.split(',')[1];
        const byteCharacters = atob(base64Data);
        const byteNumbers = new Array(byteCharacters.length);

        for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }

        const byteArray = new Uint8Array(byteNumbers);
        const blob = new Blob([byteArray], { type: 'application/pdf' });
        const blobUrl = URL.createObjectURL(blob);

        const newWindow = window.open(blobUrl, '_blank');

        if (newWindow) {
            try {
                newWindow.document.title = fileName;
            } catch (e) {
                console.log("No se pudo configurar título");
            }
        }

        setTimeout(function () {
            URL.revokeObjectURL(blobUrl);
        }, 30000);

        return true;

    } catch (err) {
        console.error("Error:", err);
        return false;
    }
};

// Función alternativa: solo descarga (sin vista previa) - MÉTODO MÁS ROBUSTO
window.descargarPdfDirecto = function (base64DataUrl, fileName) {
    try {
        fileName = fileName || 'documento.pdf';

        // FORZAR extensión .pdf
        if (!fileName.toLowerCase().endsWith('.pdf')) {
            fileName = fileName + '.pdf';
        }

        console.log("💾 Descargando archivo:", fileName);

        const base64Data = base64DataUrl.split(',')[1];
        const byteCharacters = atob(base64Data);
        const byteNumbers = new Array(byteCharacters.length);

        for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }

        const byteArray = new Uint8Array(byteNumbers);

        // Crear blob con tipo MIME correcto
        const blob = new Blob([byteArray], {
            type: 'application/pdf'
        });

        // Método 1: Usar URL.createObjectURL
        const blobUrl = URL.createObjectURL(blob);

        const link = document.createElement('a');
        link.href = blobUrl;
        link.download = fileName; // Nombre con .pdf garantizado
        link.type = 'application/pdf';
        link.style.display = 'none';

        document.body.appendChild(link);
        link.click();

        // Pequeño delay antes de limpiar
        setTimeout(function () {
            document.body.removeChild(link);
            URL.revokeObjectURL(blobUrl);
            console.log("✅ Archivo descargado:", fileName);
        }, 100);

        return true;

    } catch (err) {
        console.error("Error:", err);
        return false;
    }
};

// Función para verificar que el nombre tenga .pdf
window.verificarNombrePdf = function (fileName) {
    fileName = fileName || 'documento.pdf';
    if (!fileName.toLowerCase().endsWith('.pdf')) {
        fileName = fileName + '.pdf';
    }
    console.log("Nombre verificado:", fileName);
    return fileName;
};