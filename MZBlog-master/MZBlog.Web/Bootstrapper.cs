using iBoxDB.LocalServer;
using MZBlog.Core;
using MZBlog.Core.Cache;
using MZBlog.Core.Documents;
using MZBlog.Core.Extensions;
using MZBlog.Web.Features;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Session;
using Nancy.TinyIoc;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MZBlog.Web
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            StaticConfiguration.DisableErrorTraces = false;

            pipelines.AfterRequest += (ctx) =>
            {
                if (ctx.Response.ContentType == "text/html")
                {
                    ctx.Response.ContentType = "text/html;charset=utf-8";
                }
            };

            CookieBasedSessions.Enable(pipelines);
            base.ApplicationStartup(container, pipelines);
            StaticConfiguration.Caching.EnableRuntimeViewUpdates = true;
            pipelines.OnError += ErrorHandler;
        }

        private Response ErrorHandler(NancyContext ctx, Exception ex)
        {
            if (ex is iBoxDB.E.DatabaseShutdownException)
            {
                return "DB can't connect.";
            }
            return null;
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register(typeof(ISpamShieldService), typeof(SpamShieldService));
            container.Register(typeof(ICache), typeof(RuntimeCache));

            RegisterIViewProjections(container);
            TagExtension.SetupViewProjectionFactory(container.Resolve<IViewProjectionFactory>());

            RegisterICommandInvoker(container);
            container.Register<DB.AutoBox>(this.Database);
            //container.Register(typeof(MongoDatabase), (cContainer, overloads) => Database);
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {

            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("scripts"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("content"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("images"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("web"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("chart"));

        }

        public DB.AutoBox Database
        {
            get
            {
                var dbPath = Path.Combine(this.RootPathProvider.GetRootPath(), "App_Data", "ibox");
                if (!Directory.Exists(dbPath))
                {
                    Directory.CreateDirectory(dbPath);
                }

                var server = new DB(dbPath);
                var config = server.GetConfig();
                config.EnsureTable<Author>(DBTableNames.Authors, "Id");
                config.EnsureTable<BlogPost>(DBTableNames.BlogPosts, "Id");
                config.EnsureTable<BlogComment>(DBTableNames.BlogComments, "Id");
                config.EnsureTable<SpamHash>(DBTableNames.SpamHashes, "Id");
                config.EnsureTable<Tag>(DBTableNames.Tags, "Slug");
                config.EnsureTable<Log>(DBTableNames.Logs, "Id");
                config.EnsureTable<Distribution>(DBTableNames.Distributions, "Id");
                
                var db = server.Open();
                return db;
            }
        }

        public static void RegisterICommandInvoker(TinyIoCContainer container)
        {
            var commandInvokerTypes = Assembly.GetAssembly(typeof(ICommandInvoker<,>))
                .DefinedTypes
                .Select(t => new
                {
                    Type = t.AsType(),
                    Interface = t.ImplementedInterfaces.FirstOrDefault(
                        i =>
                            i.IsGenericType() &&
                            i.GetGenericTypeDefinition() == typeof(ICommandInvoker<,>))
                })
                .Where(t => t.Interface != null)
                .ToArray();

            foreach (var commandInvokerType in commandInvokerTypes)
            {
                container.Register(commandInvokerType.Interface, commandInvokerType.Type);
            }

            container.Register(typeof(ICommandInvokerFactory),
                (cContainer, overloads) => new CommandInvokerFactory(cContainer));
        }

        public static void RegisterIViewProjections(TinyIoCContainer container)
        {
            var definedTypes = Assembly.GetAssembly(typeof(IViewProjection<,>)).DefinedTypes;

            var viewProjectionTypes = definedTypes.Select(t => new
            {
                Type = t.AsType(),
                Interface = t.ImplementedInterfaces.FirstOrDefault(
                    i =>
                        i.IsGenericType() &&
                        i.GetGenericTypeDefinition() == typeof(IViewProjection<,>))
            }).Where(t => t.Interface != null).ToArray();

            foreach (var viewProjectionType in viewProjectionTypes)
            {
                container.Register(viewProjectionType.Interface, viewProjectionType.Type);
            }

            container.Register(typeof(IViewProjectionFactory),
                (cContainer, overloads) => new ViewProjectionFactory(cContainer));
        }

    }
}