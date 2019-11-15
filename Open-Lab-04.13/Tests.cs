using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Open_Lab_04._13
{
    [TestFixture]
    public class Tests
    {

        private StringTools tools;
        private bool shouldStop;

        private const int RandStrMinSize = 100;
        private const int RandStrMaxSize = 500;
        private const int RandSeed = 413413413;
        private const int RandTestCasesCount = 98;

        [OneTimeSetUp]
        public void Init()
        {
            tools = new StringTools();
            shouldStop = false;
        }

        [TearDown]
        public void TearDown()
        {
            var outcome = TestContext.CurrentContext.Result.Outcome;

            if (outcome == ResultState.Failure || outcome == ResultState.Error)
                shouldStop = true;
        }

        [TestCase("league", "legends", new[] { "le" })]
        [TestCase("mojstryko", "cstrike", new[] {"str"})]
        public void GetLongestCommonSequenceTest(string str1, string str2, string[] expected)
        {
            var constrain = Is.EqualTo(expected.Length > 0 ? expected[0] : "");

            for (var i = 1; i < expected.Length; i++)
                constrain = constrain.Or.EqualTo(expected[i]);

            Assert.That(tools.GetLongestCommonSequence(str1, str2), constrain);
        }

        [TestCaseSource(nameof(GetRandom))]
        public void GetLongestCommonSequenceTestRandom(string str1, string str2, string[] expected)
        {
            if (shouldStop)
                Assert.Ignore("Previous test failed!");

            GetLongestCommonSequenceTest(str1, str2, expected);
        }

        private static IEnumerable GetRandom()
        {
            var rand = new Random(RandSeed);

            for (var i = 0; i < RandTestCasesCount; i++)
            {
                var arrs = new char[2][];

                for (var j = 0; j < 2; j++)
                {
                    arrs[j] = new char[rand.Next(RandStrMinSize, RandStrMaxSize + 1)];

                    for (var k = 0; k < arrs[j].Length; k++)
                        arrs[j][k] = (char) rand.Next('a', 'z' + 1);
                }

                var list = new List<string>();
                var builder = new StringBuilder();

                for (var j = 0; j < arrs[0].Length; j++)
                {
                    for (var k = 0; k < arrs[1].Length; k++)
                    {
                        for (var l = k; l < arrs[1].Length; l++)
                            if (arrs[0].Length > j + l - k && arrs[0][j + l - k] == arrs[1][l])
                                builder.Append(arrs[1][l]);
                            else
                                break;

                        if (builder.Length <= 0)
                            continue;

                        if (list.Count == 0)
                            list.Add(builder.ToString());
                        else if (list[0].Length <= builder.Length)
                        {
                            if (list[0].Length < builder.Length)
                                list.Clear();

                            list.Add(builder.ToString());
                        }

                        builder.Clear();
                    }
                }

                yield return new TestCaseData(new string(arrs[0]), new string(arrs[1]), list.Distinct().ToArray());
            }
        }

    }
}
