using System;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TagKid.Core.Exceptions;
using TagKid.Core.Validation.Extensions;

namespace TagKid.Tests.ValidationTests
{
    [TestClass]
    public class ValidationTest
    {
        [TestMethod, TestCategory("FluentValidation")]
        public void Test_Custom_State()
        {
            var user = new User();
            var validator = new UserValidator1();
            var valRes = validator.Validate(user);

            Assert.IsFalse(valRes.IsValid);
            Assert.AreEqual(1, valRes.Errors.Count);
            Assert.AreEqual(Errors.Unknown, valRes.Errors[0].CustomState);
        }

        [TestMethod, TestCategory("FluentValidation")]
        public void Test_Stop_On_First_Failure()
        {
            var user = new User();
            var validator = new UserValidator2();
            var valRes = validator.Validate(user);

            Assert.IsFalse(valRes.IsValid);
            Assert.AreEqual(1, valRes.Errors.Count);
            Assert.AreEqual(1, valRes.Errors[0].CustomState);
        }

        [TestMethod, TestCategory("FluentValidation")]
        public void Test_Latter_Custom_State()
        {
            var user = new User
            {
                Username = String.Empty
            };
            var validator = new UserValidator2();
            var valRes = validator.Validate(user);

            Assert.IsFalse(valRes.IsValid);
            Assert.AreEqual(1, valRes.Errors.Count);
            Assert.AreEqual(2, valRes.Errors[0].CustomState);
        }

        [TestMethod, TestCategory("FluentValidation")]
        public void Test_Common_Custom_State()
        {
            var user = new User();
            var validator = new UserValidator3();
            var valRes = validator.Validate(user);

            Assert.IsFalse(valRes.IsValid);
            Assert.AreEqual(1, valRes.Errors.Count);
            Assert.IsNull(valRes.Errors[0].CustomState);
        }
    }

    public class UserValidator1 : AbstractValidator<User>
    {
        public UserValidator1()
        {
            RuleFor(u => u.Username)
                .TrimmedLength(4, 20, Errors.Unknown);
        }
    }

    public class UserValidator2 : AbstractValidator<User>
    {
        public UserValidator2()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(u => u.Username)
                .NotNull()
                .WithState(u => 1)
                .NotEmpty()
                .WithState(u => 2);
        }
    }

    public class UserValidator3 : AbstractValidator<User>
    {
        public UserValidator3()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(u => u.Username)
                .NotNull()
                .NotEmpty()
                .WithState(u => 1);
        }
    }

    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
