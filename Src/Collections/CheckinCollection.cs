﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuddySDK
{
    public class CheckinCollection : BuddyCollectionBase<Checkin>
    {
        internal CheckinCollection(BuddyClient client)
            : base(null, client)
        {
        }

        public Task<BuddyResult<Checkin>> AddAsync(string comment, string description, BuddyGeoLocation location, string tag = null,
            BuddyPermissions readPermissions = BuddyPermissions.Default, BuddyPermissions writePermissions = BuddyPermissions.Default)
        {
            
                var c = new Checkin(null, this.Client)
                    {
                        Comment = comment,
                        Description = description,
                        Location = location,
                        Tag = tag,
                        ReadPermissions = readPermissions,
                        WritePermissions = writePermissions
                    };

                var t = c.SaveAsync();

                return t.WrapResult<bool, Checkin>(r => r.IsSuccess ? c : null);
        }

        public Task<SearchResult<Checkin>> FindAsync(string comment = null, string ownerUserId = null, BuddyGeoLocationRange locationRange = null, DateRange created = null, DateRange lastModified = null, int pageSize = 100, string pagingToken = null)
        {

            return base.FindAsync (userId: ownerUserId,
                created: created,
                lastModified: lastModified,
                locationRange: locationRange,
                pagingToken: pagingToken,
                pageSize: pageSize,
                parameterCallback:  (p) => {
                p["comment"] = comment;
            });

        } 
    }
}
