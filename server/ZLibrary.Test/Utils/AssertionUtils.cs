using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ZLibrary.Test.Utils
{
    public static class AssertionExtensions
    {
        public static void AssertListEquals<T>(this IList<T> a, IList<T> b)
        {
            Assert.AreEqual(a.Count(), b.Count(), "The size of lists do not match");

            Assert.IsTrue(a.SequenceEqual(b));
        }
    }
}