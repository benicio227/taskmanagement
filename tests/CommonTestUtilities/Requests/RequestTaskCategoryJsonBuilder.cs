using Bogus;
using TaskManagement.Communication.Requests;

namespace CommonTestUtilities.Requests;
public static class RequestTaskCategoryJsonBuilder
{
    public static RequestCategoryJson Build()
    {
        return new Faker<RequestCategoryJson>()
            .RuleFor(c => c.Name, faker => faker.Commerce.ProductName())
            .RuleFor(c => c.Description, faker => faker.Commerce.ProductDescription());
    }
}
