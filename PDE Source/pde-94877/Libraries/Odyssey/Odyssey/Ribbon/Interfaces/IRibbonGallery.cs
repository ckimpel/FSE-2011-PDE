﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region Copyright
// Odyssey.Controls.Ribbonbar
// (c) copyright 2009 Thomas Gerber
// This source code and files, is licensed under The Microsoft Public License (Ms-PL)
#endregion
namespace Odyssey.Controls.Ribbon.Interfaces
{
    public interface IRibbonGallery:IRibbonControl
    {
        int Columns { get; set; }
        bool IsCollapsed { get; set; }
        int DropDownColumns { get; set; }

        void SetDropDownColumns(int columns);
    }
}
