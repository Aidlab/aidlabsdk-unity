public static class AverageCalc
{
    public static float GetAverage(float[] values, int size)
    {
        float average = 0.0f;
        for (int i = 0; i < size; i++)
            average += values[i];
        return average /= (float)size;
    }

    public static int GetAverage(int[] values, int size)
    {
        int average = 0;
        for (int i = 0; i < size; i++)
            average += values[i];
        return (int)((float)average / (float)size);
    }
}
