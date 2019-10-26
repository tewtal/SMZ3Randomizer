using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebRandomizer.Models;

namespace WebRandomizer.Hubs {

    public class MultiworldHub : Hub {

        private readonly RandomizerContext context;

        public MultiworldHub(RandomizerContext context) {
            this.context = context;
        }


        public async Task<List<int>> GetSequences(string sessionGuid) {
            /* Check that the session is valid and grab it from the database */
            var session = await context.Sessions.Include(x => x.Clients).ThenInclude(x => x.Events).SingleOrDefaultAsync(x => x.Guid == sessionGuid);
            if (session != null) {
                /* Check that the sender is a client in the session */
                var client = session.Clients.SingleOrDefault(x => x.ConnectionId == this.Context.ConnectionId);
                if (client != null) {
                    return new List<int> { client.Events.Where(x => x.Type == EventType.ItemSent)?.Max(x => x.SequenceNum) ?? 0, client.Events.Where(x => x.Type == EventType.ItemReceived)?.Max(x => x.SequenceNum) ?? 0 };
                }
            }

            return null;
        }

        public async Task<List<Event>> GetEvents(string sessionGuid, string eventTypeStr, int fromSequence) {
            var session = await context.Sessions.Include(x => x.Clients).ThenInclude(x => x.Events).SingleOrDefaultAsync(x => x.Guid == sessionGuid);
            if (session != null) {
                /* Check that the sender is a client in the session */
                var client = session.Clients.SingleOrDefault(x => x.ConnectionId == this.Context.ConnectionId);
                if (client != null) {
                    if (Enum.TryParse(eventTypeStr, out EventType eventType)) {
                        return client.Events.Where(x => x.Type == eventType && x.SequenceNum > fromSequence)?.OrderBy(x => x.SequenceNum).ToList() ?? null;
                    }
                }
            }

            return null;
        }

        public async Task<bool> CreateEvent(string sessionGuid, Event ev) {
            var session = await context.Sessions.Include(x => x.Clients).ThenInclude(x => x.Events).SingleOrDefaultAsync(x => x.Guid == sessionGuid);
            if (session != null) {
                /* Check that the sender is a client in the session */
                var client = session.Clients.SingleOrDefault(x => x.ConnectionId == this.Context.ConnectionId);
                if (client != null) {
                    while (true) {
                        try {
                            client.Events.Add(ev);
                            await context.SaveChangesAsync();
                            return true;
                        } catch (DbUpdateConcurrencyException) {
                            continue;
                        } catch {
                            break;
                        }
                    }
                }
            }

            return false;
        }

        public async Task<bool> SendItem(string sessionGuid, int worldId, int itemId, int itemIndex, int sequenceId) {            
            /* Check that the session is valid and grab it from the database */
            var session = await context.Sessions.Include(x => x.Clients).ThenInclude(x => x.Events).SingleOrDefaultAsync(x => x.Guid == sessionGuid);
            if(session != null) {
                
                /* Check that the sender is a client in the session */
                var fromClient = session.Clients.SingleOrDefault(x => x.ConnectionId == this.Context.ConnectionId);
                if(fromClient != null) {
                    
                    /* If we're seeing a repeat item index it means we're getting a dupe item (snes was reset/player died)
                     * We just accept it without creating a new event here since the target player should hopefully already have the item */
                    if(fromClient.Events.Any(x => x.Type == EventType.ItemSent && x.ItemIndex == itemIndex)) {
                        return true;
                    }
                    
                    /* Get the receiving client */
                    var toClient = session.Clients.SingleOrDefault(x => x.WorldId == worldId);
                    if(toClient != null) {
                        while(true) { 
                            try {
                                /* Create a item sent event */
                                fromClient.Events.Add(new Event {
                                    ClientId = fromClient.Id,
                                    Description = $"{fromClient.Name} sent item {itemId} to {toClient.Name}",
                                    ItemId = itemId,
                                    ItemIndex = itemIndex,
                                    PlayerId = worldId,
                                    TimeStamp = DateTime.Now,
                                    Type = EventType.ItemSent,
                                    SequenceNum = sequenceId
                                });

                                fromClient.SentSeq = sequenceId;
                                toClient.RecievedSeq += 1;

                                /* Create item received event */
                                toClient.Events.Add(new Event {
                                    ClientId = toClient.Id,
                                    Description = $"Received item {itemId} from {fromClient.Name}",
                                    ItemId = itemId,
                                    ItemIndex = itemIndex,
                                    PlayerId = fromClient.WorldId,
                                    TimeStamp = DateTime.Now,
                                    Type = EventType.ItemReceived,
                                    SequenceNum = toClient.RecievedSeq
                                });
                                
                                context.Clients.Update(fromClient);
                                context.Clients.Update(toClient);
                                await context.SaveChangesAsync();
                                return true;

                            } catch (DbUpdateConcurrencyException) {
                                /* Concurrency fault trying to create events (possible ID conflict due to asynchronous updates) */
                                /* re-read session and client data to get the new latest events */
                                session = await context.Sessions.Include(x => x.Clients).SingleOrDefaultAsync(x => x.Guid == sessionGuid);
                                fromClient = session.Clients.SingleOrDefault(x => x.ConnectionId == this.Context.ConnectionId);
                                toClient = session.Clients.SingleOrDefault(x => x.WorldId == worldId);
                                continue;
                            } catch {
                                break;
                            }
                        }                        
                    }
                }
            }
            return false;
        }

