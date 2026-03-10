double calculateAverage(int[] numbers)
{
    Console.WriteLine("Calculating the average of the following numbers: " + string.Join(", ", numbers));
    int sum = 0;
    foreach (int number in numbers)
    {
        sum += number;
    }
    double average = (double)sum / numbers.Length;
    Console.WriteLine("The average is: " + average);
    return average;
}

calculateAverage(new int[] { 1, 2, 3, 4, 5 });
calculateAverage(new int[] { 10, 20, 30 });