using Microsoft.VisualStudio.TestTools.UnitTesting;
using qlbaigiuxe;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParkingLotManagement.Tests
{
    [TestClass]
    public class ParkingLotValidationTests
    {
        private bool ValidateModel(ParkingLot lot, out List<ValidationResult> results)
        {
            var context = new ValidationContext(lot, null, null);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(lot, context, results, true);
        }

        [TestMethod]
        public void Should_Fail_When_Name_Is_Empty()
        {
            var lot = new ParkingLot { Name = "", Address = "Q1", Capacity = 50 };

            var isValid = ValidateModel(lot, out var results);

            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Exists(r => r.ErrorMessage.Contains("Name is required")));
        }

        [TestMethod]
        public void Should_Fail_When_Address_Is_Empty()
        {
            var lot = new ParkingLot { Name = "Bãi Xe A", Address = "", Capacity = 50 };

            var isValid = ValidateModel(lot, out var results);

            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Exists(r => r.ErrorMessage.Contains("Address is required")));
        }

        [TestMethod]
        public void Should_Fail_When_Capacity_Is_Negative()
        {
            var lot = new ParkingLot { Name = "Bãi Xe A", Address = "Q1", Capacity = -10 };

            var isValid = ValidateModel(lot, out var results);

            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Exists(r => r.ErrorMessage.Contains("Capacity must be non-negative")));
        }

        [TestMethod]
        public void Should_Pass_When_Valid()
        {
            var lot = new ParkingLot { Name = "Bãi Xe A", Address = "Q1", Capacity = 100 };

            var isValid = ValidateModel(lot, out var results);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, results.Count);
        }
    }
}
