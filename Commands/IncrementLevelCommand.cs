namespace ExosHeroesManager.Commands;

internal class IncrementLevelCommand : ICommand
{
    private readonly HeroViewModel ViewModel;

    public IncrementLevelCommand(HeroViewModel viewmodel)
    {
        ViewModel = viewmodel;
    }


    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object? parameter) => ViewModel.SelectedHero.Level < 100;
    public void Execute(object? parameter) => ViewModel.SelectedHero.Level += 1;
}
