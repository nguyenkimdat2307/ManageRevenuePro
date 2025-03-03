using ManageRevenue.BLL.Common;
using Moq;

namespace ManageRevenue.UnitTest.Mocks
{
    public class SessionInfoMock
    {
        public static Mock<ISessionInfo> GetSessionInfo()
        {
            var mockSession = new Mock<ISessionInfo>();

            mockSession.Setup(s => s.GetUserId()).Returns(1);

            return mockSession;
        }
    }
}
