﻿/*
 | Version 10.1.84
 | Copyright 2013 Esri
 |
 | Licensed under the Apache License, Version 2.0 (the "License");
 | you may not use this file except in compliance with the License.
 | You may obtain a copy of the License at
 |
 |    http://www.apache.org/licenses/LICENSE-2.0
 |
 | Unless required by applicable law or agreed to in writing, software
 | distributed under the License is distributed on an "AS IS" BASIS,
 | WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 | See the License for the specific language governing permissions and
 | limitations under the License.
 */

using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Documents;
using System.Globalization;
using System.Collections.Generic;

using ESRI.ArcLogistics.App.GridHelpers;

namespace ESRI.ArcLogistics.App.Converters
{
    /// <summary>
    /// Converts report's name to a string with special postfix if needed
    /// </summary>
    [ValueConversion(typeof(object), typeof(ICollection<Inline>))]
    internal class ReportNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<Inline> inlines = new List<Inline>();

            if (null != value)
            {
                System.Diagnostics.Debug.Assert(value is SpecialName);
                SpecialName name = value as SpecialName;

                inlines.Add(new Run(name.Name));
                if (name.IsSpecial)
                {
                    inlines.Add(new Run(NEW_LINE));
                    Italic noOrders = new Italic(new Run((string)App.Current.FindResource("ReportEnforceSplittedText")));
                    noOrders.Foreground = new SolidColorBrush(Colors.Gray);
                    noOrders.FontSize = (double)App.Current.FindResource("StandartHelpFontSize");
                    inlines.Add(noOrders);
                }
            }

            return inlines;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        private const string NEW_LINE = "\n";
    }
}
