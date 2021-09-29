using System;
using Xunit;
using DAL;
using Persistance;

namespace DALTest
{
    public class StaffDalTest
    {
        private StaffDal uDal = new StaffDal();
        [Fact]
        public void LoginTest1()
        {
            string userName = "staff01";
            string userPass = "abcd1234";
            User result = uDal.Login(userName, userPass);
            Assert.True(result != null);
            Assert.True(result.UserAccount.Equals(userName));
            Assert.True(result.Role == 1);
        }

        [Theory]
        [InlineData("pf111", "pf11VTCAcademy")]
        [InlineData("pf11", "pf11VTCAcademy1")]
        [InlineData("asdfg", "pf11VTCAcademy")]
        [InlineData("pf11", "sdfgsdhfb")]
        public void LoginTest2(string userName, string userPass)
        {
            User result = uDal.Login(userName, userPass);
            Assert.True(result == null);
        }
    }
}
