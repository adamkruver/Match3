using Match3.Domain;

namespace Sources.InfrastructureInterfaces.Services
{
    public interface ITableService
    {
        void Fill();
        void DropDown();
        bool TryGetMatches(out Cell[] matchedCells);
    }
}