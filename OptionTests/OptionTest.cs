using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ignitus.Option;

namespace OptionTests
{
    [TestClass]
    public class OptionTest
    {
        public static List<string> dummyData = new List<string> { "this", "is", "a", "list", "of", "dummy", "data" };

        [TestMethod]
        public void SearchForIndexOfItem_ItemNotFound()
        {
            string notFound = "notFound";

            Option<int> option = FindPosition(dummyData, notFound);

            Assert.IsTrue(option is None<int>, $"The returned Option type is not a None: {option}");
        }

        [TestMethod]
        public void SearchForIndexOfItem_ItemFound()
        {
            string found = "data";

            Option<int> option = FindPosition(dummyData, found);

            Assert.IsTrue(option is Some<int>, $"The returned Option type not a Some: {option}");
        }

        [TestMethod]
        public void ValueInSomeIsEqualToExpectedValue()
        {
            string itemToFind = "data";
            const int indexExpected = 6;

            Option<int> result = FindPosition(dummyData, itemToFind);

            if(result is Some<int> validResult)
            {
                int valReturned = validResult.Value;

                Assert.AreEqual(indexExpected, valReturned, $"Values are not the same: {indexExpected}, {valReturned}");
            }

        }

        [TestMethod]
        public void CheckSomeEqualToSomeWithSameValue()
        {
            int value = 10;

            Some<int> val1 = new Some<int>(value);
            Some<int> val2 = new Some<int>(value);

            Assert.AreEqual(val1, val2, $"Options are not equal to each other: {val1}, {val2}");
        }

        [TestMethod]
        public void CheckSomeEqualToSomeWithDifferentValue()
        {
            int value1 = 10;
            int value2 = 20;

            Some<int> val1 = new Some<int>(value1);
            Some<int> val2 = new Some<int>(value2);

            Assert.AreNotEqual(val1, val2, $"Two Some's with different internal values should not be equal: {val1}, {val2}");
        }

        [TestMethod]
        public void TestThatSomesWithDifferentTypesArentEqual()
        {
            Some<int> val1 = new Some<int>(10);
            Some<string> val2 = new Some<string>("test");

            Assert.AreNotEqual(val1, val2, $"Two Some's with different types should not be equal: {val1}, {val2}");
        }

        [TestMethod]
        public void CheckNoneEqualToNone()
        {
            None<int> val1 = new None<int>();
            None<int> val2 = new None<int>();


            Assert.AreEqual(val1, val2, $"The two Nones are not equal: {val1}, {val2}");
        }

        [TestMethod]
        public void TestExtractionOfReferenceTypesInSomeWithSameObject()
        {
            Some<List<string>> list = new Some<List<string>>(dummyData);

            List<string> value = list.Value;

            Assert.AreEqual(dummyData, value, "Reference type is not extracted properly from the Some");
        }

        [TestMethod]
        public void TestExtractionOfReferenceTypesInSomeWithDifferentObject()
        {
            Some<List<string>> list = new Some<List<string>>(new List<string> { "test" });

            List<string> value = list.Value;

            Assert.AreNotEqual(dummyData, value, "Reference type is not extracted properly from the Some");
        }

        [TestMethod]
        public void OptionifyReturnsNoneWhenPassedNull()
        {
            Option<string> result = Option<string>.Optionify(null);

            Assert.IsTrue(result is None<string>, "Optionify returned a non-None value when passed in null");
        }

        [TestMethod]
        public void OptionifyReturnsValueWhenPassedAValue()
        {
            const string VALUE = "test";

            Option<string> result = Option<string>.Optionify(VALUE);

            if (result is Some<string> res)
            {
                Assert.AreEqual(VALUE, res.Value, "The value passed into Optionify was not propogated to the returned Some");
            }
            else
            {
                Assert.Fail("Optionify returned something other than Some");
            }
        }

        public static Option<int> FindPosition(List<string> list, string value)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Equals(value))
                {
                    return new Some<int>(i);
                }
            }

            return new None<int>();
        }
    }
}
