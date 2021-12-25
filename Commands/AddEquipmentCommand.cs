namespace ExosHeroesManager.Commands;

internal class AddEquipmentCommand : ICommand
{
    private CustomControls.EquipmentPanel EquipmentPanel;

    public AddEquipmentCommand(CustomControls.EquipmentPanel panel)
    {
        EquipmentPanel = panel;
    }


    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object? parameter) => EquipmentPanel.EquipmentPiece is PendingEquipmentPiece piece && piece.IsValid && piece.MainOption.IsValid && piece.Option1.IsValid && piece.Option2.IsValid && piece.Option3.IsValid && piece.Option4.IsValid; 
    public void Execute(object? parameter) => ((PendingEquipmentPiece)EquipmentPanel.EquipmentPiece).Upload();
}
