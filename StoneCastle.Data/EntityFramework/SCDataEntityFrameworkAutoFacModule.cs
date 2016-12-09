using Autofac;
using StoneCastle.Data.EntityFramework;

namespace StoneCastle.Data
{
    public class DataEntityFrameworkAutoFacModule : Module
    {
        private string connStr;

        public DataEntityFrameworkAutoFacModule(string connString)
        {
            this.connStr = connString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new SCDataContext(this.connStr)).As<ISCDataContext>().InstancePerLifetimeScope();
           

            //builder.RegisterType<DbContextBase>().WithParameter(new TypedParameter(typeof(string), this.connStr)).As<IDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<SCDataContext>().WithParameter(new TypedParameter(typeof(string), this.connStr)).As<SCDataContext>().InstancePerLifetimeScope();
        }
    }
}
