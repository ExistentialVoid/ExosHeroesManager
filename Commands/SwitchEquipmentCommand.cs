namespace ExosHeroesManager.Commands;

internal class SwitchEquipmentCommand : ICommand
{
    private CustomControls.EquipmentPanel EquipmentPanel;

    public SwitchEquipmentCommand(CustomControls.EquipmentPanel panel)
    {
        EquipmentPanel = panel;
    }


    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object? parameter) => true;
    public void Execute(object? parameter)
    {
        // Make a form to select from available equipment
    }
}
