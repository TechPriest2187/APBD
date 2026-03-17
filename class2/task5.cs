int calculateMax(int[] values)
{
    int max = values[0];
    for (int i = 1; i < values.Length; i++)
    {
        max = Math.Max(max, values[i]);
    }
    return max;
}