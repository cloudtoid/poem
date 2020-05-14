namespace QnA
{
    public class PotentialAnswer
    {
        public PotentialAnswer(double confidenceScore, string domain, string answer)
        {
            ConfidenceScore = confidenceScore;
            Domain = domain;
            Answer = answer;
        }

        public double ConfidenceScore { get; }

        public string Domain { get; }

        public string Answer { get; }
    }
}
