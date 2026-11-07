using Microsoft.Extensions.DependencyInjection;
using ATMS.Data.Repositories;
using DbUp;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMS
{
    internal static class Program
    {
        public static IConfiguration Configuration { get; private set; } = null!;

        // This is our global IoC Container
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // 1. Load dynamic configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            // 2. Configure Serilog Rolling File Sink
            // Logs will be saved to C:\Users\<User>\AppData\Local\ATMS\Logs\
            string logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ATMS", "Logs");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(Path.Combine(logDirectory, "atms-log-.txt"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30) // Keep logs for 30 days
                .CreateLogger();

            try
            {
                Log.Information("ATMS Application starting up.");

                // --- DBUP LOGIC STARTS HERE ---
                string dbConnection = Configuration.GetConnectionString("AtmsDb");
                PerformDatabaseUpgrade(dbConnection);
                // --- DBUP LOGIC ENDS HERE ---

                // 3. Global Exception Traps
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                // Traps WinForms UI thread errors
                Application.ThreadException += (sender, args) =>
                {
                    Log.Error(args.Exception, "Unhandled UI Thread Exception");
                    ShowCrashDialog("A user interface error occurred.", args.Exception);
                };

                // Traps background thread errors
                AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                {
                    var ex = args.ExceptionObject as Exception;
                    Log.Fatal(ex, "Fatal AppDomain Exception");
                    ShowCrashDialog("A critical system error occurred.", ex);
                };

                // Traps unawaited async Task errors
                TaskScheduler.UnobservedTaskException += (sender, args) =>
                {
                    Log.Error(args.Exception, "Unobserved Task Exception");
                    args.SetObserved(); // Swallow the exception to prevent process termination
                };

                // Seed Default Users with BCrypt Hashes
                ATMS.Data.DataSeeder.SeedDefaultUsers(dbConnection);

                // Build DI Container
                var services = new ServiceCollection();
                services.AddSingleton<IConfiguration>(Configuration);
                ConfigureServices(services, dbConnection);
                ServiceProvider = services.BuildServiceProvider();

                // Force Login before loading the Shell
                var loginForm = ServiceProvider.GetRequiredService<LoginForm>();
                Application.Run(loginForm);

                if (loginForm.IsAuthenticated)
                {
                    var mainForm = ServiceProvider.GetRequiredService<MainShellForm>();
                    Application.Run(mainForm);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application failed to start.");
            }
            finally
            {
                Log.Information("ATMS Application shutting down.");
                Log.CloseAndFlush();
            }
        }

        private static void ConfigureServices(IServiceCollection services, string dbConnection)
        {
            // Register Repositories & Services
            services.AddTransient<StudentRepository>(p => new StudentRepository(dbConnection));
            services.AddTransient<FeeRepository>(p => new FeeRepository(dbConnection));
            services.AddTransient<ATMS.Data.Repositories.AuthService>(p => new ATMS.Data.Repositories.AuthService(dbConnection));

            // Register Forms
            services.AddTransient<LoginForm>();
            services.AddTransient<MainShellForm>();
            services.AddTransient<StudentManagementControl>();
            services.AddTransient<FeeCollectionControl>();
            services.AddTransient<UserManagementControl>();
        }

        private static void PerformDatabaseUpgrade(string connectionString)
        {
            EnsureDatabase.For.PostgresqlDatabase(connectionString);

            var upgrader = DeployChanges.To
                .PostgresqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Log.Fatal(result.Error, "Database migration failed.");
                throw new Exception("Database schema upgrade failed. Check logs for details.", result.Error);
            }

            Log.Information("Database migration successful.");
        }

        private static void ShowCrashDialog(string userMessage, Exception ex)
        {
            MessageBox.Show(
                $"{userMessage}\n\nDetails: {ex?.Message}\n\nThe exact technical details have been saved to the local log files for the administrator.",
                "ATMS System Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}