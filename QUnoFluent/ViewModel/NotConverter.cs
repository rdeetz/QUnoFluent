// <copyright file="NotConverter.cs" company="Mooville">
//   Copyright © 2024 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Fluent.ViewModel
{
    using System;
    using Microsoft.UI.Xaml.Data;

    public class NotConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool)
            {
                return !((bool)value);
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
