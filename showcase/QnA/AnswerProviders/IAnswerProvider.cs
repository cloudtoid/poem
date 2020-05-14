using System.Threading.Tasks;

namespace QnA
{
    public interface IAnswerProvider
    {
        Task<PotentialAnswer?> AskAsync(string question);
    }
}
