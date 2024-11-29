using Bogus;
using TaskManagement.Communication.Enums;
using TaskManagement.Communication.Requests;

namespace CommonTestUtilities.Requests;
public static class RequestTaskJsonBuilder
{
    public static RequestTaskJson Build()
    {
        return new Faker<RequestTaskJson>()
            .RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(r => r.Date, faker => faker.Date.Past())
            .RuleFor(r => r.Type, faker => faker.PickRandom<TaskType>());
    }
}
