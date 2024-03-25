using GymNexus.Core.Models;

namespace GymNexus.Core.Contracts;

public interface INomenclatureService
{
    Task<IEnumerable<CategoryDto>> GetProductCategoriesAsync();

}