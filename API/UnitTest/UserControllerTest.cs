namespace API.UnitTest
{
    [TestFixture]
    [Order(4)]
    public class UserControllerTest
    {
        [Test]

        [Order(2)]

        public async Task TestGetAllUsers()

        {
            int i = 0;
            string result = await UnitTestHelper.TryCatchAsync(async () =>
            {
                i++;
                return await UnitTestHelper.GetAsync("/user/all");

            });

            Console.WriteLine("Users: " + result);

            Assert.That(result, Does.Not.Empty);

        }

    }
}
