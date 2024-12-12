using System;

namespace SportissimoProject.Commands
{
    public class CreateReservationCommand
    {
        public string ClientId { get; set; }
        public string TerrainId { get; set; }
        public DateTime DateReservation { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
    }



}
