using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace VehicleTest
{
    [TestClass]
    public class UnitTest1
    {
        private VehicleTracker testTracker;
        private Vehicle vehicle1;
        private Vehicle vehicle2;
        private Vehicle vehicle3;
        private int capacity = 5;
        [TestInitialize]
        public void TestInitialize()
        {
            vehicle1 = new Vehicle("AB001", false);
            vehicle2 = new Vehicle("AB002", false);
            vehicle3 = new Vehicle("AB003", true);
            testTracker = new VehicleTracker(capacity, "QingDao");
        }

        // GenerateSlots
        [TestMethod]
        public void MakeSureGenerateSlotsRight()
        {
            //Arrange
            var expectedNumber = capacity;
            int testAvailable = testTracker.VehicleList.Where(v => v.Value == null).Count();
            //Assert
            Assert.AreEqual(expectedNumber, testAvailable);
        }

        [TestMethod]
        public void WhenGenerateSlotWrong()
        {
            // Arrange
            var capacity1 = -1;

            var address = "QingDao";

            //Act and Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                var testTracker = new VehicleTracker(capacity1, address);
            });
        }

        //AddVehicle
        [TestMethod]
        public void AddVehicle()
        {
            //Arrange
            var ExpectedValue = 2;

            //Act
            testTracker.AddVehicle(vehicle1);
            testTracker.AddVehicle(vehicle2);
            int testNumber = testTracker.VehicleList.Where((v) => v.Value != null).Count();
            //Assert
            Assert.AreEqual(ExpectedValue, testNumber);

        }

        [TestMethod]
        public void AddVehicleWrong()
        {
                //Assign
                testTracker.AddVehicle(vehicle1);
 
                testTracker.AddVehicle(vehicle2);
                
                testTracker.AddVehicle(vehicle3);

                testTracker.AddVehicle(vehicle3);

                testTracker.AddVehicle(vehicle3);

                //Action asd Assert
                Assert.ThrowsException<IndexOutOfRangeException>(() =>
                {
                    testTracker.AddVehicle(vehicle3);// expected throw exception
                });
        }

        //RemoveVehicle
        [TestMethod]
        public void MakeSureRemoveVehicle()
        {
            //Arrange 
            testTracker.AddVehicle(vehicle1);
            var licenceOfVehicle1 = vehicle1.Licence;

            //Act
            testTracker.RemoveVehicle(licenceOfVehicle1);

            //Arrange

            Assert.AreEqual(false, testTracker.VehicleList.Values.Contains(vehicle1));
        }

        [TestMethod]
        public void RemoveVehicleWrong()
        {
            //Arrange
            var wrongLicence = "abcdefg";

            //Act Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                testTracker.RemoveVehicle(wrongLicence);
            });
        }
    }
}