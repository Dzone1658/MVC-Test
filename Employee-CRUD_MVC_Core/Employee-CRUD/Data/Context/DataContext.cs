using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Employee_CRUD.Models;

using Microsoft.EntityFrameworkCore;

namespace Employee_CRUD.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        DbSet<Country> Countries {get;set;}
        DbSet<Download> Downloads {get;set;}
        DbSet<NoteCategory> NoteCategories {get;set;}
        DbSet<ReferenceData> ReferenceDatas {get;set;}
        DbSet<Type> Types {get;set;}
        DbSet<SellerNote> SellerNotes {get;set;}
        DbSet<SellerNoteAttachment> SellerNoteAttachments {get;set;}
        DbSet<SellerNoteReportedIssue> SellerNoteReportedIssues {get;set;}
        DbSet<SellerNoteReview> SellerNoteReviews {get;set;}
        DbSet<SystemConfiguration> SystemConfigurations {get;set;}
        DbSet<User> Users {get;set;}
        DbSet<UserProfile> UserProfiles {get;set;}
        DbSet<UserRole> UserRoles {get;set;}
    }
}
