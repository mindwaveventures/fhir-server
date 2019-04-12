﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using System.Net;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Health.Extensions.DependencyInjection;
using Microsoft.Health.Fhir.Core.Extensions;
using Microsoft.Health.Fhir.Core.Features.Export;
using Microsoft.Health.Fhir.Core.Features.Persistence;
using NSubstitute;
using Xunit;

namespace Microsoft.Health.Fhir.Core.UnitTests.Features.Export
{
    public class CreateExportRequestHandlerTests
    {
        private readonly IDataStore _dataStore;
        private readonly IMediator _mediator;
        private const string RequestUrl = "https://localhost/$export/";
        private const string DestinationType = "destinationType";
        private const string ConnectionString = "destinationConnection";

        public CreateExportRequestHandlerTests()
        {
            _dataStore = Substitute.For<IDataStore>();

            var collection = new ServiceCollection();
            collection.Add(x => new CreateExportRequestHandler(_dataStore)).Singleton().AsSelf().AsImplementedInterfaces();

            ServiceProvider provider = collection.BuildServiceProvider();
            _mediator = new Mediator(type => provider.GetService(type));
        }

        [Fact]
        public async void GivenAFhirMediator_WhenSavingAnExportJobSucceeds_ThenResponseShouldBeSuccess()
        {
            _dataStore.UpsertExportJobAsync(Arg.Any<ExportJobRecord>())
                .Returns(x => HttpStatusCode.Created);

            var outcome = await _mediator.ExportAsync(new Uri(RequestUrl), DestinationType, ConnectionString);

            Assert.True(outcome.JobCreated);
        }

        [Fact]
        public async void GivenAFhirMediator_WhenSavingAnExportJobFails_ThenResponseShouldBeFailure()
        {
            _dataStore.UpsertExportJobAsync(Arg.Any<ExportJobRecord>())
                .Returns(x => HttpStatusCode.BadRequest);

            var outcome = await _mediator.ExportAsync(new Uri(RequestUrl), DestinationType, ConnectionString);

            Assert.False(outcome.JobCreated);
        }
    }
}
