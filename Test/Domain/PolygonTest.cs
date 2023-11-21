using Model.ObjectValue.Geometry;
using NUnit.Framework;
using Model.Geometry;
using System.Collections.Generic;
using System;
using Model.Exception;
using System.Collections;

namespace Test.Domain
{
    [TestFixture]
    internal class PolygonTest
    {
        [Category("Unit")]
        [TestCaseSource(nameof(ValidCoordinateCases))]
        public void Should_Create_Polygon(List<Coordinate> coordinates)
        {
            //Act
            Polygon polygon = new(coordinates);

            //Assert
            Assert.NotNull(polygon);
        }

        static IEnumerable<List<Coordinate>> ValidCoordinateCases()
        {
            yield return new List<Coordinate>()
            {
                new Coordinate(0, 0),
                new Coordinate(1, 1),
                new Coordinate(2, 0),
                new Coordinate(0, 0)
            };
            yield return new List<Coordinate>()
            {
                new Coordinate(0, 0),
                new Coordinate(1, 1),
                new Coordinate(2, 0),
                new Coordinate(2, 3),
                new Coordinate(0, 0)
            };
            yield return new List<Coordinate>()
            {
                new Coordinate(0, 0),
                new Coordinate(1, 1),
                new Coordinate(2, 0),
                new Coordinate(2, 5),
                new Coordinate(5, 2),
                new Coordinate(0, 0)
            };

        }

        [Test, Description("Should calculate area of polygon")]
        [Category("Unit")]
        public void Should_Calculate_Area_Polygon()
        {
            //Arrange
            List<Coordinate> Coordinates = new()
            {
                new Coordinate(0, 0),
                new Coordinate(1, 1),
                new Coordinate(2, 0),
                new Coordinate(0, 0)
            };

            //Act
            Polygon polygon = new(Coordinates);

            //Assert
            Assert.AreEqual(1, polygon.Area);
        }

        [Test, Description("Should update area when update coordinate list")]
        [Category("Unit")]
        public void Should_Update_Area_Using_Set_Coordinate()
        {
            //Arrange
            List<Coordinate> Coordinates = new()
            {
                new Coordinate(0, 0),
                new Coordinate(1, 1),
                new Coordinate(2, 0),
                new Coordinate(0, 0)
            };

            //Act
            Polygon polygon = new(Coordinates);

            //Assert
            Assert.AreEqual(1, polygon.Area);

            //Arrange
            Coordinates.RemoveAt(Coordinates.Count - 1);
            Coordinates.Add(new Coordinate(1, -1));
            Coordinates.Add(new Coordinate(0, 0));

            //Act
            polygon.SetCoordinates(Coordinates);

            //Assert
            Assert.AreEqual(2, polygon.Area);
        }

        [Category("Unit")]
        [TestCaseSource(nameof(CoordinatesTestCase))]
        public bool Should_Test_Ray_Casting(Coordinate point)
        {
            //Arrange
            List<Coordinate> coordinates = new()
            {
                new Coordinate(0, 0),
                new Coordinate(1, 2),
                new Coordinate(2, 0),
                new Coordinate(0, 0)
            };
            Polygon polygon = new(coordinates);

            //Act
            bool result = polygon.IsPointInsidePolygon(point);

            //Assert
            return result;
        }

        public static IEnumerable CoordinatesTestCase
        {
            get
            {
                yield return new TestCaseData(new Coordinate(12, 33)).Returns(false);
                yield return new TestCaseData(new Coordinate(1, 1)).Returns(true);
                yield return new TestCaseData(new Coordinate(12, 4)).Returns(false);
            }
        }

        [Category("Unit")]
        [TestCaseSource(nameof(InvalidCoordinateCases))]
        public void Should_Throw_Exception_Less_Three_Coordinates(List<Coordinate> coordinates)
        {
            //Act
            var ex = Assert.Throws<ArgumentException>(() => {
                Polygon polygon = new(coordinates);
            });

            if (ex != null)
            {
                //Assert
                Assert.AreEqual(Messages.LessCoordinatePolygon, ex.Message);
            } else
            {
                Assert.Fail();
            }
        }

        static IEnumerable<List<Coordinate>> InvalidCoordinateCases()
        {
            yield return new List<Coordinate>()
            {
                        new Coordinate(0, 0),
                        new Coordinate(1, 1),
                        new Coordinate(0, 0)
                    };
            yield return new List<Coordinate>() {
                        new Coordinate(0, 0),
                        new Coordinate(1, 1)
                    };
            yield return new List<Coordinate>() {
                        new Coordinate(1, 1)
                    };
        }
    }
}
