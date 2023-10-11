info: 09.10.2023 02:14:50.480 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (14ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      PRAGMA journal_mode = 'wal';
info: 09.10.2023 02:14:50.581 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE "Floors" (
          "Id" INTEGER NOT NULL CONSTRAINT "PK_Floors" PRIMARY KEY AUTOINCREMENT
      );
info: 09.10.2023 02:14:50.582 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE "Kitchens" (
          "Id" INTEGER NOT NULL CONSTRAINT "PK_Kitchens" PRIMARY KEY AUTOINCREMENT,
          "FloorId" INTEGER NOT NULL,
          CONSTRAINT "FK_Kitchens_Floors_FloorId" FOREIGN KEY ("FloorId") REFERENCES "Floors" ("Id") ON DELETE CASCADE
      );
info: 09.10.2023 02:14:50.583 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE "Students" (
          "Id" INTEGER NOT NULL CONSTRAINT "PK_Students" PRIMARY KEY AUTOINCREMENT,
          "FirstName" TEXT NOT NULL,
          "LastName" TEXT NOT NULL,
          "Room" INTEGER NOT NULL,
          "QueueForDuty" INTEGER NOT NULL,
          "IsOrderly" INTEGER NOT NULL,
          "KitchenId" INTEGER NULL,
          CONSTRAINT "FK_Students_Kitchens_KitchenId" FOREIGN KEY ("KitchenId") REFERENCES "Kitchens" ("Id")
      );
info: 09.10.2023 02:14:50.583 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      INSERT INTO "Floors" ("Id")
      VALUES (1);
      INSERT INTO "Floors" ("Id")
      VALUES (2);
      INSERT INTO "Floors" ("Id")
      VALUES (3);
      INSERT INTO "Floors" ("Id")
      VALUES (4);
      INSERT INTO "Floors" ("Id")
      VALUES (5);
      INSERT INTO "Floors" ("Id")
      VALUES (6);
      INSERT INTO "Floors" ("Id")
      VALUES (7);
      INSERT INTO "Floors" ("Id")
      VALUES (8);
      INSERT INTO "Floors" ("Id")
      VALUES (9);
      INSERT INTO "Floors" ("Id")
      VALUES (10);
      INSERT INTO "Floors" ("Id")
      VALUES (11);
      INSERT INTO "Floors" ("Id")
      VALUES (12);
info: 09.10.2023 02:14:50.584 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (103, 1);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (202, 2);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (209, 2);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (211, 2);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (302, 3);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (305, 3);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (309, 3);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (311, 3);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (402, 4);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (405, 4);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (409, 4);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (411, 4);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (502, 5);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (505, 5);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (509, 5);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (511, 5);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (602, 6);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (605, 6);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (609, 6);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (611, 6);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (702, 7);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (705, 7);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (709, 7);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (711, 7);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (802, 8);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (805, 8);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (809, 8);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (811, 8);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (902, 9);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (905, 9);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (909, 9);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (911, 9);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (1003, 10);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (1005, 10);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (1103, 11);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (1105, 11);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (1203, 12);
      INSERT INTO "Kitchens" ("Id", "FloorId")
      VALUES (1205, 12);
info: 09.10.2023 02:14:50.585 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX "IX_Kitchens_FloorId" ON "Kitchens" ("FloorId");
info: 09.10.2023 02:14:50.585 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX "IX_Students_KitchenId" ON "Students" ("KitchenId");
info: 09.10.2023 02:18:15.826 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (26ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT COUNT(*) FROM "sqlite_master" WHERE "type" = 'table' AND "rootpage" IS NOT NULL;
info: 09.10.2023 02:22:04.421 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (32ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT COUNT(*) FROM "sqlite_master" WHERE "type" = 'table' AND "rootpage" IS NOT NULL;
info: 09.10.2023 02:26:45.067 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (37ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT COUNT(*) FROM "sqlite_master" WHERE "type" = 'table' AND "rootpage" IS NOT NULL;
info: 09.10.2023 02:26:45.179 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT "s"."Id", "s"."FirstName", "s"."IsOrderly", "s"."KitchenId", "s"."LastName", "s"."QueueForDuty", "s"."Room"
      FROM "Students" AS "s"
info: 09.10.2023 02:26:48.801 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT COUNT(*) FROM "sqlite_master" WHERE "type" = 'table' AND "rootpage" IS NOT NULL;
info: 09.10.2023 02:26:48.802 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT "s"."Id", "s"."FirstName", "s"."IsOrderly", "s"."KitchenId", "s"."LastName", "s"."QueueForDuty", "s"."Room"
      FROM "Students" AS "s"
info: 09.10.2023 02:26:50.024 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (23ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT COUNT(*) FROM "sqlite_master" WHERE "type" = 'table' AND "rootpage" IS NOT NULL;
info: 09.10.2023 02:26:50.129 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT "s"."Id", "s"."FirstName", "s"."IsOrderly", "s"."KitchenId", "s"."LastName", "s"."QueueForDuty", "s"."Room"
      FROM "Students" AS "s"
info: 09.10.2023 02:27:23.652 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT COUNT(*) FROM "sqlite_master" WHERE "type" = 'table' AND "rootpage" IS NOT NULL;
info: 09.10.2023 02:27:23.653 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT "s"."Id", "s"."FirstName", "s"."IsOrderly", "s"."KitchenId", "s"."LastName", "s"."QueueForDuty", "s"."Room"
      FROM "Students" AS "s"
info: 09.10.2023 02:27:24.841 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (23ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT COUNT(*) FROM "sqlite_master" WHERE "type" = 'table' AND "rootpage" IS NOT NULL;
info: 09.10.2023 02:27:24.947 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT "s"."Id", "s"."FirstName", "s"."IsOrderly", "s"."KitchenId", "s"."LastName", "s"."QueueForDuty", "s"."Room"
      FROM "Students" AS "s"
info: 09.10.2023 02:40:51.200 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (36ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT COUNT(*) FROM "sqlite_master" WHERE "type" = 'table' AND "rootpage" IS NOT NULL;
info: 09.10.2023 02:40:51.305 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT "s"."Id", "s"."FirstName", "s"."IsOrderly", "s"."KitchenId", "s"."LastName", "s"."QueueForDuty", "s"."Room"
      FROM "Students" AS "s"
