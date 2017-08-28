namespace Test
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = (new DatabaseDesignTimeDbContextFactory()).CreateDbContext(new string[] {}))
            {
                var entity = db.Entities.Add(new Entity() {
                    Name = "foo"
                });

                var field = db.Fields.Add(new Field() {
                    Name = "bar",
                    Entity = entity.Entity
                });

                var uvField = db.UVFields.Add(new UVField() {
                    Field = field.Entity,
                    Name = "baz"
                });

                db.SaveChanges();

                var res = db.Entities.GroupJoin(db.UVFields.Include(uvf => uvf.Field),
                    ent => ent.Id,
                    uvf => uvf.Field.EntityId,
                    (ent, uvfs) => new {
                        Entity = ent,
                        UVFields = uvfs.Where(f => true).ToList()
                        // UVFields = uvfs.ToList()
                    }
                 ).ToList();

                 System.Console.WriteLine(res[0].UVFields[0].FieldId);
                 System.Console.WriteLine(res[0].UVFields[0].Field.Name);
            }
        }
    }
}
