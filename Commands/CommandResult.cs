namespace SportissimoProject.Commands
{
    public class CommandResult
    {
        public bool IsSuccess { get; set; }  // Indique si la commande a réussi
        public string ErrorMessage { get; set; }  // Message d'erreur en cas d'échec
        public Reservation Reservation { get; set; }  // La réservation (si applicable)

        public CommandResult()
        {
            // Initialisation par défaut
            IsSuccess = false;
            ErrorMessage = string.Empty;
            Reservation = null;
        }
    }
}
