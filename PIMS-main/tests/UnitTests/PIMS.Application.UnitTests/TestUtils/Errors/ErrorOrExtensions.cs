using ErrorOr;
using FluentAssertions;

namespace PIMS.Application.UnitTests.TestUtils.Errors
{
	public static class ErrorOrExtensions
	{
		public static AndConstraint<FluentAssertions.Primitives.BooleanAssertions> ErrorShouldBeFalse<T>(this ErrorOr<T> errorOr)
		{
			return errorOr.IsError.Should().BeFalse();
		}
	}
}
