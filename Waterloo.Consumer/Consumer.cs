using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Waterloo.Repository.Journey;

namespace Waterloo.Consumer;

public class Consumer
{
    private readonly ILogger<Consumer> _logger;
    private readonly IJourneyRepository _journeyRepository;

    public Consumer(
        ILogger<Consumer> logger,
        IJourneyRepository journeyRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _journeyRepository = journeyRepository ?? 
            throw new ArgumentNullException(nameof(journeyRepository));
    }

    [Function("DeletedUserConsumer")]
    public async Task DeletedUserHandler(
        [ServiceBusTrigger("%TopicDeletedUser%", "TopicDeletedUserSubscription%", Connection = "ServiceBusConnection")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        _logger.LogInformation("Message ID: {id}", message.MessageId);
        _logger.LogInformation("Message Body: {body}", message.Body);
        _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        try
        {
            var json = message.Body.ToArray();
            var userDeleted = JsonSerializer.Deserialize<UserDeleted>(json);

            var result = await _journeyRepository.RemoveJourneyByUserIdAsync(
                userDeleted!.UserId, 
                userDeleted.DeletedAt);

            if(result.IsFailure) {
                _logger.LogError(result.Error);
            }
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Could not deserialize deleted user.");
        }

        await messageActions.CompleteMessageAsync(message);
    }
}