using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RequestDispatcher.Test.RequestDispatcher
{
    [TestClass]
    public class Constructor
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsWhenCalledWithNullForHttpClient()
        {
            new BaseRequestDispatcher(null);
        }
    }
}
