using FraudDetection.Shared.DTOs;
using System.Threading.Tasks;

namespace FraudDetection.Shared.Contracts
{
    public interface IFraudService
    {
        Task<FraudResultDto?> ScoreAsync(TransactionDto transaction);
        Task<ExplainResultDto?> ExplainAsync(TransactionDto transaction);
    }
}