        public async Task<bool> RegisterConnection(string sessionGuid) {
            var session = await context.Sessions.Include(x => x.Seed).ThenInclude(x => x.Worlds).SingleOrDefaultAsync(x => x.Guid == sessionGuid);
            if (session != null) {
                if (session.State == SessionState.Created) {
                    /* If we're re-connecting, remove the old connection id from the session group if it's registered */
                    try {
                        await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, session.Guid);
                    }
                    catch { }

                    /* Register this connection to the group */
                    await this.Groups.AddToGroupAsync(this.Context.ConnectionId, session.Guid);

                    return true;
                }
            }
            return false;
        }

        public async Task<bool> UnregisterPlayer(string sessionGuid, string worldGuid) {
            var session = await context.Sessions.Include(x => x.Seed).ThenInclude(x => x.Worlds).SingleOrDefaultAsync(x => x.Guid == sessionGuid);
            if (session != null) {
                if (session.State == SessionState.Created) {
                    Client client = await context.Clients.SingleOrDefaultAsync(x => x.SessionId == session.Id && x.Guid == worldGuid);
                    if(client != null) {
                        /* Delete this client registration from the database */
                        context.Remove(client);
                        await context.SaveChangesAsync();

                        /* Send client list update to all registered session */
                        var newClients = context.Clients.Where(x => x.SessionId == session.Id);
                        await Clients.Group(session.Guid).SendAsync("UpdateClients", newClients);

                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<Client> RegisterPlayer(string sessionGuid, string worldGuid) {
            var session = await context.Sessions.Include(x => x.Seed).ThenInclude(x => x.Worlds).SingleOrDefaultAsync(x => x.Guid == sessionGuid);
            if(session != null) {
                if(session.State == SessionState.Created) {
                    try {
                        /* Make sure we don't register an existing client, otherwise we update the connection id and return the existing one */
                        Client client = await context.Clients.SingleOrDefaultAsync(x => x.SessionId == session.Id && x.Guid == worldGuid);
                        var world = session.Seed.Worlds.Find(x => x.Guid == worldGuid);

                        if (client == null) {
                            client = new Client {
                                SessionId = session.Id,
                                Name = world.Player,
                                Guid = world.Guid,
                                WorldId = world.WorldId,
                                State = ClientState.Registered,
                                Device = "",
                                ConnectionId = this.Context.ConnectionId
                            };
                            context.Clients.Add(client);
                            await context.SaveChangesAsync();
                        } else {
                            client.ConnectionId = this.Context.ConnectionId;
                            client.Name = world.Player;
                            context.Clients.Update(client);
                            await context.SaveChangesAsync();
                        }

                        /* Push an update of the client list to everyone registered to the hub */
                        var newClients = context.Clients.Where(x => x.SessionId == session.Id);
                        await Clients.Group(session.Guid).SendAsync("UpdateClients", newClients);

                        return client;
                    } catch {
                        return null;
                    }
                }
            }

            return null;
        }

        public async Task<Client> UpdateClient(Client client) {
            try {
                var currentClient = await context.Clients.SingleOrDefaultAsync(x => x.Id == client.Id && x.ConnectionId == this.Context.ConnectionId);
                if (currentClient != null) {
                    currentClient.Name = client.Name;
                    currentClient.Device = client.Device;
                    currentClient.State = client.State;
                    context.Clients.Update(currentClient);
                    await context.SaveChangesAsync();

                    /* Push an update of the client list to everyone registered to the hub */
                    var session = await context.Sessions.Include(x => x.Clients).SingleOrDefaultAsync(x => x.Id == client.SessionId);
                    await Clients.Group(session.Guid).SendAsync("UpdateClients", session.Clients);
                    return currentClient;
                }
                else {
                    return null;
                }
            }
            catch {
                return null;
            }
        }

    }

}
