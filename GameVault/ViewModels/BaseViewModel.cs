using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GameVault.ViewModels;

/// <summary>
/// Clase base de todos los ViewModels: implementa INotifyPropertyChanged y concentra
/// el estado que se repite en cada pantalla (ocupado / mensaje de error).
///
/// Los miembros de infraestructura se llaman igual que en
/// <c>CommunityToolkit.Mvvm.ComponentModel.ObservableObject</c> (SetProperty,
/// OnPropertyChanged, IsBusy). Así, si más adelante se decide usar el toolkit,
/// basta con cambiar la herencia de esta clase y ningún ViewModel se rompe.
/// El vocabulario del dominio, en cambio, sí va en español.
/// </summary>
public abstract class BaseViewModel : INotifyPropertyChanged
{
    private bool _isBusy;
    private string _tituloPantalla = string.Empty;
    private string? _mensajeError;

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>Indica que hay una operación en curso (carga de datos, guardado...).</summary>
    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            if (SetProperty(ref _isBusy, value))
            {
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }
    }

    /// <summary>Inverso de <see cref="IsBusy"/>, para deshabilitar controles desde XAML.</summary>
    public bool IsNotBusy => !IsBusy;

    /// <summary>Título que la vista muestra en su encabezado.</summary>
    public string TituloPantalla
    {
        get => _tituloPantalla;
        set => SetProperty(ref _tituloPantalla, value);
    }

    /// <summary>
    /// Mensaje de error visible para el usuario. En la Fase 4 aquí aterrizan los
    /// errores de red y de deserialización de la API.
    /// </summary>
    public string? MensajeError
    {
        get => _mensajeError;
        set
        {
            if (SetProperty(ref _mensajeError, value))
            {
                OnPropertyChanged(nameof(TieneError));
            }
        }
    }

    public bool TieneError => !string.IsNullOrWhiteSpace(MensajeError);

    /// <summary>
    /// Gancho de ciclo de vida. Cada página lo invoca desde su OnAppearing, de modo
    /// que la pantalla se refresca cada vez que se muestra (por ejemplo al volver
    /// del formulario después de guardar).
    /// </summary>
    public virtual Task OnAppearingAsync() => Task.CompletedTask;

    /// <summary>
    /// Ejecuta una operación asíncrona controlando IsBusy, reentradas y excepciones.
    /// Centralizar el try/catch aquí es lo que va a permitir manejar los errores de
    /// HttpClient en la Fase 4 sin repetir código en cada ViewModel.
    /// </summary>
    protected async Task EjecutarAsync(Func<Task> operacion, string mensajeSiFalla)
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            IsBusy = true;
            MensajeError = null;
            await operacion();
        }
        catch (Exception ex)
        {
            MensajeError = $"{mensajeSiFalla} ({ex.Message})";
        }
        finally
        {
            IsBusy = false;
        }
    }

    protected bool SetProperty<T>(ref T campo, T valor, [CallerMemberName] string? nombrePropiedad = null)
    {
        if (EqualityComparer<T>.Default.Equals(campo, valor))
        {
            return false;
        }

        campo = valor;
        OnPropertyChanged(nombrePropiedad);
        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string? nombrePropiedad = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombrePropiedad));
}
