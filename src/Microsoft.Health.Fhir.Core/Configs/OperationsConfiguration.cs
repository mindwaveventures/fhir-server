﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

namespace Microsoft.Health.Fhir.Core.Configs
{
    public class OperationsConfiguration
    {
        /// <summary>
        /// Determines whether bulk export is enabled or not.
        /// </summary>
        public bool SupportsBulkExport { get; set; }
    }
}
