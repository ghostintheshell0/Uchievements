namespace Uchievements
{
    public struct Reward
    {
        public int[] Values;

        public Reward(params int[] values)
        {
            Values = values;
        }
    }
}