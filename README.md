# ![presentwithname](https://user-images.githubusercontent.com/5412869/53291535-94b05f00-37ac-11e9-8754-dbf8cf6a21fe.png)

Present is a toolset for quickly creating lightweight wrappers around existing C# APIs and libraries. These wrappers are designed to help you write code that is easier to maintain and test, while keeping the amount of changes necessary to a minimum.

## The Problem

~~~~
using System.IO;

public class SalesSaver
{
    public void SaveMonthlySales(int month, string[] salesData)
    {
        string salesFolder;
    
        if (month > 6)
        {
            salesFolder = "Second Half Sales";
        }
        else
        {
            salesFolder = "First Half Sales";
        }
        
        var salesFilePath = Path.Combine("C:\Sales", salesFolder, "Sales.txt");
        File.WriteAllLines(salesFilePath, salesData);
    }
}
~~~~

This method would be difficult to test because we are using methods from static classes available in the .NET API. Any unit tests we write would be forced to run the implementation of the `Path.Combine` and `File.WriteAllLines` methods as they cannot be substituted. This would lead to tests that are more complex and more brittle. 

If these were libraries of our own these classes would not be static and they would have interfaces. This would allow us to inject the real implementation in our production code and supply a mock in our unit tests, so we can focus on the behaviour of just this method.

We could write own wrapper to do this, but that would be time consuming and tedious...

## The Solution
