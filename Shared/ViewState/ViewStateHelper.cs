﻿using System;
using System.Text;

namespace Shared.ViewState
{
    public class ViewStateHelper
    {
        public static string GetRawBase64Data(string value)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(value));
        }
    }
}
