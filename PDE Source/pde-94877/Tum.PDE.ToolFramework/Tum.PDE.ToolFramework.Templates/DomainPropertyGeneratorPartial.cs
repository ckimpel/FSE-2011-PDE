﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tum.PDE.ToolFramework.Templates
{
    public partial class DomainPropertyGenerator
    {
        private static DomainPropertyGenerator instanceHolder = null;

        /// <summary>
        /// Gets a singleton instance of the domainproperty class.
        /// </summary>
        public static DomainPropertyGenerator Instance
        {
            get
            {
                if (instanceHolder == null)
                    instanceHolder = new DomainPropertyGenerator();

                return instanceHolder;
            }
        }
    }
}