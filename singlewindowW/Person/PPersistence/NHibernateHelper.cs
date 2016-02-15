using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using PDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPersistence
{
    public class NHibernateHelper
    {
        private static ISessionFactory sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory==null)
                {
                    InitializeSessionFactory();
                }
                return sessionFactory;
            }
        }

        public static void InitializeSessionFactory()
        {
            sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(ConnectionStrings.LocationNHibernate).ShowSql)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PersonRepositoryNHibernate>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg)/*.Create(true, true)*/)
                .BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
           
            return SessionFactory.OpenSession();
        }
    }
}
