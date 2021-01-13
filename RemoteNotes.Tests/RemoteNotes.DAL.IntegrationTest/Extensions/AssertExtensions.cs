using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace RemoteNotes.DAL.IntegrationTest.Extensions
{
    /// <summary>
    /// The assert ex.
    /// <remarks> 
    /// The solution was taken from <see href = "http://stackoverflow.com/questions/318210/compare-equality-between-two-objects-in-nunit"/>
    /// </remarks>
    /// </summary>
    public static class AssertExtension
    {
        /// <summary>
        /// The field values are equal.
        /// </summary>
        /// <param name="actual">
        /// The actual.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        public static void FieldValuesAreEqual(this object actual, object expected)
        {
            FieldValuesAreEqual(actual, expected, new List<string>());
        }

        /// <summary>
        /// The field values are equal.
        /// </summary>
        /// <param name="actual">
        /// The actual.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        /// <param name="exceptionCollection">
        /// The exception collection.
        /// </param>
        public static void FieldValuesAreEqual(this object actual, object expected, List<string> exceptionCollection)
        {
            FieldInfo[] properties = expected.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in properties)
            {
                if (exceptionCollection.Contains(field.Name))
                {
                    continue;
                }

                object expectedValue = field.GetValue(expected);
                object actualValue = field.GetValue(actual);

                if (actualValue is IList)
                {
                    AssertListsAreEqual(field, (IList)actualValue, (IList)expectedValue);
                }
                else if (actualValue is DateTime)
                {
                    DateTime actualDateTime = (DateTime)actualValue;
                    DateTime expectedDateTime = (DateTime)expectedValue;

                    if (actualDateTime.Date == expectedDateTime.Date &&
                        actualDateTime.Hour == expectedDateTime.Hour &&
                        actualDateTime.Minute == expectedDateTime.Minute &&
                        actualDateTime.Second == expectedDateTime.Second)
                    {
                    }
                    else
                    {
                        if (!field.Name.Equals("createTime")&& !field.Name.Equals("modifyTime"))
                            Assert.Fail(
                                "Field {0}.{1} does not match. Expected: {2} but was: {3}",
                                field.DeclaringType.Name,
                                field.Name,
                                expectedValue,
                                actualValue);
                    }
                }
                else if (actualValue is decimal)
                {
                    decimal actualDecimal = (decimal)actualValue;
                    decimal expectedDecimal = (decimal)expectedValue;

                    if (actualDecimal == expectedDecimal)
                    { }
                    else
                    {
                        Assert.Fail(
                           "Field {0}.{1} does not match. Expected: {2} but was: {3}",
                           field.DeclaringType.Name,
                           field.Name,
                           expectedValue,
                           actualValue);
                    }
                }
                else if (actualValue != null && !actualValue.GetType().IsPrimitive)
                {
                    FieldValuesAreEqual(actualValue, expectedValue);
                }
                else if (!Equals(expectedValue, actualValue))
                {
                    Assert.Fail(
                        "Field {0}.{1} does not match. Expected: {2} but was: {3}",
                        field.DeclaringType.Name,
                        field.Name,
                        expectedValue,
                        actualValue);
                }
            }
        }

        /// <summary>
        /// The property values are equal.
        /// </summary>
        /// <param name="actual">
        /// The actual.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        public static void PropertyValuesAreEqual(this object actual, object expected)
        {
            PropertyInfo[] properties = expected.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object expectedValue = property.GetValue(expected, null);
                object actualValue = property.GetValue(actual, null);

                if (actualValue is IList)
                {
                    AssertListsAreEqual(property, (IList)actualValue, (IList)expectedValue);
                }
                else if (actualValue is DateTime)
                {
                    DateTime actualDateTime = (DateTime)actualValue;
                    DateTime expectedDateTime = (DateTime)expectedValue;

                    if (actualDateTime.Date == expectedDateTime.Date &&
                        actualDateTime.Hour == expectedDateTime.Hour &&
                        actualDateTime.Minute == expectedDateTime.Minute &&
                        actualDateTime.Second == expectedDateTime.Second)
                    {
                    }
                    else
                    {
                        Assert.Fail(
                            "Property {0}.{1} does not match. Expected: {2} but was: {3}",
                            property.DeclaringType.Name,
                            property.Name,
                            expectedValue,
                            actualValue);
                    }
                }
                else if (actualValue != null && !actualValue.GetType().IsPrimitive)
                {
                    PropertyValuesAreEqual(actualValue, expectedValue);
                }
                else if (!Equals(expectedValue, actualValue))
                {
                    Assert.Fail(
                        "Property {0}.{1} does not match. Expected: {2} but was: {3}",
                        property.DeclaringType.Name,
                        property.Name,
                        expectedValue,
                        actualValue);
                }
            }
        }

        /// <summary>
        /// The assert lists are equal.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <param name="actualList">
        /// The actual list.
        /// </param>
        /// <param name="expectedList">
        /// The expected list.
        /// </param>
        private static void AssertListsAreEqual(PropertyInfo property, IList actualList, IList expectedList)
        {
            if (actualList.Count != expectedList.Count)
            {
                Assert.Fail(
                    "Property {0}.{1} does not match. Expected IList containing {2} elements but was IList containing {3} elements",
                    property.PropertyType.Name,
                    property.Name,
                    expectedList.Count,
                    actualList.Count);
            }

            for (int i = 0; i < actualList.Count; i++)
            {
                actualList[i].PropertyValuesAreEqual(expectedList[i]);
            }
        }

        /// <summary>
        /// The assert lists are equal.
        /// </summary>
        /// <param name="fieldInfo">
        /// The property.
        /// </param>
        /// <param name="actualList">
        /// The actual list.
        /// </param>
        /// <param name="expectedList">
        /// The expected list.
        /// </param>
        private static void AssertListsAreEqual(FieldInfo fieldInfo, IList actualList, IList expectedList)
        {
            if (actualList.Count != expectedList.Count)
            {
                Assert.Fail(
                    "Property {0}.{1} does not match. Expected IList containing {2} elements but was IList containing {3} elements",
                    fieldInfo.FieldType.Name,
                    fieldInfo.Name,
                    expectedList.Count,
                    actualList.Count);
            }

            for (int i = 0; i < actualList.Count; i++)
            {
                actualList[i].FieldValuesAreEqual(expectedList[i]);
            }
        }
    }
}
