namespace Library.IntegrationTests;

public abstract class IntegrationTestBase : IClassFixture<IntegrationTestFactory>
{
    protected readonly HttpClient Client;

    protected IntegrationTestBase(IntegrationTestFactory factory)
    {
        Client = factory.CreateClient();
    }
}
