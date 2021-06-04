public class GeneralMathFunctions
{
    public float GetPercentageChangeInValue(float initialVal, float endVal) // Gets the percentage from a diffrence of 2 values
    {
        float perChangeInValue = (endVal / initialVal);
        return perChangeInValue;
    }
        
    public float GetAdditionToExistingAverage(float currentAvg, int currentNumItems, float newValue, int newNumItem)
    {
        float numAvg;
        
        // If the number of items that are being bought or sold is equal to the stock held, set the average to 0 otherwise
        // perform the calculation
        if (currentNumItems + newNumItem == 0) 
        {
            numAvg = 0f;
        }
        else
        {
            numAvg = (currentAvg * currentNumItems + newValue * newNumItem) / (currentNumItems + newNumItem);
        }

        // If the average is negative set the average to 0
        if (numAvg < 0)
        {
            numAvg = 0f;
        }
        return numAvg;
    }
}