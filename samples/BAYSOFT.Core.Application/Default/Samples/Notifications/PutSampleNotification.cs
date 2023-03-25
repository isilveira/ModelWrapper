﻿using BAYSOFT.Core.Domain.Default.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BAYSOFT.Core.Application.Default.Samples.Notifications
{
    public class PutSampleNotification : INotification
    {
        public Sample Payload { get; set; }
        public DateTime CreatedAt { get; set; }
        public PutSampleNotification(Sample payload)
        {
            Payload = payload;
            CreatedAt = DateTime.UtcNow;
        }
    }
    public class PutSampleNotificationHandler : INotificationHandler<PutSampleNotification>
    {
        public ILoggerFactory Logger { get; private set; }
        public PutSampleNotificationHandler(ILoggerFactory logger)
        {
            Logger = logger;
        }
        public async Task Handle(PutSampleNotification notification, CancellationToken cancellationToken)
        {
            Logger.CreateLogger<PutSampleNotificationHandler>().Log(LogLevel.Information, $"Sample putted! - Event Created At: {notification.CreatedAt:yyyy-MM-dd HH:mm:ss} Payload: {JsonSerializer.Serialize(notification.Payload)}");
        }
    }
}
