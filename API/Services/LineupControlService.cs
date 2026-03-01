using Microsoft.EntityFrameworkCore;
using NFLFantasyChallenge.API.DTOs;
using NFLFantasyChallenge.API.DTOs.Lineup;
using NFLFantasyChallenge.API.Enums;
using NFLFantasyChallenge.API.Services.Interfaces;
using NFLFantasyChallenge.Middleware;
using NFLFantasyChallenge.Models;
using System;
using System.Diagnostics.Metrics;

namespace NFLFantasyChallenge.API.Services;

public class LineupControlService : ILineupControlService
{
    private readonly FantasyDbContext _context;

    public LineupControlService(FantasyDbContext context)
    {
        _context = context;
    }

    public async Task<UserLineupDTO> GetUserLineup(int userId)
    {
        var user = await _context.Users.Where(w => w.UserId == userId).FirstOrDefaultAsync();
        if (user == null)
        {
            throw new FantasyAPIException("Invalid user");
        }

        var positionOrder = new List<string> { "QB", "RB", "WR", "TE", "K", "D" };

        var lineup = await _context.Lineups
            .Where(w => w.UserId == userId)
            .Select(s => new
            {
                s.User.FullName,
                s.IsLocked,
                Slots = s.Slots
                    .Where(p => p.Player != null)
                    .Select(p => new LineupSlotDTO
                    {
                        PlayerId = p.PlayerId ?? 0,
                        PlayerName = p.Player.Name,
                        Position = p.Position,
                        SlotIndex = p.SlotIndex,
                        Team = p.Player.Team
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();

        var lineupDTO = new UserLineupDTO()
        {
            FullName = user.FullName,
            IsLocked = false,
            PositionGroups = new List<LineupPlayersByPosition>()
        };
        if (lineup == null)
        {            
            foreach (var position in positionOrder)
            {
                lineupDTO.PositionGroups.Add(new LineupPlayersByPosition()
                {
                    Position = Enum.Parse<PlayerPosition>(position),
                    Players = new List<LineupSlotDTO>()
                });
            }
        }
        else
        {
            lineupDTO.IsLocked = lineup.IsLocked;
            lineupDTO.PositionGroups = lineup.Slots
                .GroupBy(p => p.Position)
                .Select(g => new LineupPlayersByPosition
                {
                    Position = Enum.Parse<PlayerPosition>(g.Key),
                    Players = g.ToList()
                })
                .ToList();

            foreach (var position in positionOrder)
            {
                if (!lineupDTO.PositionGroups.Any(a => a.PositionName == position))
                {
                    lineupDTO.PositionGroups.Add(new LineupPlayersByPosition()
                    {
                        Position = Enum.Parse<PlayerPosition>(position),
                        Players = new List<LineupSlotDTO>()
                    });
                }
            }
        }

        var counts = await _context.ChallengeRules
            .Where(w => w.Name.EndsWith("Count"))
            .ToDictionaryAsync(
                k => k.Name,
                v => int.Parse(v.Description)
            );

        var qbCount = GetPlayerCount("QbCount", counts);
        var rbCount = GetPlayerCount("RbCount", counts);
        var wrCount = GetPlayerCount("WrCount", counts);
        var teCount = GetPlayerCount("TeCount", counts);
        var kCount = GetPlayerCount("KCount", counts);
        var dCount = GetPlayerCount("DCount", counts);

        AddPlayers(PlayerPosition.QB, qbCount, lineupDTO.PositionGroups);
        AddPlayers(PlayerPosition.RB, rbCount, lineupDTO.PositionGroups);
        AddPlayers(PlayerPosition.WR, wrCount, lineupDTO.PositionGroups);
        AddPlayers(PlayerPosition.TE, teCount, lineupDTO.PositionGroups);
        AddPlayers(PlayerPosition.K, kCount, lineupDTO.PositionGroups);
        AddPlayers(PlayerPosition.D, dCount, lineupDTO.PositionGroups);

        lineupDTO.PositionGroups = lineupDTO.PositionGroups
            .OrderBy(o => positionOrder.IndexOf(o.Position.ToString()))
            .ToList();

        return lineupDTO;
    }

    private List<LineupSlotDTO> AddPlayers(PlayerPosition position, int requiredCount, List<LineupPlayersByPosition> positionGroup)
    {
        var playersForGroup = positionGroup.Where(w => w.Position == position).FirstOrDefault();
        if (playersForGroup == null)
        {
            playersForGroup = new LineupPlayersByPosition()
            {
                Position = position,
                Players = new List<LineupSlotDTO>()
            };
        }
        var playersToAdd = new List<LineupSlotDTO>();
        var playerCount = playersForGroup.Players.Count();
        for (int i = playerCount + 1; i <= requiredCount; i++)
        {
            playersToAdd.Add(new LineupSlotDTO()
            {               
                PlayerId = 0,
                PlayerName = "",                
                SlotIndex = i,
                Team = ""
            });
        }
        playersForGroup.Players.AddRange(playersToAdd);
        return playersToAdd;
    }

    private int GetPlayerCount(string key, Dictionary<string, int> counts) => counts.TryGetValue(key, out var value) ? value : 0;

    public async Task<List<PlayerDTO>> GetPlayerList(int userId, string Position)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            throw new FantasyAPIException("Invalid User");
        }

        var userLineup = await _context.Lineups
            .Include(i => i.Slots)
            .ThenInclude(t => t.Player)
            .Where(w => w.UserId == userId)
            .FirstOrDefaultAsync();

        var playerQuery = _context.Players
            .Where(w => w.Position == Position)
            .AsQueryable();

        if (userLineup != null)
        {
            var lineupPlayerIds = userLineup.Slots.Select(s => s.PlayerId).ToList();
            playerQuery = playerQuery.Where(w => !lineupPlayerIds.Contains(w.PlayerId));

            var teamsSelected = userLineup.Slots.Select(s => s.Player.Team).ToList();
            playerQuery = playerQuery.Where(w => !teamsSelected.Contains(w.Team));
        }        

        var result = await playerQuery
            .Select(s => new PlayerDTO()
            {
                PlayerID = s.PlayerId,
                PlayerName = s.Name,
                Team = s.Team,
            })
            .OrderBy(o => o.PlayerName)
            .ToListAsync();

        return result;
    }

    public async Task AddPlayerToLineup(int userId, int PlayerId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            throw new FantasyAPIException("Invalid User");
        }

        var dbPlayer = await _context.Players.FindAsync(PlayerId);
        if (dbPlayer == null)
        {
            throw new FantasyAPIException("Invalid Player");
        }

        var userLineup = await _context.Lineups
            .Include(i => i.Slots)
            .ThenInclude(t => t.Player)
            .Where(w => w.UserId == userId)
            .FirstOrDefaultAsync();

        if (userLineup == null)
        {
            var lineUp = new Lineup()
            {
                UserId = userId,
                Slots = new List<LineupSlot>()
                {
                    new LineupSlot()
                    {
                        Position = dbPlayer.Position,
                        SlotIndex = 1,
                        PlayerId = dbPlayer.PlayerId
                    }
                }
            };

            _context.Lineups.Add(lineUp);
            await _context.SaveChangesAsync();
            return;
        }

        var playerCount = await _context.ChallengeRules
            .Where(w => w.Name.ToUpper().EndsWith("COUNT")
                && w.Name.ToUpper().StartsWith(dbPlayer.Position.ToUpper()))
            .FirstOrDefaultAsync();
        
        if (playerCount != null)
        {
            var lineupPositionCount = userLineup.Slots.Where(w => w.Position == dbPlayer.Position).Count();
            if (lineupPositionCount >= int.Parse(playerCount.Description))
            {
                throw new FantasyAPIException($"Max Number Of {dbPlayer.Position} Reached ({playerCount.Description})");
            }
        }

        var lineupContainsTeam = userLineup.Slots.Any(a => a.Player.Team == dbPlayer.Team);
        if (lineupContainsTeam)
        {
            throw new FantasyAPIException($"Lineup Already Contains Player From {dbPlayer.Team}");
        }

        userLineup.Slots.Add(new LineupSlot()
        {
            Position = dbPlayer.Position,
            SlotIndex = 1,
            PlayerId = dbPlayer.PlayerId
        });

        await _context.SaveChangesAsync();
    }

    public async Task RemovePlayerFromLineup(int userId, int playerId)
    {
        var dbUser = await _context.Users.FindAsync(userId);
        if (dbUser == null)
        {
            throw new FantasyAPIException("Invalid User");
        }

        var userLineup = await _context.Lineups
            .Include(i => i.User)
            .Include(i => i.Slots)
            .Where(w => w.UserId == userId)
            .FirstOrDefaultAsync();

        if (userLineup == null)
        {
            throw new FantasyAPIException("Lineup Not Found");
        }

        var playerInLineup = userLineup.Slots.Where(w => w.PlayerId == playerId).FirstOrDefault();
        if (playerInLineup == null)
        {
            throw new FantasyAPIException("Player not found in lineup");
        }

        _context.Slots.Remove(playerInLineup);
        await _context.SaveChangesAsync();
    }

    public async Task LockLineup(int userId)
    {
        var userLineup = await _context.Lineups
            .Include(i => i.User)
            .Include(i => i.Slots)
            .ThenInclude(t => t.Player)
            .Where(w => w.UserId == userId)
            .FirstOrDefaultAsync();

        if (userLineup == null)
        {
            throw new FantasyAPIException("Lineup Not Found");
        }

        await ValidateLineup(userLineup);

        userLineup.IsLocked = true;
        await _context.SaveChangesAsync();
    }

    private async Task ValidateLineup(Lineup userLineup)
    {
        var duplicatePlayer = userLineup.Slots
            .Where(s => s.PlayerId != null)
            .GroupBy(s => new { s.PlayerId, s.Player.Name })
            .FirstOrDefault(g => g.Count() > 1);

        if (duplicatePlayer != null)
        {
            throw new FantasyAPIException($"{duplicatePlayer.Key.Name} is in lineup more than once");
        }

        var duplicateTeam = userLineup.Slots
            .Where(w => w.PlayerId != null)
            .GroupBy(s => new { s.Player.Team })
            .FirstOrDefault(g => g.Count() > 1);

        if (duplicateTeam != null)
        {
            throw new FantasyAPIException($"{duplicateTeam.Key.Team} is in lineup more than once");
        }

        var playerCounts = await _context.ChallengeRules
            .Where(w => w.Name.ToLower().EndsWith("count"))
            .ToListAsync();

        var positionCounts = userLineup.Slots
            .GroupBy(s => s.Player.Position)
            .ToDictionary(g => g.Key.ToUpper(), g => g.Count());

        foreach (var rule in playerCounts)
        {
            var position = rule.Name
                .Replace("Count", "", StringComparison.OrdinalIgnoreCase)
                .ToUpper();

            if (!int.TryParse(rule.Description, out int expectedCount))
                throw new FantasyAPIException("Invalid Player Count, Could Not Validate Lineup");

            positionCounts.TryGetValue(position, out int actualCount);

            if (actualCount != expectedCount)
            {
                throw new FantasyAPIException(
                    $"Expected {expectedCount} players for {position}, but counted {actualCount}");
            }
        }
    }

    public async Task<PlayerScoresDTO> GetIndividualScores(int playerId)
    {
        var player = await _context.Players
            .FindAsync(playerId);

        if (player == null)
        {
            throw new FantasyAPIException("Invalid Player");
        }

        var scoreSum = (player.WildcardScore ?? 0)
             + (player.DivisionalScore ?? 0)
             + (player.ConferenceScore ?? 0)
             + (player.SuperBowlScore ?? 0);

        return new PlayerScoresDTO()
        {
            PlayerName = player.Name,
            Team = player.Team,
            WildCardScore = player.WildcardScore ?? 0,
            DivisionalScore = player.DivisionalScore ?? 0,
            ConferenceScore = player.ConferenceScore ?? 0,
            SuperBowlScore = player.SuperBowlScore ?? 0,
            TotalScore = scoreSum
        };
    }
}
