using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Extensions
{
    public static class AssertExt
    {
        public static void AssertException<ExceptionType>(Action action, string exMessage = null) where ExceptionType : Exception
        {
            var exThrowed = false;
            try
            {
                action();
            }
            catch (ExceptionType ex)
            {
                exThrowed = true;

                if (exMessage != null)
                {
                    Assert.AreEqual(exMessage, ex.Message);
                }
            }
            Assert.IsTrue(exThrowed, "The Exception was not throwed.");
        }
    }
}
