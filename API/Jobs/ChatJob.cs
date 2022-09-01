using API.Hubs;
using API.Models;
using Coravel.Invocable;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Jobs
{
    public class ChatJob : IInvocable
    {
        private readonly ILogger<ChatJob> _logger;
        private readonly IDictionary<string, UserConnection> _connection;
        private ChatHub _chatHub;

        public ChatJob(ILogger<ChatJob> logger, IDictionary<string, UserConnection> connection, ChatHub chatHub)
        {
            _logger = logger;
            _connection = connection;
            _chatHub = chatHub;
        }

        public async Task Invoke()
        {
            var jobId = Guid.NewGuid();
            _logger.LogInformation($"Starting job Id {jobId}");

            //var users = _connection.Where(user => user.Value.Time.AddSeconds(15) <= DateTime.Now);
            var users = _connection;
            foreach (var user in users)
            {
                if (user.Value.Time.AddSeconds(15) <= DateTime.Now)
                {
                    var userconnection = new UserConnection
                    {
                        User = user.Value.User,
                        Room = user.Value.Room,
                        Time = user.Value.Time
                    };
                    await _chatHub.SendDisconnected(user.Key);
                    _logger.LogInformation($"user disconnected {user.Value.Time} : {DateTime.Now}");

                    //var context =  ConnectionManager.GetHubContext<ChatHub>();
                }
            }

            //await _chatHub.OnDisconnectedAsync(new HubException("This error will be sent to the client!"));

            _logger.LogInformation($"Job with Id {jobId} is complete");
        }
    }
}
