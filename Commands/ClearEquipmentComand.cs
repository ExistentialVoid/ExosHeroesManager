namespace ExosHeroesManager.Commands;

internal class ClearEquipmentComand : ICommand
{
    private CustomControls.EquipmentPanel EquipmentPanel;
    private EquipmentPieceBase Piece => EquipmentPanel.EquipmentPiece;

    public ClearEquipmentComand(CustomControls.EquipmentPanel panel)
    {
        EquipmentPanel = panel;
    }


    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object? parameter) => true;
    public void Execute(object? parameter) => EquipmentPanel.EquipmentPiece = new PendingEquipmentPiece(Piece.Type, Piece.EquipedHero);
}
