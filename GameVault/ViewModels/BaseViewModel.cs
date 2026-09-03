using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GameVault.ViewModels;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    private bool _isBusy;
    private string _tituloPantalla = string.Empty;
    private string? _mensajeError;

    public event PropertyChangedEventHandler? PropertyChanged;

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

    public bool IsNotBusy => !IsBusy;

    public string TituloPantalla
    {
        get => _tituloPantalla;
        set => SetProperty(ref _tituloPantalla, value);
    }

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

    public virtual Task OnAppearingAsync() => Task.CompletedTask;

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
