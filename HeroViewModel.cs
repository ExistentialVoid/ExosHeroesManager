using System.Runtime.CompilerServices;

namespace ExosHeroesManager;

internal class HeroViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new(propertyName));
    }

    public List<string> AvailableHeroes
    {
        get => availableHeroes;
        set
        {
            availableHeroes = value;
            NotifyPropertyChanged();
        }
    }
    public Commands.IncrementLevelCommand CommandIncrementLevel
    {
        get => commandIncrementLv;
        set
        {
            commandIncrementLv = value;
            NotifyPropertyChanged();
        }
    }
    public Commands.IncrementTranscendLevelCommand CommandIncrementTranscendLevel
    {
        get => commandIncrementTranscendLv;
        set
        {
            commandIncrementTranscendLv = value;
            NotifyPropertyChanged();
        }
    }
    public Commands.ToggleFatecoreCommand CommandToggleFatecore
    {
        get => commandToggleFC;
        set
        {
            commandToggleFC = value;
            NotifyPropertyChanged();
        }
    }
    public Hero SelectedHero
    {
        get => selectedHero;
        private set
        {
            selectedHero = value;
            NotifyPropertyChanged();
        }
    }
    public string SelectedHeroName
    {
        get => selectedHeroName;
        set
        {
            selectedHeroName = value;
            NotifyPropertyChanged();
            SelectedHero = new(value);
        }
    }

    private List<string> availableHeroes;
    private Commands.IncrementLevelCommand commandIncrementLv;
    private Commands.IncrementTranscendLevelCommand commandIncrementTranscendLv;
    private Commands.ToggleFatecoreCommand commandToggleFC;
    private Hero selectedHero;
    private string selectedHeroName;


    public HeroViewModel()
    {
        HeroesAccessor heroesAccessor = new();
        AvailableHeroes = heroesAccessor.GetAllNames();

        SelectedHero = new("Bathory");

        commandIncrementLv = new(this);
        commandIncrementTranscendLv = new(this);
        commandToggleFC = new(this);
    }
}
