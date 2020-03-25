﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if NETCOREAPP3_0
using System;
#endif
using System.Runtime.CompilerServices;
using Microsoft.Toolkit.HighPerformance.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.HighPerformance.Extensions
{
    public partial class Test_ArrayExtensions
    {
        [TestCategory("ArrayExtensions")]
        [TestMethod]
        public void Test_ArrayExtensions_2D_DangerousGetReference_Int()
        {
            int[,] array =
            {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 10, 11, 12 }
            };

            ref int r0 = ref array.DangerousGetReference();
            ref int r1 = ref array[0, 0];

            Assert.IsTrue(Unsafe.AreSame(ref r0, ref r1));
        }

        [TestCategory("ArrayExtensions")]
        [TestMethod]
        public void Test_ArrayExtensions_2D_DangerousGetReference_String()
        {
            string[,] array =
            {
                { "a", "bb", "ccc" },
                { "dddd", "eeeee", "ffffff" }
            };

            ref string r0 = ref array.DangerousGetReference();
            ref string r1 = ref array[0, 0];

            Assert.IsTrue(Unsafe.AreSame(ref r0, ref r1));
        }

        [TestCategory("ArrayExtensions")]
        [TestMethod]
        public void Test_ArrayExtensions_2D_DangerousGetReferenceAt_Zero()
        {
            int[,] array =
            {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 10, 11, 12 }
            };

            ref int r0 = ref array.DangerousGetReferenceAt(0, 0);
            ref int r1 = ref array[0, 0];

            Assert.IsTrue(Unsafe.AreSame(ref r0, ref r1));
        }

        [TestCategory("ArrayExtensions")]
        [TestMethod]
        public void Test_ArrayExtensions_2D_DangerousGetReferenceAt_Index()
        {
            int[,] array =
            {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 10, 11, 12 }
            };

            ref int r0 = ref array.DangerousGetReferenceAt(1, 3);
            ref int r1 = ref array[1, 3];

            Assert.IsTrue(Unsafe.AreSame(ref r0, ref r1));
        }

        [TestCategory("ArrayExtensions")]
        [TestMethod]
        public void Test_ArrayExtensions_2D_FillArrayMid()
        {
            bool[,] test = new bool[4, 5];

            test.Fill(true, 1, 1, 3, 2);

            var expected = new[,]
            {
                { false, false, false, false, false },
                { false,  true,  true,  true, false },
                { false,  true,  true,  true, false },
                { false, false, false, false, false },
            };

            CollectionAssert.AreEqual(expected, test);
        }

        [TestCategory("ArrayExtensions")]
        [TestMethod]
        public void Test_ArrayExtensions_2D_FillArrayTwice()
        {
            bool[,] test = new bool[4, 5];

            test.Fill(true, 0, 0, 1, 2);
            test.Fill(true, 1, 3, 2, 2);

            var expected = new[,]
            {
                { true,  false, false, false, false },
                { true,  false, false,  true,  true },
                { false, false, false,  true,  true },
                { false, false, false, false, false },
            };

            CollectionAssert.AreEqual(expected, test);
        }

        [TestCategory("ArrayExtensions")]
        [TestMethod]
        public void Test_ArrayExtensions_2D_FillArrayNegativeSize()
        {
            bool[,] test = new bool[4, 5];

            test.Fill(true, 3, 4, -3, -2);

            var expected = new[,]
            {
                { false, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false },
            };

            CollectionAssert.AreEqual(expected, test);
        }

        [TestCategory("ArrayExtensions")]
        [TestMethod]
        public void Test_ArrayExtensions_2D_FillArrayBottomEdgeBoundary()
        {
            bool[,] test = new bool[4, 5];

            test.Fill(true, 1, 2, 2, 4);

            var expected = new[,]
            {
                { false, false, false, false, false },
                { false, false,  true,  true, false },
                { false, false,  true,  true, false },
                { false, false,  true,  true, false },
            };

            CollectionAssert.AreEqual(expected, test);
        }

        [TestCategory("ArrayExtensions")]
        [TestMethod]
        public void Test_ArrayExtensions_2D_FillArrayTopLeftCornerNegativeBoundary()
        {
            bool[,] test = new bool[4, 5];

            test.Fill(true, -1, -1, 3, 3);

            var expected = new[,]
            {
                { true,   true, false, false, false },
                { true,   true, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false },
            };

            CollectionAssert.AreEqual(expected, test);
        }

        [TestCategory("ArrayExtensions")]
        [TestMethod]
        public void Test_ArrayExtensions_2D_FillArrayBottomRightCornerBoundary()
        {
            bool[,] test = new bool[5, 4];

            test.Fill(true, 3, 2, 3, 3);

            var expected = new[,]
            {
                { false, false, false, false },
                { false, false, false, false },
                { false, false, false, false },
                { false, false,  true,  true },
                { false, false,  true,  true },
            };

            CollectionAssert.AreEqual(expected, test);
        }

#if NETCOREAPP3_0
        [TestCategory("ArrayExtensions")]
        [TestMethod]
        public void Test_ArrayExtensions_2D_AsSpan_Empty()
        {
            int[,] array = new int[0, 0];

            Span<int> span = array.AsSpan();

            Assert.AreEqual(span.Length, array.Length);
            Assert.IsTrue(span.IsEmpty);
        }

        [TestCategory("ArrayExtensions")]
        [TestMethod]
        public void Test_ArrayExtensions_2D_AsSpan_Populated()
        {
            int[,] array =
            {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 10, 11, 12 }
            };

            Span<int> span = array.AsSpan();

            Assert.AreEqual(span.Length, array.Length);

            ref int r0 = ref array[0, 0];
            ref int r1 = ref span[0];

            Assert.IsTrue(Unsafe.AreSame(ref r0, ref r1));
        }
#endif
    }
}
