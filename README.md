[![Build Status](https://dev.azure.com/presentdotnet/Present/_apis/build/status/Build%20(including%20SonarCloud)?branchName=develop)](https://dev.azure.com/presentdotnet/Present/_build/latest?definitionId=2&branchName=develop) [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=ImAMightyPirate_Present&metric=alert_status)](https://sonarcloud.io/dashboard?id=ImAMightyPirate_Present) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=ImAMightyPirate_Present&metric=coverage)](https://sonarcloud.io/dashboard?id=ImAMightyPirate_Present) [![CodeFactor](https://www.codefactor.io/repository/github/imamightypirate/present/badge)](https://www.codefactor.io/repository/github/imamightypirate/present)

# ![presenttextonly](https://user-images.githubusercontent.com/5412869/53685427-70a3cf00-3d12-11e9-9d1c-affafe802ae7.png)

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
