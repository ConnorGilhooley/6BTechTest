using Microsoft.EntityFrameworkCore;
using SixBeeHealthTech.Components.Models;
using System.Threading;
using System.Threading.Tasks;

namespace SixBeeHealthTech.Components.DBContext
{
    public interface IApplicationDbContext
    {
        DbSet<Appointment> Appointments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
