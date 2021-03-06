﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.VisualStudio.TestPlatform.CommandLine.UnitTests
{
    using Microsoft.VisualStudio.TestPlatform.CommandLine;
    using Microsoft.VisualStudio.TestPlatform.CommandLine.TestPlatformHelpers;
    using Microsoft.VisualStudio.TestPlatform.Common.Logging;
    using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Tracing.Interfaces;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using vstest.console.UnitTests.TestDoubles;

    [TestClass]
    public class TestRequestManagerTests
    {
        private DummyLoggerEvents mockLoggerEvents;
        private TestLoggerManager mockLoggerManager;

        public TestRequestManagerTests()
        {
            this.mockLoggerEvents = new DummyLoggerEvents(TestSessionMessageLogger.Instance);
            this.mockLoggerManager = new DummyTestLoggerManager(this.mockLoggerEvents);
        }

        [TestMethod]
        public void TestRequestManagerShouldInitializeConsoleLogger()
        {
            CommandLineOptions.Instance.IsDesignMode = false;
            var requestManager = new TestRequestManager(CommandLineOptions.Instance,
                new Mock<ITestPlatform>().Object,
                this.mockLoggerManager,
                TestRunResultAggregator.Instance,
                new Mock<ITestPlatformEventSource>().Object);

            Assert.IsTrue(mockLoggerEvents.EventsSubscribed());
        }

        [TestMethod]
        public void TestRequestManagerShouldNotInitializeConsoleLoggerIfDesignModeIsSet()
        {
            CommandLineOptions.Instance.IsDesignMode = true;
            var requestManager = new TestRequestManager(CommandLineOptions.Instance,
                new Mock<ITestPlatform>().Object,
                this.mockLoggerManager,
                TestRunResultAggregator.Instance,
                new Mock<ITestPlatformEventSource>().Object);

            Assert.IsFalse(mockLoggerEvents.EventsSubscribed());
        }
    }
}
