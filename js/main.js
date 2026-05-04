// ================================
// AQUASAVE — Dashboard Hídrico
// Tomás Gil Gómez
// ================================


// ---- 1. DATOS BASE ----
// Litros por unidad de cada actividad

const LITROS = {
  ducha:    10,   // por minuto
  inodoro:  8,    // por descarga
  platos:   15,   // por lavada
  manos:    2,    // por lavada
  dientes:  3,    // por cepillada
  lavadora: 80    // por ciclo
};

const PROMEDIOS = {
  colombia: 187,
  mundial:  310,
  onu:      50
};


// ---- 2. REFERENCIAS AL DOM ----
// Capturamos todos los elementos que vamos a manipular

const sliders = {
  ducha:    document.getElementById('ducha'),
  inodoro:  document.getElementById('inodoro'),
  platos:   document.getElementById('platos'),
  manos:    document.getElementById('manos'),
  dientes:  document.getElementById('dientes'),
  lavadora: document.getElementById('lavadora')
};

const valores = {
  ducha:    document.getElementById('val-ducha'),
  inodoro:  document.getElementById('val-inodoro'),
  platos:   document.getElementById('val-platos'),
  manos:    document.getElementById('val-manos'),
  dientes:  document.getElementById('val-dientes'),
  lavadora: document.getElementById('val-lavadora')
};

const desglose = {
  ducha:    document.getElementById('d-ducha'),
  inodoro:  document.getElementById('d-inodoro'),
  platos:   document.getElementById('d-platos'),
  manos:    document.getElementById('d-manos'),
  dientes:  document.getElementById('d-dientes'),
  lavadora: document.getElementById('d-lavadora')
};

const totalEl      = document.getElementById('total-litros');
const nivelBadge   = document.getElementById('nivel-badge');
const barraUsuario = document.getElementById('barra-usuario');
const labelUsuario = document.getElementById('label-usuario');
const recosGrid    = document.getElementById('recos-grid');
const recoIntro    = document.getElementById('reco-intro');


// ---- 3. FUNCIÓN PRINCIPAL — calcular y actualizar todo ----

function actualizar() {

  // Calculamos litros por actividad
  const litros = {
    ducha:    parseFloat(sliders.ducha.value)    * LITROS.ducha,
    inodoro:  parseFloat(sliders.inodoro.value)  * LITROS.inodoro,
    platos:   parseFloat(sliders.platos.value)   * LITROS.platos,
    manos:    parseFloat(sliders.manos.value)    * LITROS.manos,
    dientes:  parseFloat(sliders.dientes.value)  * LITROS.dientes,
    lavadora: parseFloat(sliders.lavadora.value) * LITROS.lavadora
  };

  // Total del día
  const total = Object.values(litros).reduce((a, b) => a + b, 0);
  // Object.values obtiene los valores del objeto
  // reduce los suma todos

  // Actualizar número principal con animación
  animarNumero(totalEl, total);

  // Actualizar etiquetas de los sliders
  valores.ducha.textContent    = `${sliders.ducha.value} min`;
  valores.inodoro.textContent  = `${sliders.inodoro.value} veces`;
  valores.platos.textContent   = `${sliders.platos.value} veces`;
  valores.manos.textContent    = `${sliders.manos.value} veces`;
  valores.dientes.textContent  = `${sliders.dientes.value} veces`;
  valores.lavadora.textContent = `${parseFloat(sliders.lavadora.value).toFixed(1)} ciclos`;

  // Actualizar desglose
  desglose.ducha.textContent    = `${Math.round(litros.ducha)} L`;
  desglose.inodoro.textContent  = `${Math.round(litros.inodoro)} L`;
  desglose.platos.textContent   = `${Math.round(litros.platos)} L`;
  desglose.manos.textContent    = `${Math.round(litros.manos)} L`;
  desglose.dientes.textContent  = `${Math.round(litros.dientes)} L`;
  desglose.lavadora.textContent = `${Math.round(litros.lavadora)} L`;

  // Actualizar barra del usuario en la gráfica
  // El máximo de referencia es el promedio mundial (310L = 100%)
  const porcentaje = Math.min((total / PROMEDIOS.mundial) * 100, 100);
  barraUsuario.style.width = porcentaje + '%';
  labelUsuario.textContent = `${Math.round(total)} L`;

  // Determinar nivel y actualizar badge
  const nivel = obtenerNivel(total);
  nivelBadge.textContent = nivel.texto;
  nivelBadge.className = `resultado__nivel nivel--${nivel.clase}`;

  // Actualizar recomendaciones
  mostrarRecomendaciones(total, nivel.clase);
}


// ---- 4. NIVEL DE CONSUMO ----

function obtenerNivel(total) {
  if (total <= PROMEDIOS.onu * 1.5) {
    return { texto: '✓ Consumo bajo — Excelente', clase: 'bajo' };
  } else if (total <= PROMEDIOS.colombia) {
    return { texto: '◎ Consumo moderado', clase: 'moderado' };
  } else {
    return { texto: '⚠ Consumo alto', clase: 'alto' };
  }
}


// ---- 5. RECOMENDACIONES DINÁMICAS ----

