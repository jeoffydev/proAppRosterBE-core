


namespace AppTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var test1 = AddTest.Add(4, 4);
        test1.Equals(4);
    }
}

public class AddTest
{
    public static int Add(int pOne, int pTwo) => (pOne + pTwo);
}