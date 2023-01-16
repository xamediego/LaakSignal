using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Socket;

public class ShowHub : Hub
{
    public async Task SelectSeat(string showId, string[] seats)
    {
        Console.WriteLine($"Show: {showId} | ConnectionId: {Context.ConnectionId} | Select Seats :");
        
        foreach (var seat in seats)
        {
            Console.WriteLine(seat);
        }
        
        await Clients.OthersInGroup(showId).SendAsync("SeatsFilled", seats);
    }
    
    public async Task AddToGroup(string showId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, showId);
    }

    public override Task OnConnectedAsync()
    {
        Console.WriteLine(Context.ConnectionId + "  connected");
        
        return base.OnConnectedAsync();
    }
}