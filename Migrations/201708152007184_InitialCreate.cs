namespace FitnessNotebook.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        ExerciseID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        ExerciseName = c.String(nullable: false, maxLength: 100),
                        Repetitions = c.Int(nullable: false),
                        Sets = c.Int(nullable: false),
                        WeightType = c.String(nullable: false, maxLength: 25),
                        Weight = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Duration = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DistanceDone = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ExerciseID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Exercises");
        }
    }
}
