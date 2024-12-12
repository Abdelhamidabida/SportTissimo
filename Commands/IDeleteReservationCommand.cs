using SportissimoProject.Commands.SportissimoProject.Commands;
using System.Threading.Tasks;

namespace SportissimoProject.Commands
{
    public interface IDeleteReservationCommand
    {
        Task<bool> ExecuteAsync(DeleteReservationCommand command);
    }

}
