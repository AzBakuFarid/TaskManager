using TaskManager.Domain.Entites;

namespace TaskManager.Domain.DataAccessAbstractions
{
    public interface IOrganizationRepository : IBaseRepository
    {
        Organization FindByName(string name, params string[] relations);
    }
}
