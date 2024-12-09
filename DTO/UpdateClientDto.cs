namespace SportissimoProject.DTO
{
    public class UpdateClientDto
    {
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Email { get; set; }
        public string? MotDePasse { get; set; }
        public int? NbReservation { get; set; }
    }
}
