namespace ExosHeroesManager.Commands;

internal class IncrementTranscendLevelCommand : ICommand
{
    private readonly HeroViewModel ViewModel;

    public IncrementTranscendLevelCommand(HeroViewModel viewmodel)
    {
        ViewModel = viewmodel;
    }


    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object? parameter) => ViewModel.SelectedHero.TransendLevel < 5;
    public void Execute(object? parameter) => ViewModel.SelectedHero.TransendLevel++;
}
