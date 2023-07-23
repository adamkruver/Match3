using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Match3.Domain;

namespace Sources.InfrastructureInterfaces.Services
{
    public interface ITableService
    {
        List<Cell> Fill();
        void DropDown();
        UniTask<bool> TryGetMatches();
    }
}