using MediatR;
using Microsoft.AspNetCore.SignalR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain;
using Sepehr.Domain.Entities;

namespace Sepehr.WebApi.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IDictionary<string, UserChatConnection> _userConnections;
        public ChatHub(IDictionary<string, UserChatConnection> userConnections)
        {
            _userConnections = userConnections;
        }

        public async Task JoinRoom(string userName)
        {
            var now = DateTime.Now;

            _userConnections[Context.ConnectionId] = new UserChatConnection
            {
                UserName = userName,
                JoinedAt = now
            };

            await Clients.All.SendAsync("ReceiveMessage", new
            {
                from = "پیام سیستم",
                text = $"کاربر \"{userName}\" به گروه اضافه شد.",
                sentAt = now.ToShamsiDate(),
                isIncoming = true
            });

            await SendConnectedUsers();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (_userConnections.TryGetValue(Context.ConnectionId, out var userConnection))
            {
                _userConnections.Remove(Context.ConnectionId);

                Clients.All.SendAsync("ReceiveMessage", new
                {
                    from = "پیام سیستم",
                    text = $"کاربر \"{userConnection.UserName}\" گروه را ترک کرد.",
                    sentAt = DateTime.Now.ToShamsiDate(),
                    isIncoming = true
                });

                SendConnectedUsers();
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            if (_userConnections.TryGetValue(Context.ConnectionId, out var userConnection))
            {
                var sentAt = DateTime.Now.ToShamsiDate();

                await Clients.Caller.SendAsync("ReceiveMessage", new
                {
                    from = userConnection.UserName,
                    text = message,
                    sentAt,
                    isIncoming = false
                });
                await Clients.AllExcept(Context.ConnectionId).SendAsync("ReceiveMessage", new
                {
                    from = userConnection.UserName,
                    text = message,
                    sentAt,
                    isIncoming = true
                });
            }
        }

        public Task SendConnectedUsers()
        {
            return Clients.All.SendAsync("ReceiveConnectedUsers", _userConnections
                .Select(u => new
                {
                    name = u.Value.UserName,
                    joinedAt = u.Value.JoinedAt.ToShamsiDate()
                }).OrderByDescending(u => u.joinedAt));
        }
    }
}
