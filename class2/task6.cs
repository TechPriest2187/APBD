int calculateMin(int[] values)
{
    int min = values[0];
    for (int i = 1; i < values.Length; i++)
    {
        min = Math.Min(min, values[i]);
    }
    return min;
}