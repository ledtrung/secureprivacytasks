namespace task2;

public class BinaryStringEvaluator
{
    private int[] binaryContainers = new int[2] { 0, 0 };

    public bool IsGood(string binaryString)
    {
        for (int i = 0; i < binaryString.Length; i++)
        {
            if (int.TryParse(binaryString[i].ToString(), out int currentNum) && (currentNum == 0 || currentNum == 1))
            {
                binaryContainers[currentNum]++;
                if (binaryContainers[0] <= binaryContainers[1])
                    continue;
            }

            return false;
        }

        return binaryContainers[0] == binaryContainers[1];
    }
}