using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Robot_P16.Map;
using Robot_P16.Map.Surface;

namespace TestMap
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestTranslation()
        {

            PointOriente point1 = new PointOriente(10,-10, -5);
            PointOriente point2 = new PointOriente(0,15,10);

            PointOriente resultat = new PointOriente(10,5,5);


            Assert.AreEqual(point1.translater(point2), resultat);
        }

        [TestMethod]
        public void TestRectangle1()
        {
            PointOriente[] pointsIn  = { new PointOriente(10, 5, -5),
                                        new PointOriente(-10, 15),
                                        new PointOriente(10, 15, 10),
                                        new PointOriente(-10, 5, 10),
                                        new PointOriente(0, 10, 10)
                                    };

            PointOriente[] pointsOut = { 
                                        new PointOriente(-10, 0, -5),
                                        new PointOriente(-10.01, 10, -5),
                                        new PointOriente(0, 15.001, -5)
                                    };


            PointOriente centre = new PointOriente(0, 10, 0);
            double LX = 10;
            double LY = 5;

            Rectangle rect = new Rectangle(centre, LX, LY);

            foreach (PointOriente pt in pointsIn)
                Assert.IsTrue(rect.Appartient(pt));

            foreach (PointOriente pt in pointsOut)
                Assert.IsFalse(rect.Appartient(pt));

        }

        [TestMethod]
        public void TestRectangleOrienteAngleDroit() // Attention, Math.Cos(PI/2) = 10e-17
        {
            PointOriente[] pointsIn = { new PointOriente(10, 5, -5),
                                        new PointOriente(-10, 15),
                                        //new PointOriente(10, 15, 10),
                                        new PointOriente(9.9999, 14.999999, 10),
                                        new PointOriente(-9.9999999, 5.0001, 10),
                                        //new PointOriente(-10, 5, 10), // Attention à l'arrondi
                                        new PointOriente(0, 10, 10)
                                    };


            PointOriente[] pointsOut = { 
                                        new PointOriente(-10, 0, -5),
                                        new PointOriente(-10.01, 10, -5),
                                        new PointOriente(0, 15.001, -5)
                                    };


            PointOriente centre = new PointOriente(0, 10, Math.PI / 2);
            double LX = 5;
            double LY = 10;

            Rectangle rect = new Rectangle(centre, LX, LY);

            //Assert.AreEqual(System.Math.Sin(Math.PI / 2), 1d);
            //Assert.AreEqual(System.Math.Cos(Math.PI / 2), 0d);

            foreach (PointOriente pt in pointsIn)
                Assert.IsTrue(rect.Appartient(pt));

            foreach (PointOriente pt in pointsOut)
                Assert.IsFalse(rect.Appartient(pt));

        }

        [TestMethod]
        public void TestRectangleOrienteRetourne()
        {
            PointOriente[] pointsIn = { new PointOriente(10, 5, -5),
                                        new PointOriente(-10, 15),
                                        //new PointOriente(10, 15, 10),
                                        new PointOriente(9.9999, 14.999999, 10),
                                        new PointOriente(-9.9999999, 5.0001, 10),
                                        //new PointOriente(-10, 5, 10), // Attention à l'arrondi
                                        new PointOriente(0, 10, 10)
                                    };


            PointOriente[] pointsOut = { 
                                        new PointOriente(-10, 0, -5),
                                        new PointOriente(-10.01, 10, -5),
                                        new PointOriente(0, 15.001, -5)
                                    };


            PointOriente centre = new PointOriente(0, 10, Math.PI);
            double LX = 10;
            double LY = 5;

            Rectangle rect = new Rectangle(centre, LX, LY);

            foreach (PointOriente pt in pointsIn)
                Assert.IsTrue(rect.Appartient(pt));

            foreach (PointOriente pt in pointsOut)
                Assert.IsFalse(rect.Appartient(pt));

        }

    }
}
