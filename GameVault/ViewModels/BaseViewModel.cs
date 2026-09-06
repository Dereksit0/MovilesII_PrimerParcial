using CommunityToolkit.Mvvm.ComponentModel;

namespace GameVault.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    protected BaseViewModel()
    {
        TituloPantalla = string.Empty;
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    [NotifyPropertyChangedFor(nameof(MostrarContenido))]
    public partial bool IsBusy { get; set; }

    [ObservableProperty]
    public partial string TituloPantalla { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TieneError))]
    [NotifyPropertyChangedFor(nameof(MostrarContenido))]
    public partial string? MensajeError { get; set; }

    public bool IsNotBusy => !IsBusy;

    public bool TieneError => !string.IsNullOrWhiteSpace(MensajeError);

    public bool MostrarContenido => !IsBusy && !TieneError;
}
