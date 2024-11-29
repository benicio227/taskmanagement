using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.DataAccess;
internal class TaskManagementDbContext : DbContext
{
    public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options) : base(options) { }
    public DbSet<Domain.Entities.Task> Tasks { get; set; }
    public DbSet<TaskCategory> TaskCategories { get; set; }
    public DbSet<User> Users { get; set; }
    


    //Abaixo vamos registar o relacionamento entre "Task" e "TaskCategory"

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Define como a entidade User será configurada no banco de dados usando o EntityFramewrok
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id); //Define que a propriedade Id será a chave primária da tabela User
            entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(200);
            entity.Property(u => u.Password).IsRequired().HasMaxLength(200);
        });


        //Define como a entidade TaskCategory será configurada no banco de dados usando o EntityFramework
        modelBuilder.Entity<TaskCategory>(entity =>
        {
            entity.HasKey(c => c.Id); //Define que a propriedade Id será a chave primária da tabela TaskCategory(toda tabela precisa de uma chave primaria para identificar unicamente cada registro)
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100); //Configura a propriedade Name
            entity.Property(c => c.Description).HasMaxLength(500);

            //Relacionamento: Cada categoria pertence a um usuário
            entity.HasOne(c => c.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        modelBuilder.Entity<Domain.Entities.Task>(entity =>
        {
            entity.HasKey(t => t.Id); //É a chave primária da tabela Task
            entity.Property(t => t.Title).IsRequired(); //Diz que a propriedade Title é obrigátorio ser informado

            entity.HasOne(t => t.Category) //Indica que a entidade Task tem um relacionamento "muitos-para-um" com a entidade TaskCategory
                .WithMany(t => t.Tasks) //Define o lado inverso do relacionamento, ou seja, uma categoria pode ter várias tarefas associadas.
                .HasForeignKey(t => t.CategoryId)
                 //Configura que o campo CategoryId da tabela Task é uma chave estrangeira que referencia a tabela TaskCategory
                //Essa configuração informa ao EntityFramework que a coluna CategoryId da tabela Task é a chave estrangeira
                //que aponta para a tabela TaskCategory. Lembre que quando vamos cadastrar uma tarefa colocamos o id da categoria no campo CategoryId
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(t =>t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        });
    }

}
