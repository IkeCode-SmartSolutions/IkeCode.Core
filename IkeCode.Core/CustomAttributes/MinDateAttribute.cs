using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Globalization;

namespace IkeCode.Core.CustomAttributes
{
    public class MinDateAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "'{0}' deve ser maior que {1:d}.";

        public DateTime MinDate { get; set; }
        private bool ValidateSqlMinValue { get; set; }
        private string _dateFormat = "dd/MM/yyyy";
        private string DateFormat
        {
            get { return _dateFormat; }
            set
            {
                if (value == _dateFormat || value == null)
                    return;

                _dateFormat = value;
            }
        }

        public MinDateAttribute(string minDate)
            : base(DefaultErrorMessage)
        {
            MinDate = ParseDate(minDate);
        }

        public MinDateAttribute(string minDate, bool validateSqlMinValue)
            : this(minDate)
        {
            ValidateSqlMinValue = validateSqlMinValue;
        }

        public MinDateAttribute(string minDate, string dateFormat, bool validateSqlMinValue)
            : this(minDate, validateSqlMinValue)
        {
            DateFormat = dateFormat;
        }

        public override bool IsValid(object value)
        {
            if (value == null || !(value is DateTime))
            {
                return true;
            }

            DateTime dateValue = (DateTime)value;

            var isValid = dateValue >= MinDate;

            if (ValidateSqlMinValue)
                isValid = isValid && dateValue >= (DateTime)SqlDateTime.MinValue;

            return isValid;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, MinDate);
        }

        private DateTime ParseDate(string dateValue)
        {
            return DateTime.ParseExact(dateValue, DateFormat, CultureInfo.InvariantCulture);
        }
    }
}
