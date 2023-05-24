using System.ComponentModel.DataAnnotations;

namespace RestaurantAggregator.Common.Attributes.ValidationAttributes;

public class DateRangeAttribute : ValidationAttribute
{
    private readonly double _earlierThanTodayBy;
    private readonly double _laterThanTodayBy;
    private readonly bool _isNullable;

    public DateRangeAttribute(double earlierThanTodayBy = short.MinValue, double laterThanTodayBy = short.MaxValue,
        bool isNullable = false)
    {
        _earlierThanTodayBy = earlierThanTodayBy;
        _laterThanTodayBy = laterThanTodayBy;
        _isNullable = isNullable;
    }

    public override bool IsValid(object? value)
    {
        if (_isNullable && value == null) return true;
        if (value is not DateTime date) return false;

        var minDate = DateTime.Now.AddDays(-_earlierThanTodayBy);
        var maxDate = DateTime.Now.AddDays(_laterThanTodayBy);

        var result = true;

        if (minDate.Date > date.Date)
        {
            ErrorMessage = string.Format($"Date must be not earlier than {minDate:dd/MM/yyyy}");
            result = false;
        }
        else if (maxDate.Date < date.Date)
        {
            ErrorMessage = string.Format($"Date must be not later than {maxDate:dd/MM/yyyy}");
            result = false;
        }

        return result;
    }
}