const todasRecomendaciones = [
  {
    icono: '🚿',
    titulo: 'Reduce el tiempo de ducha',
    texto: 'Cada minuto menos en la ducha ahorra 10 litros. Una ducha de 5 minutos en lugar de 10 ahorra 50L al día.',
    ahorro: 'Hasta 50L/día',
    nivel: ['moderado', 'alto']
  },
  {
    icono: '🚽',
    titulo: 'Usa inodoros de doble descarga',
    texto: 'Los inodoros modernos de doble descarga usan 3-4L en lugar de 8L. Un cambio simple con gran impacto.',
    ahorro: 'Hasta 40L/día',
    nivel: ['moderado', 'alto']
  },
  {
    icono: '🪥',
    titulo: 'Cierra el grifo al cepillarte',
    texto: 'Dejar el grifo abierto mientras te cepillas gasta hasta 12L. Ciérralo y ahorra el 80% de ese consumo.',
    ahorro: 'Hasta 10L/día',
    nivel: ['moderado', 'alto', 'bajo']
  },
  {
    icono: '🍽️',
    titulo: 'Lava los platos en tanda',
    texto: 'En lugar de lavar plato por plato, acumúlalos y lávalos todos juntos. Reduce lavadas de 3 a 1 por día.',
    ahorro: 'Hasta 30L/día',
    nivel: ['alto']
  },
  {
    icono: '👕',
    titulo: 'Usa la lavadora a plena carga',
    texto: 'Una lavadora a media carga usa casi la misma agua que a plena carga. Espera a tener suficiente ropa.',
    ahorro: 'Hasta 40L/ciclo',
    nivel: ['moderado', 'alto']
  },
  {
    icono: '💧',
    titulo: 'Reutiliza el agua de cocinar',
    texto: 'El agua en que cocinas verduras, una vez fría, sirve para regar plantas. Cero desperdicio.',
    ahorro: '2-5L/día',
    nivel: ['bajo', 'moderado']
  },
  {
    icono: '🌧️',
    titulo: 'Recolecta agua lluvia',
    texto: 'Un recipiente estratégico puede recolectar agua lluvia para limpiar pisos, regar plantas o lavar el carro.',
    ahorro: 'Variable',
    nivel: ['bajo', 'moderado', 'alto']
  },
  {
    icono: '🔧',
    titulo: 'Revisa fugas en tu hogar',
    texto: 'Un grifo que gotea puede desperdiciar hasta 30 litros al día. Revisa grifos, tuberías e inodoros.',
    ahorro: 'Hasta 30L/día',
    nivel: ['alto']
  }
];

function mostrarRecomendaciones(total, nivelClase) {

  // Filtramos recomendaciones según el nivel
  const recosFiltradas = todasRecomendaciones.filter(r =>
    r.nivel.includes(nivelClase)
  );

  // Actualizamos el texto intro
  const mensajes = {
    bajo:     '¡Excelente! Tu consumo está por debajo del promedio colombiano. Aquí algunos tips para mantenerlo.',
    moderado: 'Tu consumo es moderado. Con estos cambios puedes bajar del promedio colombiano.',
    alto:     'Tu consumo supera el promedio colombiano. Estos cambios pueden ayudarte a reducirlo significativamente.'
  };

  recoIntro.textContent = mensajes[nivelClase];

  // Limpiamos el grid y lo llenamos de nuevo
  recosGrid.innerHTML = '';

  recosFiltradas.forEach((reco, i) => {
    const card = document.createElement('div');
    card.className = 'reco__card animar';

    card.innerHTML = `
      <span class="reco__icono">${reco.icono}</span>
      <h3 class="reco__titulo">${reco.titulo}</h3>
      <p class="reco__texto">${reco.texto}</p>
      <span class="reco__ahorro">Ahorro: ${reco.ahorro}</span>
    `;

    recosGrid.appendChild(card);

    // Animamos cada card con delay escalonado
    setTimeout(() => {
      card.classList.add('visible');
    }, i * 100);
  });
}


// ---- 6. ANIMACIÓN DE NÚMERO ----
// Hace que el número suba o baje suavemente

function animarNumero(elemento, valorFinal) {
  const valorActual = parseInt(elemento.textContent) || 0;
  const diferencia  = valorFinal - valorActual;
  const pasos       = 20;
  const incremento  = diferencia / pasos;
  let paso          = 0;

  const intervalo = setInterval(() => {
    paso++;
    elemento.textContent = Math.round(valorActual + incremento * paso);
    if (paso >= pasos) {
      elemento.textContent = Math.round(valorFinal);
      clearInterval(intervalo);
    }
  }, 16);
}


// ---- 7. ESCUCHAR CAMBIOS EN SLIDERS ----
// Cada vez que el usuario mueve un slider, recalculamos todo

Object.values(sliders).forEach(slider => {
  slider.addEventListener('input', actualizar);
});


// ---- 8. ANIMACIONES DE SCROLL ----

const elementosAnimables = document.querySelectorAll(
  '.stat__card, .control__grupo, .dato__card, ' +
  '.resultado__principal, .grafica, .desglose'
);

elementosAnimables.forEach(el => el.classList.add('animar'));

const observador = new IntersectionObserver(
  (entradas) => {
    entradas.forEach((entrada, i) => {
      if (entrada.isIntersecting) {
        setTimeout(() => {
          entrada.target.classList.add('visible');
        }, i * 80);
        observador.unobserve(entrada.target);
      }
    });
  },
  { threshold: 0.1 }
);

elementosAnimables.forEach(el => observador.observe(el));


// ---- 9. ARRANQUE ----
// Calculamos con los valores iniciales al cargar la página

actualizar();