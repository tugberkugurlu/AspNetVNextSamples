using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace MoveShape.Web.MoveShape
{
    [HubName("moveShape")]
    public class MoveShapeHub : Hub
    {
        public void MoveShape(double x, double y)
        {
            Clients.Others.shapeMoved(x, y);
        }
    }
}