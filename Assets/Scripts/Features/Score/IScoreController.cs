namespace Assets.Scripts.Features.Score
{
    public interface IScoreController
    {
        float GetScoreDelta();

        void SetScore(float score);
        void UpdateScore();
    }
}