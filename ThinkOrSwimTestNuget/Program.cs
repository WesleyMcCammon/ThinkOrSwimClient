
Console.WriteLine("Hello, World!");

bool runLiveTOS = true;

var thinkOrSwimTest = new ThinkOrSwimTest();

if (runLiveTOS)
{
    thinkOrSwimTest.Start(false);
}
else
    thinkOrSwimTest.TestDataSource();


Console.ReadKey();