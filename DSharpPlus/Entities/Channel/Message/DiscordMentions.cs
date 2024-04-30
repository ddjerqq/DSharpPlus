namespace DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

/// <summary>
/// Handles mentionables
/// </summary>
internal class DiscordMentions
{
    //https://discord.com/developers/docs/resources/channel#allowed-mentions-object

    private const string ParseUsers = "users";
    private const string ParseRoles = "roles";
    private const string ParseEveryone = "everyone";

    /// <summary>
    /// Collection roles to serialize
    /// </summary>
    [JsonProperty("roles", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<ulong>? Roles { get; }

    /// <summary>
    /// Collection of users to serialize
    /// </summary>
    [JsonProperty("users", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<ulong>? Users { get; }

    /// <summary>
    /// The values to be parsed
    /// </summary>
    [JsonProperty("parse", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<string>? Parse { get; }

    // WHY IS THERE NO DOCSTRING HERE
    [JsonProperty("replied_user", NullValueHandling = NullValueHandling.Ignore)]
    public bool? RepliedUser { get; }

    internal DiscordMentions(IEnumerable<IMention> mentions, bool mention = false, bool repliedUser = false)
    {
        //Null check just to be safe
        if (mentions is null)
        {
            return;
        }

        RepliedUser = repliedUser;
        //If we have no item in our mentions, its likely to be a empty array.
        // This is a special case were we want parse to be a empty array
        // Doing this allows for "no parsing"
        if (!mentions.Any())
        {
            Parse = [];
            return;
        }


        //Prepare a list of allowed IDs. We will be adding to these IDs.
        HashSet<ulong> roles = [];
        HashSet<ulong> users = [];
        HashSet<string> parse = [];

        foreach (IMention m in mentions)
        {
            switch (m)
            {
                case UserMention u:
                    if (u.Id.HasValue)
                    {
                        users.Add(u.Id.Value);      //We have a user ID so we will add them to the implicit
                    }
                    else
                    {
                        parse.Add(ParseUsers);      //We have no ID, so let all users through
                    }

                    break;

                case RoleMention r:
                    if (r.Id.HasValue)
                    {
                        roles.Add(r.Id.Value);      //We have a role ID so we will add them to the implicit
                    }
                    else
                    {
                        parse.Add(ParseRoles);      //We have role ID, so let all users through
                    }

                    break;

                case EveryoneMention:
                    parse.Add(ParseEveryone);
                    break;

                case RepliedUserMention:
                    break;

                default: throw new NotSupportedException($"The type {m.GetType()} is not supported in allowed mentions.");
            }
        }

        //Check the validity of each item. If it isn't in the explicit allow list and they have items, then add them.
        if (!parse.Contains(ParseUsers) && users.Count > 0)
        {
            Users = users;
        }

        if (!parse.Contains(ParseRoles) && roles.Count > 0)
        {
            Roles = roles;
        }

        //If we have a empty parse array, we don't want to add it.
        if (parse.Count > 0)
        {
            Parse = parse;
        }
    }
}
