global using System.Windows.Input;

namespace ExosHeroesManager.Commands;

internal class ToggleFatecoreCommand : ICommand
{
    private readonly HeroViewModel ViewModel;

    public ToggleFatecoreCommand(HeroViewModel viewmodel)
    {
        ViewModel = viewmodel;
    }

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object? parameter) => ViewModel.SelectedHero.AvailableFatecores.Length > 0;

    public void Execute(object? parameter)
    {
        Hero hero = ViewModel.SelectedHero;

        if (hero.AvailableFatecores.Length == 0)
        {
            return;
        }

        List<string> availableFCNames = hero.AvailableFatecores.ToList();
        int currentIndex = availableFCNames.IndexOf(hero.Fatecore?.Name ?? string.Empty);

        hero.Fatecore = currentIndex == availableFCNames.Count - 1 ? null : (new(availableFCNames[currentIndex + 1]));
    }
}
