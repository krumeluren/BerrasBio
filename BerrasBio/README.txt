
För att starta igång webbappen

1: kör Update-Database för att skapa databasen

2: Kör programmet så att webbsidan visas 

3: Avsluta programmet och stäng webbsidan

4: Gå till mappen SQL QUERIES och kör alla SQL filer förutom _seeding. Välj databasanslutning "BerrasBioContext"

5: Kör _seeding.sql för att generera test/startdata. Kör om när du vill för att återställa / Lägg till egen startdata

6: Starta programmet igen

7: Gå till "Registera" i menyn

8: Skapa ett konto. Det första kontot blir administratör automatiskt (logik i Controllers/UserController.Register)

9: Logga in med det nya kontot.

10: Nu kan administratören ha tillgång till admin-verktyg som visas som röda knappar i menyn.



Det går inte att skapa fler administratörer direkt i programmet. 
Man får gå in i databasen och titta i 
	dbo.AspNetUserRoles
	dbo.AspNetRoles
	dbo.AspNetUsers 

och ändra värdet i dbo.AspNetUserRoles.RoleId till det AspNetRoles.Id som man vill ha för en viss användare
sedan får användaren logga in på nytt på webbappen för att få sina behörigheter
