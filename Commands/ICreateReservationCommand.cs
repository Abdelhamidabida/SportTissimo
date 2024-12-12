using SportissimoProject.Models;
using System.Threading.Tasks;

namespace SportissimoProject.Commands
{
    public interface ICreateReservationCommand
    {
        Task<Reservation> ExecuteAsync(CreateReservationCommand command);
    }

}
