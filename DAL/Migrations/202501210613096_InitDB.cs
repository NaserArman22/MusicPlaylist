namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.playlists",
                c => new
                    {
                        PlaylistId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PlaylistId);
            
            CreateTable(
                "dbo.songs",
                c => new
                    {
                        SongId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Artist = c.String(),
                        Album = c.String(),
                        PlaylistId = c.Int(nullable: false),
                        Lyrics = c.String(),
                    })
                .PrimaryKey(t => t.SongId)
                .ForeignKey("dbo.playlists", t => t.PlaylistId, cascadeDelete: true)
                .Index(t => t.PlaylistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.songs", "PlaylistId", "dbo.playlists");
            DropIndex("dbo.songs", new[] { "PlaylistId" });
            DropTable("dbo.songs");
            DropTable("dbo.playlists");
        }
    }
}
