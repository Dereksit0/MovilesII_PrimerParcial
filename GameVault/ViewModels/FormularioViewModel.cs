using System.Globalization;
using System.Windows.Input;
using GameVault.Data;
using GameVault.Models;

namespace GameVault.ViewModels;

/// <summary>
/// ViewModel del formulario, que se usa para las dos operaciones.
///
/// Si Shell no manda parámetros, la pantalla trabaja en modo agregar; si llega
/// "formulario?id=7", se carga ese juego y pasa a modo editar. Toda la pantalla
/// (título, texto del botón, validaciones) se deriva de ese único interruptor,
/// así que no hay dos vistas ni dos ViewModels que mantener.
/// </summary>
public class FormularioViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IVideojuegoRepository _repositorio;

    private int _juegoId;
    private bool _yaCargado;
    private bool _esEdicion;

    private string _titulo = string.Empty;
    private string? _plataforma;
    private string? _genero;
    private string? _estado;
    private string _valorTexto = string.Empty;
    private string _imagenUrl = string.Empty;
    private bool _esFavorito;
    private bool _completado;

    public FormularioViewModel(IVideojuegoRepository? repositorio = null)
    {
        _repositorio = repositorio ?? new VideojuegoRepository();

        Plataformas = _repositorio.GetPlataformas();
        Generos = _repositorio.GetGeneros();
        Estados = _repositorio.GetEstados();

        GuardarCommand = new Command(async () => await GuardarAsync());
        CancelarCommand = new Command(async () => await CancelarAsync());

        ActualizarTextosDeModo();
    }

    // Catálogos de los Pickers.
    public IReadOnlyList<string> Plataformas { get; }
    public IReadOnlyList<string> Generos { get; }
    public IReadOnlyList<string> Estados { get; }

    public ICommand GuardarCommand { get; }
    public ICommand CancelarCommand { get; }

    public bool EsEdicion
    {
        get => _esEdicion;
        private set
        {
            if (SetProperty(ref _esEdicion, value))
            {
                ActualizarTextosDeModo();
            }
        }
    }

    /// <summary>Texto del botón principal, distinto en cada modo.</summary>
    public string TextoBotonGuardar => EsEdicion ? "Guardar cambios" : "Agregar a la coleccion";

    public string Titulo
    {
        get => _titulo;
        set => SetProperty(ref _titulo, value);
    }

    public string? Plataforma
    {
        get => _plataforma;
        set => SetProperty(ref _plataforma, value);
    }

    public string? Genero
    {
        get => _genero;
        set => SetProperty(ref _genero, value);
    }

    public string? Estado
    {
        get => _estado;
        set => SetProperty(ref _estado, value);
    }

    /// <summary>
    /// El valor se maneja como texto porque un Entry entrega texto: así se puede
    /// validar y avisar al usuario en vez de tragarse un error de conversión.
    /// </summary>
    public string ValorTexto
    {
        get => _valorTexto;
        set => SetProperty(ref _valorTexto, value);
    }

    public string ImagenUrl
    {
        get => _imagenUrl;
        set
        {
            if (SetProperty(ref _imagenUrl, value))
            {
                OnPropertyChanged(nameof(HayVistaPrevia));
            }
        }
    }

    public bool HayVistaPrevia => !string.IsNullOrWhiteSpace(ImagenUrl);

    public bool EsFavorito
    {
        get => _esFavorito;
        set => SetProperty(ref _esFavorito, value);
    }

    public bool Completado
    {
        get => _completado;
        set => SetProperty(ref _completado, value);
    }

    /// <summary>
    /// Shell entrega aquí lo que venga en la query string. Sin parámetro "id" la
    /// pantalla queda en modo agregar.
    /// </summary>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(AppRoutes.ParametroId, out var valor) &&
            int.TryParse(Convert.ToString(valor), out var id) &&
            id > 0)
        {
            _juegoId = id;
            EsEdicion = true;
        }
        else
        {
            _juegoId = 0;
            EsEdicion = false;
        }

        // Los parámetros pueden cambiar entre navegaciones (editar un juego y luego
        // agregar otro reutilizan la misma página), así que se fuerza la recarga.
        _yaCargado = false;
    }

    /// <summary>
    /// Solo carga la primera vez tras cada navegación: si el usuario vuelve a la
    /// pantalla (por ejemplo tras minimizar la app) no se le borra lo que escribió.
    /// </summary>
    public override async Task OnAppearingAsync()
    {
        if (_yaCargado)
        {
            return;
        }

        _yaCargado = true;

        if (!EsEdicion)
        {
            LimpiarFormulario();
            return;
        }

        await EjecutarAsync(async () =>
        {
            var juego = await _repositorio.GetByIdAsync(_juegoId);

            if (juego is null)
            {
                MensajeError = "No se encontro el juego que se queria editar.";
                EsEdicion = false;
                _juegoId = 0;
                return;
            }

            Titulo = juego.Titulo;
            Plataforma = juego.Plataforma;
            Genero = juego.Genero;
            Estado = juego.Estado;
            ValorTexto = juego.ValorEstimado.ToString("0.##", CultureInfo.InvariantCulture);
            ImagenUrl = juego.ImagenUrl;
            EsFavorito = juego.EsFavorito;
            Completado = juego.Completado;
        },
        "No se pudo cargar el juego");
    }

    private Task GuardarAsync() => EjecutarAsync(async () =>
    {
        if (!TryValidar(out var valorEstimado))
        {
            return;
        }

        var juego = new Videojuego
        {
            // Id 0 hace que el repositorio inserte; cualquier otro valor actualiza.
            Id = _juegoId,
            Titulo = Titulo.Trim(),
            Plataforma = Plataforma!,
            Genero = Genero!,
            Estado = Estado!,
            ValorEstimado = valorEstimado,
            ImagenUrl = ImagenUrl.Trim(),
            EsFavorito = EsFavorito,
            Completado = Completado
        };

        await _repositorio.GuardarAsync(juego);

        // ".." saca la pagina de la pila y devuelve a la pestana desde la que se
        // abrio; su OnAppearing recarga la lista y el juego ya aparece.
        await Shell.Current.GoToAsync("..");
    },
    "No se pudo guardar el juego");

    private static Task CancelarAsync() => Shell.Current.GoToAsync("..");

    /// <summary>
    /// Validación de la Fase 1: campos obligatorios y valor numérico. Se acepta punto
    /// o coma decimal porque el teclado numérico cambia según el idioma del equipo.
    /// </summary>
    private bool TryValidar(out decimal valorEstimado)
    {
        valorEstimado = 0m;

        if (string.IsNullOrWhiteSpace(Titulo))
        {
            MensajeError = "El titulo es obligatorio.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Plataforma))
        {
            MensajeError = "Elige una plataforma.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Genero))
        {
            MensajeError = "Elige un genero.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Estado))
        {
            MensajeError = "Elige el estado dentro de la coleccion.";
            return false;
        }

        var texto = (ValorTexto ?? string.Empty).Trim().Replace(',', '.');

        if (!decimal.TryParse(texto, NumberStyles.Number, CultureInfo.InvariantCulture, out valorEstimado) ||
            valorEstimado < 0m)
        {
            MensajeError = "El valor estimado debe ser un numero mayor o igual a cero.";
            return false;
        }

        MensajeError = null;
        return true;
    }

    private void LimpiarFormulario()
    {
        Titulo = string.Empty;
        Plataforma = null;
        Genero = null;
        Estado = null;
        ValorTexto = string.Empty;
        ImagenUrl = string.Empty;
        EsFavorito = false;
        Completado = false;
        MensajeError = null;
    }

    private void ActualizarTextosDeModo()
    {
        TituloPantalla = EsEdicion ? "Editar juego" : "Nuevo juego";
        OnPropertyChanged(nameof(TextoBotonGuardar));
    }
}
