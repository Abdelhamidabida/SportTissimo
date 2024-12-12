using SportissimoProject.Models;
using System.Threading.Tasks;

namespace SportissimoProject.Commands
{
    public interface IUpdateReservationCommand
    {
        Task<Reservation> ExecuteAsync(UpdateReservationCommand command);
    }

}
