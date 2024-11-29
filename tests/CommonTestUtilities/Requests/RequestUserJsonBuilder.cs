using Bogus;
using Bogus.DataSets;
using TaskManagement.Communication.Requests;

namespace CommonTestUtilities.Requests;
public static class RequestUserJsonBuilder
{
    public static RequestUserJson Build()
    {
        return new Faker<RequestUserJson>()
            .RuleFor(user => user.Name, faker => faker.Name.FindName())
            .RuleFor(user => user.Email, faker => faker.Internet.Email())
            .RuleFor(user => user.Password, faker => faker.Internet.Password())
            .RuleFor(user => user.CreatedAt, faker => faker.Date.Soon());
    }
}
