using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace Bulletinboard.Helpers
{
	public static class StartupHelpers
	{
		public static async Task SeedRoles(IServiceProvider serviceProvider)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			
			foreach (var role in Enum.GetNames<Roles>())
				if (!await roleManager.RoleExistsAsync(role))
					await roleManager.CreateAsync(new IdentityRole(role));
		}

		public static async Task SeedAdmin(IServiceProvider serviceProvider)
		{
			var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

			const string USERNAME = "myadmin@myadmin.com",
						 PASSWORD = "Admin1@";

			var admin = await userManager.FindByNameAsync(USERNAME);

			if (admin == null)
			{
				admin = new User()
				{
					UserName = USERNAME,
					Email = USERNAME,
					EmailConfirmed = true,
				};

				var result = await userManager.CreateAsync(admin, PASSWORD);
				if (result.Succeeded)
					await userManager.AddToRoleAsync(admin, Roles.Administrator.ToString());
			}
		}
	}
}
