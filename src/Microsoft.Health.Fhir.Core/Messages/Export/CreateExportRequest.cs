// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using EnsureThat;
using MediatR;
using Microsoft.Health.Fhir.Core.Features.Operations;
using Newtonsoft.Json;

namespace Microsoft.Health.Fhir.Core.Messages.Export
{
    public class CreateExportRequest : IRequest<CreateExportResponse>
    {
        public CreateExportRequest(Uri requestUri)
        {
            EnsureArg.IsNotNull(requestUri, nameof(requestUri));

            RequestUri = requestUri;
        }

        public CreateExportRequest(Uri requestUri, string destinationType, string destinationConnectionString)
        {
            EnsureArg.IsNotNull(requestUri, nameof(requestUri));
            EnsureArg.IsNotNullOrWhiteSpace(destinationType, nameof(destinationType));
            EnsureArg.IsNotNullOrWhiteSpace(destinationConnectionString, nameof(destinationConnectionString));

            RequestUri = requestUri;
            DestinationType = destinationType;
            DestinationConnectionString = destinationConnectionString;
        }

        [JsonConstructor]
        protected CreateExportRequest()
        {
        }

        [JsonProperty(JobRecordProperties.RequestUri)]
        public Uri RequestUri { get; private set; }

        [JsonProperty(JobRecordProperties.DestinationType)]
        public string DestinationType { get; private set; }

        [JsonProperty(JobRecordProperties.DestinationConnectionString)]
        public string DestinationConnectionString { get; private set; }
    }
}
